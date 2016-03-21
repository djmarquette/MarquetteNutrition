using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    public class PaymentsController : Controller
    {
        private static ebookViewModel ebookVM;
        //
        // GET: /Payments/

        public ActionResult Index()
        {
            return RedirectToAction("Http403", "Errors");   // Can't just surf the Payments area, ist Verboten!
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ActionResult PostToPayPal(string id, string title, string amount)
        {
            PayPal paypal = new PayPal();
            paypal.cmd = "_xclick";

            //bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
#if DEBUG
            bool useSandbox = true;
#else
            bool useSandbox = false;
#endif
            ebookVM = new ebookViewModel(id, title, amount);
            ebookVM.previousURL = HttpContext.Request.UrlReferrer;      // save this for redirect if cancel, etc.

            if (useSandbox)
            {
                paypal.business = ConfigurationManager.AppSettings["LocalBusinessAccountKey"];
                ViewBag.actionURL = ConfigurationManager.AppSettings["PPSandbox"];

                paypal.cancel_return = ConfigurationManager.AppSettings["LocalCancelURL"];
                paypal.@return = ConfigurationManager.AppSettings["LocalReturnURL"];
                paypal.notify_url = ConfigurationManager.AppSettings["LocalNotifyURL"];
            }
            else
            {
                paypal.business = ConfigurationManager.AppSettings["BusinessAccountKey"];
                ViewBag.actionURL = ConfigurationManager.AppSettings["PPProduction"];

                paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
                paypal.@return = ConfigurationManager.AppSettings["ReturnURL"];
                paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"];
            }

            paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];
            paypal.item_name = title;
            paypal.amount = amount;

            return View(paypal);
        }

        public ActionResult CancelFromPayPal()
        {
            // User Canceled the payment process from PayPal.  Send 'em back where they came from.
            if (ebookVM.previousURL != null)
            {
                string prevController = ebookVM.previousURL.Segments[1];
                string prevAction = ebookVM.previousURL.Segments[2];
                return RedirectToAction(prevAction, prevController);
            }
            return RedirectToAction("Index", "ebooks");
        }

        public ActionResult RedirectFromPayPal()
        {
            // Purchase was completed thru PayPal and returned to this page.  "Redirect page has links".

            return View(ebookVM);
        }

        public ActionResult NotifyFromPayPal()
        {
            //TODO: Maybe send an email to us if there is a notify received, find out why this would happen
            return View();
        }

    }
}
