using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class SignInDto
    {
        [EmailAddress]
        [Required( ErrorMessage ="Email is required to sign in")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required !!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
