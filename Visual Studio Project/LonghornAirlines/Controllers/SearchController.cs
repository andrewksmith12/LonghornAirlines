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
            var query = from f in _db.Flights
                        select f;

            //One way trips, only include flight from departCity to arriveCity
            query = query.Where(f => f.Date.Date == bookingSearchModel.DepartDate.Date);
            query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == bookingSearchModel.DepartCityID);
            query = query.Where(f => f.FlightInfo.Route.CityTo.CityID == bookingSearchModel.ArriveCityID);
            ViewBag.DepartingFlightsQty = query.Count();
            
            //If is roundtrip, add flights on arrival date from arriveCity to departCity
            if (bookingSearchModel.isRoundTrip)
            {
                var returnQuery = from f in _db.Flights
                            select f;
                returnQuery = returnQuery.Where(f => f.Date.Date == bookingSearchModel.ArriveDate.Date);
                returnQuery = returnQuery.Where(f => f.FlightInfo.Route.CityFrom.CityID == bookingSearchModel.ArriveCityID);
                returnQuery = returnQuery.Where(f => f.FlightInfo.Route.CityTo.CityID == bookingSearchModel.DepartCityID);
                
                foreach(Flight f in returnQuery)
                {
                    query = query.Append(f);
                }
            }
            return View("FlightResults", query.Include(f => f.FlightInfo).ToList());
        }
    }
}