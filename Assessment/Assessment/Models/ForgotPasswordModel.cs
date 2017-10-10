using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string UserName { get; set; }
    }
}