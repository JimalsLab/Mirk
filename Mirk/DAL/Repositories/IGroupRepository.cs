using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Repositories
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        IEnumerable<Group> GetGroups();
        Group GetGroupByID(int GroupID);
    }
}