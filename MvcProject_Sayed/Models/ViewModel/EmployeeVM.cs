using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace MvcProject_Sayed.Models.ViewModel
{
    public class EmployeeVM
    {
        public int EmloyeeID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Must be 2 characher Long")]
        [StringLength(30, ErrorMessage = "Maximum 30 character Long")]
        public string Name { get; set; }
        public string Address { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        //public System.DateTime DOB { get; set; }
    }
}