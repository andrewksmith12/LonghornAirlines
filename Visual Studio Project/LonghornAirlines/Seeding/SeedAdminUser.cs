using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

//Seeded as of 4/10/2020 

namespace LonghornAirlines.Seeding
{
    public class SeedAdminUser
    {
        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //Check if user is in database
            AppUser newUser = _db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

            if (newUser == null) //the user isn't in the database
            {
                newUser = new AppUser();
                newUser.UserName = "admin@example.com";
                newUser.Email = "admin@example.com";
                newUser.FirstName = "Example";
                newUser.LastName = "Admin";
                newUser.isActive = true;

                var result = await _userManager.CreateAsync(newUser, "Abc123!");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser = _db.Users.FirstOrDefault(u => u.UserName == "admin@example.com");
                if (await _userManager.IsInRoleAsync(newUser, "Admin") == false)
                {
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
                _db.SaveChanges();
            }
        }
    }
}
