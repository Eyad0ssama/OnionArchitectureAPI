using System.ComponentModel.DataAnnotations;

namespace Onion.APIs.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*$",
            ErrorMessage = "Password must be between 6 and 10 characters and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }
}
