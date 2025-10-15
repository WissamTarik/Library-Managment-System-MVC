using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Company.DAL.Models
{
    public class Book:BaseEntity
    {
        [Range(1,int.MaxValue,ErrorMessage ="ISBN must be greater than zero")]
        public int ISBN { get; set; }
        public string Title { get; set; }
        [Range(1500,2025,ErrorMessage ="Publish year must be between 1500 and 2025")]
        [DisplayName("Published Year")]
        public int PublishedYear { get; set; }

        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
