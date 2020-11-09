using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CD_Front_End.Models
{
    public class Staff
    {
        [Key]
 
        public int StaffID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }
        [Display(Name = "Employment Status")]
        public string EmploymentStatus { get; set; }



    }
}