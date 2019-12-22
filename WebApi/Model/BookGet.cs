using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class BookGet
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string AuthorName { get; set; }
    }
}
