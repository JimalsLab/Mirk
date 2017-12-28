using Mirk.DAL.Beans;
using Mirk.DAL.Services;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Mirk.ViewModelBuilders
{
    public class DetailViewModelBuilder
    {
        public DetailViewModel dvm;
        public IUserService userService;

        public DetailViewModelBuilder(IUserService us)
        {
            this.userService = us;
             
            dvm = new DetailViewModel();
        }

        public DetailViewModel Build(UserBean u, string id)
        {
            dvm.currUser = u;
            dvm.User = userService.GetById(int.Parse(id));
            if (dvm.User.Photo == "" || dvm.User.Photo == null)
            {
                dvm.User.Photo = "Default.png";
            }
            dvm.url = "/Ressources/User_Pictures/" + dvm.User.Photo;
            return dvm;
        }


    }
}
