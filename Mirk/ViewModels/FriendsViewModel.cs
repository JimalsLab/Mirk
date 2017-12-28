using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModels
{
    public class FriendsViewModel
    {
        public FriendsViewModel()
        {
            currUser = new UserBean();
            friends = new List<UserBean>();
            relations = new List<FriendBean>();
            mutularelations = new List<UserBean>();
            pendingrelations = new List<UserBean>();
            favStatus = 0;
        }

        public UserBean currUser { get; set; }

        public List<UserBean> friends { get; set; }

        public List<FriendBean> relations { get; set; }

        public List<UserBean> mutularelations { get; set; }

        public List<UserBean> pendingrelations { get; set; }

        public int favStatus { get; set; }
    }
}