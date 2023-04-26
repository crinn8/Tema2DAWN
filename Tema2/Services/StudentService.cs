using Azure.Core;
using Microsoft.Net.Http.Headers;
using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Mappings;
using Tema2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Tema2.Services
{
    public class StudentService : IStudentService
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> GetStudent(int id)
        {
            return (await _unitOfWork.Students.GetStudentById(id)).ToStudentDto();
        }

        public async Task<List<StudentDto>> GetAllStudents()
        {
            return (await _unitOfWork.Students.GetAllStudents()).ToStudentDtos();
        }

        public async Task<List<string>> GetClassStudents(int classId)
        {
            return await _unitOfWork.Students.GetClassStudents(classId);
        }

        public async Task<GradesByStudent> GetCourseGradesForStudent(int studentId, string courseType)
        {
            var student = await _unitOfWork.Students.GetCourseGradesForStudent(studentId, courseType);

            return new GradesByStudent(student);
        }

        public async Task<Dictionary<int, List<Student>>> GetGroupedStudents()
        {
            return await _unitOfWork.Students.GetGroupedStudents();
        }

        public async Task<List<GradeDto>> GetAllGradesForStudent(string accessToken)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            var result = jwtTokenHandler.ReadJwtToken(accessToken.Replace("Bearer ", string.Empty));
            var userId = result.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;

            if(userId == null)
            {
                throw new Exception("Error in reading the token!");
            }

            var student = await _unitOfWork.Students.GetStudentById(int.Parse(userId));

            if(student == null)
            {
                return new List<GradeDto>();
            }

            return student.Grades.ToList().ToGradeDtos();
        }

        public async Task<List<GradesByStudent>> GetAllGradesForAllStudents()
        {
            var result = await _unitOfWork.Students.GetAllGradesForStudents();

            if (result == null)
            {
                return new List<GradesByStudent>();
            }

            return result;
        }

        public async Task<bool> UpdateStudent(int id, Student student)
        {
            if (student == null || student.FirstName == null || student.LastName == null)
            {
                return false;
            }

            var result = await _unitOfWork.Students.GetStudentById(id);
            if (result == null)
            {
                return false;
            }
            result.FirstName = student.FirstName;
            result.LastName = student.LastName;

            await _unitOfWork.Students.UpdateStudent(result);

            return true;
        }
    }
}
