using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNF4.Models
{
    interface IPromoRepository : IRepository<PromoCode>
    {
        IQueryable<PromoCode> PromoList();
        PromoCode Find(int id);
        PromoCode FindByName(string q);  // don't think we need this
    }
}
