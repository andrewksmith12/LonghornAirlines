using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class CustomerSearchModel
    {
        [Display(Name = "Existing Customer")]
        public Boolean ExistingCustomer { get; set; }

        [Display(Name = "Last Name: ")]
        public String LastName { get; set; }

        [Display(Name = "Advantage Number: ")]
        public Int32? AdvantageNumber { get; set; }

        [Display(Name = "New Customer")]
        public Boolean NewCustomer { get; set; }

        public Int32? TicketID { get; set; }
    }
}
