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
    public class SeedEmployees
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
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 2,
                FirstName = "Eryn",
                LastName = "Rice",
                Street = "3405 Rio Grande",
                City = "Dallas",
                State = "TX",
                ZIP = "75043",
                Email = "e.rice@longhornairlines.neet",
                Phone = "4693876657",
                EmpType = "Manager",
                Password = "ricearoni"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 3,
                FirstName = "Brad",
                LastName = "Ingram",
                Street = "6548 La Posada Ct.",
                City = "Dallas",
                State = "TX",
                ZIP = "75209",
                Email = "b.ingram@longhornairlines.neet",
                Phone = "4694678821",
                EmpType = "Pilot",
                Password = "ingram45"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 4,
                FirstName = "Allison",
                LastName = "Taylor",
                Street = "467 Nueces St.",
                City = "Dallas",
                State = "TX",
                ZIP = "75206",
                Email = "a.taylor@longhornairlines.neet",
                Phone = "4694748452",
                EmpType = "Agent",
                Password = "nostalgic"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 5,
                FirstName = "Gregory",
                LastName = "Martinez",
                Street = "8295 Sunset Blvd.",
                City = "Austin",
                State = "TX",
                ZIP = "78653",
                Email = "g.martinez@longhornairlines.neet",
                Phone = "5128746718",
                EmpType = "Co-Pilot",
                Password = "fungus"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 6,
                FirstName = "Martin",
                LastName = "Sheffield",
                Street = "3886 Avenue A",
                City = "Dallas",
                State = "TX",
                ZIP = "75032",
                Email = "m.sheffield@longhornairlines.neet",
                Phone = "4695479167",
                EmpType = "Agent",
                Password = "longhorns"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 7,
                FirstName = "Jennifer",
                LastName = "MacLeod",
                Street = "2504 Far West Blvd.",
                City = "Houston",
                State = "TX",
                ZIP = "77001",
                Email = "j.macleod@longhornairlines.neet",
                Phone = "2814748138",
                EmpType = "Agent",
                Password = "smitty"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 8,
                FirstName = "Jeremy",
                LastName = "Tanner",
                Street = "4347 Almstead",
                City = "Austin",
                State = "TX",
                ZIP = "78705",
                Email = "j.tanner@longhornairlines.neet",
                Phone = "5124590929",
                EmpType = "Agent",
                Password = "tanman"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 9,
                FirstName = "Megan",
                LastName = "Rhodes",
                Street = "4587 Enfield Rd.",
                City = "Dallas",
                State = "TX",
                ZIP = "75039",
                Email = "m.rhodes@longhornairlines.neet",
                Phone = "4693744746",
                EmpType = "Flight Attendant",
                Password = "countryrhodes"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 10,
                FirstName = "Eric",
                LastName = "Stuart",
                Street = "5576 Toro Ring",
                City = "Austin",
                State = "TX",
                ZIP = "78710",
                Email = "e.stuart@longhornairlines.neet",
                Phone = "5128178335",
                EmpType = "Agent",
                Password = "stewboy"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 11,
                FirstName = "Charles",
                LastName = "Miller",
                Street = "8962 Main St.",
                City = "El Paso",
                State = "TX",
                ZIP = "79901",
                Email = "c.miller@longhornairlines.neet",
                Phone = "9157458615",
                EmpType = "Agent",
                Password = "squirrel"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 12,
                FirstName = "Rachel",
                LastName = "Taylor",
                Street = "345 Longview Dr.",
                City = "Houston",
                State = "TX",
                ZIP = "77002",
                Email = "r.taylor@longhornairlines.neet",
                Phone = "2814512631",
                EmpType = "Manager",
                Password = "swansong"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 13,
                FirstName = "Victoria",
                LastName = "Lawrence",
                Street = "6639 Butterfly Ln.",
                City = "Houston",
                State = "TX",
                ZIP = "77003",
                Email = "v.lawrence@longhornairlines.neet",
                Phone = "2819457399",
                EmpType = "Agent",
                Password = "lottery"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 14,
                FirstName = "Allen",
                LastName = "Rogers",
                Street = "4965 Oak Hill",
                City = "Dallas",
                State = "TX",
                ZIP = "75043",
                Email = "a.rogers@longhornairlines.neet",
                Phone = "4698752943",
                EmpType = "Manager",
                Password = "evanescent"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 15,
                FirstName = "Elizabeth",
                LastName = "Markham",
                Street = "7861 Chevy Chase",
                City = "Austin",
                State = "TX",
                ZIP = "78712",
                Email = "e.markham@longhornairlines.neet",
                Phone = "5124579845",
                EmpType = "Agent",
                Password = "monty3"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 16,
                FirstName = "Christopher",
                LastName = "Baker",
                Street = "1245 Lake Anchorage Blvd.",
                City = "Austin",
                State = "TX",
                ZIP = "78710",
                Email = "c.baker@longhornairlines.neet",
                Phone = "5125571146",
                EmpType = "Flight Attendant",
                Password = "hecktour"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 17,
                FirstName = "Sarah",
                LastName = "Saunders",
                Street = "332 Avenue C",
                City = "Austin",
                State = "TX",
                ZIP = "78613",
                Email = "s.saunders@longhornairlines.neet",
                Phone = "5123497810",
                EmpType = "Agent",
                Password = "rankmary"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 18,
                FirstName = "William",
                LastName = "Sewell",
                Street = "2365 51st St.",
                City = "Austin",
                State = "TX",
                ZIP = "78705",
                Email = "w.sewell@longhornairlines.neet",
                Phone = "5124510084",
                EmpType = "Manager",
                Password = "walkamile"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 19,
                FirstName = "Jack",
                LastName = "Mason",
                Street = "444 45th St",
                City = "Houston",
                State = "TX",
                ZIP = "77012",
                Email = "j.mason@longhornairlines.neet",
                Phone = "2818833432",
                EmpType = "Flight Attendant",
                Password = "changalang"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 20,
                FirstName = "Jack",
                LastName = "Jackson",
                Street = "222 Main",
                City = "Houston",
                State = "TX",
                ZIP = "77004",
                Email = "j.jackson@longhornairlines.neet",
                Phone = "2815554545",
                EmpType = "Co-Pilot",
                Password = "offbeat"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 21,
                FirstName = "Mary",
                LastName = "Nguyen",
                Street = "465 N. Bear Cub",
                City = "Dallas",
                State = "TX",
                ZIP = "75001",
                Email = "m.nguyen@longhornairlines.neet",
                Phone = "4695524141",
                EmpType = "Pilot",
                Password = "landus"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 22,
                FirstName = "Susan",
                LastName = "Barnes",
                Street = "888 S. Main",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "s.barnes@longhornairlines.neet",
                Phone = "2816662323",
                EmpType = "Flight Attendant",
                Password = "rhythm"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 23,
                FirstName = "Lester",
                LastName = "Jones",
                Street = "999 LeBlat",
                City = "Houston",
                State = "TX",
                ZIP = "77011",
                Email = "l.jones@longhornairlines.neet",
                Phone = "2816662222",
                EmpType = "Co-Pilot",
                Password = "kindly"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 24,
                FirstName = "Hector",
                LastName = "Garcia",
                Street = "777 PBR Drive",
                City = "Houston",
                State = "TX",
                ZIP = "77012",
                Email = "h.garcia@longhornairlines.neet",
                Phone = "2811114444",
                EmpType = "Pilot",
                Password = "instrument"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 25,
                FirstName = "Cindy",
                LastName = "Silva",
                Street = "900 4th St",
                City = "Austin",
                State = "TX",
                ZIP = "78718",
                Email = "c.silva@longhornairlines.neet",
                Phone = "5121113333",
                EmpType = "Flight Attendant",
                Password = "arched"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 26,
                FirstName = "Marshall",
                LastName = "Lopez",
                Street = "90 SW North St",
                City = "Austin",
                State = "TX",
                ZIP = "78719",
                Email = "m.lopez@longhornairlines.neet",
                Phone = "5124442222",
                EmpType = "Co-Pilot",
                Password = "median"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 27,
                FirstName = "Bill",
                LastName = "Larson",
                Street = "1212 N. First Ave",
                City = "Houston",
                State = "TX",
                ZIP = "77025",
                Email = "b.larson@longhornairlines.neet",
                Phone = "2815554444",
                EmpType = "Pilot",
                Password = "approval"
            });
            EmployeesList.Add(new SeedUserModel
            {
                UserID = 28,
                FirstName = "Suzie",
                LastName = "Rankin",
                Street = "23 Polar Bear Road",
                City = "Dallas",
                State = "TX",
                ZIP = "75088",
                Email = "s.rankin@longhornairlines.neet",
                Phone = "4693336666",
                EmpType = "Flight Attendant",
                Password = "decorate"
            });

                foreach (SeedUserModel seedUser in EmployeesList)
                {
                    //Check if user is in database
                    AppUser newUser = _db.Users.FirstOrDefault(u => u.Email == seedUser.Email);

                    if (newUser == null) //the user isn't in the database
                    {
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
