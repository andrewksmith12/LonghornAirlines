using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Http;

namespace LonghornAirlines.Views
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }


        // Main action for reservations
        // Branches off into customer reservation, manager reservation, or an error message
        public async Task<IActionResult> Create(int FlightID, bool isRoundTrip, int NumPassengers)
        {
            if (User.IsInRole("Customer"))
            {
                return await CustomerCreate(FlightID, isRoundTrip, NumPassengers);
            }
            else if (User.IsInRole("Manager"))
            {
                //TODO: Manager Reservation Creation
                return View();
            }
            else
            {
                return View();
            }
        }

        // GET: Reservation/Details/5
        // Details page shows all tickets and allows for ticket change
        // This is the default page everyone sees after a reservation is created
        public async Task<ActionResult> Details(int id)
        {
            Models.Business.Reservation reservation = _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).First(r => r.ReservationID == id);
            SelectList reservationCustomers;
            reservationCustomers = await GetReservationCustomersAsync(reservation);
            ViewBag.ReservationCustomers = reservationCustomers;
            return View(reservation);
        }

        // Handles reservation creation for customers
        // Branches off into one way reservations or round trip reservations
        public async Task<IActionResult> CustomerCreate(int FlightID, bool isRoundTrip, int NumPassengers)
        {
            if (!isRoundTrip){
                return await CreateOneWayReservation(FlightID, NumPassengers);
            }
            else
            {
                return await CreateRoundTripReservation();
            }
        }

        // Handles creating one way reservations
        // Creates a blank one way reservation and adds {passengerCount} tickets to it if it's empty
        public async Task<ActionResult> CreateOneWayReservation(Int32 flightID, Int32 passengerCount)
        {
            Models.Business.Reservation reservation = await CreateBlankReservation(TypeOfReservation.OneWay);

            if (reservation.Tickets.Count() > 0)
            {
                return RedirectToAction("Details", new { id = reservation.ReservationID });
            }
            else
            {
                CreateTickets(reservation, flightID, passengerCount);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = reservation.ReservationID });
            }
        }
        
        //TODO: Implement Round Trip
        public async Task<ActionResult> CreateRoundTripReservation()
        {
            return View();
        }

        // Creates blank reservation
        private async Task<Models.Business.Reservation> CreateBlankReservation(TypeOfReservation type)
        {
            Models.Business.Reservation reservation = new Models.Business.Reservation
            {
                ReservationType = type,
                Customer = await _userManager.FindByNameAsync(User.Identity.Name),
                ReservationComplete = false,
                Tickets = new List<Ticket>()
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        private void CreateTickets(Models.Business.Reservation reservation, Int32 flightID, Int32 passengerCount)
        {
            for (int i = 0; i < passengerCount; i++)
            {
                Ticket reservationTicket = new Ticket
                {
                    Reservation = _context.Reservations.First(r => r.ReservationID == reservation.ReservationID),
                    Flight = _context.Flights.Include(f => f.FlightInfo).FirstOrDefault(f => f.FlightID == flightID)
                };
                reservation.Tickets.Add(reservationTicket);
                _context.Tickets.Add(reservationTicket);
                _context.Update(reservation);
            }
        }

        

        public ActionResult SelectSeat(Int32 ticketID, Int32 SelectedCustomerID)
        {
            Ticket ticket = _context.Tickets.FirstOrDefault(t => t.TicketID == ticketID);
            return View(ticket);
        }

        public async Task<SelectList> GetReservationCustomersAsync(Models.Business.Reservation reservation)
        {
            HashSet<AppUser> reservationUsers = new HashSet<AppUser>();
            Boolean hasUser = false;
            foreach (Ticket t in reservation.Tickets)
            {
                if (t.Customer != null)
                {
                    reservationUsers.Add(t.Customer);
                    hasUser = true;
                }
            }
            if (!hasUser)
            {
                reservationUsers.Add(await _userManager.FindByNameAsync(User.Identity.Name));
            }
            return new SelectList(reservationUsers, "UserID", "FirstName");

        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            //get all the reservations from the database
            List<Models.Business.Reservation> reservations = new List<Models.Business.Reservation>();

            if (User.IsInRole("Admin"))
            {
                reservations = _context.Reservations.Include(r => r.Tickets)
                    .ThenInclude (r => r.Flight)
                    .ThenInclude (r => r.FlightInfo)
                    .ToList();
            }

            else
            {
                reservations = _context.Reservations.Where(r => r.Customer.UserName == User.Identity.Name).Include(r => r.Tickets)
                    .ThenInclude(r => r.Flight)
                    .ThenInclude(r => r.FlightInfo)
                    .ToList();
            }

            return View(reservations);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a reservation to view!" });
            }

            //update this statement to have an include clause to get the reservation details and ticket info
            Models.Business.Reservation reservation = await _context.Reservations
                .Include( r => r.Tickets)
                .ThenInclude( r => r.Flight)
                .ThenInclude(r => r.FlightInfo)
                .FirstOrDefaultAsync(m => m.ReservationID == id);

            if (reservation == null) //reservation not found
            {
                return View("Error", new String[] { "Cannot find this reservation!" });
            }

            if (User.IsInRole("Admin") == false && reservation.Customer.UserName != User.Identity.Name) //they are trying to see something that isn't theirs
            {
                return View("Error", new String[] { "Unauthorized: You are attempting to view another customer's reservation!" });
            }

            return View(reservation);
        }

    }
}

     

/*        // GET: Reservations/Create
        public IActionResult Create(TypeOfReservation ReservationType)
        {
            Reservation res = new Reservation();
            res.ReservationType = ReservationType;
            return View(res);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,ReservationType,ReservationMethod,ReservationComplete")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,ReservationType,ReservationMethod,ReservationComplete")] Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationID))
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    */