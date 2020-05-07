using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LonghornAirlines
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            String connectionString = "Server=tcp:mis333ksp20.database.windows.net,1433;Initial Catalog=MIS333kSP20-102;Persist Security Info=False;User ID=MIS333kSP20102user;Password=!333Ksp201675;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = false;
                opts.Password.RequiredLength = 3;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
            //Seeding.SeedRoles.AddRoles(service).Wait();
            //Seeding.SeedAdminUser.SeedAdmin(service).Wait();
            //Seeding.SeedCustomers.AddCustomers(service).Wait();
            //Seeding.SeedEmployees.AddEmployees(service).Wait();

        }
    }
}
