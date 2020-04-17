using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
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
            var query = from f in _db.Flights
                        select f;
            if (bookingSearchModel.isRoundTrip)
            {
                //TODO What if is round trip
            }
            else
            {
                //If its one-way
                query = query.Where(f => f.Date.Date == bookingSearchModel.DepartDate.Date);
                query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == bookingSearchModel.ArriveCityID || f.FlightInfo.Route.CityTo.CityID == bookingSearchModel.DepartCityID);
            }
            return View("FlightResults", query.Include(f => f.FlightInfo).ToList());
        }
    }
}