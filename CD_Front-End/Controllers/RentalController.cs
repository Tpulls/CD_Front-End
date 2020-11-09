using CD_Front_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CD_Front_End.ViewModels;
using System.Web.Configuration;

namespace CD_Front_End.Controllers
{


    public class RentalController : Controller
    {

        #region Index
        // GET: Rental
        /// <summary>
        /// Method to collect the data fields according to the statement
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Get the collection of Rental records
            IEnumerable<Rental> rentals = WebClient.ApiRequest<Rental>.GetEnumerable("Rentals");
            // Get the collection of CD records
            IList<CD> CDs = WebClient.ApiRequest<CD>.GetList("CDs");
            // Get the collection of Staff records
            IList<Staff> staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");
            // Create the pointers for the rentalsViewModel
            var rentalsViewModel = rentals.Select(r => new RentalsViewModel()
            {
                RentalID = r.RentalID,
                DateRented = r.DateRented,
                DateReturned = r.DateReturned,
                FirstName = staffs.Where(c => c.StaffID == r.StaffID).Select(s => s.FirstName).FirstOrDefault(),
                LastName = staffs.Where(c => c.StaffID == r.StaffID).Select(s => s.LastName).FirstOrDefault()
            }).OrderByDescending(o => o.RentalID).ToList();

            return View(rentalsViewModel);
        }

        #endregion

        #region Details
        /// <summary>
        /// Method to retrieve the selected rental data set
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            // Get a singular record based on the id stored and store as a variable
            var rental = WebClient.ApiRequest<Rental>.GetSingleRecord($"Rentals/{id}");

            IList<Staff> staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");

            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");

            rental.RentalItems = WebClient.ApiRequest<RentalItem>.GetList($"RentalItemsById/{id}");

            var rentalDetails = new RentalDetailsViewModel
            {
                Rental = rental,
                FirstName = staffs.Where(Staff => Staff.StaffID == rental.StaffID).Select(st => st.FirstName).FirstOrDefault(),
                LastName = staffs.Where(Staff => Staff.StaffID == rental.StaffID).Select(st => st.LastName).FirstOrDefault(),
                RentedCDs = rental.RentalItems.Select(rentalItem => new CDsViewModel
                {
                    RentalID = rentalItem.RentalID,
                    Title = cds.Where(cd => cd.cdID == rentalItem.cdID).Select(sel => sel.Title).FirstOrDefault()
                }).ToList()
            };

