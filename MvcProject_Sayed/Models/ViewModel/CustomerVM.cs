using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace MvcProject_Sayed.Models.ViewModel
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Nullable<int> PhoneNo { get; set; }
    }
}