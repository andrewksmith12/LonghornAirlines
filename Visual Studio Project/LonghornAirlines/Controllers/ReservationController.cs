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
            Reservation reservation = _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).First(r => r.ReservationID == id);
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
            Reservation reservation = await CreateBlankReservation(TypeOfReservation.OneWay);

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
        private async Task<Reservation> CreateBlankReservation(TypeOfReservation type)
        {
            Reservation reservation = new Reservation
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

        private void CreateTickets(Reservation reservation, Int32 flightID, Int32 passengerCount)
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

        public async Task<SelectList> GetReservationCustomersAsync(Reservation reservation)
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
    }
}

/*         // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Reservations/Create
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