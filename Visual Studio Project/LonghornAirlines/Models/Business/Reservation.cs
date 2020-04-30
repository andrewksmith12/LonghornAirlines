﻿using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Business
{
    public enum TypeOfReservation 
    {OneWay = 0,
     RoundTrip=1}
    public class Reservation
    {
        [Display(Name = "Reservation ID: ")]
        public Int32 ReservationID { get; set; }

        [Display(Name = "Reservation Type: ")]
        public TypeOfReservation ReservationType { get; set; }

        [Display(Name = "Payment Method")]
        public string ReservationMethod { get; set;}

        //Navigational Properties
        [Display(Name = "Customer ID: ")]
        public AppUser Customer { get; set; }

        [Display(Name = "Ticket IDs: ")]
        public List<Ticket> Tickets { get; set; }
        [Display(Name = "Reservation Complete?: ")]
        public Boolean ReservationComplete { get; set; }
        public Int32 NumPassengers { get; set; }
    }
}
