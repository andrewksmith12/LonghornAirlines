using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Business
{
    public class Ticket
    {
        [Display(Name = "Ticket ID: ")]
        public Int32 TicketID { get; set; }

        [Display(Name = "Fare: ")]
        [DisplayFormat (DataFormatString = "{0:C}")]
        public Decimal Fare { get; set; }

        [Display(Name = "Seat Number: ")]
        public String Seat { get; set; }
        
        //Navigation Properties
        [Display(Name = "Customer ID: ")]
        public AppUser Customer { get; set; }

        [Display(Name = "Reservation ID: ")]
        public Reservation Reservation { get; set; }

        [Display(Name = "Flight ID: ")]
        public Flight Flight { get; set; }

        [Display(Name = "Use Milage for Upgrade to First Class")]
        public Boolean UpgradeWithMilage { get; set; }

        [Display(Name = "Checked In?")]
        public Boolean CheckedIn { get; set; }
    }
}
