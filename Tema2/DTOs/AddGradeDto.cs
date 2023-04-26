namespace Tema2.DTOs
{
    public class AddGradeDto
    {
        public int StudentId { get; set; }

        public double Value { get; set; }

        public string CourseName { get; set; } = string.Empty;
    }
}
