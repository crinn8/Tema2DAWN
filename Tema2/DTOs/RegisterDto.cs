using System.ComponentModel.DataAnnotations;

namespace Tema2.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int? ClassId { get; set; }
        [Required]
        public string Role { get; set; } = "Student";
    }
}
