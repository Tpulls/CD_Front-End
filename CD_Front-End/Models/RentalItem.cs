using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_Front_End.Models
{
    public class RentalItem
    {

        public int RentalItemID { get; set; }

        public int RentalID { get; set; }

        public int cdID { get; set; }

        // Create a dropdown list for access in your model
        public IEnumerable<SelectListItem> CDs { get; set; }
    }
}