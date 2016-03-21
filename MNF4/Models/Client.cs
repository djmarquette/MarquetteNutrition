using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNF4.Models
{
    #region Model
    public class Client
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

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public string Height { get; set; }
        public float? StartWeight { get; set; }
        public string Doctor { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }

        public string Notes { get; set; }
        // Source from a lookup of Source table
        [ForeignKey("Source")]
        public int SourceID { get; set; }
        public virtual Source Source { get; set; }

        public Client()
        {

        }

        public Client(Client clientModel)
        {
            Client client = clientModel;
        }
    }
    #endregion

    #region Services

    #endregion

    #region Validation

    #endregion
}