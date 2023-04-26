using System.ComponentModel.DataAnnotations;

namespace Tema2.DTOs
{
    public class ClassAddDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
