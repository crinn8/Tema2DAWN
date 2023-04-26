namespace Tema2.DTOs
{
    public class TotalDto
    {
        public string Name { get; set; } = string.Empty;

        public double Total { get; set; }

        public Dictionary<string, double> TotalOnCourse { get; set; } = new Dictionary<string, double>();
        
    }
}
