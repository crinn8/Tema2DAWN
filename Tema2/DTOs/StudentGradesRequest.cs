using Tema2.Models;
using System.ComponentModel.DataAnnotations;

namespace Tema2.DTOs
{
    public class StudentGradesRequest
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string CourseType { get; set; } = string.Empty;
    }
}
