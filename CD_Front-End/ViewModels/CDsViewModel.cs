using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD_Front_End.ViewModels
{
    /// <summary>
    /// View models are models for a view. This model will be used to show the details of the Rental
    public class CDsViewModel
    {

        public int RentalID { get; set; }

        public int RentalItemID { get; set; }

        public string Title { get; set; }
    }
}