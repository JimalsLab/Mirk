using Mirk.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mirk
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Add(new MirkCustomViewEngine());
        }


        //pr quelques tests, à enlever /////////////////////////////////////////////////
        protected void Application_End()
        {
            //System.Diagnostics.Debugger.Break();
        }

        protected void Application_PostAuthenticateRequest()
        {
            RecordEvent("PostAuthenticateRequest");
        }

        private void RecordEvent(string name)
        {
            List<string> eventList = Application["events"] as List<string>;
            if (eventList == null)
            {
                Application["events"] = eventList = new List<string>();
            }
            eventList.Add(name);
        }
        // ////////////////////////////////////////////////////////////////////////////////
    }
}
