using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Mappings
{
    public static class StudentMapping
    {
        public static List<StudentDto> ToStudentDtos(this List<Student> students)
        {
            var results = students.Select(e => e.ToStudentDto()).ToList();

            return results;
        }

        public static StudentDto ToStudentDto(this Student student)
        {
            if (student == null) return new StudentDto();

            var result = new StudentDto
            {
                Id = student.Id,
                Name = student.FirstName + " " + student.LastName,
                ClassId = student.ClassId,
                ClassName = student.Class.Name,
                Grades = student.Grades.ToList().ToGradeDtos()
            };

            return result;
        }
    }

}
