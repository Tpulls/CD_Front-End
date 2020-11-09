using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CD_Front_End.ViewModels
{
    /// <summary>
    /// View models are models for a view. This model will be used to show the list of Rentals with the Staff name instead of ID
    /// </summary>
    public class RentalsViewModel
    {
        [Key]
        [Display(Name = "Rental ID")]
        public int RentalID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date Rented")]
        public DateTime? DateRented { get; set; }
        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }

    }
}