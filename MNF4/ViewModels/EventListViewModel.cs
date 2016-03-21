using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MNF4.Models;
using MNF4.Models.Repositories;

namespace MNF4.ViewModels
{
    public class EventListViewModel
    {
        public MNFEvent mnfEvent { get; set; }
        public virtual EventType EventType { get; set; }

        public EventListViewModel()
        {
            
        }

        public List<EventListViewModel>ListEvents()
        {
            var eventRepository = new EventRepository();
            var eventTypeRepository = new EventTypeRepository();

            var events = eventRepository.ListEvents();
            var eventList = new List<EventListViewModel>();

            foreach (MNFEvent e in events)
            {
                var vm = new EventListViewModel();
                vm.mnfEvent = e;
                vm.mnfEvent.EventType = eventTypeRepository.Find(e.EventTypeID);
                eventList.Add(vm);
            }
            return eventList;
        }

        public List<EventListViewModel> ListEvents(string s)
        {
            var eventRepository = new EventRepository();
            var eventTypeRepository = new EventTypeRepository();

            IQueryable<MNFEvent> events = eventRepository.FindByName(s).OrderByDescending(n => n.Name);
            var eventList = new List<EventListViewModel>();

            foreach (MNFEvent e in events)
            {
                var vm = new EventListViewModel();
                vm.mnfEvent = e;
                vm.mnfEvent.EventType = eventTypeRepository.Find(e.EventTypeID);
                eventList.Add(vm);
            }
            return eventList;
        }
        public List<EventListViewModel> DisplayEvents()
        {
            var eventRepository = new EventRepository();

            var events = eventRepository.ListEvents();
            var eventList = new List<EventListViewModel>();

            foreach (MNFEvent displayEvent in events)
            {
                if (displayEvent.EventDate >= DateTime.Today && displayEvent.ShowEvent)
                {
                    var vm = new EventListViewModel();
                    vm.mnfEvent = displayEvent;
                    eventList.Add(vm);
                }
            }
            return eventList;
        }
    }
}