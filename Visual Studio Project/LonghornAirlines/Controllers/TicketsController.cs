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

        public async Task<IActionResult> ChangeTicketPrice(Int32 id)
        {
            Ticket ticket = await _context.Tickets.FirstAsync(t => t.TicketID == id);
            return View(ticket);
        }

        public async Task<IActionResult> SavePriceChange(Ticket ticket)
        {
            Ticket _dbTicket = await _context.Tickets.Include(t => t.Reservation).FirstAsync(t => t.TicketID == ticket.TicketID);
            _dbTicket.Fare = ticket.Fare;
            _context.Update(_dbTicket);
            await _context.SaveChangesAsync();
            //TODO: SEND EMAIL NOTIFYING CHANGE OF PRICE 
            return RedirectToAction("ChangeTicketPrices","Reservation", new { ReservationID = _dbTicket.Reservation.ReservationID });
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
        public async Task<IActionResult> Edit(int? id, Int32? UserID)
        {

            SelectList reservationCustomers;
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.Include(t => t.Reservation).Include(t => t.Customer).Include(t => t.Flight).ThenInclude(flight => flight.FlightInfo).FirstAsync(t => t.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }
            Int32 customerID;
            Int32 reservationID;
            String SeatID;
            try
            {
                reservationID = ticket.Reservation.ReservationID;
            }
            catch
            {
                reservationID = -1;
            }
            if (UserID.HasValue)
            {
                customerID = UserID.Value;

                reservationCustomers = await GetReservationCustomersAsync(reservationID, UserID);
            }
            else
            {
                try
                {
                    customerID = ticket.Customer.UserID;
                }
                catch
                {
                    customerID = -1;
                }

                reservationCustomers = await GetReservationCustomersAsync(reservationID, null);
            }
            
            try
            {
                SeatID = ticket.Seat;
            }
            catch
            {
                SeatID = "";
            }
            TicketCreationModel tcm = new TicketCreationModel
            {
                TicketID = ticket.TicketID,
                CustomerID = customerID,
                SeatID = SeatID
            };
            ViewBag.ReservationCustomers = reservationCustomers;

            //Fist Class, Budget price
            ViewBag.firstClassFare = ticket.Flight.FlightInfo.FirstClassFare.ToString("C");
            ViewBag.baseFare = ticket.Flight.FlightInfo.BaseFare.ToString("C");
            ViewBag.takenSeats = Utilities.GetTakenSeats.FromFlight(ticket.Flight.FlightID, _context);
            return View(tcm);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(TicketCreationModel tcm)
        {
            Ticket ticket = _context.Tickets.Include(t => t.Customer).Include(t => t.Reservation).First(t => t.TicketID == tcm.TicketID);
            ticket.Seat = tcm.SeatID;
            ticket.Customer = _context.Users.First(c => c.UserID == tcm.CustomerID);
            Decimal fare = GetFare(tcm.TicketID, tcm.SeatID);
            ticket.Fare = fare;
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
            return RedirectToAction("Description", "Reservation", new { id = ticket.Reservation.ReservationID }); ;
        }

        private decimal GetFare(int ticketID, string seatID)
        {
            Ticket ticket = _context.Tickets.Include(t => t.Flight).Include(t => t.Customer).First(t => t.TicketID == ticketID);
            String[] firstClassSeats = { "1A", "1B", "2A", "2B" };
            String[] budgetSeats = { "3A", "3B", "3C", "3D",
                                     "4A", "4B", "4C", "4D",
                                     "5A", "5B", "5C", "5D"};
            Decimal fare;

            if (firstClassSeats.Contains(seatID))
            {
                fare = ticket.Flight.FirstClassFare;
            }
            else
            {
                fare = ticket.Flight.BaseFare;
                Int32 Age;
                DateTime today = DateTime.Now.Date;
                Decimal discount = 0;
                //Age Discounts
                try
                {
                    Int16 age = Convert.ToInt16(Math.Floor(today.Subtract(ticket.Customer.Birthday.Date).TotalDays / 365));
                    if (age > 65)
                    {
                        discount = .1m;
                    }
                    else if (age < 12)
                    {
                        discount = .15m;
                    }
                }
                catch
                {
                    discount = 0;
                }
                fare *= (1 - discount);
            }
            return fare;
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

        public async Task<ActionResult> AssignUser(Int32 TicketID, Int32 CustomerID)
        {
            Ticket t = _context.Tickets.First(tick => tick.TicketID == TicketID);
            AppUser user = _context.Users.First(c => c.UserID == CustomerID);

            t.Customer = user;
            _context.Update(t);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = TicketID });
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

        public async Task<SelectList> GetReservationCustomersAsync(Int32 ReservationID, Int32? NewCustomerID)
        {
            Models.Business.Reservation reservation = _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Customer).First(r => r.ReservationID == ReservationID);
            HashSet<AppUser> reservationUsers = new HashSet<AppUser>();
            foreach (Ticket t in reservation.Tickets)
            {
                if (t.Customer != null)
                {
                    reservationUsers.Add(t.Customer);
                }
            }
            reservationUsers.Add(await _userManager.FindByNameAsync(User.Identity.Name));
            if (NewCustomerID.HasValue)
            {
                reservationUsers.Add(_context.Users.First(u => u.UserID == NewCustomerID.Value));
            }
            return new SelectList(reservationUsers, "UserID", "FirstName");
        }


        [HttpGet]
        public IActionResult CustomerCreateAccount(Int32 TicketID)
        {
            CustomerCreationModel ccm = new CustomerCreationModel();
            ccm.TicketID = TicketID;

            return View("Customer_CustomerCreation", ccm);
        }

        public async Task<IActionResult> CustomerCreateAccount(CustomerCreationModel ccm)
        {
            AppUser user = new AppUser
            {
                //TODO: Add the rest of the custom user fields here
                UserName = ccm.Email,
                Email = ccm.Email,
                FirstName = ccm.FirstName,
                LastName = ccm.LastName,
                Birthday = ccm.Birthday,
                PhoneNumber = ccm.PhoneNumber,
                ZIP = ccm.ZIP,
                State = ccm.State,
                Street = ccm.Street,
                City = ccm.City,
                AdvantageNumber = Utilities.GenerateAccountNumber.GetFFNum(_context),
                UserID = Convert.ToInt32(ccm.AdvantageNumber),
                Mileage = 0
            };

            IdentityResult result = await _userManager.CreateAsync(user, ccm.Password);
            if (result.Succeeded)
            {
                //TODO: Add user to desired role. This example adds the user to the customer role
                await _userManager.AddToRoleAsync(user, "Customer");

                return await AssignUser(ccm.TicketID, user.UserID);
            }

            return View("Error", new { message = "Customer Creation Failed" });

        }
    }
}
