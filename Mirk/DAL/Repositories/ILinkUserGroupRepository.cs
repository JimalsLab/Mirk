using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Repositories
{
    public interface ILinkUserGroupRepository : IGenericRepository<LinkUserGroup>
    {
        IEnumerable<LinkUserGroup> GetLinksUserGroup();
        LinkUserGroup GetLinkUserGroupByID(int LinkUserGroupID);
    }
}