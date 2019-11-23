using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlowOutNew.Models
{
    [Table("instrument")]
    public class Instrument
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int instrumentID { get; set; }

        [Display(Name = "Instrument Name")]
        public string Desc { get; set; }

        [Display(Name = "New or Used")]
        public string type { get; set; }

        [Display(Name = "Monthly Rental Price")]
        public string price { get; set; }

        [ForeignKey("client")]
        //[HiddenInput(DisplayValue = false)]
        public virtual int? clientID { get; set; }
        public virtual Client client { get; set; }

        [HiddenInput(DisplayValue =false)]
        public string image { get; set; }
    }
}