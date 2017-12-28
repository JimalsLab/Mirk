using Mirk.DAL.Beans;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Mappers
{
    public class FriendMapper : AbstractMapper<Friend, FriendBean>
    {
        protected override Friend TransformBeanToModel(FriendBean b)
        {
            Friend friend = new Friend();

            friend.DateCreated = b.DateCreated;
            friend.Id = b.Id;
            friend.IdFriend = b.IdFriend;
            friend.IdUser = b.IdUser;
            friend.IsBlocked = b.IsBlocked;
            friend.IsFavorite = b.IsFavorite;
            friend.Pseudo = b.Pseudo;

            return friend;
        }

        protected override FriendBean TransformModelToBean(Friend m)
        {
            FriendBean friend = new FriendBean();

            friend.DateCreated = m.DateCreated;
            friend.Id = m.Id;
            friend.IdFriend = m.IdFriend;
            friend.IdUser = m.IdUser;
            friend.IsBlocked = m.IsBlocked;
            friend.IsFavorite = m.IsFavorite;
            friend.Pseudo = m.Pseudo;

            return friend;
        }
    }
}