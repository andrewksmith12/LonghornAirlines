using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class ManagerCustomerCreation : RegisterViewModel
    {
        public Int32 FlightID { get; set; }
        public Boolean isRoundTrip { get; set; }
        public Int32 NumPassengers { get; set; }
        public Int32 cityToID { get; set; }
        public Int32 cityFromID { get; set; }
        public DateTime returnDate { get; set; }
    }
}
