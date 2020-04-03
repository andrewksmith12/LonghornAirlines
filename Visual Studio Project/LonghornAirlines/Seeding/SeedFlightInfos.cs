using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Seeding
{
    public class SeedFlightInfos
    {
        public void SeedAllFlightInfos(AppDbContext db)
        {
            List<FlightInfo> AllFlightInfos = new List<FlightInfo>();
            AllFlightInfos.Add(new FlightInfo
            {
                FlightNumber = 1000,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 0),
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
                FlightNumber = 1001,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 1),
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
                FlightNumber = 1002,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 2),
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
                FlightNumber = 1003,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 3),
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
                FlightNumber = 1004,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 10),
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
                FlightNumber = 1005,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 11),
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
                FlightNumber = 1006,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 8),
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
                FlightNumber = 1007,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 9),
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
                FlightNumber = 1008,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 4),
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
                FlightNumber = 1009,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 5),
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
                FlightNumber = 1010,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 6),
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
                FlightNumber = 1011,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 7),
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
                FlightNumber = 1012,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 4),
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
                FlightNumber = 1013,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 5),
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
                FlightNumber = 1014,
                Route = db.Routes.FirstOrDefault(r => r.RouteID == 6),
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
        }
    }
}
