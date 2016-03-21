using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    #region Model
    public class Media
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        [ForeignKey("MediaType")]
        public int TypeID { get; set; }
        public virtual MediaType MediaType { get; set; }
    #endregion

    #region Services
    public Media()
    {

    }

    #endregion

    #region Validation

    #endregion
    }
}