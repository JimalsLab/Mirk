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
    public class LinkUserGroupService : GenericService<LinkUserGroupBean, LinkUserGroup>, ILinkUserGroupService
    {
        public ILinkUserGroupRepository LinkUserGroupRepository;
        public LinkUserGroupService(ILinkUserGroupRepository LinkUserGroupRepository, IUnitOfWork unitOfWork)
            : base(LinkUserGroupRepository, unitOfWork)
        {
            base.mapper = new LinkUserGroupMapper();
        }
    }
}