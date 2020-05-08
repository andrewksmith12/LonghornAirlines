using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.Users;
using LonghornAirlines.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
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
            String cityToName = _db.Cities.FirstOrDefault(c => c.CityID == bookingSearchModel.ArriveCityID).CityName; ;
            String cityFromName = _db.Cities.FirstOrDefault(c => c.CityID == bookingSearchModel.DepartCityID).CityName; ;
            ViewBag.CityToName = cityToName;
            ViewBag.CityFromName = cityFromName;
            if (cityToName == cityFromName)
            {
                String message = "Cities cannot be the same";
                return RedirectToAction("Index", "Home", new { errorMessage = message });
            }
            if(bookingSearchModel.DepartDate.Date < DateTime.Now.Date || bookingSearchModel.ArriveDate.Date < DateTime.Now.Date)
            {
                String message = "You cannot get flights in the past";
                return RedirectToAction("Index", "Home", new { errorMessage = message });
            }
            var query = from f in _db.Flights
                        select f;

            query = query.Where(f => f.Date.Date == bookingSearchModel.DepartDate.Date);
            query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == bookingSearchModel.DepartCityID);
            query = query.Where(f => f.FlightInfo.Route.CityTo.CityID == bookingSearchModel.ArriveCityID);
            query = query.Where(f => f.Canceled == false);
            foreach(Flight f in query)
            {
                //If there are not enough seats on flight delete flight from query
                if(!Utilities.GetTakenSeats.isAvailable(f.FlightID, bookingSearchModel.PassengerCount, _db))
                {
                    query = query.Where(flight => flight.FlightID != f.FlightID);
                }
            }
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