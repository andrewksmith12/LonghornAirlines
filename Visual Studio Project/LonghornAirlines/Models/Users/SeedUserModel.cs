using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Users
{
    public class SeedUserModel
    {
        public Int32 UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime BDay { get; set; }
        public Decimal Mileage { get; set; }
        public String AdvantageNumber { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZIP { get; set; }
        public Boolean isActive { get; set; }
        public String Password { get; set; }
        public String EmpType { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String SSN { get; set; }
        public String MI { get; set; }
        public Decimal Miles { get; set; }
    }
}
