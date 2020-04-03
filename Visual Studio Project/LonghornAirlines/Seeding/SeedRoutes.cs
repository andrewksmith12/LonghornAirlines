﻿using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonghornAirlines.Seeding
{
    public class SeedRoutes
    {
        public static void SeedAllRoutes(AppDbContext db)
        {
            List<Route> AllRoutes = new List<Route>();
            //AUS -> DFW
            AllRoutes.Add(new Route
            {
                RouteID = 0,
                Distance = 190,
                FlightTime = 55,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "AUS"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "DFW"),
            });
            //DFW -> AUS
            AllRoutes.Add(new Route
            {
                RouteID = 1,
                Distance = 190,
                FlightTime = 55,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "DFW"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "AUS"),
            });

            //AUS -> HOU
            AllRoutes.Add(new Route
            {
                RouteID = 2,
                Distance = 148,
                FlightTime = 60,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "AUS"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "HOU"),
            });
            //HOU -> AUS
            AllRoutes.Add(new Route
            {
                RouteID = 3,
                Distance = 148,
                FlightTime = 60,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "HOU"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "AUS"),
            });

            //AUS -> ELP
            AllRoutes.Add(new Route
            {
                RouteID = 4,
                Distance = 527,
                FlightTime = 100,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "AUS"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "ELP"),
            });
            //ELP -> AUS
            AllRoutes.Add(new Route
            {
                RouteID = 5,
                Distance = 527,
                FlightTime = 100,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "ELP"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "AUS"),
            });

            //DFW -> HOU
            AllRoutes.Add(new Route
            {
                RouteID = 6,
                Distance = 224,
                FlightTime = 73,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "DFW"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "HOU"),
            });
            //HOU -> DFW
            AllRoutes.Add(new Route
            {
                RouteID = 7,
                Distance = 527,
                FlightTime = 100,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "HOU"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "DFW"),
            });

            //DFW -> ELP
            AllRoutes.Add(new Route
            {
                RouteID = 8,
                Distance = 551,
                FlightTime = 113,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "DFW"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "ELP"),
            });
            //ELP -> DFW
            AllRoutes.Add(new Route
            {
                RouteID = 9,
                Distance = 551,
                FlightTime = 113,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "ELP"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "DFW"),
            });

            //HOU -> ELP
            AllRoutes.Add(new Route
            {
                RouteID = 10,
                Distance = 667,
                FlightTime = 129,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "HOU"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "ELP"),
            });
            //ELP -> HOU
            AllRoutes.Add(new Route
            {
                RouteID = 11,
                Distance = 667,
                FlightTime = 129,
                CityFrom = db.Cities.FirstOrDefault(c => c.CityCode == "ELP"),
                CityTo = db.Cities.FirstOrDefault(c => c.CityCode == "HOU"),
            });

            //create a counter to help debug
            int intRouteID = 0;

            try
            {
                foreach (Route seedRoute in AllRoutes)
                {
                    //updates the counter to get info on where the problem is
                    intRouteID = seedRoute.RouteID;

                    //find the genre in the database
                    Route dbRoute = db.Routes.FirstOrDefault(r => r.RouteID == intRouteID);

                    if (dbRoute == null) //the genre isn't in the database
                    {
                        //add the genre
                        db.Routes.Add(seedRoute);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("There was an error adding the ");
                msg.Append(intRouteID);
                msg.Append(" route.");
                throw new Exception(msg.ToString(), ex);
            }
        }
    }
}  