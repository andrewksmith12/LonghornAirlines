using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.Models.Business;
using LonghornAirlines;
using LonghornAirlines.DAL;
using System.Text;
using System.Security;

namespace LonghornAirlines.Utilities
{
    public static class RemoveFlight
    {
        public static void removeBools(AppDbContext db, FlightInfo flightInfo, Boolean sun, Boolean mon, Boolean tue, Boolean wed, Boolean thurs, Boolean fri, Boolean sat)
        {
            List<Flight> AllFlights = new List<Flight>();
            Boolean[] days = new bool[7];
            days[0] = sun;
            days[1] = mon;
            days[2] = tue;
            days[3] = wed;
            days[4] = thurs;
            days[5] = fri;
            days[6] = sat;

            //For Loop - Goes over all days of the week and add flights to all dates available if true
            for (int i = 0; i < 7; i++)
            {
                if (days[i])
                {
                    DateTime startDate = DateTime.Now;
                    DateTime endDate = Info.END_DATE;
                    int weekDay = i;
                    int startWeekDay = Convert.ToInt32(startDate.DayOfWeek);

                    DateTime flightDate = startDate.AddDays(weekDay - startWeekDay);
                    if (weekDay >= startWeekDay)
                    {
                        //Adds the flight for the first week
                        Flight tempFlight = db.Flights.FirstOrDefault(m => m.FlightInfo.FlightInfoID == flightInfo.FlightInfoID && m.Date == flightDate);
                        {
                            tempFlight.Canceled = true;
                        };
                        db.Update(tempFlight);
                    }
                    flightDate = flightDate.AddDays(7);
                    //Adds flights for all weeks after first week
                    while (flightDate <= endDate)
                    {
                        Flight tempFlight = db.Flights.FirstOrDefault(m => m.FlightInfo.FlightInfoID == flightInfo.FlightInfoID && m.Date == flightDate);
                        {
                            tempFlight.Canceled = true;
                        };
                        db.Update(tempFlight);
                        flightDate = flightDate.AddDays(7);
                    }
                }
            }
        }
    }
}
