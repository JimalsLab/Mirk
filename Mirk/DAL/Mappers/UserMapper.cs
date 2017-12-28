using Mirk.DAL.Beans;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Mappers
{
    public class UserMapper : AbstractMapper<User, UserBean>
    {
        protected override User TransformBeanToModel(UserBean b)
        {
            User user = new Models.User();
            user.Adress = b.Adress;
            user.DateCreation = b.DateCreation;
            user.Email = b.Email;
            user.Firstname = b.Firstname;
            user.Id = b.Id;
            user.Lastname = b.Lastname;
            user.Options_ActivateVideoMessage = b.Options_ActivateVideoMessage;
            user.Options_Audio_Global = b.Options_Audio_Global;
            user.Options_Audio_Notifications = b.Options_Audio_Notifications;
            user.Options_Audio_Voices = b.Options_Audio_Voices;
            user.Options_Camera_Resolution = b.Options_Camera_Resolution;
            user.Options_CurrentState = b.Options_CurrentState;
            user.Options_CustomText = b.Options_CustomText;
            user.Options_ModeRacing = b.Options_ModeRacing;
            user.Options_Nightmode = b.Options_Nightmode;
            user.Options_ShowCustomText = b.Options_ShowCustomText;
            user.Options_ShowProfileInfo = b.Options_ShowProfileInfo;
            user.Phone = b.Phone;
            user.Photo = b.Photo;
            user.Username = b.Username;
            user.Password = b.Password;

            //exemple pour quand on aura un mapper pour les friends :

            //FriendMapper friendMapper = new FriendMapper();
            //user.FriendList = friendMapper.TransformToModels(b.listFriends).ToList();

            return user;
        }
        protected override UserBean TransformModelToBean(User m)
        {

            UserBean user = new UserBean();
            user.Adress = m.Adress;
            user.DateCreation = m.DateCreation;
            user.Email = m.Email;
            user.Firstname = m.Firstname;
            user.Id = m.Id;
            user.Lastname = m.Lastname;
            user.Options_ActivateVideoMessage = m.Options_ActivateVideoMessage;
            user.Options_Audio_Global = m.Options_Audio_Global;
            user.Options_Audio_Notifications = m.Options_Audio_Notifications;
            user.Options_Audio_Voices = m.Options_Audio_Voices;
            user.Options_Camera_Resolution = m.Options_Camera_Resolution;
            user.Options_CurrentState = m.Options_CurrentState;
            user.Options_CustomText = m.Options_CustomText;
            user.Options_ModeRacing = m.Options_ModeRacing;
            user.Options_Nightmode = m.Options_Nightmode;
            user.Options_ShowCustomText = m.Options_ShowCustomText;
            user.Options_ShowProfileInfo = m.Options_ShowProfileInfo;
            user.Phone = m.Phone;
            user.Photo = m.Photo;
            user.Username = m.Username;
            user.Password = m.Password;

            return user;
        }
    }
}