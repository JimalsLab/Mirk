using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.ViewModels
{
    public class DetailViewModel
    {
        
        public UserBean currUser { get; set; }

        public UserBean User { get; set; }

        public string url { get; set; }

        public DetailViewModel()
        {
            currUser = new UserBean();
            User = new UserBean();
        }
    }
}
