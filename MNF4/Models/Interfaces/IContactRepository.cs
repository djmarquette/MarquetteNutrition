using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNF4.Models
{
    public interface IContactRepository : IRepository<Contact>
    {
        IQueryable<Contact> ListContacts();
        IQueryable<Contact> FindByName(string q);
        Contact Find(int id);
    }
}
