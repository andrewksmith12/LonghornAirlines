using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Users;

namespace LonghornAirlines.Seeding
{
    public class SeedRoles
    {
        public static async Task AddRoles(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Admin") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await _roleManager.RoleExistsAsync("Customer") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if (await _roleManager.RoleExistsAsync("Employee") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }
            if (await _roleManager.RoleExistsAsync("Agent") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Agent"));
            }
            if (await _roleManager.RoleExistsAsync("Manager") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }
            if (await _roleManager.RoleExistsAsync("Pilot") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Pilot"));
            }
            if (await _roleManager.RoleExistsAsync("Co-Pilot") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Co-Pilot"));
            }
            if (await _roleManager.RoleExistsAsync("Flight Attendant") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Flight Attendant"));
            }
            _db.SaveChanges();
        }
    }
}
