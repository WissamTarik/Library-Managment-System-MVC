using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class ResetPasswordDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required !!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
       ErrorMessage = "Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required !!")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
