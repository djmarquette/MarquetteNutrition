using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MNF4.Models
{
    public class EventType
    #region Model
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    #endregion

        #region Services
        public EventType()
        {

        }

        #endregion

        #region Validation

        #endregion
    }
}