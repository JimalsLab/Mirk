using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Beans
{
    public class UserBean
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> DateCreation { get; set; }
        public Nullable<int> Options_Audio_Global { get; set; }
        public Nullable<int> Options_Audio_Voices { get; set; }
        public Nullable<int> Options_Audio_Notifications { get; set; }
        public Nullable<int> Options_Nightmode { get; set; }
        public Nullable<int> Options_CustomText { get; set; }
        public Nullable<int> Options_ModeRacing { get; set; }
        public Nullable<int> Options_Camera_Resolution { get; set; }
        public Nullable<int> Options_CurrentState { get; set; }
        public Nullable<int> Options_ActivateVideoMessage { get; set; }
        public Nullable<int> Options_ShowProfileInfo { get; set; }
        public Nullable<int> Options_ShowCustomText { get; set; }
    }
}