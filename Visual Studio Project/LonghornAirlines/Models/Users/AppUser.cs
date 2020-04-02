using LonghornAirlines.Models.Business;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LonghornAirlines.Models.Users
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "User ID: ")]
        public Int32 UserID { get; set; }

        [Display(Name = "First Name: ")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        public String LastName { get; set; }

        [Display(Name = "Birthday: ")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Mileage: ")]
        public Decimal Mileage { get; set; }

        [Display(Name = "Advantage Number: ")]
        public String AdvantageNumber { get; set; }

        [Display(Name = "Street: ")]
        public String Street { get; set; }

        [Display(Name = "City: ")]
        public String City { get; set; }

        [Display(Name = "State: ")]
        public String State { get; set; }

        [Display(Name = "Zip Code: ")]
        public String ZIP { get; set; }
        
        //Navigation Properties
        [Display(Name = "Reservation IDs: ")]
        public List<Reservation> Reservations { get; set; }

        [Display(Name = "Ticket IDs: ")]
        public List<Ticket> Tickets { get; set; }

        [Display(Name = "User ID: ")]
        public UserRole UserRole { get; set; }
    }
}
