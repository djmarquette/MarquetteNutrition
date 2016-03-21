using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MNF4.Models;

namespace MNF4.ViewModels
{
    public class PromoFormViewModel
    {
        #region Model

        public int ID { get; set; }

        [Required]
        [Display(Name="Promo Code")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Promo Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Promotion Message - Displays above the Opt-In box")]
        public string Message { get; set; }

        [Display(Name = "Document")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [Column(TypeName = "Date")]
        public string StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [Column(TypeName = "Date")]
        public string EndDate { get; set; }

        [Display(Name = "Activate Promotion?")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Promo Markup - Displays in middle of Promo page")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Markup { get; set; }

        #endregion
        
        #region Services

        public PromoFormViewModel()
        {
            StartDate = DateTime.Today.ToShortDateString();
            Active = false;
        }

        public PromoFormViewModel(int promoID)
        {
            var promoRepository = new PromoCodeRepository();

            try
            {
                var promoCode = promoRepository.Find(promoID);

                ID = promoCode.ID;
                Code = promoCode.Code;
                Name = promoCode.Name;
                Description = promoCode.Description;
                Message = promoCode.Message;
                Document = promoCode.Document;
                StartDate = promoCode.StartDate.ToShortDateString();
                EndDate = promoCode.EndDate.ToShortDateString();
                Active = promoCode.Active;
                Markup = promoCode.Markup;
            }
            catch (Exception)
            {
                // record not found in db, return -1 for ID
                ID = -1;
            }
        }

        public bool Save(PromoFormViewModel viewModel)
        {
            var promoRepository = new PromoCodeRepository();
            var sourceRepository = new SourceRepository();

            var promoCode = new PromoCode
                {
                    ID = viewModel.ID,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Message = viewModel.Message,
                    Document = viewModel.Document,
                    StartDate = DateTime.Parse(viewModel.StartDate),
                    EndDate = DateTime.Parse(viewModel.EndDate),
                    Active = viewModel.Active,
                    Markup = viewModel.Markup
                };

            var source = sourceRepository.FindByName(promoCode.Code + " Opt-In") ?? new Source
            {
                Name = promoCode.Code + " Opt-In",
                Notes = "Opt-In for " + promoCode.Code + " Promo Code",
                StartDate = promoCode.StartDate,
                EndDate = promoCode.EndDate
            };

            try
            {
                sourceRepository.InsertOrUpdate(source);
                sourceRepository.Save();

                promoRepository.InsertOrUpdate(promoCode);
                promoRepository.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        #endregion
    }
}