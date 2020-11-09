using CD_Front_End.Models;
using CD_Front_End.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CD_Front_End.Controllers
{
    public class ReportController : Controller
    {
        /// <summary>
        /// Method to retrieve the dataset for the table
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRentalCountData()
        {
            // Return a list of all the rentals
            IEnumerable<RentalCountReport> cdRentalCountReport = WebClient.ApiRequest<RentalCountReport>.GetEnumerable("Reports/GetRentalCountReports");

            return View(cdRentalCountReport.OrderBy(c => c.Title));
        }

        /// <summary>
        ///  Method to link the data set for the chart
        /// </summary>
        /// <returns></returns>
        public ActionResult DrawCDRentalCountChart()
        {
            IEnumerable<RentalCountReport> cdRentalCountReport = WebClient.ApiRequest<RentalCountReport>.GetEnumerable("Reports/GetRentalCountReports");

            return View(cdRentalCountReport);
        }

        /// <summary>
        ///  Method to populate the Reports table
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public ActionResult GetRentedCDReport(string criteria)
        {
            IEnumerable<RentedCDsViewModel> rentedCDReport = WebClient.ApiRequest<RentedCDsViewModel>.GetEnumerable("Reports/GetRentedCDsReport");

            if (string.IsNullOrEmpty(criteria) == false) rentedCDReport = rentedCDReport.Where(w => w.Title.ToLower().Contains(criteria.ToLower())
                                                                || w.FirstName.ToLower().Contains(criteria.ToLower()) || w.LastName.ToLower().Contains(criteria.ToLower())).ToList();

            TempData["RentedCDs"] = rentedCDReport;

            return View(rentedCDReport);
        }

        /// <summary>
        ///  Method to handle how the exported data is structured
        /// </summary>
        public void ExportRentedCDData()
        {
            List<RentedCDsViewModel> rentedCDs = TempData["RentedCDs"] as List<RentedCDsViewModel>;
            TempData.Keep("rentedCDs");

            // Use stringwriter to create the csv file
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"RentalID\", \"Title\", \"FirstName\", \"LastName\", \"DateRented\"");

            Response.ClearContent();

            Response.AddHeader("content-disposition", "attachment; filename=RentedItems.csv");

            Response.ContentType = "application/octet-stream";

            foreach(var rentedCD in rentedCDs)
            {
                sw.WriteLine($"{rentedCD.RentalID}, {rentedCD.Title}, {rentedCD.FirstName}, {rentedCD.LastName}, {rentedCD.DateRented}");
            }

            Response.Write(sw.ToString());

            Response.End();

        }
    }
}