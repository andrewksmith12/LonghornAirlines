using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Business
{
    public class Route
    {
        [Display(Name = "Route ID: ")]
        public int RouteID { get; set; }
        
        [Display(Name = "Flight Distance: ")]
        public int Distance { get; set; }
        
        [Display(Name = "Flight Time - in minutes: ")]
        public int FlightTime { get; set; }
        
        //Navigation Properties
        [Display(Name = "From: ")]
        public City CityFrom { get; set; }

        [Display(Name = "To: ")]
        public City CityTo { get; set; }

        [Display(Name = "Flight Numbers")]
        public List<FlightInfo> FlightInfos { get; set; }
    }
}
