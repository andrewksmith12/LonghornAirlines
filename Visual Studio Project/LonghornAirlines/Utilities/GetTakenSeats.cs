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
            Flight flight = _context.Flights.Include(f=> f.Tickets).ThenInclude(t => t.Reservation).First(f => f.FlightID == FlightID);
            List<String> seats = new List<string>();
            foreach (Ticket t in flight.Tickets)
            {
                if ((t.Seat != "" || t.Seat != null) && t.Reservation.ReservationComplete)
                {
                    seats.Add(t.Seat);
                }
            }

            return seats;
        }

        public static Boolean isAvailable(int FlightID, int RequiredSeats, AppDbContext _context)
        {
            int numOfTakenSeats = FromFlight(FlightID, _context).Count;
            return RequiredSeats <= (16 - numOfTakenSeats);
        }
    }
}
