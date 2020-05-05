using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
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
    }
}