using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using Microsoft.AspNetCore.Mvc;

namespace LonghornAirlines.Controllers
{
    public class SeedController : Controller
    {
        private AppDbContext _db;

        public SeedController(AppDbContext context)
        {
            _db = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeedCities()
        {
            try
            {
                Seeding.SeedCities.SeedAllCities(_db);
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error adding genres to the database",
                                                    ex.Message,
                                                    ex.InnerException.Message,
                                                    ex.InnerException.InnerException.Message});
            }

            return View("Confirm");
        }
    }
}