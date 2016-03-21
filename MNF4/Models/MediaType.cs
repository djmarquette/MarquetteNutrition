using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class MediaType
    #region Model
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Extension { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    #endregion

    #region Services
        public MediaType()
        {

        }

    #endregion

    #region Validation

    #endregion
    }
}