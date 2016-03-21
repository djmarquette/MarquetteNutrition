using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public interface IClientRepository : IRepository<Client>
    {
        IQueryable<Client> ListClients();
        IQueryable<Client> FindByName(string q);
        Client Find(int id);
    }
}