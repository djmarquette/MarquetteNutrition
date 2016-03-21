using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNF4.Models
{
    #region Model
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public virtual Client Client { get; set; }

        public Appointment()
        {

        }

    }
    #endregion
    
    #region Services
    #endregion

    #region Validation
    #endregion
}