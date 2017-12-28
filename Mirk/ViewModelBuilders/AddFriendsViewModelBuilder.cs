using Mirk.DAL.Beans;
using Mirk.DAL.Services;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModelBuilders
{
    public class AddFriendsViewModelBuilder
    {
        AddFriendsViewModel afvm;
        public IUserService userService;
        public IFriendService friendService;

        public AddFriendsViewModelBuilder(IUserService us, IFriendService fs)
        {
            this.userService = us;
            this.friendService = fs;

            afvm = new AddFriendsViewModel();
        }

        public AddFriendsViewModel Build(FriendsViewModel fvm)
        {
            //qqch marche pas la dedans

            List<UserBean> propList;

            propList = userService.GetAll().ToList();
            //je recupère les user qui ne sont pas friend avec currUser
            if (fvm.friends != null)
            {
                foreach (UserBean ub in fvm.friends)
                {
                    propList.Remove(propList.Where(usb => usb.Id == ub.Id).First());
                }
            }

            //j'enleve l'admin et l'user actuel des possibilités
            propList.Remove(propList.Where(uu => uu.Id == 0).First());
            propList.Remove(propList.Where(us => us.Id == fvm.currUser.Id).First());

            foreach (UserBean uub in propList)
            {
                if (friendService.GetMany(f => f.IdUser == uub.Id && f.IdFriend == fvm.currUser.Id).FirstOrDefault() != null)
                {
                    afvm.pendingList.Add(uub);
                }
                else
                {
                    afvm.suggestionList.Add(uub);
                }
            }

            //j'insère les val récupérées dans le viewmodel
            afvm.currUser = fvm.currUser;

            return afvm;
        }
    }
}