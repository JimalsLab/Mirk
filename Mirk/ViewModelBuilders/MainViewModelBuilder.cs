using Mirk.DAL.Beans;
using Mirk.DAL.Services;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModelBuilders
{
    public class MainViewModelBuilder
    {
        public IUserService userService;
        public IFriendService friendService;
        public ILinkUserGroupService linkService;
        public IGroupService groupService;
        public FriendsViewModelBuilder fvmb;

        private MainViewModel mvm;

        public MainViewModelBuilder(IUserService us, IFriendService fs,ILinkUserGroupService ls,IGroupService gs,FriendsViewModelBuilder f)
        {
            this.userService = us;
            this.friendService = fs;
            this.linkService = ls;
            this.groupService = gs;
            this.fvmb = f;

            mvm = new MainViewModel();
        }

        public MainViewModel Build(UserBean u)
        {
            mvm.currUser = u;
            mvm.UsersList = fvmb.Build(u).mutularelations;
            foreach (UserBean uuu in mvm.UsersList)
            {
                mvm.FriendsList.Add(friendService.GetMany(ff => ff.IdFriend == uuu.Id && ff.IdUser == u.Id).FirstOrDefault());
            }
            mvm.LinkList = linkService.GetMany(l => l.IdUser == u.Id).ToList();
            mvm.publicGroupList = groupService.GetMany(g => g.IsPrivate == 0).ToList();
            foreach (LinkUserGroupBean l in mvm.LinkList)
            {
                if (groupService.GetById(l.IdGroup).IsPrivate == 1)
                {
                    mvm.privateGroupList.Add(groupService.GetById(l.IdGroup));
                }
            }

            return mvm;
        }

        public MainViewModel BuildDiscussion(UserBean u,string id)
        {
            Build(u);
            //on recupere un build simple et on ajoute le friend avc qui il parle
            mvm.friend = friendService.GetById(int.Parse(id));

            return mvm;
        }

        public MainViewModel BuildGroup(UserBean u,string id)
        {
            Build(u);
            mvm.friend.IsFavorite = 403;
            mvm.group = groupService.GetById(int.Parse(id));

            return mvm;
        }

    }
}