using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RentCMvc.Models
{
    public class SignIn
    {
        [Display(Name = "User Id")]
        [Required(ErrorMessage = "Id is mandatory.")]
        public int UserId { get; set; }
        [Display(Name = "Role Id")]
        [Required(ErrorMessage = "Id is mandatory.")]
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        public string Password { get; set; }
    }
}
