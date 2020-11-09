using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CD_Front_End.Models;

namespace CD_Front_End.Controllers
{
    public class CDController : Controller
    {

        #region Index

        /// <summary>
        /// Get the Datatable and display in the Index directory view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Use IEnumerate to source the data table collection
            IEnumerable<CD> cds = WebClient.ApiRequest<CD>.GetEnumerable("CDs");
            //Return the data table
            return View(cds);
        }

        #endregion

        #region Details
        // GET: CD/Details/5
        /// <summary>
        /// Method to source the details of individual records and in the details page view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            // Initialiaze a variable that hold the ID specified record in CDs
            var cd = WebClient.ApiRequest<CD>.GetSingleRecord($"CDs/{id}");
            // Return the record to the details view
            return View(cd);
        }
        #endregion

        #region Create
        // GET: CD/Create
        /// <summary>
        /// Blank create page for new entries
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: CD/Create
        /// <summary>
        /// Method to post new data records to the datatable
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CD cD)
        {
            try
            {
                // Create a request to post a new record to the BackEnd datatable
                WebClient.ApiRequest<CD>.Post("CDs", cD);
                // If successfull, return a message
                TempData["SuccessMessage"] = "Record added successfully.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit

        // GET: CD/Edit/5
        /// <summary>
        /// Method to Edit data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            // Create a request for the single record according to the id input
            var cd = WebClient.ApiRequest<CD>.GetSingleRecord($"CDS/{id}");
            // Returnt the record to the view
            return View(cd);
        }

        // POST: CD/Edit/5
        /// <summary>
        /// Method to Edit and update data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, CD cd)
        {
            try
            {
                // Request to change the data field to the new input data fields
                if(WebClient.ApiRequest<CD>.Put($"CDs/{id}", cd))
                {
                    TempData["SuccessMessage"] = "Record updated successfully.";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        // GET: CD/Delete/5
        /// <summary>
        ///  Method to view the record prior to deletion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            // Request a single record based off the id input
            var cd = WebClient.ApiRequest<CD>.GetSingleRecord($"CDs/{id}");
            // Return the record to the view
            return View(cd);
        }

        // POST: CD/Delete/5
        /// <summary>
        /// Method to delete a data field from the data table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collec
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Request to delete the record based off the input id
                WebClient.ApiRequest<CD>.Delete($"CDs/{id}");
                TempData["SuccessMessage"] = "Record deleted successfully";
                                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Drag and Drop

        /// <summary>
        ///  Method to save the uploaded picture to a folder
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach(var file in files)
            {
                // Make sure to create a folder in the root directory of your project and name it 'UploadedFiles'
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName));
            }
            return Json("File uploaded successfully!");
        }
        #endregion

        #region Chart

        /// <summary>
        ///  Method to open the chart according to the Rental Controller methods
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenChart()
        {
            return RedirectToAction("GetRentalCountData", "Report");
        }
        #endregion
    }
}
