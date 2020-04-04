using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LonghornAirlines.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db; 
        public HomeController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(BookingSearchModel model)
        {
            ViewBag.AllCities = GetAllCities();
            return View(model);
        }

        public SelectList GetAllCities()
        {
            List<City> cityList = _db.Cities.ToList();

            return new SelectList(cityList.OrderBy(c => c.CityID), "CityID", "CityName");
        }
    }
}