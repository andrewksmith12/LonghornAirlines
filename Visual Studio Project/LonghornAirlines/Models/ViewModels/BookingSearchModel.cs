using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class BookingSearchModel
    {
        public Int32 DepartCityID { get; set; }
        public Int32 ArriveCityID { get; set; }
        
        [Range(typeof(DateTime), "04/15/2020", "06/20/2020",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DepartDate { get; set; }
        
        [Range(typeof(DateTime), "04/15/2020", "06/20/2020",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? ArriveDate { get; set; }
        
        [Range(minimum:1, maximum:99)]
        public Int32 PassengerCount { get; set; }
        public Boolean isRoundTrip { get; set; }
    }
}
