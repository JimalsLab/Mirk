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
    public class UserService : GenericService<UserBean, User>, IUserService
    {

        public IUserRepository userRepository;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
            : base(userRepository, unitOfWork)
        {
            base.mapper = new UserMapper();
        }
    }
}