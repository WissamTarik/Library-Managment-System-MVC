using Library.Company.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class AuthorDto
    {
        [Required(ErrorMessage ="Author Name is required !!")]
        public string Name { get; set; }

       

        public string? Bio { get; set; }
        [Required(ErrorMessage = "Author DOB is required !!")]

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

       public  List<Book>? Books { get; set; }
    }
}
