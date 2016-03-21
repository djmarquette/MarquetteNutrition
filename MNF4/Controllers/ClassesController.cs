using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    public class ClassesController : Controller
    {
        //
        // GET: /Classes/

        public ActionResult Index()
        {
            return View("PCOS/5StepFormula");
        }

        public ActionResult PCOS()
        {
            return View("PCOS/5StepFormula");
        }

        //
        // GET: /PCOS/5Step - sales page
        [HttpGet]
        public ActionResult PCOS5StepFormula()
        {
            return View("PCOS/5StepFormula");
        }

        [HttpPost]
        public ActionResult PCOS5StepFormula(ProductViewModel model)
        {
            if (model.Command.StartsWith("$250"))  // down payment page for 5 payment option
            {
                // setup page with first installment.  Following pmts to be setup manually.
                model.StorePage = "/index.php?p=product&id=20";
            }
            else
            {
                // default to single payment ($999) page - $699 after coupon code entered
                model.StorePage = "/index.php?p=product&id=19";
            }
            return RedirectToAction("Product", "MNFStore", model);
        }



    }
}
