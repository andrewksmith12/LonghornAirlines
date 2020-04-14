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
    public class SeedCustomers
    {
        public static async Task AddCustomers(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Create Seed List
            List<SeedUserModel> CustomersList = new List<SeedUserModel>();
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5001,
                FirstName = "Christopher",
                LastName = "Baker",
                Street = "1245 Lake Anchorage Blvd.",
                City = "Dallas",
                State = "TX",
                ZIP = "75001",
                Email = "cbaker@freserve.co.uk",
                Phone = "4695571146",
                EmpType = "Customer",
                Password = "hello",
                Miles = 5000,
                BDay = Convert.ToDateTime("11/23/1949"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5002,
                FirstName = "Michelle",
                LastName = "Banks",
                Street = "1300 Tall Pine Lane",
                City = "Dallas",
                State = "TX",
                ZIP = "75002",
                Email = "banker@longhorn.net",
                Phone = "4692678873",
                EmpType = "Customer",
                Password = "potato",
                Miles = 0,
                BDay = Convert.ToDateTime("11/27/1962"),
                MI = ""
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5003,
                FirstName = "Franco",
                LastName = "Broccolo",
                Street = "62 Browning Road",
                City = "Houston",
                State = "TX",
                ZIP = "77003",
                Email = "franco@aoll.com",
                Phone = "2815659699",
                EmpType = "Customer",
                Password = "painting",
                Miles = 10000,
                BDay = Convert.ToDateTime("10/11/1992"),
                MI = "V"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5004,
                FirstName = "Wendy",
                LastName = "Chang",
                Street = "202 Bellmont Hall",
                City = "Austin",
                State = "TX",
                ZIP = "78719",
                Email = "wchang@gogle.com",
                Phone = "5125943222",
                EmpType = "Customer",
                Password = "texas",
                Miles = 5000,
                BDay = Convert.ToDateTime("05/16/1997"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5005,
                FirstName = "Lim",
                LastName = "Chou",
                Street = "1600 Teresa Lane",
                City = "Fort Meyers",
                State = "FL",
                ZIP = "33917",
                Email = "limchou@yoho.com",
                Phone = "8137724599",
                EmpType = "Customer",
                Password = "Anchorage",
                Miles = 0,
                BDay = Convert.ToDateTime("04/06/1970"),
                MI = ""
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5006,
                FirstName = "Shan",
                LastName = "Dixon",
                Street = "234 Holston Circle",
                City = "Sheffield",
                State = "AL",
                ZIP = "35662",
                Email = "shdixon@utx.edu",
                Phone = "2052643255",
                EmpType = "Customer",
                Password = "pepperoni",
                Miles = 0,
                BDay = Convert.ToDateTime("01/12/1984"),
                MI = "D"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5007,
                FirstName = "Jim Bob",
                LastName = "Evans",
                Street = "506 Farrell Circle",
                City = "Austin",
                State = "TX",
                ZIP = "78705",
                Email = "j.b.evans@aheca.org",
                Phone = "5122565834",
                EmpType = "Customer",
                Password = "longhorns",
                Miles = 9000,
                BDay = Convert.ToDateTime("09/09/1959"),
                MI = ""
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5008,
                FirstName = "Lou Ann",
                LastName = "Feeley",
                Street = "600 S 8th Street W",
                City = "El Paso",
                State = "TX",
                ZIP = "79901",
                Email = "feeley@longhorn.org",
                Phone = "9152556749",
                EmpType = "Customer",
                Password = "aggies",
                Miles = 6000,
                BDay = Convert.ToDateTime("01/12/2002"),
                MI = "K"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5009,
                FirstName = "Tesa",
                LastName = "Freeley",
                Street = "4448 Fairview Ave.",
                City = "Minnetonka",
                State = "MN",
                ZIP = "55343",
                Email = "tfreeley@minnetonka.ci.us",
                Phone = "6123255687",
                EmpType = "Customer",
                Password = "raiders",
                Miles = 0,
                BDay = Convert.ToDateTime("02/04/1991"),
                MI = "P"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5010,
                FirstName = "Margaret",
                LastName = "Garcia",
                Street = "594 Longview",
                City = "Dallas",
                State = "TX",
                ZIP = "75043",
                Email = "mgarcia@gogle.com",
                Phone = "4696593544",
                EmpType = "Customer",
                Password = "mustangs",
                Miles = 4000,
                BDay = Convert.ToDateTime("10/02/1991"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5011,
                FirstName = "Charles",
                LastName = "Haley",
                Street = "One Cowboy Pkwy",
                City = "Dallas",
                State = "TX",
                ZIP = "75032",
                Email = "chaley@thug.com",
                Phone = "4698475583",
                EmpType = "Customer",
                Password = "onetime",
                Miles = 7000,
                BDay = Convert.ToDateTime("07/10/1974"),
                MI = "E"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5012,
                FirstName = "Jeffrey",
                LastName = "Hampton",
                Street = "337 38th St.",
                City = "Dallas",
                State = "TX",
                ZIP = "75209",
                Email = "jeffh@sonic.com",
                Phone = "4696978613",
                EmpType = "Customer",
                Password = "hampton1",
                Miles = 5000,
                BDay = Convert.ToDateTime("03/10/2014"),
                MI = "T"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5013,
                FirstName = "John",
                LastName = "Hearn",
                Street = "4225 North First",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "wjhearniii@umich.org",
                Phone = "2818965621",
                EmpType = "Customer",
                Password = "jhearn22",
                Miles = 7000,
                BDay = Convert.ToDateTime("08/05/1950"),
                MI = "B"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5014,
                FirstName = "Anthony",
                LastName = "Hicks",
                Street = "32 NE Garden Ln., Ste 910",
                City = "Houston",
                State = "TX",
                ZIP = "77012",
                Email = "ahick@yaho.com",
                Phone = "2815788965",
                EmpType = "Customer",
                Password = "hickhickup",
                Miles = 6000,
                BDay = Convert.ToDateTime("12/08/2005"),
                MI = "J"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5015,
                FirstName = "Brad",
                LastName = "Ingram",
                Street = "6548 La Posada Ct.",
                City = "Austin",
                State = "TX",
                ZIP = "78613",
                Email = "ingram@jack.com",
                Phone = "5124678821",
                EmpType = "Customer",
                Password = "ingram2015",
                Miles = 8000,
                BDay = Convert.ToDateTime("09/05/2016"),
                MI = "S"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5016,
                FirstName = "Todd",
                LastName = "Jacobs",
                Street = "4564 Elm St.",
                City = "El Paso",
                State = "TX",
                ZIP = "79991",
                Email = "toddj@yourmom.com",
                Phone = "9154653365",
                EmpType = "Customer",
                Password = "toddy25",
                Miles = 5000,
                BDay = Convert.ToDateTime("01/20/1999"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5017,
                FirstName = "Victoria",
                LastName = "Lawrence",
                Street = "6639 Butterfly Ln.",
                City = "El Paso",
                State = "TX",
                ZIP = "79930",
                Email = "thequeen@aska.net",
                Phone = "9159457399",
                EmpType = "Customer",
                Password = "something",
                Miles = 0,
                BDay = Convert.ToDateTime("04/14/2000"),
                MI = "M"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5018,
                FirstName = "Erik",
                LastName = "Lineback",
                Street = "1300 Netherland St",
                City = "Austin",
                State = "TX",
                ZIP = "78613",
                Email = "linebacker@gogle.com",
                Phone = "5122449976",
                EmpType = "Customer",
                Password = "Password1",
                Miles = 6000,
                BDay = Convert.ToDateTime("12/02/2013"),
                MI = "W"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5019,
                FirstName = "Ernest",
                LastName = "Lowe",
                Street = "3201 Pine Drive",
                City = "Dallas",
                State = "TX",
                ZIP = "75039",
                Email = "elowe@netscare.net",
                Phone = "4695344627",
                EmpType = "Customer",
                Password = "aclfest2017",
                Miles = 2000,
                BDay = Convert.ToDateTime("12/07/1977"),
                MI = "S"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5020,
                FirstName = "Chuck",
                LastName = "Luce",
                Street = "2345 Rolling Clouds",
                City = "Austin",
                State = "TX",
                ZIP = "78712",
                Email = "cluce@gogle.com",
                Phone = "5126983548",
                EmpType = "Customer",
                Password = "nothinggood",
                Miles = 8000,
                BDay = Convert.ToDateTime("03/16/1949"),
                MI = "B"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5021,
                FirstName = "Jennifer",
                LastName = "MacLeod",
                Street = "2504 Far West Blvd.",
                City = "Houston",
                State = "TX",
                ZIP = "77012",
                Email = "mackcloud@george.com",
                Phone = "2814748138",
                EmpType = "Customer",
                Password = "whatever",
                Miles = 5000,
                BDay = Convert.ToDateTime("02/21/1947"),
                MI = "D"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5022,
                FirstName = "Elizabeth",
                LastName = "Markham",
                Street = "7861 Chevy Chase",
                City = "Dallas",
                State = "TX",
                ZIP = "75249",
                Email = "cmartin@beets.com",
                Phone = "4694579845",
                EmpType = "Customer",
                Password = "whocares",
                Miles = 7000,
                BDay = Convert.ToDateTime("03/20/1972"),
                MI = "P"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5023,
                FirstName = "Clarence",
                LastName = "Martin",
                Street = "87 Alcedo St.",
                City = "San Diego",
                State = "CA",
                ZIP = "82448",
                Email = "clarence@yoho.com",
                Phone = "9204955201",
                EmpType = "Customer",
                Password = "xcellent",
                Miles = 2000,
                BDay = Convert.ToDateTime("07/19/1992"),
                MI = "A"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5024,
                FirstName = "Gregory",
                LastName = "Martinez",
                Street = "8295 Sunset Blvd.",
                City = "Austin",
                State = "TX",
                ZIP = "78708",
                Email = "gregmartinez@drdre.com",
                Phone = "5128746718",
                EmpType = "Customer",
                Password = "snowsnow",
                Miles = 1000,
                BDay = Convert.ToDateTime("05/28/1947"),
                MI = "R"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5025,
                FirstName = "Charles",
                LastName = "Miller",
                Street = "8962 Main St.",
                City = "Dallas",
                State = "TX",
                ZIP = "75215",
                Email = "cmiller@bob.com",
                Phone = "4697458615",
                EmpType = "Customer",
                Password = "mydogspot",
                Miles = 2000,
                BDay = Convert.ToDateTime("10/15/1990"),
                MI = "R"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5026,
                FirstName = "Kelly",
                LastName = "Nelson",
                Street = "2601 Red River",
                City = "Dallas",
                State = "TX",
                ZIP = "75252",
                Email = "knelson@aoll.com",
                Phone = "4692926966",
                EmpType = "Customer",
                Password = "spotmydog",
                Miles = 0,
                BDay = Convert.ToDateTime("07/13/1971"),
                MI = "T"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5027,
                FirstName = "Joe",
                LastName = "Nguyen",
                Street = "1249 4th SW St.",
                City = "Dallas",
                State = "TX",
                ZIP = "75263",
                Email = "joewin@xfactor.com",
                Phone = "4693125897",
                EmpType = "Customer",
                Password = "joejoejoe",
                Miles = 9000,
                BDay = Convert.ToDateTime("03/17/2008"),
                MI = "C"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5028,
                FirstName = "Bill",
                LastName = "O'Reilly",
                Street = "8800 Gringo Drive",
                City = "Dallas",
                State = "TX",
                ZIP = "75263",
                Email = "orielly@foxnews.cnn",
                Phone = "4693450925",
                EmpType = "Customer",
                Password = "billyboy",
                Miles = 5000,
                BDay = Convert.ToDateTime("07/08/1959"),
                MI = "T"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5029,
                FirstName = "Anka",
                LastName = "Radkovich",
                Street = "1300 Elliott Pl",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "ankaisrad@gogle.com",
                Phone = "2812345566",
                EmpType = "Customer",
                Password = "radgirl",
                Miles = 0,
                BDay = Convert.ToDateTime("05/19/1966"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5030,
                FirstName = "Megan",
                LastName = "Rhodes",
                Street = "4587 Enfield Rd.",
                City = "Houston",
                State = "TX",
                ZIP = "77013",
                Email = "megrhodes@freserve.co.uk",
                Phone = "2813744746",
                EmpType = "Customer",
                Password = "meganr34",
                Miles = 6000,
                BDay = Convert.ToDateTime("03/12/1965"),
                MI = "C"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5031,
                FirstName = "Eryn",
                LastName = "Rice",
                Street = "3405 Rio Grande",
                City = "Houston",
                State = "TX",
                ZIP = "77015",
                Email = "erynrice@aoll.com",
                Phone = "2813876657",
                EmpType = "Customer",
                Password = "ricearoni",
                Miles = 3000,
                BDay = Convert.ToDateTime("04/28/1975"),
                MI = "M"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5032,
                FirstName = "Jorge",
                LastName = "Rodriguez",
                Street = "6788 Cotter Street",
                City = "Houston",
                State = "TX",
                ZIP = "77000",
                Email = "jorge@noclue.com",
                Phone = "2818904374",
                EmpType = "Customer",
                Password = "jrod2017",
                Miles = 5000,
                BDay = Convert.ToDateTime("12/08/1953"),
                MI = ""
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5033,
                FirstName = "Allen",
                LastName = "Rogers",
                Street = "4965 Oak Hill",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "mrrogers@lovelyday.com",
                Phone = "2818752943",
                EmpType = "Customer",
                Password = "rogerthat",
                Miles = 8000,
                BDay = Convert.ToDateTime("04/22/1973"),
                MI = "B"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5034,
                FirstName = "Olivier",
                LastName = "Saint-Jean",
                Street = "255 Toncray Dr.",
                City = "Blacksburg",
                State = "VA",
                ZIP = "24060",
                Email = "stjean@athome.com",
                Phone = "3434145678",
                EmpType = "Customer",
                Password = "bunnyhop",
                Miles = 0,
                BDay = Convert.ToDateTime("02/19/1995"),
                MI = "M"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5035,
                FirstName = "Sarah",
                LastName = "Saunders",
                Street = "332 Avenue C",
                City = "El Paso",
                State = "TX",
                ZIP = "79945",
                Email = "saunders@pen.com",
                Phone = "9153497810",
                EmpType = "Customer",
                Password = "penguin12",
                Miles = 8000,
                BDay = Convert.ToDateTime("02/19/1978"),
                MI = "J"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5036,
                FirstName = "William",
                LastName = "Sewell",
                Street = "2365 51st St.",
                City = "El Paso",
                State = "TX",
                ZIP = "79946",
                Email = "willsheff@email.com",
                Phone = "9154510084",
                EmpType = "Customer",
                Password = "alaskaboy",
                Miles = 8000,
                BDay = Convert.ToDateTime("12/23/2014"),
                MI = "T"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5037,
                FirstName = "Martin",
                LastName = "Sheffield",
                Street = "3886 Avenue A",
                City = "El Paso",
                State = "TX",
                ZIP = "79950",
                Email = "sheffiled@gogle.com",
                Phone = "9155479167",
                EmpType = "Customer",
                Password = "martin1234",
                Miles = 0,
                BDay = Convert.ToDateTime("05/08/1960"),
                MI = "J"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5038,
                FirstName = "John",
                LastName = "Smith",
                Street = "23 Hidden Forge Dr.",
                City = "Fayetteville",
                State = "NC",
                ZIP = "28304",
                Email = "johnsmith187@aoll.com",
                Phone = "2838321888",
                EmpType = "Customer",
                Password = "smitty444",
                Miles = 3000,
                BDay = Convert.ToDateTime("06/25/1955"),
                MI = "A"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5039,
                FirstName = "Dustin",
                LastName = "Stroud",
                Street = "1212 Rita Rd",
                City = "Springfield",
                State = "IL",
                ZIP = "62707",
                Email = "dustroud@mail.com",
                Phone = "2172346667",
                EmpType = "Customer",
                Password = "dustydusty",
                Miles = 6000,
                BDay = Convert.ToDateTime("07/26/1967"),
                MI = "P"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5040,
                FirstName = "Eric",
                LastName = "Stuart",
                Street = "5576 Toro Ring",
                City = "Austin",
                State = "TX",
                ZIP = "78720",
                Email = "estuart@mail.net",
                Phone = "5128178335",
                EmpType = "Customer",
                Password = "stewball",
                Miles = 0,
                BDay = Convert.ToDateTime("12/04/1947"),
                MI = "D"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5041,
                FirstName = "Peter",
                LastName = "Stump",
                Street = "1300 Kellen Circle",
                City = "Austin",
                State = "TX",
                ZIP = "78721",
                Email = "peterstump@noclue.com",
                Phone = "5124560903",
                EmpType = "Customer",
                Password = "slowwind",
                Miles = 2000,
                BDay = Convert.ToDateTime("07/10/1974"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5042,
                FirstName = "Jeremy",
                LastName = "Tanner",
                Street = "4347 Almstead",
                City = "Austin",
                State = "TX",
                ZIP = "78735",
                Email = "jtanner@mustang.net",
                Phone = "5124590929",
                EmpType = "Customer",
                Password = "tanner5454",
                Miles = 5000,
                BDay = Convert.ToDateTime("01/11/1944"),
                MI = "S"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5043,
                FirstName = "Allison",
                LastName = "Taylor",
                Street = "467 Nueces St.",
                City = "Austin",
                State = "TX",
                ZIP = "78710",
                Email = "taylordjay@aoll.com",
                Phone = "5124748452",
                EmpType = "Customer",
                Password = "allyrally",
                Miles = 0,
                BDay = Convert.ToDateTime("11/14/1990"),
                MI = "R"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5044,
                FirstName = "Rachel",
                LastName = "Taylor",
                Street = "345 Longview Dr.",
                City = "Dallas",
                State = "TX",
                ZIP = "75001",
                Email = "rtaylor@gogle.com",
                Phone = "4694907631",
                EmpType = "Customer",
                Password = "taylorbaylor",
                Miles = 10000,
                BDay = Convert.ToDateTime("01/18/1976"),
                MI = "K"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5045,
                FirstName = "Frank",
                LastName = "Tee",
                Street = "5590 Lavell Dr",
                City = "Dallas",
                State = "TX",
                ZIP = "75063",
                Email = "teefrank@noclue.com",
                Phone = "4698765543",
                EmpType = "Customer",
                Password = "teeoff22",
                Miles = 0,
                BDay = Convert.ToDateTime("09/06/1998"),
                MI = "J"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5046,
                FirstName = "Clent",
                LastName = "Tucker",
                Street = "312 Main St.",
                City = "Dallas",
                State = "TX",
                ZIP = "75206",
                Email = "ctucker@alphabet.co.uk",
                Phone = "4698471154",
                EmpType = "Customer",
                Password = "tucksack1",
                Miles = 7000,
                BDay = Convert.ToDateTime("02/25/1943"),
                MI = "J"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5047,
                FirstName = "Allen",
                LastName = "Velasco",
                Street = "679 W. 4th",
                City = "Dallas",
                State = "TX",
                ZIP = "75215",
                Email = "avelasco@yoho.com",
                Phone = "4693985638",
                EmpType = "Customer",
                Password = "meow88",
                Miles = 8000,
                BDay = Convert.ToDateTime("09/10/1985"),
                MI = "G"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5048,
                FirstName = "Janet",
                LastName = "Vino",
                Street = "189 Grape Road",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "vinovino@grapes.com",
                Phone = "2815643832",
                EmpType = "Customer",
                Password = "vinovino",
                Miles = 0,
                BDay = Convert.ToDateTime("02/07/1985"),
                MI = "E"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5049,
                FirstName = "Jake",
                LastName = "West",
                Street = "RR 3287",
                City = "Houston",
                State = "TX",
                ZIP = "77025",
                Email = "westj@pioneer.net",
                Phone = "2818475244",
                EmpType = "Customer",
                Password = "gowest",
                Miles = 8000,
                BDay = Convert.ToDateTime("01/09/1976"),
                MI = "T"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5050,
                FirstName = "Louis",
                LastName = "Winthorpe",
                Street = "2500 Padre Blvd",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "winner@hootmail.com",
                Phone = "2815650098",
                EmpType = "Customer",
                Password = "louielouie",
                Miles = 6000,
                BDay = Convert.ToDateTime("04/19/1953"),
                MI = "L"
            });
            CustomersList.Add(new SeedUserModel
            {
                UserID = 5051,
                FirstName = "Reagan",
                LastName = "Wood",
                Street = "447 Westlake Dr.",
                City = "Houston",
                State = "TX",
                ZIP = "77010",
                Email = "rwood@voyager.net",
                Phone = "2814545242",
                EmpType = "Customer",
                Password = "woodyman1",
                Miles = 0,
                BDay = Convert.ToDateTime("12/28/2002"),
                MI = "B"
            });

            foreach (SeedUserModel seedUser in CustomersList)
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
                    newUser.MI = seedUser.MI;
                    newUser.SSN = seedUser.SSN;
                    newUser.Birthday = seedUser.BDay;
                    newUser.Mileage = seedUser.Miles;
                    newUser.isActive = true;


                    var result = await _userManager.CreateAsync(newUser, seedUser.Password);
                    if (result.Succeeded == false)
                    {
                        throw new Exception("This user can't be added - " + result.ToString());
                    }
                    _db.SaveChanges();
                    newUser = _db.Users.FirstOrDefault(u => u.UserName == seedUser.Email);
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
