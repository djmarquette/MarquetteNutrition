using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNF4.Models
{
    public class MNFEvent
    #region Model
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("EventType")]
        public int EventTypeID { get; set; }
        public string Cost { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string EventMarkup { get; set; }
        public bool ShowEvent { get; set; }
        public virtual EventType EventType { get; set; }
    #endregion

    #region Services
        public MNFEvent()
        {

        }

    #endregion

    #region Validation

    #endregion
    }
}