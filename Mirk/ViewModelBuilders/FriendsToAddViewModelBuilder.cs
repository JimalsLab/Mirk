using Mirk.DAL.Beans;
using Mirk.DAL.Services;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModelBuilders
{
    public class FriendsToAddViewModelBuilder
    {
        public ILinkUserGroupService linkUserGroupService;
        public FriendsViewModelBuilder fvmb;
        private FriendsToAddViewModel fta;

        public FriendsToAddViewModelBuilder(FriendsViewModelBuilder f, FriendsToAddViewModel ft, ILinkUserGroupService lug)
        {
            this.linkUserGroupService = lug;
            this.fvmb = f;
            this.fta = ft;
        }

        public FriendsToAddViewModel Build(UserBean u,int id)
        {
            fta.currUser = u;
            fta.groupNb = id;
            fta.suggestionList = new List<UserBean>();

            List<UserBean> friends = fvmb.Build(u).mutularelations;
            List<LinkUserGroupBean> linkList = linkUserGroupService.GetMany(l => l.IdGroup == id).ToList();
            if (linkList != null)
            {
                foreach (UserBean ub in friends)
                {
                    if (linkList.Where(l => l.IdUser == ub.Id).FirstOrDefault() == null)
                    {
                        fta.suggestionList.Add(ub);
                    }
                }
            }
            else
            {
                fta.suggestionList = friends;
            }
            

            return fta;
        }
    }
}