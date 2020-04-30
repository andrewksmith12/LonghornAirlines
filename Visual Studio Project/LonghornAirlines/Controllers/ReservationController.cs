using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.Users;
using LonghornAirlines.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LonghornAirlines.Controllers
{
    public class ReservationController : Controller
    {
        private UserManager<AppUser> _userManager;
        private AppDbContext _db;

        public ReservationController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reservation/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Reservation reservation = _db.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).First(r => r.ReservationID == id);
            SelectList reservationCustomers;
            reservationCustomers = await GetReservationCustomersAsync(reservation);
            ViewBag.ReservationCustomers = reservationCustomers;
            return View(reservation);
        }

        //reservationType ->
        //  0 = one way
        //  1 = round trip
        public async Task<ActionResult> Create(int reservationType, int flightID, int passengerCount)
        {
            ViewBag.reservationType = reservationType;
            if(reservationType == 0)
            {
                return await CreateOneWayReservation(flightID, passengerCount);
            }
            else if(reservationType == 1)
            {
                return CreateRoundTripReservation();
            }
            return View();
        }

        // GET: Reservation/Create
        public async Task<ActionResult> CreateOneWayReservation(Int32 flightID, Int32 passengerCount)
        {
            Reservation reservation = await CreateReservation(TypeOfReservation.OneWay);
            if (reservation.Tickets.Count() > 0)
            {
                return RedirectToAction("Details", new { id = reservation.ReservationID});
            }

            CreateTickets(reservation, flightID, passengerCount);
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", new { id = reservation.ReservationID });
        }

        private async Task<Reservation> CreateReservation(TypeOfReservation type)
        {
            Reservation reservation = new Reservation
            {
                ReservationType = type,
                Customer = await _userManager.FindByNameAsync(User.Identity.Name),
                ReservationComplete = false,
                Tickets = new List<Ticket>()
            };
            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();
            return reservation;
        }

        private void CreateTickets(Reservation reservation, Int32 flightID, Int32 passengerCount)
        {
            for (int i = 0; i < passengerCount; i++)
            {
                Ticket reservationTicket = new Ticket
                {
                    Reservation = _db.Reservations.First(r => r.ReservationID == reservation.ReservationID),
                    Flight = _db.Flights.Include(f => f.FlightInfo).FirstOrDefault(f => f.FlightID == flightID)
                };
                reservation.Tickets.Add(reservationTicket);
                _db.Tickets.Add(reservationTicket);
                _db.Update(reservation);
            }
        }

        public ActionResult CreateRoundTripReservation()
        {
            return View();
        }

        public ActionResult SelectSeat(Int32 ticketID, Int32 SelectedCustomerID)
        {
            Ticket ticket = _db.Tickets.FirstOrDefault(t => t.TicketID == ticketID);
            return View(ticket);
        }

        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<SelectList> GetReservationCustomersAsync(Reservation reservation)
        {
            HashSet<AppUser> reservationUsers = new HashSet<AppUser>();
            Boolean hasUser = false;
            foreach(Ticket t in reservation.Tickets)
            {
                if(t.Customer != null)
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