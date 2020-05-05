using LonghornAirlines.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Utilities
{
    public static class GenerateReservationNumber
    {
        public static Int32 GetReservationNum(AppDbContext _context)
        {
            Int32 intMaxReservationNumber;
            try
            {
                intMaxReservationNumber = _context.Reservations.Max(c => c.ReservationID); //this is the highest number in the database right now
            }
            catch (Exception)
            {
                intMaxReservationNumber = 1;
            }

            //add one to the current max to find the next one
            Int32 intNextReservationNumber = intMaxReservationNumber + 1;

            //return the value
            return intNextReservationNumber;
        }
    }
}
