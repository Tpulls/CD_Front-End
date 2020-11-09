using CD_Front_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CD_Front_End.ViewModels
{
    public class RentalDetailsViewModel
    {

        public Rental Rental { get; set; }
        [Display (Name ="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public List<CDsViewModel> RentedCDs { get; set; }
    }
}