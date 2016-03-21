using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MNF4.Models;

namespace MNF4.ViewModels
{
    public class PromoListViewModel
    {
        public PromoCode promoCode { get; set; }

        public PromoListViewModel()
        {
            
        }

        public List<PromoListViewModel> ListPromoCodes()
        {
            var promoRepository = new PromoCodeRepository();

            var promoCodes = promoRepository.ListPromoCodes();

            return promoCodes.Select(promo => new PromoListViewModel {promoCode = promo}).ToList();
        }

        public List<PromoListViewModel> ListPromoCodes(string s)
        {
            var promoRepository = new PromoCodeRepository();

            IQueryable<PromoCode> promoCodes = promoRepository.FindByName(s).OrderByDescending(n => n.Name);

            return promoCodes.Select(promo => new PromoListViewModel {promoCode = promo}).ToList();
        }
    }

}