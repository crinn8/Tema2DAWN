
using Microsoft.EntityFrameworkCore;
using Tema2.DBContext;

namespace Tema2.Repositories
{
    public class RepositoryBase
    {
        protected readonly DbAppContext _dbContext;

        public RepositoryBase(DbAppContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
