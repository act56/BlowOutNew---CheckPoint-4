using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlowOutNew.Models
{
    [Table("client")]
    public class Client
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
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