using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class ReservationEditModel
    {
        public Int32 ReservationID { get; set; }
        public DateTime NewDate { get; set; }
        public Int32 PrevFlightID { get; set; }
        public Boolean isRoundTrip { get; set; }
        public Int32 PassengerCount { get; set; }
    }
}
