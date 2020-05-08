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

            Int32 intMaxID; //the current maximum course number
            {
                IList<AppUser> customers = new List<AppUser>();
                intMaxID = _context.Users.Max(m => m.AdvantageNumber); //this is the highest number in the database right now
            }

            intMaxID = intMaxID + 1;

            //return the value
            return intMaxID;
        }

    }
}