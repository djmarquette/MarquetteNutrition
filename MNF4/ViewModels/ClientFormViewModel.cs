using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MNF4.Models;

namespace MNF4.ViewModels
{
    public class ClientFormViewModel
    {
        //[Key]
        [DisplayName("Client ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("*First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("*Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address entered")]
        [DisplayName("*Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Street Address")]
        public string Address { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Zip Code")]
        [RegularExpression(@"^[0-9]{5}([- /]?[0-9]{4})?$", ErrorMessage = "Invalid Zip Code entered")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Telephone number number is required")]
        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$", ErrorMessage = "Invalid phone number entered")]
        [DisplayName("*Telephone number")]
        public string Phone { get; set; }

        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        public string Height { get; set; }
        
        [DisplayName("Starting Weight")]
        public float? StartWeight { get; set; }

        public string Doctor { get; set; }

        [RegularExpression(@"[MSms]", ErrorMessage="Valid values for Marital Status are M or S")]
        [DisplayName("Marital Status (M/S)")]
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }


        [DataType(DataType.MultilineText)]
        [DisplayName("Notes")]    //  Maybe just use Notes as an ID to a Notes lookup table
        public string Notes { get; set; }

        // Source from a lookup of Source table
        [ForeignKey("Source")]
        [Display(Name = "Source")]
        public int SourceID { get; set; }
        public virtual IQueryable<Source> Source { get; set; }

    #region Services

        public ClientFormViewModel() 
        {
            var repository = new SourceRepository();
            Source = repository.All;
            //SourceID = _sourceID;
        }

        public ClientFormViewModel(int _sourceID)
        {
            var repository = new SourceRepository();
            Source = repository.All;
            SourceID = _sourceID;
        }

    #endregion

    }
}