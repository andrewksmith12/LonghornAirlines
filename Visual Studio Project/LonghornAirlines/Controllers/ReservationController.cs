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
                TempData["FlightID"] = FlightID;
                TempData["isRoundTrip"] = isRoundTrip;
                TempData["NumPassengers"] = NumPassengers;
                TempData["cityToID"] = cityToID;
                TempData["cityFromID"] = cityFromID;
                TempData["returnDate"] = returnDate;
                return RedirectToAction("ReservationCustomer", "Reservation");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> ManagerCreate(int FlightID, bool isRoundTrip, int NumPassengers, Int32 cityToID, Int32 cityFromID, DateTime returnDate, Int32 CustomerID)
        {
            if (!isRoundTrip)
            {
                return await CreateOneWayReservation(FlightID, NumPassengers, CustomerID);
            }
            else
            {
                return await CreateRoundTripReservation(FlightID, NumPassengers, cityToID, cityFromID, returnDate, CustomerID);
            }
        }
        
        // Handles reservation creation for customers
        // Branches off into one way reservations or round trip reservations
        public async Task<IActionResult> CustomerCreate(int FlightID, bool isRoundTrip, int NumPassengers, Int32 cityToID, Int32 cityFromID, DateTime returnDate)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!isRoundTrip)
            {
                return await CreateOneWayReservation(FlightID, NumPassengers, user.UserID);
            }
            else
            {
                return await CreateRoundTripReservation(FlightID, NumPassengers, cityToID, cityFromID, returnDate, user.UserID);
            }
        }
        
        public IActionResult ReservationCustomer()
        {
            ViewBag.FlightID = TempData["FlightID"];
            ViewBag.isRoundTrip = TempData["isRoundTrip"];
            ViewBag.NumPassengers = TempData["NumPassengers"];
            ViewBag.cityToID = TempData["cityToID"];
            ViewBag.cityFromID = TempData["cityFromID"];
            ViewBag.returnDate = TempData["returnDate"];
            return View("AddCustomer");
        }

        [HttpGet]
        public IActionResult ManagerSearchCustomer(Int32 FlightID, Boolean isRoundTrip, Int32 NumPassengers, Int32 cityToID, Int32 cityFromID, DateTime returnDate)
        {
            ManagerCustomerSearch mcs = new ManagerCustomerSearch
            {
                FlightID = FlightID,
                isRoundTrip = isRoundTrip,
                NumPassengers = NumPassengers,
                cityToID = cityToID,
                cityFromID = cityFromID,
                returnDate = returnDate
            };
            return View(mcs);
        }
        
        [HttpPost]
        public IActionResult ManagerSearchCustomer(ManagerCustomerSearch mcs)
        {
            var query = from c in _context.Users
                        select c;
            if (mcs.LastName != null && mcs.LastName != "")
            {
                query = query.Where(c => c.LastName.Contains(mcs.LastName));
            }
            if (mcs.AdvantageNumber != null)
            {
                query = query.Where(c => c.AdvantageNumber == (mcs.AdvantageNumber));
            }
            List<AppUser> SelectedUsers = query.ToList();
            ViewBag.FlightID = mcs.FlightID;
            ViewBag.isRoundTrip = mcs.isRoundTrip;
            ViewBag.NumPassengers = mcs.NumPassengers;
            ViewBag.cityToID = mcs.cityToID;
            ViewBag.cityFromID = mcs.cityFromID;
            ViewBag.returnDate = mcs.returnDate;
            return View("ReservationCustomersList", SelectedUsers);
        }
        
        [HttpGet]
        public async Task<IActionResult> ManagerCreateCustomer(Int32 FlightID, Boolean isRoundTrip, Int32 NumPassengers, Int32 cityToID, Int32 cityFromID, DateTime returnDate)
        {
            ManagerCustomerCreation mcc = new ManagerCustomerCreation
            {
                FlightID = FlightID,
                isRoundTrip = isRoundTrip,
                NumPassengers = NumPassengers,
                cityToID = cityToID,
                cityFromID = cityFromID,
                returnDate = returnDate
            };
            return View("ManagerCustomerCreation", mcc);
        }

        [HttpPost]
        public async Task<IActionResult> ManagerCreateCustomer(ManagerCustomerCreation mcc)
        {
            AppUser user = new AppUser
            {
                //TODO: Add the rest of the custom user fields here
                UserName = mcc.Email,
                Email = mcc.Email,
                FirstName = mcc.FirstName,
                LastName = mcc.LastName,
                Birthday = mcc.Birthday,
                PhoneNumber = mcc.PhoneNumber,
                ZIP = mcc.ZIP,
                State = mcc.State,
                Street = mcc.Street,
                City = mcc.City,
                AdvantageNumber = Utilities.GenerateAccountNumber.GetFFNum(_context),
                UserID = Convert.ToInt32(mcc.AdvantageNumber),
                Mileage = 0
            };

            IdentityResult result = await _userManager.CreateAsync(user, mcc.Password);
            if (result.Succeeded)
            {
                //TODO: Add user to desired role. This example adds the user to the customer role
                await _userManager.AddToRoleAsync(user, "Customer");
                return await ManagerCreate(mcc.FlightID, mcc.isRoundTrip, mcc.NumPassengers, mcc.cityToID, mcc.cityFromID, mcc.returnDate, user.UserID);
            }

            return View("Error", new { message = "Customer Creation Failed"});
        }

        // GET: Reservation/Details/5
        // Details page shows all tickets and allows for ticket change
        // This is the default page everyone sees after a reservation is created
        public async Task<IActionResult> Description(int id)
        {
            Models.Business.Reservation reservation = await _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Customer).Include(t => t.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).ThenInclude(f => f.Route).ThenInclude(f => f.CityTo).FirstAsync(r => r.ReservationID == id);
            return View(reservation);
        }

        

        // Handles creating one way reservations
        // Creates a blank one way reservation and adds {passengerCount} tickets to it if it's empty
        public async Task<ActionResult> CreateOneWayReservation(Int32 flightID, Int32 passengerCount, Int32? CustomerID)
        {
            Models.Business.Reservation reservation = await CreateBlankReservation(TypeOfReservation.OneWay, CustomerID);

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
        public async Task<ActionResult> CreateRoundTripReservation(Int32 flightID, Int32 passengerCount, Int32 cityToID, Int32 cityFromID, DateTime returnDate, Int32? CustomerID)
        {
            Models.Business.Reservation reservation = await CreateBlankReservation(TypeOfReservation.RoundTrip, CustomerID);
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

        public async Task<IActionResult> ChoosePaymentMethod(int? id)
        {
            Models.Business.Reservation reservation = await _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Customer).Include(t => t.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).FirstAsync(r => r.ReservationID == id);

            ViewBag.RemainingMiles = reservation.Customer.Mileage - reservation.ReservationMileageCost;
            return View(reservation);
        }

        //Shows Reservation Confirmation Page
        public async Task<IActionResult> Confirm(int? id, String PaymentMethod)
        {
            Models.Business.Reservation reservation = await _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Customer).Include(t => t.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).FirstAsync(r => r.ReservationID == id);

            if (PaymentMethod == "Cash")
            {
                reservation.ReservationMethod = PaymentOptions.Cash; 
            }
            else if(PaymentMethod == "Mileage")
            {
                reservation.ReservationMethod = PaymentOptions.Miles;
            }
            else
            {
                reservation.ReservationMethod = PaymentOptions.Cash;
            }
            _context.Update(reservation);
            await _context.SaveChangesAsync();
            ViewBag.RemainingMiles = reservation.Customer.Mileage - reservation.ReservationMileageCost;
            return View(reservation);
        }

        //Last Action for Reservation Creation
        public async Task<IActionResult> Finalize(int? id)
        {
            Models.Business.Reservation dbReservation = _context.Reservations.Include(r => r.Customer).Include(r => r.Tickets).ThenInclude(r => r.Customer)
                .Include(r => r.Tickets).ThenInclude(r => r.Flight).ThenInclude(r => r.FlightInfo).ThenInclude(r => r.Route).ThenInclude(r => r.CityFrom)
                .Include(r => r.Tickets).ThenInclude(r => r.Flight).ThenInclude(r => r.FlightInfo).ThenInclude(r => r.Route).ThenInclude(r => r.CityTo)
                .FirstOrDefault(r => r.ReservationID == id);
            dbReservation.ReservationComplete = true;
            _context.Update(dbReservation);
            await _context.SaveChangesAsync();
            String EmailBody;
            if(dbReservation.ReservationMethod == PaymentOptions.Cash)
            {
                EmailBody = "Thanks for your reservation. Your subtotal is: " + dbReservation.ReservationSubtotal + "The tax fee is: " + dbReservation.SalesTax + "Your total is: " + dbReservation.ReservationTotal;
            }
            else
            {
                Int32 remainingMiles;
                AppUser user = _context.Users.First(u => u.UserID == dbReservation.Customer.UserID);
                remainingMiles = Convert.ToInt32(user.Mileage) - dbReservation.ReservationMileageCost;
                user.Mileage = remainingMiles;
                _context.Update(user);
                await _context.SaveChangesAsync();
                EmailBody = "Thanks for your reservation. Your subtotal is: " + dbReservation.ReservationMileageCost + "You have: " + remainingMiles + " miles left.";

            }
            try
            {
                Utilities.EmailMessaging.SendEmail(dbReservation.Customer.Email, "Reservation Confirmation", EmailBody);
            }
            catch
            {
                //Couldn't send email.
            }
            foreach (Ticket dbticket in dbReservation.Tickets)
            {
                String email = dbticket.Customer.Email;
                String emailStuff = "Your Flight on Longhorn Airlines has been Booked!\nYour reservation number is " + dbReservation.ReservationID + "\nand your ticket number is " + dbticket.TicketID + "\nWe look forward to seeing you on " + dbticket.Flight.Date.ToString() + "\n at " + dbticket.Flight.FlightInfo.FlightTime.ToString() + " for your flight from " + dbticket.Flight.FlightInfo.Route.CityFrom.CityName + " to " + dbticket.Flight.FlightInfo.Route.CityTo.CityName + ".";
                
                try
                {
                    Utilities.EmailMessaging.SendEmail(email, "Reservation Confirmation", emailStuff);
                }
                catch
                {
                    //Couldn't send email.
                }
            }
            return RedirectToAction("Index", "Home");
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
        private async Task<Models.Business.Reservation> CreateBlankReservation(TypeOfReservation type, Int32? CustomerID)
        {
            Models.Business.Reservation reservation = new Models.Business.Reservation
            {
                ReservationType = type,
                ReservationComplete = false,
                Tickets = new List<Ticket>()
            };
            if (CustomerID.HasValue)
            {
                reservation.Customer = await _context.Users.FirstAsync(u => u.UserID == CustomerID.Value);
            }
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
        //Last Action for modifying reservation
        public async Task<ActionResult> ModifyReservation(Int32 ReservationID, Int32 FlightID, Int32 PrevFlightID)
        {
            Models.Business.Reservation reservation = await _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Customer).Include(r => r.Tickets).ThenInclude(t => t.Flight).FirstAsync(r => r.ReservationID == ReservationID);
            Flight flight = await _context.Flights.Include(f => f.Tickets).FirstAsync(f => f.FlightID == FlightID);

            Flight prevFlight = _context.Flights.Include(f => f.Tickets).First(f => f.FlightID == PrevFlightID);
            foreach (Ticket t in reservation.Tickets)
            {
                if (t.Flight.FlightID == PrevFlightID)
                {
                    t.Flight = flight;
                    t.Seat = "";
                    prevFlight.Tickets.Remove(t);
                    flight.Tickets.Add(t);
                    _context.Update(t);
                    _context.Update(prevFlight);
                    _context.Update(flight);
                }
            }
            await _context.SaveChangesAsync();
            //if modification was done by customer, charge $50
            if (User.IsInRole("Customer"))
            {
                HashSet<AppUser> reservationCustomers = new HashSet<AppUser>();
                foreach (Ticket t in reservation.Tickets)
                {
                    reservationCustomers.Add(t.Customer);
                    if (t.Flight.FlightID == FlightID)
                    {
                        t.Fare += 50;
                        _context.Update(t);
                    }
                }
                foreach (AppUser customer in reservationCustomers)
                {
                    Utilities.EmailMessaging.SendEmail(customer.Email, "Team 6: Reservation Modification", "Your reservation has been modified. You've been charged $50.00 on your flight.");
                }
                await _context.SaveChangesAsync();
                ViewBag.ReservationID = ReservationID;
                return View("Charge");
            }
            return RedirectToAction("Description", new { id = reservation.ReservationID });
        }

        //0 - First Leg
        //1 - Return Leg
        //1 - Return Leg
        public async Task<ActionResult> EditRoundTrip(Int32 ReservationID, Int32 Leg)
        {
            var reservation = await _context.Reservations.Include(r => r.Tickets).ThenInclude(t => t.Flight).FirstAsync(r => r.ReservationID == ReservationID);
            int prevFlightID = -1;

            if (Leg == 0)
            {
                prevFlightID = reservation.Tickets.First().Flight.FlightID;
            }
            else
            {
                prevFlightID = reservation.Tickets.Last().Flight.FlightID;
            }

            ReservationEditModel rem = new ReservationEditModel
            {
                ReservationID = reservation.ReservationID,
                NewDate = DateTime.Now,
                isRoundTrip = reservation.ReservationType == TypeOfReservation.RoundTrip,
                PrevFlightID = prevFlightID
            };
            return View("Edit", rem);
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            //get all the reservations from the database
            List<Models.Business.Reservation> reservations = new List<Models.Business.Reservation>();

            if (User.IsInRole("Manager"))
            {
                reservations = _context.Reservations.Include(r => r.Tickets)
                    .ThenInclude (r => r.Flight)
                    .ThenInclude (r => r.FlightInfo)
                    .ThenInclude (r => r.Route)
                    .ThenInclude (r => r.CityTo)
                    .Where(r => r.ReservationComplete == true)
                    .ToList();
            }

            else
            {
                reservations = _context.Reservations.Where(r => r.Customer.UserName == User.Identity.Name).Include(r => r.Tickets)
                    .ThenInclude(r => r.Flight)
                    .ThenInclude(r => r.FlightInfo)
                    .ThenInclude(r => r.Route)
                    .ThenInclude(r => r.CityTo)
                    .Where(r => r.ReservationComplete == true)
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
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.ReservationID == id);

            if (reservation == null) //reservation not found
            {
                return View("Error", new String[] { "Cannot find this reservation!" });
            }

            if (User.IsInRole("Manager") == false && reservation.Customer.UserName != User.Identity.Name) //they are trying to see something that isn't theirs
            {
                return View("Error", new String[] { "Unauthorized: You are attempting to view another customer's reservation!" });
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

            var reservation = await _context.Reservations.Include(r=>r.Tickets).ThenInclude(t => t.Flight).FirstAsync(r=>r.ReservationID==id);
            int prevFlightID = -1;
            if(reservation.ReservationType == TypeOfReservation.OneWay)
            {
                prevFlightID = reservation.Tickets.First().Flight.FlightID;
            }
            if (reservation == null)
            {
                return NotFound();
            }
            ReservationEditModel rem = new ReservationEditModel
            {
                ReservationID = reservation.ReservationID,
                NewDate = DateTime.Now,
                isRoundTrip = reservation.ReservationType == TypeOfReservation.RoundTrip,
                PrevFlightID = prevFlightID
            };
            return View(rem);
        }

        public async Task<ActionResult> ChangeTicketPrices(Int32 ReservationID)
        {
            Models.Business.Reservation reservation = await _context.Reservations.
                Include(r => r.Tickets).ThenInclude(t => t.Customer).Include(t => t.Tickets).
                ThenInclude(t => t.Flight).ThenInclude(f => f.FlightInfo).ThenInclude(f => f.Route).
                ThenInclude(f => f.CityTo).FirstAsync(r => r.ReservationID == ReservationID);

            return View(reservation);
        }

        public async Task<ActionResult> ChangeDate(ReservationEditModel rem)
        {
            Models.Business.Reservation r = await _context.Reservations.Include(res => res.Tickets).FirstAsync(res => res.ReservationID == rem.ReservationID);
            Ticket t = _context.Tickets.Include(tic => tic.Flight).ThenInclude(f => f.FlightInfo).First(tic=> tic.TicketID == r.Tickets.First().TicketID);
            FlightInfo info = _context.FlightInfos.Include(fi => fi.Route).First(fi => fi.FlightInfoID == t.Flight.FlightInfo.FlightInfoID);
            Route route = _context.Routes.Include(ro => ro.CityFrom).Include(ro => ro.CityTo).First(ro => ro.RouteID == info.Route.RouteID);
            BookingSearchModel bsm = new BookingSearchModel
            {
                DepartCityID = route.CityFrom.CityID,
                DepartDate = rem.NewDate,
                ArriveCityID = route.CityTo.CityID,
                PassengerCount = rem.PassengerCount,
                ReservationID = r.ReservationID,
                isRoundTrip = rem.isRoundTrip
            };
            ViewBag.CityToName = _context.Cities.FirstOrDefault(c => c.CityID == bsm.ArriveCityID).CityName;
            ViewBag.CityFromName = _context.Cities.FirstOrDefault(c => c.CityID == bsm.DepartCityID).CityName;
            var query = from f in _context.Flights
                        select f;

            query = query.Where(f => f.Date.Date == bsm.DepartDate.Date);
            query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == bsm.DepartCityID);
            query = query.Where(f => f.FlightInfo.Route.CityTo.CityID == bsm.ArriveCityID);
            query = query.Where(f => f.Canceled == false);
            foreach (Flight f in query)
            {
                //If there are not enough seats on flight delete flight from query
                if (!Utilities.GetTakenSeats.isAvailable(f.FlightID, bsm.PassengerCount, _context))
                {
                    query = query.Where(flight => flight.FlightID != f.FlightID);
                }
            }
            //Passing Booking Search Model information to view bag so it goes to ReservationController
            //There's probably a better way to do this
            ViewBag.DepartingFlightsQty = query.Count();
            ViewBag.isRoundTrip = bsm.isRoundTrip;
            ViewBag.passengerCount = bsm.PassengerCount;
            ViewBag.ReturnDate = bsm.ArriveDate;
            ViewBag.ReservationID = bsm.ReservationID;
            ViewBag.PrevFlightID = rem.PrevFlightID;
            return View("ReservationFlightResults", query.Include(f => f.FlightInfo)
                .Include(f => f.FlightInfo.Route)
                .Include(f => f.FlightInfo.Route.CityFrom)
                .Include(f => f.FlightInfo.Route.CityTo)
                .ToList());
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