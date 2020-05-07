using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Utilities
{
    public static class GetTakenSeats
    {
        public static List<String> FromFlight(int FlightID, AppDbContext _context)
        {
            Flight flight = _context.Flights.Include(f=> f.Tickets).First(f => f.FlightID == FlightID);
            List<String> seats = new List<string>();
            foreach (Ticket t in flight.Tickets)
            {
                if (t.Seat != "" || t.Seat != null)
                {
                    seats.Add(t.Seat);
                }
            }

            return seats;
        }
    }
}
