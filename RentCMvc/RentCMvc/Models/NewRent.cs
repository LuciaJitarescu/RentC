using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data.SqlClient;

namespace RentCMvc.Models
{
    public class NewRent
    {
        [Required(ErrorMessage = "This field is mandatory.")]
        [Display(Name = "Client Id")]
        public string ClientID { get; set; }

        [Required(ErrorMessage = "This field is mandatory.")]
        [Display(Name = "Plate")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "This field is mandatory.")]
        [Display(Name = "Start Date")]
        public String Start { get; set; }


        [Required(ErrorMessage = "This field is mandatory.")]
        [Display(Name = "End Date")]
        public String end { get; set; }


        [Required(ErrorMessage = "This field is mandatory.")]
        [Display(Name = "City")]
        public string City { get; set; }

    }
}