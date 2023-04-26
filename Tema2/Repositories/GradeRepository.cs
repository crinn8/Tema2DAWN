using Microsoft.EntityFrameworkCore;
using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Repositories
{
    public class GradeRepository : RepositoryBase
    {
        public GradeRepository(DbAppContext dbContext) : base(dbContext){ }

        public async Task<bool> AddGrade(AddGradeDto gradeDto)
        {
            var Course = await _dbContext.CourseTypes.FirstOrDefaultAsync(e => e.Title == gradeDto.CourseName);
            
            if(Course == null)
            {
                return false;
            }
            Grade grade = new()
            {
                Value = gradeDto.Value,
                CourseTypeId = Course.Id,
                StudentId = gradeDto.StudentId
            };
            _dbContext.Add(grade);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<string, List<double>>> GetGroupedGradesForStudent(Student student)
        {
            return await _dbContext.Grades
                .GroupBy(e => e.CourseType.Title)
                .Select(e => new
                {
                    CourseType = e.Key,
                    Grades = e.Select(e => e.Value).ToList()
                }).ToDictionaryAsync(e => e.CourseType, e => e.Grades);
        }

        public async Task<TotalDto> GetAverageGradesForStudent(Student student)
        {
            TotalDto averageGrades = new()
            {
                Name = student.FirstName + " " + student.LastName,
                TotalOnCourse = await _dbContext.Grades
                .GroupBy(e => e.CourseType.Title)
                .Select(e => new
                {
                    CourseType = e.Key,
                    Mark = e.Select(e => e.Value).AsQueryable().Average()
                }).ToDictionaryAsync(e => e.CourseType, e => e.Mark)
            };
            averageGrades.Total = averageGrades.TotalOnCourse.Values.Average();
            return averageGrades;
        }

    }
}
