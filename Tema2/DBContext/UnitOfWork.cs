using Tema2.Repositories;
using System;

namespace Tema2.DBContext
{
    public class UnitOfWork
    {
        public StudentRepository Students { get; }
        public ClassRepository Classes { get; }
        public UserRepository Users { get; }
        public GradeRepository Grades { get; }

        private readonly DbAppContext _dbContext;

        public UnitOfWork
        (
            DbAppContext dbContext,
            StudentRepository studentsRepository,
            ClassRepository classes,
            UserRepository users,
            GradeRepository grades
        )
        {
            _dbContext = dbContext;
            Students = studentsRepository;
            Classes = classes;
            Users = users;
            Grades = grades;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
