using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CD_Front_End.Models
{
    public class RentalCountReport
    {
        [Key]
        [Display(Name = "ID")]
        public int cdID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Borrowed Amount")]
        public int? RentalCount { get; set; }
    }
}