using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LonghornAirlines.Models.Business
{
    public class Flight
    {
        [Display(Name = "Flight ID: ")]
        public Int32 FlightID { get; set; }

        [Display(Name = "Date: ")]
        public DateTime Date { get; set; }

        [Display(Name = "Departed")]
        public Boolean hasDeparted { get; set; }
        
        //Navigational Properties
        [Display(Name = "Flight Information: ")]
        public FlightInfo FlightInfo { get; set; }

        [Display(Name = "Pilot: ")]
        public AppUser Pilot { get; set; }

        [Display(Name = "Attendant: ")]
        public AppUser Attendant { get; set; }

        [Display(Name = "Co-Pilot: ")]
        public AppUser CoPilot { get; set; }

        [Display(Name = "Ticket IDs: ")]
        public List<Ticket> Tickets { get; set; }
    }
}
