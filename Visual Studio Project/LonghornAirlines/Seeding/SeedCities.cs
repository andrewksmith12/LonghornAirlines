using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonghornAirlines.Seeding
{
    public class SeedCities
    {
        public static void SeedAllCities(AppDbContext db)
        {
            List<City> AllCities = new List<City>();

            AllCities.Add(new City { CityID = 8001, CityName = "Austin", CityCode = "AUS", CityState = "TX" });
            AllCities.Add(new City { CityID = 8002, CityName = "Dallas", CityCode = "DFW", CityState = "TX" });
            AllCities.Add(new City { CityID = 8003, CityName = "Houston", CityCode = "HOU", CityState = "TX" });
            AllCities.Add(new City { CityID = 8004, CityName = "El Paso", CityCode = "ELP", CityState = "TX" });

            //create a counter to help debug
            int intCityID = 0;
            string strCityCode = "AAA";

            try
            {
                foreach (City seedCity in AllCities)
                {
                    //updates the counter to get info on where the problem is
                    intCityID = seedCity.CityID;
                    strCityCode = seedCity.CityCode;

                    //find the genre in the database
                    City dbCity = db.Cities.FirstOrDefault(c => c.CityCode == seedCity.CityCode);

                    if (dbCity == null) //the genre isn't in the database
                    {
                        //add the genre
                        db.Cities.Add(seedCity);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("There was an error adding the ");
                msg.Append(strCityCode);
                msg.Append(" city (CityID = ");
                msg.Append(intCityID);
                msg.Append(")");
                throw new Exception(msg.ToString(), ex);
            }
        }
    }
}
