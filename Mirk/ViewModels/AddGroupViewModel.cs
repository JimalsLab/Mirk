using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mirk.ViewModels
{
    public class AddGroupViewModel
    {
        public UserBean currUser { get; set; }
        [Display(Name = "Room picture")]
        public string PathPicture { get; set; }
        [Display(Name = "Background")]
        public string Background { get; set; }
        [Display(Name = "Name")]
        public string RoomName { get; set; }
        public List<UserBean> ListAdmins { get; set; }
        [Display(Name = "Is public")]
        public bool IsPublicRoom { get; set; }
        [Display(Name = "Everyone can invite")]
        public bool EveryoneCanInvite { get; set; }
        [Display(Name = "Call conversation is allowed")]
        public bool CallConversationAllowed { get; set; }
        [Display(Name = "Call conversation is allowed")]
        public List<UserBean> Members { get; set; }

        public AddGroupViewModel()
        {
            currUser = new UserBean();
        }
    }
}