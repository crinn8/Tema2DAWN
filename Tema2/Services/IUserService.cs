using Tema2.DTOs;
using Tema2.Models;

namespace Tema2.Services
{
    public interface IUserService
    {
        Task<Dictionary<string, List<UserDto>>> GetAllUsersByRole();

        Task<bool> CheckUsername(string username);

        Task<string> ValidateUser(LoginDto payload);

        Task<bool> RegisterUser(RegisterDto registerData);
    }
}