using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Web.WebPages;
using MNF4.App_Start;
using MNF4.Models;
using MNF4.Utility;
using MNF4.Controllers;
using BundleConfig = MNF4.App_Start.BundleConfig;
using FilterConfig = MNF4.App_Start.FilterConfig;
using RouteConfig = MNF4.App_Start.RouteConfig;

namespace MNF4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);

            DisplayModeProvider.Instance.Modes.Insert(0, new MobileDisplayMode());
            // need to make it see Opera Mobile emulator as mobile
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("Mobile")
            {
                ContextCondition = (context => context.GetOverriddenUserAgent()
                    .IndexOf("Opera Mobi", StringComparison.OrdinalIgnoreCase) >= 0)
            });
        }

        //
        // Redirect pages to full URL, or from old site to new site structure
        protected void Application_BeginRequest()
        {

            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("marquettenutrition.net"))
            {
                HttpContext.Current.Response.Status =
                    "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location",
                    Request.Url.ToString().ToLower().Replace(
                        "http://www.marquettenutrition.net",
                            "http://www.marquettenutrition.com"));
            }

            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("marquettenutrition.info"))
            {
                HttpContext.Current.Response.Status =
                    "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location",
                    Request.Url.ToString().ToLower().Replace(
                        "http://www.marquettenutrition.info",
                            "http://www.marquettenutrition.com"));
            }

            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(
                "http://marquettenutrition.com"))
            {
                HttpContext.Current.Response.Status =
                    "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location",
                    Request.Url.ToString().ToLower().Replace(
                        "http://marquettenutrition.com",
                            "http://www.marquettenutrition.com"));
            }

            string requestedPage = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            string newpage = String.Empty;

            if (requestedPage.EndsWith(".aspx/"))
                requestedPage = requestedPage.Substring(0, requestedPage.LastIndexOf('/'));

            if (!requestedPage.EndsWith(".aspx")) return;

            #region ReRoute old .aspx pages

            switch (requestedPage)
            {
                case "/index.aspx":
                    newpage = "/";
                    break;
                case "/about.aspx":
                    newpage = "/About";
                    break;
                case "/chris.aspx":
                    newpage = "/About/Chris";
                    break;
                case "/celiac.aspx":
                    newpage = "/Specialties/Celiac";
                    break;
                case "/contact.aspx":
                    newpage = "/Contact";
                    break;
                case "/faq.aspx":
                    newpage = "/About/FAQ";
                    break;
                case "/foodsens.aspx":
                    newpage = "/Specialties/FoodReactions";
                    break;
                case "/forms.aspx":
                    newpage = "/About/Forms";
                    break;
                case "/links.aspx":
                    newpage = "/Resources";
                    break;
                case "/location.aspx":
                    newpage = "/About/Location";
                    break;
                case "/pcos.aspx":
                    newpage = "/Specialties/PCOS";
                    break;
                case "/services.aspx":
                    newpage = "/Services";
                    break;
                case "/Specialties.aspx":
                    newpage = "/Specialties";
                    break;
                case "/sports.aspx":
                    newpage = "/Specialties/Sports";
                    break;
                case "/storefront.aspx":
                    newpage = "/Services/ebooks";
                    break;
                case "/vegetarian.aspx":
                    newpage = "/Specialties/Vegetarian";
                    break;
                case "/videos.aspx":
                    newpage = "/About/Media";
                    break;
                case "/weightmgt.aspx":
                    newpage = "/Specialties/Weight";
                    break;
                case "/Specialities":
                    newpage = "/Specialties";
                    break;
                default:
                    newpage = "/";
                    break;
            }
            HttpContext.Current.Response.Status =
                "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location",
                                                   Request.Url.ToString().ToLower().Replace(
                                                       "http://www.marquettenutrition.com" + requestedPage,
                                                       "http://www.marquettenutrition.com" + newpage));

            #endregion
        }

        //
        // Handle web errors, route thru ErrorsController
        protected void Application_Error()
        {
            if (Context.IsCustomErrorEnabled)
                ShowCustomErrorPage(Server.GetLastError());
            //var exception = Server.GetLastError();
            //var httpException = exception as HttpException;
            //Response.Clear();
            //Server.ClearError();
            //var routeData = new RouteData();
            //routeData.Values["controller"] = "Errors";
            //routeData.Values["action"] = "General";
            //routeData.Values["exception"] = exception;
            //Response.StatusCode = 500;
            //if (httpException != null)
            //{
            //    Response.StatusCode = httpException.GetHttpCode();
            //    switch (Response.StatusCode)
            //    {
            //        case 403:
            //            routeData.Values.Add("action", "Http403");
            //            break;
            //        case 404:
            //            routeData.Values.Add("action", "Http404");
            //            break;
            //        case 500:
            //            routeData.Values.Add("action", "Http500");
            //            break;

            //        default:
            //            routeData.Values.Add("action", "GeneralError");
            //            routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
            //            break;
            //    }
            //}

            //IController errorsController = new ErrorsController();
            //var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            //errorsController.Execute(rc);
        }
        private void ShowCustomErrorPage(Exception exception)
        {
            var httpException = exception as HttpException ?? new HttpException(500, "Internal Server Error", exception);

            Response.Clear();
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Errors");
            routeData.Values.Add("fromAppErrorEvent", true);

            switch (httpException.GetHttpCode())
            {
                case 403:
                    routeData.Values.Add("action", "Http403");
                    break;

                case 404:
                    routeData.Values.Add("action", "Http404");
                    break;

                case 500:
                    routeData.Values.Add("action", "Http500");
                    break;

                default:
                    routeData.Values.Add("action", "GeneralError");
                    routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
                    break;
            }

            Server.ClearError();

            IController controller = new ErrorsController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}