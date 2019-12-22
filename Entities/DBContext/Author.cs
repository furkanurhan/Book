using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DBContext
{
    public partial class Author
    {
        public Author()
        {
        }

        public void UpdateAuthor(string name)
        {
            Name = name;
        }

        public long ID { get; set; }
        public string Name { get; set; }
    }
}
