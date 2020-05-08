using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LonghornAirlines.Controllers
{
    [Authorize(Roles ="Employee")]
    public class EmployeeViewsController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        public EmployeeViewsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult GateAgent()
        {
            List<Flight> query = new List<Flight>();
            query = _context.Flights.Include(f => f.FlightInfo).ThenInclude(f => f.Route).ThenInclude(f => f.CityTo)
            .Include(f => f.Pilot).Include(f => f.CoPilot).Include(f => f.Attendant) 
            .Where(f => f.Date == DateTime.Now.Date && f.Canceled == false).ToList();
            return View(query);
        }
    }
}