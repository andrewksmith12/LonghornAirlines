using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines
{
    public static class Info
    {
        public static DateTime START_DATE = new DateTime(2020, 04, 15);
        public static DateTime END_DATE = new DateTime(2020, 06, 30);
        public static Decimal SALES_TAX = .0775m;
        public static Int32 MILES_PER_TICKET_ECONOMY = 1000;
        public static Int32 MILES_PER_TICKET_FIRST_CLASS = 2000;
        public static Int32 MILES_PER_TICKET_UPGRADE = 500;
        public static DateTime SENIOR_BEGIN = DateTime.Now.AddYears(-65);
        public static Decimal SENIOR_DISCOUNT = .1m;
        public static DateTime CHILD_CUTOFF = DateTime.Now.AddYears(-12);
        public static Decimal CHILD_DISCOUNT = .15m;
        public static List<String> FIRST_CLASS_SEATS = new List<String>() { "1A", "2A", "1B", "2B" };
    }
}
