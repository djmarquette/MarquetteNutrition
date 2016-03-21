using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MNF4.Models
{
    public class PromoCode
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Message { get; set; }
        public string Document { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public string Markup { get; set; }

        public PromoCode()
        {

        }
    }
}