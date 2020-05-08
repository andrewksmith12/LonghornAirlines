using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class ReportsViewModel
    {

        [Display(Name = "Departure City: ")]
        public int[] DepartCityID { get; set; }

        [Display(Name = "Arrival City: ")]
        public int[] ArriveCityID { get; set; }

        [Display(Name = "Start Date: ")]
        [Range(typeof(DateTime), "04/15/2020", "06/20/2020",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? DepartDate { get; set; }

        [Display(Name = "End Date: ")]
        [Range(typeof(DateTime), "04/15/2020", "06/20/2020",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? ArriveDate { get; set; }

        [Display(Name = "Economy")]
        public Boolean Economy { get; set; }

        [Display(Name = "First Class")]
        public Boolean FirstClass {get; set;}

        [Display(Name = "Total Number of Seats Sold ")]
        public Boolean TotalSeatsSold { get; set; }

        [Display(Name = "Total Revenue ")]
        public Boolean TotalRevenue { get; set; }


    }
}
