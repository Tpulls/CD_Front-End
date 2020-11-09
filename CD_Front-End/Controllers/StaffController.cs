using CD_Front_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CD_Front_End.Controllers
{
    public class StaffController : Controller
    {
        #region Index
        // GET: Staff
        /// <summary>
        ///  Method to GET the Staff Datatable and display
        /// </summary>
        /// <returns>Staff Index Page Information</returns>
        public ActionResult Index()
        {
            // Request the collection of data from the Staff Datatable
            IEnumerable<Staff> staffs = WebClient.ApiRequest<Staff>.GetEnumerable("Staffs");
            // Return the collection to the view
            return View(staffs);
        }

        #endregion

        #region Details
        // GET: Staff/Details/5
        /// <summary>
        /// Method to Get the Staff data field information and display
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details page and selected information</returns>
        public ActionResult Details(int id)
        {
            // Request a single record based off the id and store in a variable
            var staff = WebClient.ApiRequest<Staff>.GetSingleRecord($"Staffs/{id}");
            // Return the information contained in the variable to the view
            return View(staff);
        }
        #endregion

        #region Create
        // GET: Staff/Create
        /// <summary>
        /// Method to open a blank create form for new staff
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        /// <summary>
        /// Method to create a new staff data field and POST to the database
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Staff staff)
        {
            try
            {
                // Request to post the input(staff) to the database Staffs
                WebClient.ApiRequest<Staff>.Post("Staffs", staff);
                TempData["SuccessMessage"] = "Record added successfully.";
                // Return to the index page to see the Post
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        // GET: Staff/Edit/5

        /// <summary>
        /// Method to open a record and see the data field contents
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            // Request to get a single record based on the id input and store as a variable
            var staff = WebClient.ApiRequest<Staff>.GetSingleRecord($"Staffs/{id}");
            // Return to the view with the single record
            return View(staff);
        }

        // POST: Staff/Edit/5
        /// <summary>
        /// Method to request an update to a data field
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Staff staff)
        {
            try
            {
                // Request to update the current data field with the onscreen information
                if(WebClient.ApiRequest<Staff>.Put($"Staffs/{id}", staff))
                // Return to index page
                return RedirectToAction("Index");
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        // GET: Staff/Delete/5
        /// <summary>
        /// Method to request a single record be displayed onscreen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            // Find the record that matches the id input
            var staff = WebClient.ApiRequest<Staff>.GetSingleRecord($"Staffs/{id}");
            // Return to the view with the single record
            return View(staff);
        }

        // POST: Staff/Delete/5
        /// <summary>
        /// Method to delete a data field according to an id input
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Request to delete the staff id matching the id input
                WebClient.ApiRequest<Staff>.Delete($"Staffs/{id}");
                // Return to the index view to see the deletion
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion


    }
}
