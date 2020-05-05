using LonghornAirlines.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Utilities
{
    public static class GenerateTicketNumber
    {
        public static Int32 GetTicketNum(AppDbContext _context)
        {
            Int32 intMaxTicketNumber;
            try
            {
                intMaxTicketNumber = _context.Tickets.Max(c => c.TicketID); //this is the highest number in the database right now
            }
            catch (Exception)
            {
                intMaxTicketNumber = 1;
            }

            //add one to the current max to find the next one
            Int32 intNextTicketNumber = intMaxTicketNumber + 1;

            //return the value
            return intNextTicketNumber;
        }
    }
}
