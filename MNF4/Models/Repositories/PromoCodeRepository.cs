using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class PromoCodeRepository
    {
        MNFdb _db = new MNFdb();

        public IQueryable<PromoCode> All
        {
            get { return _db.PromoCodes; }
        }

        public IQueryable<PromoCode> ListPromoCodes()
        {
            return from promoCode in All
                   orderby promoCode.StartDate, promoCode.Name 
                   select promoCode;
        }

        public PromoCode Find(int id)
        {
            return _db.PromoCodes.SingleOrDefault(c => c.ID == id);
        }

        public IQueryable<PromoCode> FindByName(string name)
        {
            return All.Where(e => e.Name.Contains(name)
                            || string.IsNullOrEmpty(e.Name));
        }

        public PromoCode LookupByCode(string code)
        {
            return _db.PromoCodes.SingleOrDefault(c => c.Code == code);
        }

        public void InsertOrUpdate(PromoCode promoCode)
        {
            // new PromoCode record
            if (promoCode.ID == 0)
                _db.PromoCodes.Add(promoCode);
            else
                _db.Entry(promoCode).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var promoCode = Find(id);
            _db.PromoCodes.Remove(promoCode);
        }

        //
        // Persistence 
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}