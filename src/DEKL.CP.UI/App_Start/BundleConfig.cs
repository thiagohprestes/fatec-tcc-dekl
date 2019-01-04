using System.Web.Optimization;

namespace DEKL.CP.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
             //Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.bundle.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                     "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                    "~/Scripts/datatables/datatables.js",
                     "~/Scripts/datatables/jquery.datatables.js",
                     "~/Scripts/datatables/datatables.bootstrap4.js"));

            //Styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/toastr.min.css",
                      "~/Content/sidebar-navbar.css"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/Content/datatables/datatables.bootstrap4.css"));

           //BundleTable.EnableOptimizations = true;
        }
    }
}
