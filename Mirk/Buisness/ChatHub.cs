using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Mirk.Buisness
{
    public class ChatHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        public void Send(string name, string message, string pId, string pId2)
        {
            Clients.All.addNewMessageToPage(name, message, pId, pId2);
        }
    }
}