using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        IEnumerable<Friend> GetFriends();
        Friend GetFriendByID(int FriendID);
    }
}
