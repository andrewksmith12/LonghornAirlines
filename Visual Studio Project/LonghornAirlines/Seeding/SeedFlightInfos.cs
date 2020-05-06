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
                    FlightInfo dbInfo = db.FlightInfos.FirstOrDefault(f => f.FlightInfoID == seedInfo.FlightInfoID);

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
