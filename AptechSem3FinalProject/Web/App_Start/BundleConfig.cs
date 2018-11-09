using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Script/main").Include("~/Script/main.js", "~/Scripts/js/*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/*.css",
                "~/Content/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Instructor/Content/css").Include(
                "~/Content/css/*.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/instructor.css"));

            bundles.Add(new StyleBundle("~/ContentLayout").Include(
                "~/Content/css/*.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Client/Register").Include("~/Scripts/Client/Register.js"));
        }
    }
}
