namespace Tema2.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ClassName { get; set; } = string.Empty;

        public List<GradeDto> Grades { get; set; } = new List<GradeDto>();
    }
}
