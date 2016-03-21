using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class ClientRepository : IClientRepository
    {
        MNFdb _db = new MNFdb();
        
        #region IClientRepository Members

        public IQueryable<Client> ListClients()
        {
            return from client in All
                   orderby client.LastName ascending
                   select client;
        }

        public IQueryable<Client> FindByName(string q)
        {
            return All.Where(d => d.FirstName.Contains(q)
                            || d.LastName.Contains(q)
                            || String.IsNullOrEmpty(q));
        }

        public Client Find(int id)
        {
            return (_db.Clients.SingleOrDefault(c => c.ID == id));
        }

        #endregion

        #region IRepository<Client> Members

        public IQueryable<Client> All
        {
            get { return _db.Clients; }
        }

        public void InsertOrUpdate(Client client)
        {
            if (client.ID == 0)
                _db.Clients.Add(client);
            else
                _db.Entry(client).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var client = Find(id);
            _db.Clients.Remove(client);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        #endregion
    }
}