using Tema2.DTOs;
using Tema2.Models;
using System.Security.Claims;

namespace Tema2.Services
{
    public interface IStudentService
    {
        Task<bool> UpdateStudent(int id, Student student);

        Task<StudentDto> GetStudent(int id);

        Task<List<StudentDto>> GetAllStudents();

        Task<GradesByStudent> GetCourseGradesForStudent(int studentId, string courseType);

        Task<List<string>> GetClassStudents(int classId);

        Task<Dictionary<int, List<Student>>> GetGroupedStudents();

        Task<List<GradeDto>> GetAllGradesForStudent(string accessToken);

        Task<List<GradesByStudent>> GetAllGradesForAllStudents();
    }
}
