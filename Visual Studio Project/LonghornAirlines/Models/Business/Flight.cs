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
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Departed")]
        public Boolean hasDeparted { get; set; } = false;

        [Display(Name = "Base Fare: ")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal BaseFare { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal FirstClassFare
        {
            get { return (BaseFare * 1.2m); }
        }
        //Navigational Properties
        [Display(Name = "Flight Information: ")]
        public FlightInfo FlightInfo { get; set; }

        [Display(Name = "Canceled Status: ")]
        public Boolean Canceled { get; set; }

        [Display(Name = "Pilot: ")]
        public AppUser Pilot { get; set; }

        [Display(Name = "Attendant: ")]
        public AppUser Attendant { get; set; }

        [Display(Name = "Co-Pilot: ")]
        public AppUser CoPilot { get; set; }

        [Display(Name = "Ticket IDs: ")]
        public List<Ticket> Tickets { get; set; }
        [Display(Name = "Pilot Ready?")]
        public Boolean PilotCheckIn { get; set; }

        [Display(Name = "Co-Pilot Ready?")]
        public Boolean CoPilotCheckIn { get; set; }

        [Display(Name = "Attendant Ready?")]
        public Boolean AttendantCheckIn { get; set; }

        public Flight()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}
