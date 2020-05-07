using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonghornAirlines.Seeding
{
    public class SeedFlightInfos
    {
        public static void SeedAllFlightInfos(AppDbContext db)
        {
            List<FlightInfo> AllFlightInfos = new List<FlightInfo>();
            AllFlightInfos.Add(new FlightInfo
            {
                FlightNumber = 1000,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 1),
                FlightTime = "8:00",
                BaseFare = 105.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 2),
                FlightNumber = 1001,
                FlightTime = "9:00",
                BaseFare = 105.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 3),
                FlightNumber = 1002,
                FlightTime = "11:15",
                BaseFare = 130.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 4),
                FlightNumber = 1003,
                FlightTime = "12:00",
                BaseFare = 130.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 11),
                FlightNumber = 1004,
                FlightTime = "13:00",
                BaseFare = 140.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 12),
                FlightNumber = 1005,
                FlightTime = "15:00",
                BaseFare = 140.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 9),
                FlightNumber = 1006,
                FlightTime = "09:00",
                BaseFare = 98.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 10),
                FlightNumber = 1007,
                FlightTime = "10:15",
                BaseFare = 100.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 5),
                FlightNumber = 1008,
                FlightTime = "13:00",
                BaseFare = 115.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 6),
                FlightNumber = 1009,
                FlightTime = "14:30",
                BaseFare = 115.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 7),
                FlightNumber = 1010,
                FlightTime = "14:00",
                BaseFare = 110.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 8),
                FlightNumber = 1011,
                FlightTime = "14:45",
                BaseFare = 110.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 5),
                FlightNumber = 1012,
                FlightTime = "18:00",
                BaseFare = 105.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 6),
                FlightNumber = 1013,
                FlightTime = "19:45",
                BaseFare = 105.00m,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            });
            AllFlightInfos.Add(new FlightInfo
            {
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 7),
                FlightNumber = 1014,
                FlightTime = "10:30",
                BaseFare = 225.00m,
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = false,
                Saturday = true,
                Sunday = false
            });

            //create a counter to help debug
            int intflightInfoID = 1000;

            try
            {
                foreach (FlightInfo seedInfo in AllFlightInfos)
                {

                    //find the genre in the database
                    FlightInfo dbInfo = db.FlightInfos.FirstOrDefault(f => f.FlightNumber == seedInfo.FlightNumber);

                    if (dbInfo == null) //the genre isn't in the database
                    {
                        //add the genre
                        db.FlightInfos.Add(seedInfo);
                        db.SaveChanges();
                    }
                    //updates the counter to get info on where the problem is
                    intflightInfoID = seedInfo.FlightInfoID;
                }
            }
            catch (Exception ex)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("There was an error adding the ");
                msg.Append(intflightInfoID);
                msg.Append(" flight.");
                throw new Exception(msg.ToString(), ex);
            }
        }
    }
}
