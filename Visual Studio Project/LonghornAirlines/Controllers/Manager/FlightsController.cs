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

            var flight = await _context.Flights.Include(m => m.FlightInfo).Include(m => m.Pilot).Include(m => m.CoPilot).Include(m => m.Attendant).Include(m => m.Tickets).ThenInclude(m => m.Customer)
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
        public async Task<IActionResult> Edit(int id, [Bind("FlightID,Date,hasDeparted")] Flight flight, int SelectedPilot, int SelectedCoPilot, int SelectedAttendant)
        {
            if (id != flight.FlightID)
            {
                return NotFound();
            }

            Flight dbFlight = _context.Flights.Find(flight.FlightID);
            dbFlight.Pilot = _context.Users.FirstOrDefault(f => f.UserID == SelectedPilot);
            dbFlight.CoPilot = _context.Users.FirstOrDefault(f => f.UserID == SelectedCoPilot);
            dbFlight.Attendant = _context.Users.FirstOrDefault(f => f.UserID == SelectedAttendant);

            _context.Flights.Update(dbFlight);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = dbFlight.FlightID });
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

        [HttpGet]
        public IActionResult CheckIn(int id)
        {
            var Flight = _context.Flights.Include(f => f.Tickets).ThenInclude(f => f.Customer).Include(f => f.Pilot).Include(f => f.CoPilot).Include(f => f.Attendant).FirstOrDefault(f => f.FlightID == id);
            return View("CheckIn", Flight);
        }

        [HttpPost]
        public IActionResult Checkin()
        {
            return View("Manifest");
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
            SelectList AllPilotsTask = new SelectList(members, "UserID", "FullName");
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
            SelectList AllCoPilotsTask = new SelectList(members, "UserID", "FullName");
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
            SelectList AllAttendantsTask = new SelectList(members, "UserID", "FullName");
            return AllAttendantsTask;
        }
        public SelectList GetAllAttendants()
        {
            var list = GetAllAttendantsAsync();
            SelectList AllAttendants = list.Result;
            return AllAttendants;
        }

        public IActionResult CheckInTicket(int id)
        {
            Ticket dbTicket =_context.Tickets.Include(f => f.Flight).FirstOrDefault(f => f.TicketID == id);
            dbTicket.CheckedIn = true;
            _context.Tickets.Update(dbTicket);
            _context.SaveChanges();
            return RedirectToAction("CheckIn", new { id = dbTicket.Flight.FlightID });
        }

        public IActionResult CheckInSpecial(int id, string person)
        {
            Flight dbFlight = _context.Flights.FirstOrDefault(f => f.FlightID == id);
            if(person == "Pilot")
            {
                dbFlight.PilotCheckIn = true;
            }
            if (person == "Co-Pilot")
            {
                dbFlight.CoPilotCheckIn = true;
            }
            if (person == "Attendant")
            {
                dbFlight.AttendantCheckIn = true;
            }
            _context.Flights.Update(dbFlight);
            _context.SaveChanges();
            return RedirectToAction("CheckIn", new { id = dbFlight.FlightID });
        }

        public IActionResult FinalizeCheckin(int id)
        {
            Flight dbFlight = _context.Flights.Include(f => f.Tickets).ThenInclude(f => f.Customer).Include(f => f.Pilot).Include(f => f.CoPilot).Include(f => f.Attendant).FirstOrDefault(f => f.FlightID == id);
            if (dbFlight.Pilot != null)
            {
                if (dbFlight.CoPilot != null)
                {
                    if (dbFlight.Attendant != null)
                    {
                        if (dbFlight.PilotCheckIn == true)
                        {
                            if(dbFlight.CoPilotCheckIn == true)
                            {
                                if(dbFlight.AttendantCheckIn == true)
                                {
                                    return View("Index");
                                }
                                else
                                {
                                    ViewBag.Error = "Attendant must be Checked-In to Takeoff!";
                                    return (View("CheckIn", new { id = dbFlight.FlightID }));
                                }
                            }
                            else
                            {
                                ViewBag.Error = "CoPilot must be Checked-In to Takeoff!";
                                return (View("CheckIn", new { id = dbFlight.FlightID }));
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Pilot must be Checked-In to Takeoff!";
                            return (View("CheckIn", new { id = dbFlight.FlightID }));
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Flight must have a Attendant Assigned in Order to Takeoff!";
                        return (View("CheckIn", new { id = dbFlight.FlightID }));
                    }
                }
                else
                {
                    ViewBag.Error = "Flight must have a Co-Pilot Assigned in Order to Takeoff!";
                    return (View("CheckIn", new { id = dbFlight.FlightID }));
                }
            }
            else
            {
                ViewBag.Error = "Flight must have a Pilot Assigned in Order to Takeoff!";
                return (View("CheckIn", new { id = dbFlight.FlightID }));
            }
        }

    }
}
