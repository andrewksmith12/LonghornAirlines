using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.ViewModels
{
    public class CustomerCreationModel : RegisterViewModel
    {
        public Int32 TicketID {get; set;}
    }
}
