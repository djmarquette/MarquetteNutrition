using System.Collections.Generic;
using System.Linq;
using MNF4.Models;

namespace MNF4.ViewModels
{
    public class ClientListViewModel
    {
        public Client client { get; set; }

        public ClientListViewModel()
        {
        }

        public List<ClientListViewModel> ListClients()
        {
            var clientRepository = new ClientRepository();

            IEnumerable<Client> clients = clientRepository.ListClients();
            List<ClientListViewModel> vmList = new List<ClientListViewModel>();
            foreach (Client c in clients)
            {
                var viewModel = new ClientListViewModel();
                viewModel.client = c;
                vmList.Add(viewModel);
            }
            return vmList;
        }

        public List<ClientListViewModel> ListClients(string q)
        {
            var clientRepository = new ClientRepository();

            IEnumerable<Client> clients = clientRepository.FindByName(q).OrderByDescending(n => n.ID);
            List<ClientListViewModel> vmList = new List<ClientListViewModel>();
            foreach (Client c in clients)
            {
                var viewModel = new ClientListViewModel();
                viewModel.client = c;
                vmList.Add(viewModel);
            }
            return vmList;
        }
    }
}