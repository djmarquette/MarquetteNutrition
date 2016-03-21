using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNF4.Models
{
    #region Model
    public class Chart
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }
        [Required]
        public virtual Client Client { get; set; }

        //[ForeignKey("Appointment")]
        //public int AppointmentID { get; set; }
        //public virtual Appointment Appointment { get; set; }

        //
        // other fields for the chart go here
        //

        [DataType(DataType.MultilineText)]
        [DisplayName("Notes")]    //  Maybe just use Notes as an ID to a Notes lookup table
        public string Notes { get; set; }
    }
    #endregion

    #region Services
    #endregion

    #region Validation
    #endregion

}