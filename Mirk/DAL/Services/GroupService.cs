using Mirk.DAL.Beans;
using Mirk.DAL.IoC;
using Mirk.DAL.Mappers;
using Mirk.DAL.Repositories;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Services
{
    public class GroupService : GenericService<GroupBean, Group>, IGroupService
    {
        public IGroupRepository GroupRepository;
        public GroupService(IGroupRepository pFriendRepository, IUnitOfWork unitOfWork)
            : base(pFriendRepository, unitOfWork)
        {
            base.mapper = new GroupMapper();
        }
    }
}