using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace LonghornAirlines.Controllers.Manager
{
    public class FlightsController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;

        public FlightsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flights.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightID,Date,hasDeparted")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Pilots = GetAllPilots();
            ViewBag.CoPilots = GetAllCoPilots();
            ViewBag.Attendants = GetAllAttendants();
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightID,Date,hasDeparted")] Flight flight)
        {
            if (id != flight.FlightID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pilots = GetAllPilots();
            ViewBag.CoPilots = GetAllCoPilots();
            ViewBag.Attendants = GetAllAttendants();
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightID == id);
        }
        public SelectList GetAllRoutes()
        {
            List<Route> routes = _context.Routes.ToList();
            SelectList AllRoutes = new SelectList(routes, "RouteID", "Route");
            return AllRoutes;
        }

        public async Task<SelectList> GetAllPilotsAsync()
        {
            //find all the users in the role
            var pilotsQuery = await _userManager.GetUsersInRoleAsync("Pilot");
            //convert from task to list
            List<AppUser> members = new List<AppUser>(pilotsQuery);
            SelectList AllPilotsTask = new SelectList(members, "Id", "FullName");
            return AllPilotsTask;
        }
        public SelectList GetAllPilots()
        {
            var list = GetAllPilotsAsync();
            SelectList AllPilots = list.Result;
            return AllPilots;
        }
        public async Task<SelectList> GetAllCoPilotsAsync()
        {
            //find all the users in the role
            var copilotsQuery = await _userManager.GetUsersInRoleAsync("Co-Pilot");
            //convert from task to list
            List<AppUser> members = new List<AppUser>(copilotsQuery);
            SelectList AllCoPilotsTask = new SelectList(members, "Id", "FullName");
            return AllCoPilotsTask;
        }
        public SelectList GetAllCoPilots()
        {
            var list = GetAllCoPilotsAsync();
            SelectList AllCoPilots = list.Result;
            return AllCoPilots;
        }
        public async Task<SelectList> GetAllAttendantsAsync()
        {
            //find all the users in the role
            var attendantsQuery = await _userManager.GetUsersInRoleAsync("Flight Attendant");
            //convert from task to list
            List<AppUser> members = new List<AppUser>(attendantsQuery);
            SelectList AllAttendantsTask = new SelectList(members, "Id", "FullName");
            return AllAttendantsTask;
        }
        public SelectList GetAllAttendants()
        {
            var list = GetAllAttendantsAsync();
            SelectList AllAttendants = list.Result;
            return AllAttendants;
        }
    }
}
