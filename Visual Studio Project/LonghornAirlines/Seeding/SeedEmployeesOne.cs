using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

//TODO: Debug why this isn't working. 

namespace LonghornAirlines.Seeding
{
    public class SeedEmployeesOne
    {
        public static async Task AddEmployees(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Create Seed List
            List<SeedUserModel> EmployeesList = new List<SeedUserModel>();
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 1,
                FirstName = "Todd",
                LastName = "Jacobs",
                Street = "4564 Elm St.",
                City = "Dallas",
                State = "TX",
                ZIP = "75032",
                Email = "t.jacobs@longhornairlines.neet",
                Phone = "4694653365",
                EmpType = "Agent",
                Password = "society"
            });

            foreach (SeedUserModel seedUser in EmployeesList)
            {
                //Check if user is in database
                AppUser newUser = _db.Users.FirstOrDefault(u => u.Email == seedUser.Email);

                if (newUser == null) //the user isn't in the database
                {
                    //add the user
                    newUser = new AppUser();
                    newUser.UserID = seedUser.UserID;
                    newUser.UserName = seedUser.Email;
                    newUser.Email = seedUser.Email;
                    newUser.FirstName = seedUser.FirstName;
                    newUser.LastName = seedUser.LastName;
                    newUser.Street = seedUser.Street;
                    newUser.City = seedUser.City;
                    newUser.ZIP = seedUser.ZIP;
                    newUser.PhoneNumber = seedUser.Phone;

                    var result = await _userManager.CreateAsync(newUser, seedUser.Password);
                    if (result.Succeeded == false)
                    {
                        throw new Exception("This user can't be added - " + result.ToString());
                    }
                    _db.SaveChanges();
                    newUser = _db.Users.FirstOrDefault(u => u.UserName == seedUser.Email);
                    if (await _userManager.IsInRoleAsync(newUser, "Employee") == false)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Employee");
                    }
                    if (await _userManager.IsInRoleAsync(newUser, seedUser.EmpType) == false)
                    {
                        await _userManager.AddToRoleAsync(newUser, seedUser.EmpType);
                    }
                    _db.SaveChanges();
                }
            }
        }
    }
}
