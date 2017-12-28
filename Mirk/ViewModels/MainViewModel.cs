using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            currUser = new UserBean();
            friend = new FriendBean();
            group = new GroupBean();
            friend.IsFavorite = 404; //pour vérifier qu'un friend a bien été trouvé (il est override si c'est le cas)

            UsersList = new List<UserBean>();
            FriendsList = new List<FriendBean>();
            privateGroupList = new List<GroupBean>();
            publicGroupList = new List<GroupBean>();
            LinkList = new List<LinkUserGroupBean>();
        }

        public UserBean currUser { get; set; }

        public FriendBean friend { get; set; }

        public GroupBean group { get; set; }

        public List<UserBean> UsersList { get; set; }

        public List<FriendBean> FriendsList { get; set; }

        public List<GroupBean> privateGroupList { get; set; }

        public List<GroupBean> publicGroupList { get; set; }

        public List<LinkUserGroupBean> LinkList { get; set; }


    }
}