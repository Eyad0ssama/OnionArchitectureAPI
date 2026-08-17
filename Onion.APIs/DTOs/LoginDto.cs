using System.ComponentModel.DataAnnotations;

namespace Onion.APIs.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
