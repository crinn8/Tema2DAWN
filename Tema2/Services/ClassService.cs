using Core.Services;
using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Services
{
    public class ClassService : IClassService
    {
        private readonly UnitOfWork _unitOfWork;

        public ClassService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClassViewDto> GetClass(int id)
        {
            var newClass =  await _unitOfWork.Classes.GetClassById(id);

            return new ClassViewDto()
            {
                Id = id,
                Name = newClass.Name,
                StudentCount = newClass.Students.Count
            };
        }

        public async Task<List<ClassViewDto>> GetAllClasses()
        {
            var classes = await _unitOfWork.Classes.GetAllWithStudentCount();

            return classes.Select(c => new ClassViewDto
            {
                Id = c.Id,
                Name = c.Name,
                StudentCount = c.Students.Count
            }).ToList();
        }

        public async Task<bool> CreateClass(Class model)
        {
            return await _unitOfWork.Classes.AddClass(model);
        }
    }
}
