using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Services
{
    public class GradeService : IGradeService
    {
        private readonly UnitOfWork _unitOfWork;

        public GradeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddGrade(AddGradeDto gradeDto)
        {
            return await _unitOfWork.Grades.AddGrade(gradeDto);
        }

        public async Task<TotalDto> GetTotalForStudent(int studentId)
        {
            var student = await _unitOfWork.Students.GetStudentById(studentId);

            if (student == null)
            {
                return new TotalDto();
            }
            return await _unitOfWork.Grades.GetAverageGradesForStudent(student);
        }

        public async Task<Dictionary<string, List<double>>> GetGroupedGradesForStudent(int studentId)
        {
            var student = await _unitOfWork.Students.GetStudentById(studentId);

            if(student == null)
            {
                return new Dictionary<string, List<double>>();
            }
            return await _unitOfWork.Grades.GetGroupedGradesForStudent(student);
        }
    }
}
