using LonghornAirlines.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LonghornAirlines.Models.Business
{
    public class UserRole
    {
        [Display(Name = "Role ID: ")]
        public Int32 RoleID { get; set; }
        [Display(Name = "Role Description: ")]
        public String RoleDescription { get; set; }
        [Display(Name = "User IDs: ")]
        public List<AppUser> AppUsers { get; set; }
    }
}
