using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class SourceRepository : ISourceRepository
    {
        MNFdb _db = new MNFdb();

        public IQueryable<Source> SourceList()
        {
            return from Source in All
                   select Source;
        }

        public Source Find(int id)
        {
            return _db.Sources.SingleOrDefault(c => c.ID == id);
        }

        public Source FindByName(string name)
        {
            return _db.Sources.SingleOrDefault(n => n.Name.StartsWith(name));
        }

        public IQueryable<Source> All
        {
            get { return _db.Sources; }
        }

        public void InsertOrUpdate(Source source)
        {
            // new Source record
            if (source.ID == 0)
                _db.Sources.Add(source);
            else
                _db.Entry(source).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var source = Find(id);
            _db.Sources.Remove(source);
        }

        //
        // Persistence 
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}