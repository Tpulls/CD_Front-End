using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CD_Front_End.Models
{
    public class CD
    {
        [Key]

        public int cdID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        [Display(Name = "File Path")]
        public string cdFileName { get; set; }

        public string Section { get; set; }
        [Display(Name = "X")]
        public string xPos { get; set; }
        [Display(Name = "Y")]
        public string yPos { get; set; }

        public string Barcode { get; set; }

        public string OnLoan { get; set; }


    }
}