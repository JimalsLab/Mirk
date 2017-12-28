using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Beans
{
    public class FriendBean
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdFriend { get; set; }
        public string Pseudo { get; set; }
        public Nullable<int> IsBlocked { get; set; }
        public Nullable<int> IsFavorite { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}