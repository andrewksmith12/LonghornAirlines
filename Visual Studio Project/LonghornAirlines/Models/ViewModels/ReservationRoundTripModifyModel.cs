using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class ReservationRoundTripModifyModel
    {
        public Int32 ReservationID { get; set; }
        public Int32 FirstLegOriginalFlightID { get; set; }
        public Int32 FirstLegNewFlightID { get; set; }
        public Int32 ReturnLegOriginalFlightID { get; set; }
        public Int32 ReturnLegNewFlightID { get; set; }
        public DateTime FirstLegNewDate { get; set; }
        public DateTime ReturnLegNewDate { get; set; }
    }
}
