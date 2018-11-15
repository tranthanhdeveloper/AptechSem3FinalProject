using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ApplyAuthorViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your email is required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Your phone number is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Your company name is required field")]
        public string Company { get; set; }
    }
}