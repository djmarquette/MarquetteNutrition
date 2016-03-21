using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using MNF4.Models;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    // Client access should be restricted to ADMIN roles only!
    [Authorize(Roles = "mnf_admin")]
    public class ClientController : Controller
    {
        IClientRepository clientRepository;
        IContactRepository contactRepository;
        ISourceRepository sourceRepository;

        public ClientController() : this (new ClientRepository())
        {
        }

        public ClientController(ClientRepository repository)
        {
            clientRepository = repository;
            contactRepository = new ContactRepository();
            sourceRepository = new SourceRepository();
        }

        //
        // GET: /Client/
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var vm = new ClientListViewModel();

            List<ClientListViewModel> vmList = vm.ListClients();
            ViewData["Count"] = vmList.Count();
            
            return View(vmList);
        }

        public ActionResult Search(string q)
        {
            var vm = new ClientListViewModel();

            List<ClientListViewModel> vmList = vm.ListClients(q);
            ViewData["Count"] = vmList.Count();

            if (Request.IsAjaxRequest())
                return PartialView("_ClientListPartial", vmList);

            return View("List", vmList);
        }

        public ActionResult Details(int id)
        {
            Client client = clientRepository.Find(id);
            Source source = sourceRepository.Find(client.SourceID);

            ViewBag.SourceName = source.Name;
            ViewData["ClientName"] = client.FirstName + " " + client.LastName;

            return View("Details", client);
        }

        //
        // GET: /Make
        //  used for converting a Contact record into a Client
        [HttpGet]
        public ActionResult Make(int id)
        {
            try
            {
                var contactModel = contactRepository.Find(id);

                var clientModel = new ClientFormViewModel()
                {
                    FirstName = contactModel.FirstName,
                    LastName = contactModel.LastName,
                    EmailAddress = contactModel.EmailAddress,
                    Address = contactModel.Address,
                    City = contactModel.City,
                    State = contactModel.State,
                    ZipCode = contactModel.ZipCode,
                    Phone = contactModel.Phone,
                    Notes = contactModel.Comments
                };

                return View("Create", clientModel);
            }
            catch (Exception ex)
            {
                base.ViewData["Exception"] = "Exception: " + ex.InnerException.ToString();
                throw;
            }
        }

        //
        // POST: /Make client from contact
        [HttpPost]
        public ActionResult Make(ClientFormViewModel collection)
        {
            Create(collection);

            return RedirectToAction("List");
        }

        //
        // GET: /Client/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new ClientFormViewModel();
            return View(viewModel);
        }

        //
        // POST: /CLient
        [HttpPost]
        public ActionResult Create(ClientFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Client client = new Client();

                    client.FirstName = viewModel.FirstName;
                    client.LastName = viewModel.LastName;
                    client.Address = viewModel.Address;
                    client.City = viewModel.City;
                    client.State = viewModel.State;
                    client.ZipCode = viewModel.ZipCode;
                    client.EmailAddress = viewModel.EmailAddress;
                    client.Phone = viewModel.Phone;
                    client.DOB = viewModel.DOB;
                    client.Height = viewModel.Height;
                    client.StartWeight = viewModel.StartWeight;
                    client.Doctor = viewModel.Doctor;
                    if (!String.IsNullOrEmpty(viewModel.MaritalStatus)) // ToUpper blows up if field is null
                        client.MaritalStatus = viewModel.MaritalStatus.ToUpper();
                    client.Occupation = viewModel.Occupation;
                    client.SourceID = viewModel.SourceID;
                    client.Notes = viewModel.Notes;

                    clientRepository.InsertOrUpdate(client);
                    clientRepository.Save();

                    return RedirectToAction("Details", new { id = client.ID });
                }
                return View();
            }
            catch (Exception ex)
            {
                base.ViewData["Exception"] = "Exception: " + ex.InnerException.ToString();
                return RedirectToAction("Create");  // not sure this gets reached in the event of an error
            }
        }

        // HTTP GET: /Client/Edit/1
        public ActionResult Edit(int id)
        {
            Client client = clientRepository.Find(id);

            if (client == null)
            {
                ViewBag.RecordType = "Client";
                return View("NotFound");
            }

            ViewData["Edit"] = client.FirstName + " " + client.LastName;

            var viewModel = new ClientFormViewModel(client.SourceID)
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                EmailAddress = client.EmailAddress,
                Address = client.Address,
                City = client.City,
                State = client.State,
                ZipCode = client.ZipCode,
                Phone = client.Phone,
                DOB = client.DOB,
                Height = client.Height,
                StartWeight = client.StartWeight,
                Doctor = client.Doctor,
                MaritalStatus = client.MaritalStatus,
                Occupation = client.Occupation,
                SourceID = client.SourceID,
                Notes = client.Notes
            };
            return View("EditClient", viewModel);
        }

        //
        // HTTP POST: /Client/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Client client = clientRepository.Find(id);

            if (client == null)
            {
                ViewBag.RecordType = "Client";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                client.FirstName = collection["FirstName"];
                client.LastName = collection["LastName"];
                client.Address = collection["Address"];
                client.City = collection["City"];
                client.State = collection["State"];
                client.ZipCode = collection["ZipCode"];
                client.EmailAddress = collection["EmailAddress"];
                client.Phone = collection["Phone"];
                client.DOB = DateTime.Parse(collection["DOB"]);
                client.Height = collection["Height"];
                client.StartWeight = float.Parse(collection["StartWeight"]);
                client.Doctor = collection["Doctor"];
                client.MaritalStatus = collection["MaritalStatus"].ToUpper();
                client.Occupation = collection["Occupation"];
                client.SourceID = int.Parse(collection["SourceID"]);
                client.Notes = collection["Notes"];

                clientRepository.Save();
                return RedirectToAction("Details", new { id = client.ID });
            }
            else
            {
                return View("EditClient", new ClientFormViewModel());
            }
        }


    }
}
