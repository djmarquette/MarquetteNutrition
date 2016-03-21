using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNF4.Models.Interfaces
{
    interface IEventTypeRepository : IRepository<EventType>
    {
        IQueryable<EventType> EventTypes();
        IQueryable<EventType> FindByName(string q);
        EventType Find(int id);
    }
}
