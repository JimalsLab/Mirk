using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Beans
{
    public class LinkUserGroupBean
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdGroup { get; set; }
        public int? IsAdmin { get; set; }
        public int? IsFavorite { get; set; }
        public int? IsNotNotificated { get; set; }
        public int? NotificationNumber { get; set; }
    }
}