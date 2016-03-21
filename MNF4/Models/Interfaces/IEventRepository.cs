using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNF4.Models.Interfaces
{
    interface IEventRepository : IRepository<MNFEvent>
    {
        IQueryable<MNFEvent> ListEvents();
        IQueryable<MNFEvent> FindByName(string q);
        MNFEvent Find(int id);
    }

}
