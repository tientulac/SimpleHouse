using System.Web;
using System.Web.Optimization;

namespace SimpleHouse
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/plugins/fontawesome-free/css/all.min.css",
                       "~/Content/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                       "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                       "~/Content/plugins/jqvmap/jqvmap.min.css",
                       "~/Content/dist/css/adminlte.min.css",
                       "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                       "~/Content/plugins/daterangepicker/daterangepicker.css",
                       "~/Content/plugins/summernote/summernote-bs4.min.css",

                       "~/Content/css/templatemo-style.css",
                       "~/Content/css/all.min.css"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                         "~/Content/plugins/jquery/jquery.min.js",
                         "~/Content/plugins/jquery-ui/jquery-ui.min.js",
                         "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
                         "~/Content/plugins/chart.js/Chart.min.js",
                         "~/Content/plugins/sparklines/sparkline.js",
                         "~/Content/plugins/jqvmap/jquery.vmap.min.js",
                         "~/Content/plugins/jqvmap/maps/jquery.vmap.usa.js",
                         "~/Content/plugins/jquery-knob/jquery.knob.min.js",
                         "~/Content/plugins/moment/moment.min.js",
                         "~/Content/plugins/daterangepicker/daterangepicker.js",
                         "~/Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                         "~/Content/plugins/summernote/summernote-bs4.min.js",
                         "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                         "~/Content/dist/js/adminlte.js",
                         "~/Content/dist/js/demo.js",
                         "~/Content/dist/js/pages/dashboard.js",

                         "~/Content/js/jquery.min.js",
                         "~/Content/js/parallax.min.js"
                         ));
        }
    }
}
