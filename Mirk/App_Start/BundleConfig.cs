using System.Web;
using System.Web.Optimization;

namespace Mirk
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Ressources/Scripts/jquery-{version}.js",
                        "~/Ressources/Scripts/jquery.filedrop.js"));

            bundles.Add(new ScriptBundle("~/bundles/functions").Include(
                        "~/Ressources/Scripts/Main.js",
                        "~/Ressources/Scripts/upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Ressources/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Ressources/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Ressources/Scripts/bootstrap.js",
                      "~/Ressources/Scripts/respond.js",
                      "~/Ressources/Scripts/bootbox.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Ressources/Content/bootstrap.css",
                      "~/Ressources/Content/site.css"));
        }
    }
}
