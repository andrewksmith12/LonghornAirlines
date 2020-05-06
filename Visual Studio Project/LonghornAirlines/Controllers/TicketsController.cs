using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.ViewModels;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LonghornAirlines.Controllers
{
    public class TicketsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TicketsController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Fare,Seat")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.Include(t => t.Reservation).Include(t => t.Customer).FirstAsync(t => t.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }
            Int32 customerID;
            Int32 reservationID;
            try
            {
                customerID = ticket.Customer.UserID;
            }
            catch {
                customerID = -1;
            }
            try
            {
                reservationID = ticket.Reservation.ReservationID;
            }
            catch
            {
                reservationID = -1;
            }
            TicketCreationModel tcm = new TicketCreationModel
            {
                TicketID = ticket.TicketID,
                CustomerID = customerID,
                SeatID = ""
            };
            SelectList reservationCustomers;
            reservationCustomers = await GetReservationCustomersAsync(reservationID);
            ViewBag.ReservationCustomers = reservationCustomers;
            return View(tcm);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,Fare,Seat")] Ticket ticket)
        {
            if (id != ticket.TicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketID))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketID == id);
        }

        public async Task<SelectList> GetReservationCustomersAsync(Int32 ReservationID)
        {
            Models.Business.Reservation reservation = _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Customer).First(r => r.ReservationID == ReservationID);
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
