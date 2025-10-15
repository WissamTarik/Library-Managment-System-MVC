using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class RoleToReturnDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="Role name is required !!")]
        public string? Name { get; set; }
    }
}
