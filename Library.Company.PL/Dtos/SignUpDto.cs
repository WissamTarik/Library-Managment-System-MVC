using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName is required !!")]
        [MinLength(3,ErrorMessage ="UserName must be at least 3 characters")]
        [MaxLength(10,ErrorMessage ="UserName shouldn't exceed 10 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First name is required !!")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Last Name shouldn't exceed 10 characters")] public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Email is required !!")]

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required !!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required !!")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Is Agree is required !!")]

        public bool IsAgree { get; set; }
    }
}
