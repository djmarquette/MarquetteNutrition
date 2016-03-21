using System;
using MNF4.Models;

namespace MNF4.ViewModels
{
    public class PromoDisplayViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Document { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public string Markup { get; set; }

        // Opt-In box processing
        public string oiMessage { get; set; }
        public string oiResult { get; set; }
        public string oiSource { get; set; }
        public string oiDocument { get; set; }


        public bool Success
        {
            get { return (LookupPromoCode(Code)); }
        }

        public PromoDisplayViewModel()
        {
            
        }

        public PromoDisplayViewModel(string promocode)
        {
            Code = promocode.ToLower();    // Client doesn't want promo codes to be case sensitive
        }

        private bool LookupPromoCode(string code)
        {
            var repository = new PromoCodeRepository();
            var codeLookup = repository.LookupByCode(code);

            if (codeLookup != null)
            {
                Code = codeLookup.Code;
                Name = codeLookup.Name;
                Description = codeLookup.Description;
                Message = codeLookup.Message;
                Document = codeLookup.Document;
                StartDate = codeLookup.StartDate;
                EndDate = codeLookup.EndDate;
                Active = codeLookup.Active;
                Markup = codeLookup.Markup;

                return true;
            }
            return false;
        }
    }
}