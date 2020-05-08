using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using LonghornAirlines.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace LonghornAirlines.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ReportController : Controller
    {
        private AppDbContext _db;

        public ReportController(AppDbContext context)
        {
            _db = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateReport()
        {
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        public MultiSelectList GetAllCities()
        {
            List<City> cityList = _db.Cities.ToList();
            return new MultiSelectList(cityList.OrderBy(c => c.CityID), "CityID", "CityName");
        }

        public IActionResult DisplayReport (ReportsViewModel rvm, int[] DepartCityID, int[] ArriveCityID)
        {
            //ViewBag.CityToName = _db.Cities.FirstOrDefault(c => c.CityID == rvm.ArriveCityID).CityName;
            //ViewBag.CityFromName = _db.Cities.FirstOrDefault(c => c.CityID == rvm.DepartCityID).CityName;
            //ViewBag.DepartDate = _db.Flights.FirstOrDefault(f => f.Date == rvm.DepartDate);
            //ViewBag.ArriveDate = _db.Flights.FirstOrDefault(f => f.Date == rvm.ArriveDate);

            var query = from t in _db.Tickets
                        select t;

            query = query.Where(t => t.Reservation.ReservationComplete == true);

            if (rvm.DepartCityID != null)
            {
                query = query.Where(t => DepartCityID.Contains(t.Flight.FlightInfo.Route.CityFrom.CityID));
            }

            if (rvm.ArriveCityID != null)
            {
                query = query.Where(t => ArriveCityID.Contains(t.Flight.FlightInfo.Route.CityTo.CityID));
            }

            if (rvm.DepartDate != null)
            {
                query = query.Where(t => t.Flight.Date >= rvm.DepartDate);
            }

            if (rvm.ArriveDate != null)
            {
                query = query.Where(t => t.Flight.Date <= rvm.ArriveDate);
            }


            if(rvm.FirstClass == true && rvm.Economy == false)
            {
                query = query.Where(t => t.Seat == "1A" || t.Seat == "1B" || t.Seat == "2A" || t.Seat == "2B");
            }

            if(rvm.Economy == true && rvm.FirstClass == false)
            {
                query = query.Where(t => t.Seat == "3A" || t.Seat == "3B" || t.Seat == "3C" || t.Seat == "3D" || t.Seat == "4A" || t.Seat == "4B" || t.Seat == "4C" || t.Seat == "4D" || t.Seat == "5A" || t.Seat == "5B" || t.Seat == "5C" || t.Seat == "5D");
            }

            List<Ticket> SelectedTickets = query.ToList();
            int NumofPassengers = SelectedTickets.Count;
            ViewBag.PassengerCount = NumofPassengers;
            decimal totalRevenue = SelectedTickets.Sum(t => t.Fare);
            ViewBag.Revenue = totalRevenue;
            return View("DisplayReport", rvm);

        }

        public IActionResult GenerateFlightManifest()
        {
            ViewBag.AllCities = GetAllCities();
            return View("FlightManifest");
        }

        public IActionResult ManifestSearchResults(ManifestViewModel mvm)
        {
            var query = from f in _db.Flights
                        select f;

            if (mvm.DepartCityID != 0)
            {
                query = query.Where(f => f.FlightInfo.Route.CityFrom.CityID == mvm.DepartCityID);
            }

            if (mvm.ArriveCityID != 0)
            {
                query = query.Where(f => f.FlightInfo.Route.CityTo.CityID == mvm.ArriveCityID);
            }

            if (mvm.DepartDate != null)
            {
                query = query.Where(f => f.Date >= mvm.DepartDate);
            }

            if (mvm.ArriveDate != null)
            {
                query = query.Where(f => f.Date <= mvm.ArriveDate);
            }

            if (mvm.hasDeparted == true && mvm.hasNotDeparted == false)
            {
                query = query.Where(f => f.hasDeparted == mvm.hasDeparted);
            }

            if (mvm.hasNotDeparted == true && mvm.hasDeparted == false)
            {
                query = query.Where(f => f.hasDeparted != mvm.hasNotDeparted);
            }

            List<Flight> SelectedFlights = query.ToList();
            return View("ManifestSearchResults", SelectedFlights.OrderByDescending(f => f.FlightID));
        }
        public async Task<IActionResult> DisplayManifest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _db.Flights
                .FirstOrDefaultAsync(f => f.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }
    }
}