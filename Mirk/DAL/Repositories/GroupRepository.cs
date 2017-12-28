using Mirk.DAL.IoC;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        private MirkDBEntities dbContext;
        private readonly DbSet<Group> dbset;

        public GroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            dbset = DataContext.Set<Group>();
        }

        public Group GetGroupByID(int id)
        {
            return GetById(id.ToString());
        }

        public IEnumerable<Group> GetGroups()
        {
            return GetAll();
        }
    }
}