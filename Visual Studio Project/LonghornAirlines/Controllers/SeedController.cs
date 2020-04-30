using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using LonghornAirlines.Models.Users;

namespace LonghornAirlines.Controllers
{
    public class SeedController : Controller
    {
        private AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public SeedController(AppDbContext context, IServiceProvider service)
        {
            _db = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();

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
                return View("Error", new String[] { "There was an error adding cities to the database",
                                                    ex.Message,
                                                    ex.InnerException.Message,
                                                    ex.InnerException.InnerException.Message});
            }

            return View("Confirm");
        }
        public IActionResult SeedRoutes()
        {
            try
            {
                Seeding.SeedRoutes.SeedAllRoutes(_db);
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error adding routes to the database",
                                                    ex.Message,
                                                    ex.InnerException.Message,
                                                    ex.InnerException.InnerException.Message});
            }

            return View("Confirm");
        }
        public IActionResult SeedFlightInfos()
        {
            try
            {
                Seeding.SeedFlightInfos.SeedAllFlightInfos(_db);
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error adding flight infos to the database",
                                                    ex.Message,
                                                    ex.InnerException.Message,
                                                    ex.InnerException.InnerException.Message});
            }

            return View("Confirm");
        }
        public IActionResult SeedFlights()
        {
            try
            {
                Seeding.SeedFlights.SeedAllFlights(_db);
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error adding flights to the database",
                                                    ex.Message,
                                                    ex.InnerException.Message,
                                                    ex.InnerException.InnerException.Message});
            }

            return View("Confirm");
        }
        public IActionResult SeedRoles(IServiceProvider service)
        {
            Seeding.SeedRoles.AddRoles(service).Wait();
            return View("Confirm");
        }
        public IActionResult SeedCustomers(IServiceProvider service)
        {
            Seeding.SeedCustomers.AddCustomers(service).Wait();
            return View("Confirm");
        }
        public IActionResult SeedEmployeess(IServiceProvider service)
        {
            Seeding.SeedEmployees.AddEmployees(service).Wait();
            return View("Confirm");
        }
        public IActionResult SeedAdmin(IServiceProvider service)
        {
            Seeding.SeedAdminUser.SeedAdmin(service).Wait();
            return View("Confirm");
        }
    }
}