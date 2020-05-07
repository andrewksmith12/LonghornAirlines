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

        public IActionResult Index(ReportsViewModel rvm)
        {
            ViewBag.AllCities = GetAllCities();
           // ViewBag.AllClasses = GetAllClasses();
            return View(rvm);
        }

        public SelectList GetAllCities()
        {
            List<City> cityList = _db.Cities.ToList();

            return new SelectList(cityList.OrderBy(c => c.CityID), "CityID", "CityName");
        }

        public IActionResult DisplayReport (ReportsViewModel rvm)
        {
            ViewBag.CityToName = _db.Cities.FirstOrDefault(c => c.CityID == rvm.ArriveCityID).CityName;
            ViewBag.CityFromName = _db.Cities.FirstOrDefault(c => c.CityID == rvm.DepartCityID).CityName;
            var query = from t in _db.Tickets
                        select t;
            if (rvm.DepartCityID != null)
            {
                query = query.Where(t => t.Flight.FlightInfo.Route.CityFrom.CityID == rvm.DepartCityID);
            }
            if (rvm.ArriveCityID != null)
            {
                query = query.Where(t => t.Flight.FlightInfo.Route.CityTo.CityID == rvm.ArriveCityID);
            }

            if (rvm.DepartDate != null)
            {
                query = query.Where(t => t.Flight.Date > rvm.DepartDate);
            }

            if (rvm.ArriveDate != null)
            {
                query = query.Where(t => t.Flight.Date > rvm.ArriveDate);
            }

            List<Ticket> SelectedTickets = query.ToList();
            return View("Index", SelectedTickets);

        }
    }
}