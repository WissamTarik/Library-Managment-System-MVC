using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class ForgetPasswordDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required to reset your password")]
        public string Email { get; set; }
    }
}
