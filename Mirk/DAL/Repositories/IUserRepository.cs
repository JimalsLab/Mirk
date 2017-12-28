using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int UserID);

    }
}
