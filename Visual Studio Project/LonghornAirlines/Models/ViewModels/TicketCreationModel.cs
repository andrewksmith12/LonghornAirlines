using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class TicketCreationModel
    {
        public Int32 TicketID { get; set; }
        public Int32 CustomerID { get; set; }
        public String SeatID { get; set; }
    }
}