            return View(rentalDetails);
        }
        #endregion

        #region Create
        /// <summary>
        /// Method to create a new rental
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var rental = new Rental {
                DateRented = DateTime.Now,
                Staffs = GetStaff(),
                RentedCDs = new List<CDsViewModel>()
            };

            return View(rental);
        }

        /// <summary>
        /// Method to create a new rented 
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            try
            {
                rental = WebClient.ApiRequest<Rental>.Post("Rentals", rental);


                return RedirectToAction("Edit", new { id = rental.RentalID });
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// Method to edit a data set
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var rental = WebClient.ApiRequest<Rental>.GetSingleRecord($"Rentals/{id}");
            IList<RentalItem> rentalItems = WebClient.ApiRequest<RentalItem>.GetList($"RentalItemsById/{id}");
            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");
            rental.Staffs = GetStaff();

            var rentedCDs = rentalItems.Select(ri => new CDsViewModel
            {
                RentalItemID = ri.RentalItemID,
                RentalID = ri.RentalID,
                Title = cds.Where(m => m.cdID == ri.cdID).Select(s => s.Title).FirstOrDefault()
            }).ToList();

            rental.RentedCDs = rentedCDs;
            return View(rental);
        }

        /// <summary>
        /// Method to edit a selected data set
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rental"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Rental rental)
        {
            try
            {
                if (WebClient.ApiRequest<Rental>.Put($"Rentals/{id}", rental))
                    return RedirectToAction("Index");
                return View(rental);

            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Method to delete a record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {

            var rental = WebClient.ApiRequest<Rental>.GetSingleRecord($"Rentals/{id}");

            IList<Staff> staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");

            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");

            rental.RentalItems = WebClient.ApiRequest<RentalItem>.GetList($"REntalItemsById/{id}");

            var rentalDetails = new RentalDetailsViewModel
            {
                Rental = rental,
                FirstName = staffs.Where(Staff => Staff.StaffID == rental.StaffID).Select(staf => staf.FirstName).FirstOrDefault(),
                LastName = staffs.Where(Staff => Staff.StaffID == rental.StaffID).Select(staf => staf.LastName).FirstOrDefault(),
                RentedCDs = rental.RentalItems.Select(RentalItem => new CDsViewModel
                {
                    RentalID = RentalItem.RentalID,
                    Title = cds.Where(cd => cd.cdID == RentalItem.cdID).Select(t => t.Title).FirstOrDefault()
                }).ToList()
            };
            return View(rentalDetails);
        }

        // Method to delete a selected
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Post a delete request according to the id input
                WebClient.ApiRequest<RentalsViewModel>.Delete($"Rentals/{id}");
                // Return to the index screen to see the deletion
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Add CDs

        public ActionResult AddCDs(int rentalID)
        {
            var rentalItem = new RentalItem
            {
                RentalID = rentalID,
                CDs = getCDs()
            };
            return View(rentalItem);
        }

        // Method to add a CD to a rental
        [HttpPost]
        public ActionResult AddCDs(RentalItem rentalItem)
        {
            try
            {
                WebClient.ApiRequest<RentalItem>.Post("RentalItems", rentalItem);
                return RedirectToAction("Edit", new { id = rentalItem.RentalID });
            }
            catch
            {
                return View();
            }
        }
        #endregion

        /// <summary>
        ///  Method to edit a rentalItem in a rental
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Edit CDs
        public ActionResult editRentedCDs(int id)
        {
            var rentalItem = WebClient.ApiRequest<RentalItem>.GetSingleRecord($"RentalItems/{id}");
            rentalItem.CDs = getCDs();

            return View(rentalItem);
        }

        [HttpPost]
        public ActionResult editRentedCDs(int id, RentalItem rentalItem)
        {

            try
            {
                if (WebClient.ApiRequest<RentalItem>.Put($"RentalItems/{id}", rentalItem))
                    return RedirectToAction("Edit", new { id = rentalItem.RentalID });

                return View(rentalItem);
            }
            catch
            {
                return View();
            }
        }

        #endregion

        /// <summary>
        ///  Method to delete a rentalItem in a Rental record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Delete CDS
        public ActionResult DeleteRentedCD(int id)
        {
            var rentalItem = WebClient.ApiRequest<RentalItem>.GetSingleRecord($"RentalItems/{id}");
            rentalItem.CDs = getCDs();

            return View(rentalItem);
        }

        /// <summary>
        ///  Method to delete a rentalItem in a Rental record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRentedCD(int id, RentalItem rentalItem)
        {
            try
            {
                rentalItem = WebClient.ApiRequest<RentalItem>.Delete($"RentalItems/{id}");
                return RedirectToAction("Edit", new { id = rentalItem.RentalID });
            }
            catch
            {
                return View();
            }
        }

            #endregion

            #region Helper Methods
                /// <summary>
                /// Method to handle the staff populated list
                /// </summary>
                /// <returns></returns>
            private IEnumerable<SelectListItem> GetStaff()
        {
            IList<Staff> staffs = WebClient.ApiRequest<Staff>.GetList("Staffs");

            List<SelectListItem> ddStaff = staffs.OrderBy(o => o.StaffID).Select(c => new SelectListItem
            {
                Value = c.StaffID.ToString(),
                Text = c.StaffID + ". " + c.FirstName + " " + c.LastName
            }).ToList();
            return new SelectList(ddStaff, "Value", "Text");
        }

        /// <summary>
        /// Method to populate the CD dropdown list
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> getCDs()
        {
            IList<CD> cds = WebClient.ApiRequest<CD>.GetList("CDs");
            List<SelectListItem> ddCD = cds.OrderBy(o => o.Title).Select(s => new SelectListItem
            {
                Value = s.cdID.ToString(),
                Text = s.Title
            }).ToList();
            return new SelectList(ddCD, "Value", "Text");
        }

        #endregion
    }
}
