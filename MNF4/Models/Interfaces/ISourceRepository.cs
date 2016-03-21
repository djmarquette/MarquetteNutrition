using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public interface ISourceRepository : IRepository<Source>
    {
        IQueryable<Source> SourceList();
        Source Find(int id);
        Source FindByName(string q);  // don't think we need this
    }
}