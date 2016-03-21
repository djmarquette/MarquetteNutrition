using System.Web.Optimization;

namespace MNF4.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", 
                        "~/Scripts/jquery.maskedinput-{version}.js",
                        "~/Scripts/jquery.maskMoney.js"));

            bundles.Add(new ScriptBundle("~/bundles/MNFScripts").Include(
                "~/Scripts/MNF4-Scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.ui.menubar.js",
                        "~/Scripts/jquery-migrate-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                        "~/Scripts/jquery-ui-timepicker-addon.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/MNF.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.menu.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/le-frog/css").Include(
                        "~/Content/themes/le-frog/jquery.ui.core.css",
                        "~/Content/themes/le-frog/jquery.ui.resizable.css",
                        "~/Content/themes/le-frog/jquery.ui.selectable.css",
                        "~/Content/themes/le-frog/jquery.ui.accordion.css",
                        "~/Content/themes/le-frog/jquery.ui.autocomplete.css",
                        "~/Content/themes/le-frog/jquery.ui.button.css",
                        "~/Content/themes/le-frog/jquery.ui.menu.css",
                        "~/Content/themes/le-frog/jquery.ui.dialog.css",
                        "~/Content/themes/le-frog/jquery.ui.slider.css",
                        "~/Content/themes/le-frog/jquery.ui.tabs.css",
                        "~/Content/themes/le-frog/jquery.ui.datepicker.css",
                        "~/Content/themes/le-frog/jquery.ui.progressbar.css",
                        "~/Content/themes/le-frog/jquery.ui.theme.css"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}