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
    public class FriendService : GenericService<FriendBean, Friend>, IFriendService
    {

        public IFriendRepository friendRepository;
        public FriendService(IFriendRepository friendRepository, IUnitOfWork unitOfWork)
            : base(friendRepository, unitOfWork)
        {
            base.mapper = new FriendMapper();
        }
    }
}