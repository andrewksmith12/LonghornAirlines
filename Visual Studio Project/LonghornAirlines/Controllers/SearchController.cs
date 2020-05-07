using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.Users;
using LonghornAirlines.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LonghornAirlines.Controllers
{
    public class SearchController : Controller
    {
        private AppDbContext _db;
        public SearchController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult FlightResults(BookingSearchModel bookingSearchModel)
        {
            ViewBag.CityToName = _db.Cities.FirstOrDefault(c => c.CityID == bookingSearchModel.ArriveCityID).CityName;
            ViewBag.CityFromName = _db.Cities.FirstOrDefault(c => c.CityID == bookingSearchModel.DepartCityID).CityName;
            var query = from f in _db.Flights
                        select f;

            query = query.Where(f => f.Date.Date == bookingSearchModel.DepartDate.Date);
            query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == bookingSearchModel.DepartCityID);
            query = query.Where(f => f.FlightInfo.Route.CityTo.CityID == bookingSearchModel.ArriveCityID);
            query = query.Where(f => f.Canceled == false);

            //Passing Booking Search Model information to view bag so it goes to ReservationController
            //There's probably a better way to do this
            ViewBag.DepartingFlightsQty = query.Count();
            ViewBag.isRoundTrip = bookingSearchModel.isRoundTrip;
            ViewBag.passengerCount = bookingSearchModel.PassengerCount;
            ViewBag.ReturnDate = bookingSearchModel.ArriveDate;
            return View("FlightResults", query.Include(f => f.FlightInfo)
                .Include(f => f.FlightInfo.Route)
                .Include(f => f.FlightInfo.Route.CityFrom)
                .Include(f => f.FlightInfo.Route.CityTo)
                .ToList());
        }

        [HttpGet]
        public IActionResult TicketCustomerSearch(Int32 TicketID)
        {
            Ticket t = _db.Tickets.Find(TicketID);
            CustomerSearchModel csm = new CustomerSearchModel
            {
                ExistingCustomer = true,
                TicketID = t.TicketID
            };
            return View(csm);
        }

        [HttpGet]
        public IActionResult CustomerCreateAccount(Int32 TicketID)
        {
            CustomerCreationModel ccm = new CustomerCreationModel();
            ccm.TicketID = TicketID;

            return View("Customer_CustomerCreation", ccm);
        }

        [HttpPost]
        public IActionResult CustomerCreateAccount(CustomerCreationModel ccm)
        {
            return RedirectToAction("Register", "Account", new { TicketID = ccm.TicketID });
        }

        [HttpPost]
        public IActionResult TicketCustomerSearch(CustomerSearchModel customerSearchModel)
        {
            var query = from c in _db.Users
                        select c;
            if (customerSearchModel.LastName != null && customerSearchModel.LastName != "")
            {
                query = query.Where(c => c.LastName.Contains(customerSearchModel.LastName));
            }
            if (customerSearchModel.AdvantageNumber != null)
            {
                query = query.Where(c => c.AdvantageNumber == (customerSearchModel.AdvantageNumber));
            }
            List<AppUser> SelectedUsers = query.ToList();
            try
            {
                ViewBag.TicketID = customerSearchModel.TicketID;
            }
            catch
            {
                ViewBag.TicketID = -1;
            }
            return View("TicketUsersList", SelectedUsers);
        }
    }
}