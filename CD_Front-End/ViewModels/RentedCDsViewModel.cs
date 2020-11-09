using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CD_Front_End.ViewModels
{
    public class RentedCDsViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int RentalID { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name = "Date Rented")]
        public DateTime DateRented { get; set; }
    }
}