using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Beans
{
    public class GroupBean
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IsPrivate { get; set; }
        public string Image { get; set; }
        public int? Background { get; set; }
    }
}