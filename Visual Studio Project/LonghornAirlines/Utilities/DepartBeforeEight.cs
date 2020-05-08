using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Utilities
{
    public static class DepartBeforeEight
    {
        public static void Depart(AppDbContext _context)
        {
            var flights = _context.Flights.Include(f => f.FlightInfo).Where(f => f.Date < Convert.ToDateTime("5/8/2020"));
            foreach (Flight f in flights)
            {
                f.hasDeparted = true;
            }
        }
    }
}
