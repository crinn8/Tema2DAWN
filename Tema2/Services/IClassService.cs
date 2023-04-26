using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Services
{
    public interface IClassService
    {
        Task<ClassViewDto> GetClass(int id);

        Task<List<ClassViewDto>> GetAllClasses();

        Task<bool> CreateClass(Class newClass);
    }
}
