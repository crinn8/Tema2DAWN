using System.ComponentModel.DataAnnotations;

namespace Tema2.DTOs
{
    public class StudentUpdateDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
    }
}
