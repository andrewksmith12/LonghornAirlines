﻿using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Business
{
    public class Ticket
    {
        private const Decimal SENIOR_DISCOUNT = .1m;
        private const Decimal CHILD_DISCOUNT = .15m;

        [Display(Name = "Ticket ID: ")]
        public Int32 TicketID { get; set; }

        [Display(Name = "Fare: ")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Fare { get; set; }

        [Display(Name = "Seat Number: ")]
        public String Seat { get; set; }

        //Navigation Properties
        [Display(Name = "Customer ID: ")]
        public AppUser Customer { get; set; }

        [Display(Name = "Reservation ID: ")]
        public Reservation Reservation { get; set; }

        [Display(Name = "Flight ID: ")]
        public Flight Flight { get; set; }

        [Display(Name = "Use Milage for Upgrade to First Class")]
        public Boolean UpgradeWithMilage { get; set; }

        [Display(Name = "Checked In?")]
        public Boolean CheckedIn { get; set; }

        [Display(Name = "Mileage: ")]
        public Int32 GetMileageFare
        {
            get
            {
                String[] firstClassSeats = { "1A", "1B", "2A", "2B" };
                String[] budgetSeats = { "3A", "3B", "3C", "3D",
                                     "4A", "4B", "4C", "4D",
                                     "5A", "5B", "5C", "5D"};
                Int32 mileageFare;

                if (firstClassSeats.Contains(this.Seat))
                {
                    mileageFare = 2000;
                }
                else if (budgetSeats.Contains(this.Seat))
                {
                    mileageFare = 1000;
                }
                else
                {
                    return -10000;
                }

                return mileageFare;
            }
        }

        public bool isFirstClass
        {
            get
            {
                String[] firstClassSeats = { "1A", "1B", "2A", "2B" };
                return firstClassSeats.Contains(this.Seat);
            }
        }
    }
}
