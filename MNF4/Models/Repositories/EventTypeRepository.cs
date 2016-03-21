using System;
using System.Data;
using System.Linq;
using MNF4.Models.Interfaces;

namespace MNF4.Models.Repositories
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private MNFdb _db = new MNFdb();


        public IQueryable<EventType> All
        {
            get { return _db.EventTypes; }
        }

        public void InsertOrUpdate(EventType eventType)
        {
            if (eventType.ID == 0)
            {
                _db.EventTypes.Add(eventType);
            }
            else
            {
                _db.Entry(eventType).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventType = Find(id);
            _db.EventTypes.Remove(eventType);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IQueryable<EventType> EventTypes()
        {
            return from EventType in All
                   select EventType;
        }

        public IQueryable<EventType> FindByName(string name)
        {
            return All.Where(d => d.Name.Contains(name)
                                  || string.IsNullOrEmpty(d.Name));
        }

        public EventType Find(int id)
        {
            return _db.EventTypes.SingleOrDefault(e => e.ID == id);
        }
    }
}