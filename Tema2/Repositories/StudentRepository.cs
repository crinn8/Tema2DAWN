using Microsoft.EntityFrameworkCore;
using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Repositories
{
    public class StudentRepository : RepositoryBase
    {
        public StudentRepository(DbAppContext dbContext) : base(dbContext) { }

        public async Task<Student> GetCourseGradesForStudent(int studentId, string courseType)
        {
            var result = await _dbContext.Students
                .Include(e => e.Grades)
                .ThenInclude(e => e.CourseType)
                .Select(e => new Student
               {
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   Id = e.Id,
                   ClassId = e.ClassId,
                   Grades = e.Grades
                        .Where(g => g.CourseType.Title == courseType)
                        .OrderByDescending(g => g.Value)
                        .ToList(),
                   User = e.User
               })
               .FirstOrDefaultAsync(e => e.Id == studentId);

            if(result == null)
            {
                return new Student();
            }

            return result;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _dbContext.Students
                .Include(e => e.Class)
                .Include(e => e.Grades)
                .ThenInclude(e => e.CourseType)
                .ToListAsync();
        }

        public async Task<Dictionary<int, List<Student>>> GetGroupedStudents()
        {
            var results = await _dbContext.Students
               .GroupBy(e => e.ClassId)
               .Select(e => new { ClassId = e.Key, Students = e.ToList() })
               .ToDictionaryAsync(e => e.ClassId, e => e.Students);

            return results;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var result = await _dbContext.Students
                .Include(e => e.Class)
                .Include(e => e.Grades)
                .ThenInclude(e => e.CourseType)
                .Select(e => new Student()
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Id = e.Id,
                    ClassId = e.ClassId,
                    Address = e.Address,
                    DateOfBirth = e.DateOfBirth,
                    Email = e.Email, 
                    User = e.User,
                    Class = e.Class,
                    Grades = _dbContext.Grades
                         .Include(e => e.CourseType)
                         .Where(g => g.StudentId == e.Id)
                         .OrderByDescending(g => g.Value)
                         .ToList()
                }).FirstOrDefaultAsync(user => user.Id == id);

            if(result == null)
            {
                return new Student();
            }
            return result;
        }

        public async Task<List<string>> GetClassStudents(int classId)
        {
            var results = await _dbContext.Students
                .Include(e => e.Grades.Where(e => e.Value > 5))
                .Where(e => e.ClassId == classId)
                .OrderByDescending(e => e.FirstName)
                .ThenByDescending(e => e.LastName)
                .Select(e => e.FirstName + "" + e.LastName)
                .ToListAsync();

            return results;
        }

        public async Task<List<GradesByStudent>> GetAllGradesForStudents()
        {
            return await _dbContext.Students.Include(e => e.Grades)
               .Select(e => new GradesByStudent(new Student()
               {
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   Id = e.Id,
                   ClassId = e.ClassId,
                   Grades = _dbContext.Grades
                         .Include(e => e.CourseType)
                         .Where(g => g.StudentId == e.Id)
                         .OrderByDescending(g => g.Value)
                         .ToList()
               })).ToListAsync();
        }

        public async Task<bool> AddStudent(Student student)
        {
            await _dbContext.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> UpdateStudent(Student student)
        {
             _dbContext.Update(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudent(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
