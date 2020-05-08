using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.Models.Business;
using LonghornAirlines;
using LonghornAirlines.DAL;
using System.Text;

namespace LonghornAirlines.Seeding
{
    public class SeedFlights
    {
        public static void SeedAllFlights(AppDbContext db)
        {
            List<FlightInfo> info = db.FlightInfos.ToList<FlightInfo>();
            List<Flight> AllFlights = new List<Flight>();
            foreach(FlightInfo flightInfo in info)
            {
                Boolean[] days = new bool[7];
                days[0] = flightInfo.Sunday;
                days[1] = flightInfo.Monday;
                days[2] = flightInfo.Tuesday;
                days[3] = flightInfo.Wednesday;
                days[4] = flightInfo.Thursday;
                days[5] = flightInfo.Friday;
                days[6] = flightInfo.Saturday;
                Decimal baseFare = flightInfo.BaseFare;

                //For Loop - Goes over all days of the week and add flights to all dates available if true
                for (int i = 0; i < 7; i++)
                {
                    if (days[i])
                    {
                        DateTime startDate = Info.START_DATE;
                        DateTime endDate = Info.END_DATE;
                        int weekDay = i;
                        int startWeekDay = Convert.ToInt32(startDate.DayOfWeek);

                        DateTime flightDate = startDate.AddDays(weekDay - startWeekDay);
                        if (weekDay >= startWeekDay)
                        {
                            //Adds the flight for the first week
                            Flight tempFlight = new Flight
                            {
                                Date = flightDate,
                                FlightInfo = db.FlightInfos.FirstOrDefault(f => f.FlightInfoID == flightInfo.FlightInfoID),
                                BaseFare = baseFare
                            };
                            AllFlights.Add(tempFlight);
                        }
                        flightDate = flightDate.AddDays(7);
                        //Adds flights for all weeks after first week
                        while (flightDate <= endDate)
                        {
                            Flight tempFlight = new Flight
                            {
                                Date = flightDate,
                                FlightInfo = db.FlightInfos.FirstOrDefault(f => f.FlightInfoID == flightInfo.FlightInfoID),
                                BaseFare = baseFare
                            };
                            flightDate = flightDate.AddDays(7);
                            AllFlights.Add(tempFlight);
                        }
                    }
                }
                int intFlightID = 1;

                try
                {
                    foreach (Flight seedFlight in AllFlights)
                    {
                        //find the flight in the database
                        Flight dbFlight = db.Flights.FirstOrDefault(f => f.FlightID == intFlightID);

                        if (dbFlight == null) //the flight isn't in the database
                        {
                            //add the genre
                            db.Flights.Add(seedFlight);
                            System.Diagnostics.Debug.Write("Flight" + dbFlight + " was created");
                            db.SaveChanges();
                        }
                        intFlightID++;
                    }
                    Utilities.DepartBeforeEight.Depart(db);
                }
                catch (Exception ex)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.Append("There was an error adding the ");
                    msg.Append(intFlightID);
                    msg.Append(" flight.");
                    throw new Exception(msg.ToString(), ex);
                }
            }
        }
    }
}
