using LonghornAirlines.Models.Users;
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

    public enum PaymentOptions
    {
        Cash = 0,
        Miles = 1
    }
    public class Reservation
    {
        private const Decimal TAX_RATE = 0.0775m;

        [Display(Name = "Reservation ID: ")]
        public Int32 ReservationID { get; set; }

        [Display(Name = "Reservation Type: ")]
        public TypeOfReservation ReservationType { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentOptions ReservationMethod { get; set;}

        //Navigational Properties
        [Display(Name = "Customer ID: ")]
        public AppUser Customer { get; set; }

        [Display(Name = "Ticket IDs: ")]
        public List<Ticket> Tickets { get; set; }
        [Display(Name = "Reservation Complete?: ")]
        public Boolean ReservationComplete { get; set; }
        public Int32 NumPassengers { get; set; }

        List<Ticket> tickets = new List<Ticket>();

        [Display(Name = "MilesPaid")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Int32 MilesPaid { get; set; }


        [Display(Name = "Reservation Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ReservationSubtotal
        {
            get
            {
                return Tickets.Sum(c => c.Fare); 
            }
  
        }

        [Display(Name = "Tax (8.25%)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SalesTax
        {
            get { return ReservationSubtotal * TAX_RATE; }
        }

        [Display(Name = "Reservation Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ReservationTotal
        {
            get { return ReservationSubtotal + SalesTax; }
        }

        [Display(Name = "Reservation Total in Miles")]
        public Int32 ReservationMileageCost
        {
            get
            {
                return Tickets.Sum(c => c.GetMileageFare);
            }
        }
    }
}
