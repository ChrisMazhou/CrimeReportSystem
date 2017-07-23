using System.Web;
using System.Web.Optimization;

namespace CrimeReportSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/bootstrap.css",
                         "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new ScriptBundle("~/bundles/AngularApp")
                   .Include("~/Scripts/angular.js")
                   .Include("~/Scripts/angular-route.js")
                   .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                   .Include("~/Scripts/angular-animate.js")

                   .Include("~/Scripts/lib/angular-input-match.js")
                   .Include("~/Scripts/lib/showErrors.js")
                   .Include("~/Scripts/lib/loading-bar.js")

                   .Include("~/app/AngularApp.js")
                   .IncludeDirectory("~/app/Services", "*.js")
                   .IncludeDirectory("~/app/Directives", "*.js")
                   .IncludeDirectory("~/app/Controllers", "*.js")
                   );

            BundleTable.EnableOptimizations = false;// !Debugger.IsAttached;
        }
    }
}
