using LonghornAirlines.DAL;
using System;
using System.Linq;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LonghornAirlines.Utilities
{
    public static class GenerateAccountNumber
    {
        public static int GetFFNum(AppDbContext _context)
        {
            // TODO: Get Advantage account number generation working

            Int32 intMaxID; //the current maximum course number
            {
                IList<AppUser> customers = new List<AppUser>();
                intMaxID = _context.Users.Max(u => u.AdvantageNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            int intNextID = intMaxID + 1;

            //return the value
            return intNextID;
        }

    }
}