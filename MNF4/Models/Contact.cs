using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNF4.Models
{
    #region Models

    public class Contact
    {
        [Key]
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public DateTime SubmitDate { get; set; }
        public Boolean optIn { get; set; }
        public Boolean Contacted { get; set; }
        public string ContactNotes { get; set; }
        // Source from a lookup of Source table
        [ForeignKey("Source")]
        public int SourceID { get; set; }
        public virtual Source Source { get; set; }
    }
    #endregion
    
    #region Services



    #endregion

    #region Validation

    #endregion
}