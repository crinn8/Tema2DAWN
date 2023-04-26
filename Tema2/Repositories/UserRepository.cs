using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Tema2.DBContext;
using Tema2.DTOs;
using Tema2.Models;
using User = Tema2.Models.User;

namespace Tema2.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository(DbAppContext dbContext) : base(dbContext) { }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _dbContext.Users.Include(e => e.Role).Include(e => e.Student).FirstOrDefaultAsync(s => s.Username == username);

            if (user == null)
            {
                return new User();
            }
            return user;
        }

        public async Task<Dictionary<string, List<UserDto>>> GetAllUsersByRole()
        {
            return await _dbContext.Users
                .GroupBy(e => e.Role.Type)
                .Select(e => new
                {
                    Name = e.Key,
                    Users = e
                     .Select(e => new UserDto()
                     {
                         Username = e.Username
                     }).ToList()
                })
                .ToDictionaryAsync(e => e.Name, e => e.Users);
        }

        public async Task<bool> AddUser(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
