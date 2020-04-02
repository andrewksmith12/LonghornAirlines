using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LonghornAirlines.Models.Business
{
    public class City
    {
        [Display(Name = "City ID: ")]
        public String CityID { get; set; }

        [Display(Name = "State: ")]
        public String CityState { get; set; }

        [Display(Name = "City: ")]
        public String CityName { get; set; }
        
        //Navigation Properties
        [Display(Name = "Route IDs: ")]
        public List<Route> Routes { get; set; }
    }
}
