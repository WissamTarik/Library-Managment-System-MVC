using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.DAL.Models
{
    public class Author:BaseEntity
    {
       
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime DOB { get; set; }
        public string? ImageName { get; set; }

       public List<Book>? Books { get; set; }
    }
}
