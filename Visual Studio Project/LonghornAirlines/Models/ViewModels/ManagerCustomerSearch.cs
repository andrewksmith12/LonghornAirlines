using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class ManagerCustomerSearch
    {
        public Int32 ManagerCustomerSearchID { get; set; }
        [Display(Name = "Last Name: ")]
        public String LastName { get; set; }

        [Display(Name = "Advantage Number: ")]
        public Int32? AdvantageNumber { get; set; }

        public Int32 FlightID { get; set; }
        public Boolean isRoundTrip { get; set; }
        public Int32 NumPassengers { get; set; }
        public Int32 cityToID { get; set; }
        public Int32 cityFromID { get; set; }
        public DateTime returnDate { get; set; }
    }
}
