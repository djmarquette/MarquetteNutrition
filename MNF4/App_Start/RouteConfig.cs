using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MNF4.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Might want to save this for when Privacy policy is on its own page, and not with Forms.
//            routes.MapRoute("privacy_policy", "Privacy",
//                            new {controller = "About", action = "Privacy"});

//            routes.MapRoute(
//                name: "PromoCode",
//                url: "code={code}",
//                defaults: new { controller = "PromoCode", action = "PromoCode", code = UrlParameter.Optional }
//                );

            routes.MapRoute(
                name: "Default",
//                url: "{controller}/{action}/{id}",
                url: "{*url}", // any entry just uses default Closed page
                defaults: new { controller = "Home", action = "Closed", id = UrlParameter.Optional }
            );


        }
    }
}