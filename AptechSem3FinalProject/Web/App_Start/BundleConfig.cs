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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region Client

            
            bundles.Add(new StyleBundle("~/ContentLayout").Include(
                "~/Content/css/*.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Client/Register").Include("~/Scripts/Client/Register.js"));

            #endregion

            #region Admin

            bundles.Add(new StyleBundle("~/Content/Stylesheets/layoutadmin").Include(
                //vendors
              
                "~/Content/Admin/Admin_NewDesign/*.css",
                "~/Content/Admin/Admin_NewDesign/skins/_all-skins.min.css",
                "~/Content/bootstrap.min.css",
                "~/Content/Admin/Admin_NewDesign/ionicons.min.css",
                "~/Content/Admin/Admin_NewDesign/bootstrap3-wysihtml5.min.css"

            ));


            bundles.Add(new ScriptBundle("~/Scripts/LayoutAdmin").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/Admin/dashboard.js",
                "~/Scripts/Admin/demo.js",
                "~/Scripts/Admin/jquery-jvectormap-1.2.2.min.js",
                "~/Scripts/Admin/jquery.knob.min.js",
                "~/Scripts/Admin/jquery.slimscroll.min.js",
                "~/Scripts/Admin/moment.min.js",
                "~/Scripts/Admin/raphael.min.js",
                "~/Scripts/Admin/adminlte.js",
                "~/Scripts/Admin/bootstrap-datepicker.min.js",
                "~/Scripts/Admin/fastclick.js",
                "~/Scripts/Admin/jquery-jvectormap-world-mill-en.js",
                "~/Scripts/Admin/sparkline.min.js",
                "~/Scripts/Admin/morris.min.js",
                "~/Scripts/js/jquery-ui.js",
                "~/Scripts/jqeury-3.3.1.min.js"
                ));
            #endregion
        }
    }
}
