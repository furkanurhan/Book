using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DBContext
{
    public partial class Book
    {
        public Book()
        {
            Author = new Author();
        }

        public void UpdateBook(string title, decimal? price, DateTime? publishedDate)
        {
            Title = title;
            Price = price;
            PublishedDate = publishedDate;
        }

        public long ID { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PublishedDate { get; set; }


        //reference Author
        public long AuthorID { get; set; }
        public virtual Author Author { get; set; }
    }
}
