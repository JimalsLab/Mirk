using Mirk.DAL.Beans;
using Mirk.DAL.Services;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModelBuilders
{
    public class FriendsViewModelBuilder
    {
        FriendsViewModel fvm;
        public IUserService userService;
        public IFriendService friendService;

        public FriendsViewModelBuilder(IUserService us, IFriendService fs)
        {
            this.userService = us;
            this.friendService = fs;

            fvm = new FriendsViewModel();
        }

        public FriendsViewModel Build(UserBean u)
        {
            //je prends les friends appartenant au current user
            IEnumerable<FriendBean> fb = getRelations(u);
            List<UserBean> pendingrelations = new List<UserBean>();

            fvm.friends = getFriends(u);
            fvm.currUser = u;
            fvm.relations = fb.ToList();
            if (fvm.friends != null)
            {
                fvm.mutularelations = fvm.friends.Where(uu => friendService.GetMany(f => f.IdUser == uu.Id && f.IdFriend == u.Id).FirstOrDefault() != null).ToList();
                foreach (UserBean ub in fvm.friends)
                {
                    if (fvm.mutularelations.Where(fr => fr.Id == ub.Id).FirstOrDefault() == null)
                    {
                        pendingrelations.Add(ub);
                    }
                }
                fvm.pendingrelations = pendingrelations;
            }
            return fvm;
        }

        public List<UserBean> getFriends(UserBean u)
        {
            IEnumerable<FriendBean> fb = getRelations(u);
            List<UserBean> friends = new List<UserBean>();

            if (fb.Any() /*fb != null*/)
            {
                List<int> ids = new List<int>();
                foreach (FriendBean f in fb)
                {
                    ids.Add(f.IdFriend);
                }

                //je prends les user dont l'id est contenu dans la liste des friends
                friends = userService.GetMany(usr => usr.Id == ids.Where(id => id == usr.Id).FirstOrDefault()).ToList();
                if (friends.Where(f=>f.Id == 0) != null)
                {
                    friends.Remove(friends.Where(uu => uu.Id == 0).First());
                }               
            }
            else
            {
                friends = null;
            }

            return friends;
        }

        public IEnumerable<FriendBean> getRelations(UserBean u)
        {
            return friendService.GetMany(f => f.IdUser == u.Id);
        }

    }
}