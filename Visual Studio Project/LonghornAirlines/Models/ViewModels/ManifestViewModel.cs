using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace LonghornAirlines.Models.ViewModels
{
    public class ManifestViewModel
    {
        [Display(Name = "Departure City: ")]
        public Int32? DepartCityID { get; set; }

        [Display(Name = "Arrival City: ")]
        public Int32? ArriveCityID { get; set; }
        
        [Display(Name = "Flight Number")]
        public Int32? FlightNumber { get; set; }
    }
}
