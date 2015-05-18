using System.Web;
using System.Web.Optimization;

namespace ElectronicBinnacle
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content", "*.css"));
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/vendor/jquery.js",
                "~/Scripts/vendor/jquery.mousewheel.js",
                "~/Scripts/vendor/jquery.widget.js",
                "~/Scripts/vendor/metro.js",
                "~/Scripts/vendor/utils.js",
                "~/Scripts/vendor/gmap-tools.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/vendor/angular.js",
                "~/Scripts/vendor/angular-route.js",
                "~/Scripts/vendor/angular-resource.js",
                "~/Scripts/vendor/angular-cookies.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/controllers")
                .Include("~/Scripts/controllers/global.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/directives_services")
                .IncludeDirectory("~/Scripts/directives", "*.js")
                .IncludeDirectory("~/Scripts/services", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app.js"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}