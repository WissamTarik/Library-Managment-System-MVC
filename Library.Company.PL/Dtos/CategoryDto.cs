using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class CategoryDto
    {
        [Required(ErrorMessage ="Category name is required !!")]
        public string Name { get; set; }
    }
}
