using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LonghornAirlines.Controllers
{
    public class ReportController : Controller
    {
        private AppDbContext _db;

        public ReportController(AppDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateReport()
        {
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        public SelectList GetAllCities()
        {
            List<City> cityList = _db.Cities.ToList();

            return new SelectList(cityList.OrderBy(c => c.CityID), "CityID", "CityName");
        }

        public IActionResult DisplayReport (ReportsViewModel rvm)
        {
            //ViewBag.CityToName = _db.Cities.FirstOrDefault(c => c.CityID == rvm.ArriveCityID).CityName;
            //ViewBag.CityFromName = _db.Cities.FirstOrDefault(c => c.CityID == rvm.DepartCityID).CityName;
            //ViewBag.DepartDate = _db.Flights.FirstOrDefault(f => f.Date == rvm.DepartDate);
            //ViewBag.ArriveDate = _db.Flights.FirstOrDefault(f => f.Date == rvm.ArriveDate);

            var query = from t in _db.Tickets
                        select t;

            if (rvm.DepartCityID != 0)
            {
                query = query.Where(t => t.Flight.FlightInfo.Route.CityFrom.CityID == rvm.DepartCityID);
            }

            if (rvm.ArriveCityID != 0)
            {
                query = query.Where(t => t.Flight.FlightInfo.Route.CityTo.CityID == rvm.ArriveCityID);
            }

            if (rvm.DepartDate != null)
            {
                query = query.Where(t => t.Flight.Date >= rvm.DepartDate);
            }

            if (rvm.ArriveDate != null)
            {
                query = query.Where(t => t.Flight.Date <= rvm.ArriveDate);
            }


            if(rvm.FirstClass != false)
            {
                query = query.Where(t => t.Seat == "1A" || t.Seat == "1B" || t.Seat == "2A" || t.Seat == "2B");
            }

            if(rvm.Economy != false)
            {
                query = query.Where(t => t.Seat == "3A" || t.Seat == "3B" || t.Seat == "3C" || t.Seat == "3D" || t.Seat == "4A" || t.Seat == "4B" || t.Seat == "4C" || t.Seat == "4D" || t.Seat == "5A" || t.Seat == "5B" || t.Seat == "5C" || t.Seat == "5D");
            }

            List<Ticket> SelectedTickets = query.ToList();
            int NumofPassengers = SelectedTickets.Count;
            ViewBag.PassengerCount = NumofPassengers;
            decimal totalRevenue = SelectedTickets.Sum(t => t.Fare);
            ViewBag.Revenue = totalRevenue;

            return View("DisplayReport", rvm);

        }

        public IActionResult GenerateFlightManifest(ReportsViewModel rvm)
        {
            var query = from f in _db.Flights
                        select f;

            return View("FlightManifest");
        }

        public IActionResult DisplayManifest()
        {
            return View("DisplayManifest");
        }
    }
}