using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MNF4.Models;
using MNF4.Utility;

namespace MNF4.ViewModels
{
    public class ContactFormViewModel
    
    #region Models
    {
        //[Key]
        public int ID { get; set; }

        // use VM to give validation, must accept VM in HTTPPost, not form collection
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address entered")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        public string City { get; set; }

        [StringLength(2)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
//        [RegularExpression(@"^[0-9]{5}(-[0-9]{4})?$", ErrorMessage = "Invalid Zip Code entered")] - tweak this later
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$", ErrorMessage = "Invalid phone number entered")]
        [Display(Name = "*Phone")]
        public string Phone { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

        [Display(Name = "Check here to receive our newsletter")]
        public Boolean optIn { get; set; }

        [Display(Name="Source")]
        public int SourceID { get; set; }
        public virtual IQueryable<Source>Source { get; set; }

        [Display(Name="Contacted?")]
        public Boolean Contacted { get; set; }

        [Display(Name="Contact Notes")]
        [DataType(DataType.MultilineText)]
        public string ContactNotes { get; set; }

    #endregion

    #region Services

        public ContactFormViewModel()
        {

        }

        public ContactFormViewModel(int _sourceID)
        {
            var repository = new SourceRepository();

            Source = repository.All;
            SourceID = _sourceID;
        }

        public bool Save(ContactFormViewModel viewModel)
        {
            var contactRepository = new ContactRepository();
            var contact = new Contact();

            try 
	        {	        
                contact.FirstName = viewModel.FirstName;
                contact.LastName = viewModel.LastName;
                contact.Address = viewModel.Address;
                contact.City = viewModel.City;
                contact.State = viewModel.State;
                contact.ZipCode = viewModel.ZipCode;
                contact.EmailAddress = viewModel.EmailAddress;
                contact.Phone = viewModel.Phone;
                contact.Comments = viewModel.Comments;
                contact.optIn = viewModel.optIn;                    //looking for [true],[false]
                contact.SubmitDate = DateTime.Now.ToLocalTime();    // finally, store the time as local
                contact.Contacted = false;
                contact.SourceID = 1;                               // default of 1 means Contact Form

                contactRepository.InsertOrUpdate(contact);
                contactRepository.Save();

                Email emailController = new Email();
                emailController.SubmitForm(contact);        // email controller will handle Debug vs Release
                emailController.SendReplyMessage(contact);

                return true;
	        }
	        catch (Exception ex)
	        {
                return false;
	        }
        }

    #endregion

    }
}