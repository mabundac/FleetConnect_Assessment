using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Locked")]
        public Nullable<bool> isLocked { get; set; }

        public Nullable<System.DateTime> LastLogin { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public Nullable<bool> ResetPassword { get; set; }

        [Required(ErrorMessage = "At least one Role is required")]
        [Display(Name = "Roles")]
        public string Roles { get; set; }
        public bool IsEmployer { get; set; }
    }
}