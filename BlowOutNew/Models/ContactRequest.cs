using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlowOutNew.Models
{
    public class ContactRequest
    {
        [Required]
        [StringLength(15)]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "PhoneNumber should contain only numbers")]
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }

        //[Required]
        //[Display(Name ="Instrument Name")]
        //public string instrumentName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Email Subject")]
        public string subject { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name ="Message")]
        public string Message { get; set; }
    }
}