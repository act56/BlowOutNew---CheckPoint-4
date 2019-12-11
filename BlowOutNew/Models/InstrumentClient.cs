using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlowOutNew.Models
{
    public class InstrumentClient
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int instrumentID { get; set; }

        [Display(Name = "Instrument Name")]
        public string description { get; set; }

        [Display(Name = "New or Used")]
        public string type { get; set; }

        [Display(Name = "Monthly Rental Price")]
        public string price { get; set; }

        public int clientID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string first_Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_Name { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Please enter at least 5 digits")]
        [Display(Name = "Zip")]
        public string zip { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Phone number must be in the following format (xxx) xxx-xxxx")]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }
    }
}