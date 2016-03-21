using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.Utility;
using MNF4.ViewModels;


namespace MNF4.Controllers
{
    public class PromoCodeController : Controller
    {
        public static PromoDisplayViewModel PromoDisplayVM;
        public ActionResult PromoCode(string code="")
        {
            //var vm = new PromoDisplayViewModel(code);
            PromoDisplayVM = new PromoDisplayViewModel(code);

            if (PromoDisplayVM.Success)
            {
                PromoDisplayVM.oiMessage = PromoDisplayVM.Message;
                PromoDisplayVM.oiSource = PromoDisplayVM.Code;
                PromoDisplayVM.oiDocument = PromoDisplayVM.Document;
                return View("PromoPage", PromoDisplayVM);
            }
            return View("NoCodeError");
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult List()
        {
            var vm = new PromoListViewModel();
            var vmList = vm.ListPromoCodes();
            ViewData["Count"] = vmList.Count();

            return View("PromoCodeList", vmList);
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // GET: /Promo/Details/5
        public ActionResult Details(int id)
        {
            var vm = new PromoFormViewModel(id);

            return View("PromoCodeDetails", vm);
        }

        #region CRUD functionality
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // GET: /Promo/Create
        public ActionResult Create()
        {
            var vm = new PromoFormViewModel();
            return View("CreatePromoCode", vm);
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // POST: /Promo/Create
        [HttpPost]
        public ActionResult Create(PromoFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.Save(viewModel))
                        return RedirectToAction("List");
                }
                return RedirectToAction("List");
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // GET: /Promo/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new PromoFormViewModel(id);
            if (vm.ID == -1)
            {
                return HttpNotFound();
            }
            return View("EditPromoCode", vm);
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // POST: /Promo/Edit/5
        [HttpPost]
        public ActionResult Edit(PromoFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.Save(viewModel))
                        return RedirectToAction("List");
                }
                else
                {
                    var errors = ModelState.Where(v => v.Value.Errors.Any());
                    throw new ModelValidationException("ModelState Error");
                }
            }
            catch (Exception ex)
            {
                ViewData["Exception"] = "Exception: " + ex.Message + ex.InnerException + ex.StackTrace;
                return RedirectToAction("Edit");  // not sure this gets reached in the event of an error
            }
            return RedirectToAction("Edit");
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // GET: /Promo/Delete/5
        public ActionResult Delete(int id = 0)
        {
            var vm = new PromoFormViewModel(id);

            if (vm.ID == -1)
            {
                return HttpNotFound();
            }
            return View("DeletePromoCode", vm);
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        //
        // POST: /Promo/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // TODO: Maybe move delete logic to viewModel?
            var repository = new PromoCodeRepository();
            repository.Delete(id);
            repository.Save();

            return RedirectToAction("List");
        }
        #endregion

        #region Opt-In Processing
        // GET: /OptIn/ -- restaurant guide optIn
        public ActionResult OptIn(OptInViewModel viewModel)
        {
            viewModel.oiMessage = PromoDisplayVM.oiMessage;
            viewModel.oiDocument = PromoDisplayVM.oiDocument;
            viewModel.oiSource = PromoDisplayVM.Code + " Opt-In";
            try
            {
                if (ModelState.IsValid && string.IsNullOrEmpty(viewModel.botCheck))
                {
                    viewModel.Save(viewModel);

                    return RedirectToAction("ResetOptIn", viewModel);
                }
            }
            catch (Exception ex)
            {
 #if DEBUG
                ViewData["Exception"] = "Exception: " + ex.Message + " Stack Trace: " + ex.StackTrace;
#endif
            }
            return PartialView("_OptInPartial", viewModel);
        }

        public ActionResult ResetOptIn()
        {
            var vm = new OptInViewModel
                {
                    oiResult = @"Check your inbox for your requested free document "
                               + "(email will come from 'mail@marquettenutrition.com')"
                };
            return PartialView("_OptInPartial", vm);
        }
        #endregion //Opt-In Processing
    }
}
