using Tema2.DTOs;

namespace Tema2.Services
{
    public interface IGradeService
    {
        Task<bool> AddGrade(AddGradeDto gradeDto);

        Task<TotalDto> GetTotalForStudent(int studentId);

        Task<Dictionary<string, List<double>>> GetGroupedGradesForStudent(int studentId);
    }
}
