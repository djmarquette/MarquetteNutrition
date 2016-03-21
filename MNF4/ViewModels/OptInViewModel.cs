using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.Utility;

namespace MNF4.ViewModels
{
    /// <summary>
    /// ViewModel to expose only name and email address to partial view
    /// used for OptIn functionality.
    /// </summary>
    public class OptInViewModel
    {
        #region Members
        [Key]
        int ID { get; set; }

        [Required(ErrorMessage="First Name is required")]
        [StringLength(25, ErrorMessage="First Name may not be longer than 25 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(25, ErrorMessage = "Last Name may not be longer than 25 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Address entered")]
        [StringLength(50, ErrorMessage = "Email Address may not be longer than 50 characters")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string oiSource { get; set; }
        public string oiDocument { get; set; }
        public string oiMessage { get; set; }
        public string oiResult { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string botCheck { get; set; }
        #endregion

        #region Services
        public OptInViewModel()
        {
            
        }

        public OptInViewModel(string message, string result)
        {
            oiMessage = message;
            oiResult = result;
        }

        public Object Save(OptInViewModel viewModel)
        {
            var contactRepository = new ContactRepository();
            var sourceRepository = new SourceRepository();

            if (string.IsNullOrEmpty(botCheck))
            {
                try
                {
                    var source = sourceRepository.FindByName(viewModel.oiSource);
                    var contact = new Contact
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            EmailAddress = EmailAddress,
                            SourceID = source.ID,
                            Comments = source.Notes,
                            SubmitDate = DateTime.Now,
                            optIn = true
                        };

                    contactRepository.InsertOrUpdate(contact);
                    contactRepository.Save();

                    new Email().SendRequestedDocument(contact, viewModel.oiDocument);
                }
                catch (Exception ex)
                {
                    throw ex;
                    return false;
                }
                return true;
            }
                return false;
        }
         
        #endregion

        #region Validation

        #endregion
    }
}