using System;
using System.Collections.Generic;
using System.Linq;
using MNF4.Models;

//using System.Web;
//using System.Web.UI.WebControls;

namespace MNF4.ViewModels
{
    public class PromoViewModel
    {
        public string PromoCode { get; set; }
        public string PromoPage { get; set; }
        public string PromoMessage { get; set; }
        public bool Success
        {
            get { return (LookupPromoCode(PromoCode)); }
        }

        public PromoViewModel()
        {
            
        }

        public PromoViewModel(string promocode)
        {
            PromoCode = promocode.ToLower();    // Client doesn't want promo codes to be case sensitive
        }

        private bool LookupPromoCode(string code)
        {
            // TODO: do lookup here, set event field above if success, else return false
            PromoPage = "TransformPromo";
            PromoMessage = @"You are now eligible to receive my ebook ""Transform Your Health Through Nutrition"" FREE!!";
            return (PromoCode == "transform");
        }
    }
}