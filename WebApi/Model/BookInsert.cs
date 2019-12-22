using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class BookInsert
    {
        BookInsert() 
        { 
        }

        public void ToUpper()
        {
            Title = Title == null ? null : Title.ToUpper();
            AuthorName = AuthorName == null ? null : AuthorName.ToUpper();
        }

        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string AuthorName { get; set; }
    }
}
