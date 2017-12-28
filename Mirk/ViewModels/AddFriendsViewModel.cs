using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModels
{
    public class AddFriendsViewModel
    {
        public AddFriendsViewModel()
        {
            currUser = new UserBean();
            suggestionList = new List<UserBean>();
            pendingList = new List<UserBean>();
        }

        public List<UserBean> suggestionList { get; set; }

        public List<UserBean> pendingList { get; set; }

        public UserBean currUser { get; set; }
    }
}