using System.Collections.Generic;
using System.Linq;
using MNF4.Models;

namespace MNF4.ViewModels
{
    public class ContactListViewModel
    {
        public Contact contact { get; set; }
        public virtual Source source { get; set; }

        public ContactListViewModel()
        {
        }

        public List<ContactListViewModel> ListContacts()
        {
            var contactRepository = new ContactRepository();
            var sourceRepository = new SourceRepository();

            IEnumerable<Contact> contacts = contactRepository.ListContacts();
            List<ContactListViewModel> contactList = new List<ContactListViewModel>();

            foreach (Contact c in contacts)
            {
                var vm = new ContactListViewModel();
                vm.contact = c;
                vm.source = sourceRepository.Find(c.SourceID);
                contactList.Add(vm);
            }
            return contactList;
        }

        public List<ContactListViewModel> ListContacts(string q)
        {
            var contactRepository = new ContactRepository();
            var sourceRepository = new SourceRepository();

            IQueryable<Contact> contacts = contactRepository.FindByName(q).OrderByDescending(d => d.SubmitDate);
            List<ContactListViewModel> contactList = new List<ContactListViewModel>();

            foreach (Contact c in contacts)
            {
                var vm = new ContactListViewModel();
                vm.contact = c;
                vm.source = sourceRepository.Find(c.SourceID);
                contactList.Add(vm);
            }
            return contactList;
        }
    }
}
