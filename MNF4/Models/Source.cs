using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    #region Model
    public class Source
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Notes")] 
        public string Notes { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        //
        // other fields for the Source go here
        //
        
    }
    #endregion

    #region Services
    #endregion

    #region Validation
    #endregion

}