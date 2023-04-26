
using Tema2.DTOs;
using Tema2.DTOs;
using Core.Services;
using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Models;
using Tema2.Repositories;

namespace Tema2.Services
{
    public class UserService : IUserService
    {
        private readonly AuthorizationService _authService;
        private readonly UnitOfWork _unitOfWork;

        public UserService(AuthorizationService authService, UnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<string, List<UserDto>>> GetAllUsersByRole()
        {
            return await _unitOfWork.Users.GetAllUsersByRole();
        }

        public async Task<string> ValidateUser(LoginDto payload)
        {
            var user = await _unitOfWork.Users.GetByUsername(payload.Username);

            if (user == null)
            {
                return "";
            }

            var passwordFine = _authService.VerifyHashedPassword(user.Password, payload.Password);

            if (passwordFine)
            {
                return _authService.GetToken(user, user.Role.Type);
            }
            else
            {
                return "";
            }

        }

        public async Task<bool> RegisterUser(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return false;
            }


            var hashedPassword = _authService.HashPassword(registerData.Password);
            var user = new User
            {
                Username = registerData.Username,
                Password = hashedPassword
            };

            if (registerData.Role == "Teacher")
            {
                user.RoleId = 1;
                await _unitOfWork.Users.AddUser(user);
            }
            else if (registerData.Role == "Student")
            {
                user.RoleId = 2;
                await _unitOfWork.Users.AddUser(user);
                var id = _unitOfWork.Users.GetByUsername(user.Username).Result.Id;

                if (registerData.ClassId == null)
                {
                    return false;
                }

                Student student = new()
                {
                    Id = id,
                    FirstName = registerData.FirstName,
                    LastName = registerData.LastName,
                    Email = registerData.Email,
                    Address = registerData.Address,
                    ClassId = registerData.ClassId.Value,
                    DateOfBirth = registerData.DateOfBirth
                };

                await _unitOfWork.Students.AddStudent(student);
            }
            else
            {
                return false;
            }

            _unitOfWork.SaveChanges();
            return true;
        }
        
        public async Task<bool> CheckUsername(string username)
        {
            var user = await _unitOfWork.Users.GetByUsername(username);

            if (user == null)
            {
                return true;
            }
            else return false;
        }
    }
}

