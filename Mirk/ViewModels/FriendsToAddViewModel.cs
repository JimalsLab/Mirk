using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.ViewModels
{
    public class FriendsToAddViewModel
    {
        public FriendsToAddViewModel()
        {
            currUser = new UserBean();
            suggestionList = new List<UserBean>();
        }

        public List<UserBean> suggestionList { get; set; }

        public UserBean currUser { get; set; }

        public int groupNb { get; set; }
    }
}