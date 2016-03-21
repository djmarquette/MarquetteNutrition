using System;
using System.Data;
using System.Linq;
using MNF4.Models.Interfaces;

namespace MNF4.Models.Repositories
{
    public class EventRepository : IEventRepository
    {
        private MNFdb _db = new MNFdb();


        public IQueryable<MNFEvent> All
        {
            get { return _db.MNFEvents; }
        }

        public IQueryable<MNFEvent> ListEvents()
        {
            return from mnfEvents in All
                   orderby mnfEvents.EventDate, mnfEvents.StartTime
                   select mnfEvents;
        }

        public IQueryable<MNFEvent> FindByName(string q)
        {

            return All.Where(e => e.Name.Contains(q)
                            || string.IsNullOrEmpty(e.Name));
        }

        public MNFEvent Find(int id)
        {
            return _db.MNFEvents.SingleOrDefault(e => e.ID == id);
        }

        public void InsertOrUpdate(MNFEvent mnfEvent)
        {
            if (mnfEvent.ID == 0)
            {
                // new Event record
                _db.MNFEvents.Add(mnfEvent);
            }
            else
            {
                // update existing record
                _db.Entry(mnfEvent).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mnfEvent = Find(id);
            _db.MNFEvents.Remove(mnfEvent);
        }

        //
        // Persistence 
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}