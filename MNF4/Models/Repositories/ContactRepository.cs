using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class ContactRepository : IContactRepository
    {
        MNFdb _db = new MNFdb();
        
        #region IRepository<Contact> Members

        public IQueryable<Contact> ListContacts()
        {
            return from contact in All
                   orderby contact.SubmitDate descending
                   select contact;
        }

        public IQueryable<Contact> All
        {
            get { return _db.Contacts; }
        }

        public Contact Find(int id)
        {
            return _db.Contacts.SingleOrDefault(c => c.ID == id);
        }

        public IQueryable<Contact> FindByName(string q)
        {
            return All.Where(d => d.FirstName.Contains(q)
                            || d.LastName.Contains(q)
                            || String.IsNullOrEmpty(q));
        }

        public IQueryable<Contact> FindByContacted(bool contacted)
        {
            return All.Where(c => c.Contacted.Equals(contacted));
        }

        //
        // Insert / Delete Methods
        public void InsertOrUpdate(Contact contact)
        {
            if (contact.ID == 0)
            {
                // New contact record
                _db.Contacts.Add(contact);
            }
            else
            {
                // Existing contact record
                _db.Entry(contact).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contact = Find(id);
            _db.Contacts.Remove(contact);
        }

        #endregion


        //
        // Persistence 
        public void Save()
        {
            _db.SaveChanges();
        }


    }
}