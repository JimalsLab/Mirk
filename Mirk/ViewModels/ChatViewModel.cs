using Mirk.DAL.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.ViewModels
{
    public class ChatViewModel
    {
        public UserBean currUser { get; set; }
        public int IdGroup { get; set; }
        public string KeyRoom { get; set; }
        public int NbUsersInConf { get; set; }


        public ChatViewModel()
        {
            currUser = new UserBean();
        }
    }
}