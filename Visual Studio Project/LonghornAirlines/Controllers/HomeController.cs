using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.ViewModels;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using LonghornAirlines.Utilities;

namespace LonghornAirlines.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private AppDbContext _db; 
        public HomeController(AppDbContext context)
        {
            _db = context;
        }

        public IActionResult Index(BookingSearchModel model, String errorMessage)
        {
            if(errorMessage != null && errorMessage != "")
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            if (User.IsInRole("Employee"))
            {
                return View("../EmployeeViews/EmployeeIndex");
            }
            ViewBag.AllCities = GetAllCities();
            return View(model);
        }

        public IActionResult bookForCust(BookingSearchModel model)
        {
            ViewBag.AllCities = GetAllCities();
            return View("Index",model);
        }


        public SelectList GetAllCities()
        {
            List<City> cityList = _db.Cities.ToList();

            return new SelectList(cityList.OrderBy(c => c.CityID), "CityID", "CityName");
        }
        public IActionResult Support()
        {
            return View("Support");
        }

        public IActionResult ConfirmReservation(Int32 confirmationNumber)
        {
            Reservation dbReservation = _db.Reservations.Include(r => r.Customer).Include(r => r.Tickets).ThenInclude(r => r.Customer)
                .Include(r => r.Tickets).ThenInclude(r => r.Flight).ThenInclude(r => r.FlightInfo).ThenInclude(r => r.Route).ThenInclude(r => r.CityFrom)
                .Include(r => r.Tickets).ThenInclude(r => r.Flight).ThenInclude(r => r.FlightInfo).ThenInclude(r => r.Route).ThenInclude(r => r.CityTo)
                .FirstOrDefault(r => r.ReservationID == confirmationNumber);

            String EmailBody = "Thanks for your reservation. Your subtotal is: " + dbReservation.ReservationSubtotal+ "The tax fee is: " + dbReservation.SalesTax + "Your total is: " + dbReservation.ReservationTotal;
            Utilities.EmailMessaging.SendEmail(dbReservation.Customer.Email,"Reservation Confirmation", EmailBody);

            foreach(Ticket dbticket in dbReservation.Tickets)
            {
                String email = dbticket.Customer.Email;
                String emailStuff = "Your Flight on Longhorn Airlines has been Booked!\nYour reservation number is " + dbReservation.ReservationID + "\nand your ticket number is " + dbticket.TicketID + "\nWe look forward to seeing you on " + dbticket.Flight.Date.ToString() + "\n at " + dbticket.Flight.FlightInfo.FlightTime.ToString() + " for your flight from " + dbticket.Flight.FlightInfo.Route.CityFrom.CityName + " to " + dbticket.Flight.FlightInfo.Route.CityTo.CityName + ".";
                Utilities.EmailMessaging.SendEmail(email, "Reservation Confirmation", emailStuff);
            }

            return View("ReservationConfirmation", dbReservation.Tickets.ToList());
        }

        [HttpPost]
        public IActionResult CustomerSearch(CustomerSearchModel customerSearchModel)
        {
            var query = from c in _db.Users
                        select c;
            if (customerSearchModel.LastName != null && customerSearchModel.LastName != "")
            {
                query = query.Where(c => c.LastName.Contains(customerSearchModel.LastName));
            }
            if (customerSearchModel.AdvantageNumber != null && customerSearchModel.AdvantageNumber != null)
            {
                query = query.Where(c => c.AdvantageNumber == (customerSearchModel.AdvantageNumber));
            }
            List<AppUser> SelectedUsers = query.Include(c => c.UserID).ToList();
            return View(SelectedUsers);

        }
    }
}