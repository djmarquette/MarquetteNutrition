using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.Models.Repositories;

namespace MNF4.ViewModels
{
    public class EventFormViewModel

    #region Model
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public int EventTypeID { get; set; }
        public virtual IQueryable<EventType>EventTypes { get; set; }

        [Display(Name = "Event Cost")]
        public string Cost { get; set; }

        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [Column(TypeName = "Date")]
        public string EventDate { get; set; }

        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        public string Location { get; set; }

        [Display(Name = "Show Event?")]
        public bool ShowEvent { get; set; }

        [Display(Name = "Event Markup")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string EventMarkup { get; set; }


    #endregion
    #region Services
        public EventFormViewModel()
        {
            var etRepository = new EventTypeRepository();

            Cost = "$0.00";
            EventTypes = etRepository.EventTypes();
            EventDate = DateTime.Today.ToShortDateString();
        }

        public EventFormViewModel(int eventID)
        {
            var repository = new EventRepository();
            var etRepository = new EventTypeRepository();
            EventTypes = etRepository.EventTypes();

            try
            {
                var mnfEvent = repository.Find(eventID);

                ID = mnfEvent.ID;
                Name = mnfEvent.Name;
                EventTypeID = mnfEvent.EventTypeID;
                Cost = mnfEvent.Cost;
                Description = mnfEvent.Description;
                EventDate = mnfEvent.EventDate.ToShortDateString();
                StartTime = mnfEvent.StartTime.ToShortTimeString();
                EndTime = mnfEvent.EndTime.ToShortTimeString();
                Location = mnfEvent.Location;
                ShowEvent = mnfEvent.ShowEvent;
                EventMarkup = mnfEvent.EventMarkup;
            }
            catch (Exception)
            {
                // record not found in db, return -1 for ID
                ID = -1;
            }
        }

        public bool Save(EventFormViewModel viewModel)
        {
            var eventRepository = new EventRepository();

            var mnfEvent = new MNFEvent
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                EventTypeID = viewModel.EventTypeID,
                Cost = viewModel.Cost,
                Description = viewModel.Description,
                EventDate = DateTime.Parse(viewModel.EventDate),
                // store following starttime/endtime WITH the date to convert to ISO 8601 on display page
                StartTime = DateTime.Parse(viewModel.EventDate).Add(DateTime.Parse(viewModel.StartTime).TimeOfDay),
                EndTime = DateTime.Parse(viewModel.EventDate).Add(DateTime.Parse(viewModel.EndTime).TimeOfDay),
                Location = viewModel.Location,
                ShowEvent = viewModel.ShowEvent,
                EventMarkup = viewModel.EventMarkup
            };

            try
            {
                eventRepository.InsertOrUpdate(mnfEvent);   // handles new or edit record based on ID
                eventRepository.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion  //Services
    }
}