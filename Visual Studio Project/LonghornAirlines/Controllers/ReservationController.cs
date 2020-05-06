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
using LonghornAirlines.Models.ViewModels;

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
        public async Task<IActionResult> Create(int FlightID, bool isRoundTrip, int NumPassengers, Int32 cityToID, Int32 cityFromID, DateTime returnDate)
        {
            if (User.IsInRole("Customer"))
            {
                return await CustomerCreate(FlightID, isRoundTrip, NumPassengers, cityToID, cityFromID, returnDate);
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
        public async Task<ActionResult> Description(int id)
        {
            Models.Business.Reservation reservation = _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).First(r => r.ReservationID == id);
            return View(reservation);
        }

        // Handles reservation creation for customers
        // Branches off into one way reservations or round trip reservations
        public async Task<IActionResult> CustomerCreate(int FlightID, bool isRoundTrip, int NumPassengers, Int32 cityToID, Int32 cityFromID, DateTime returnDate)
        {
            if (!isRoundTrip){
                return await CreateOneWayReservation(FlightID, NumPassengers);
            }
            else
            {
                return await CreateRoundTripReservation(FlightID, NumPassengers, cityToID, cityFromID, returnDate);
            }
        }

        // Handles creating one way reservations
        // Creates a blank one way reservation and adds {passengerCount} tickets to it if it's empty
        public async Task<ActionResult> CreateOneWayReservation(Int32 flightID, Int32 passengerCount)
        {
            Models.Business.Reservation reservation = await CreateBlankReservation(TypeOfReservation.OneWay);

            if (reservation.Tickets.Count() > 0)
            {
                return RedirectToAction("Description", new { id = reservation.ReservationID });
            }
            else
            {
                CreateTickets(reservation.ReservationID, flightID, passengerCount);
                await _context.SaveChangesAsync();

                return RedirectToAction("Description", new { id = reservation.ReservationID });
            }
        }
        
        //TODO: Implement Round Trip
        public async Task<ActionResult> CreateRoundTripReservation(Int32 flightID, Int32 passengerCount, Int32 cityToID, Int32 cityFromID, DateTime returnDate)
        {
            Models.Business.Reservation reservation = await CreateBlankReservation(TypeOfReservation.RoundTrip);
            CreateTickets(reservation.ReservationID, flightID, passengerCount);
            await _context.SaveChangesAsync();

            BookingSearchModel bsm = new BookingSearchModel
            {
                DepartCityID = cityToID,
                ArriveCityID = cityFromID,
                DepartDate = returnDate,
                ArriveDate = returnDate,
                PassengerCount = passengerCount,
                isRoundTrip = false,
                ReservationID = reservation.ReservationID
            };

            return ReturnFlightLookup(bsm);
        }

        public async Task<ActionResult> FinishRoundTripReservation(Int32 NumPassengers, Int32 FlightID, Int32 ReservationID)
        {
            CreateTickets(ReservationID, FlightID, NumPassengers);
            await _context.SaveChangesAsync();

            return RedirectToAction("Description", new { id = ReservationID });
        }

        public ActionResult ReturnFlightLookup(BookingSearchModel bookingSearchModel)
        {
            ViewBag.CityToName = _context.Cities.FirstOrDefault(c => c.CityID == bookingSearchModel.ArriveCityID).CityName;
            ViewBag.CityFromName = _context.Cities.FirstOrDefault(c => c.CityID == bookingSearchModel.DepartCityID).CityName;
            var query = from f in _context.Flights
                        select f;

            query = query.Where(f => f.Date.Date == bookingSearchModel.DepartDate.Date);
            query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == bookingSearchModel.DepartCityID);
            query = query.Where(f => f.FlightInfo.Route.CityTo.CityID == bookingSearchModel.ArriveCityID);

            //Passing Booking Search Model information to view bag so it goes to ReservationController
            //There's probably a better way to do this
            ViewBag.DepartingFlightsQty = query.Count();
            ViewBag.isRoundTrip = bookingSearchModel.isRoundTrip;
            ViewBag.passengerCount = bookingSearchModel.PassengerCount;
            ViewBag.reservationID = bookingSearchModel.ReservationID;
            return View("ReturnLookup", query.Include(f => f.FlightInfo)
                .Include(f => f.FlightInfo.Route)
                .Include(f => f.FlightInfo.Route.CityFrom)
                .Include(f => f.FlightInfo.Route.CityTo)
                .ToList());
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

        private void CreateTickets(Int32 ReservationID, Int32 flightID, Int32 passengerCount)
        {
            Models.Business.Reservation reservation = _context.Reservations.Include(r => r.Tickets).First(r=> r.ReservationID == ReservationID);
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