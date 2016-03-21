using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MNF4.ViewModels
{
    public class EmailViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address entered")]
        [Display(Name = "Email Address (From)", Prompt="Enter your email address here")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Subject may not be longer than 50 characters")]
        public string Subject { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

        public string SentFrom { get; set; }
        public string returnUrl { get; set; }

    }
    #region Services

    #endregion

    #region Validation

    #endregion


}