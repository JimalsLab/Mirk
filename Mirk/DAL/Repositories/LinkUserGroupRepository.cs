using Mirk.DAL.IoC;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Repositories
{
    public class LinkUserGroupRepository : GenericRepository<LinkUserGroup>, ILinkUserGroupRepository
    {
        private MirkDBEntities dbContext;
        private readonly DbSet<LinkUserGroup> dbset;

        public LinkUserGroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            dbset = DataContext.Set<LinkUserGroup>();
        }

        public LinkUserGroup GetLinkUserGroupByID(int id)
        {
            return GetById(id.ToString());
        }

        public IEnumerable<LinkUserGroup> GetLinksUserGroup()
        {
            return GetAll();
        }
    }
}