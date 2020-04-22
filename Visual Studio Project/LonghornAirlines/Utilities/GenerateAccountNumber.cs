using LonghornAirlines.DAL;
using System;
using System.Linq;
using LonghornAirlines.Models.Users;

namespace LonghornAirlines.Utilities
{
    public static class GenerateAccountNumber
    {
        public static Int32 GetNextCourseNumber(AppDbContext _context)
        {
            // TODO: Get Advantage account number generation working

            Int32 intMaxCourseNumber; //the current maximum course number
            Int32 intNextCourseNumber; //the course number for the next class
            {
                //intMaxCourseNumber = _context.AppUser.Max(c => c.AdvantageNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            //intNextCourseNumber = intMaxCourseNumber + 1;

            //return the value
            return 9999999;
        }

    }
}