using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CD_Front_End.ViewModels;

namespace CD_Front_End.Models
{
    public class Rental
    {
        [Key]
        [Display(Name ="Rental ID")]
        public int RentalID { get; set; }
        [Display(Name = "Staff ID")]
        public int StaffID { get; set; }

        public string Title { get; set; }
        [Display(Name = "Date Rented")]
        public System.DateTime DateRented { get; set; }
        [Display(Name = "Date Returned")]
        public System.DateTime? DateReturned { get; set; }

        public virtual ICollection<RentalItem> RentalItems { get; set; }

        // Add this property so we can use it in a dropdown box
        public IEnumerable<SelectListItem> Staffs { get; set; }

        // To store the collection of CDs
        public IEnumerable<CDsViewModel> RentedCDs { get; set; }

    }
}