using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Business
{
    public class FlightInfo
    {
        [Display(Name = "Flight Number: ")]
        public Int32 FlightNumber { get; set; }

        [Display(Name = "Flight Time: ")]
        public String FlightTime { get; set; }

        [Display(Name = "Base Fare: ")]
        public Decimal BaseFare { get; set; }

        //Boolean Properties for Days Operational
        [Display(Name = "Monday")]
        public Boolean Monday { get; set; }
        [Display(Name = "Tuesday")]
        public Boolean Tuesday { get; set; }
        [Display(Name = "Wednesday")]
        public Boolean Wednesday { get; set; }
        [Display(Name = "Thursday")]
        public Boolean Thursday { get; set; }
        [Display(Name = "Friday")]
        public Boolean Friday { get; set; }
        [Display(Name = "Saturday")]
        public Boolean Saturday { get; set; }
        [Display(Name = "Sunday")]
        public Boolean Sunday { get; set; }

        //Navigational Properties
        [Display(Name = "Route ID: ")]
        public Route Route { get; set; }
        [Display(Name = "Flight IDs: ")]
        public List<Flight> Flights { get; set; }
    }
}
