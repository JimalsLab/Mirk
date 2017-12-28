using Mirk.DAL.IoC;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private MirkDBEntities dbContext;
        private readonly DbSet<User> dbset;

        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            dbset = DataContext.Set<User>();
        }

        public User GetUserByID(int id)
        {
            return GetById(id.ToString());
        }

        public IEnumerable<User> GetUsers()
        {
            return GetAll();

        }

    }
}