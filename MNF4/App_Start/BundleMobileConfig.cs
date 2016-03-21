using System.Web.Optimization;

namespace MNF4.App_Start {
    public class BundleMobileConfig 
    {
        public static void RegisterBundles(BundleCollection bundles) 
        {
            bundles.Add(new ScriptBundle("~/bundles/jquerymobile").Include(
                "~/Scripts/jquery.mobile-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/MobileCSS").Include(
                "~/Content/jquery.mobile-{version}.css",
                "~/Content/Site.Mobile.css",
                "~/Content/MNFmobile.css"));
            
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}