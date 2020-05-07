using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LonghornAirlines.Models.ViewModels;
using LonghornAirlines.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using LonghornAirlines.Models.Business;

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

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult DisplayReport (ReportsViewModel rvm)
       // {
         //   var query = from r in _db.Tickets
           //             select r;
            //if(rvm.)
        //}
    }
}