using Mirk.DAL.IoC;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Repositories
{
    public class FriendRepository : GenericRepository<Friend>, IFriendRepository
    {

        public FriendRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public Friend GetFriendByID(int id)
        {
            return GetById(id.ToString());
        }

        public IEnumerable<Friend> GetFriends()
        {
            return GetAll();

        }

    }
}