using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.BookDbContext;
using Entities.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //protected BookDbContext BookDbContext { get; }
        private readonly BookDbContext BookDbContext;

        public BooksController(BookDbContext bookDbContext)
        {
            BookDbContext = bookDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var query = BookDbContext.Book.Include(a => a.Author);
                var bookList = query.Select(g => new BookGet
                {
                    ID = g.ID,
                    Title = g.Title,
                    Price = g.Price,
                    PublishedDate = g.PublishedDate,
                    AuthorName = g.Author.Name
                })
                .ToList();
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Success, Message = "Liste Geldi", Data = bookList });

            }
            catch (Exception ex)
            {
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Error, Message = ex.ToString(), Data = null });
            }
        }
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] BookInsert bookInsert)
        {
            try
            {
                bookInsert.ToUpper();

                var bookQuery = BookDbContext.Book.Include(a => a.Author).Where(a => a.Title.Equals(bookInsert.Title));
                var book = bookQuery.FirstOrDefault();

                var message = "";

                if (book == null)
                {
                    var authorQuery = BookDbContext.Author.Where(a => a.Name.Equals(bookInsert.AuthorName));
                    var author = authorQuery.FirstOrDefault();

                    if(author == null)
                    {
                        author = new Author() { Name = bookInsert.AuthorName };
                        BookDbContext.Author.Add(author);
                        message = message + $"{author.Name} adlı yazar kaydedildi";
                    }
                    BookDbContext.SaveChanges();


                    book = new Book
                    {
                        Title = bookInsert.Title,
                        Price = bookInsert.Price,
                        PublishedDate = bookInsert.PublishedDate,
                        AuthorID = author.ID
                    };

                    BookDbContext.Book.Add(book);
                    BookDbContext.SaveChanges();
                    message = message + $" ve {book.Title} adlı kitap kaydedildi";
                }
                else
                {
                    message = "Bu isimde bir kitap kaydı var.";
                }
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Success, Message = message, Data = null });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Error, Message = ex.ToString(), Data = null });
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] BookUpdate bookUpdate)
        {
            try
            {

                bookUpdate.ToUpper();


                var bookQuery = BookDbContext.Book.Include(c => c.Author).Where(a => a.ID == bookUpdate.ID);
                var book = bookQuery.FirstOrDefault();

                var message = "";

                if (book == null)
                {
                    message = $"{bookUpdate.ID}'li kayıt yok";
                }
                else
                {
                    if(book.Author != null && book.Author.Name != bookUpdate.AuthorName)
                    {
                        var author = new Author() { Name = bookUpdate.AuthorName };
                        BookDbContext.Author.Add(author);
                        BookDbContext.SaveChanges();
                        book.AuthorID = author.ID;
                    }
                    book.UpdateBook(bookUpdate.Title, bookUpdate.Price, bookUpdate.PublishedDate);
                    BookDbContext.SaveChanges();
                    message = message + $"{book.ID}'li kayıt güncellendi";
                }
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Success, Message = message, Data = null });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Error, Message = ex.ToString(), Data = null });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] BookDelete bookDelete)
        {
            try
            {
                var message = "";
                var bookQuery = BookDbContext.Book.Where(a => a.ID == bookDelete.ID);
                var book = bookQuery.FirstOrDefault();
                BookDbContext.Book.Remove(book);
                BookDbContext.SaveChanges();
                message = $"{book.ID}'li kayıt silindi";
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Success, Message = message, Data = null });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new CustomActionResult() { StatusCode = WebApi.Enum.StatusCode.Error, Message = ex.ToString(), Data = null });
            }
        }
    }
}