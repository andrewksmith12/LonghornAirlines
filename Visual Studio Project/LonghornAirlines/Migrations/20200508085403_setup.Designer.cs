﻿// <auto-generated />
using System;
using LonghornAirlines.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LonghornAirlines.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200508085403_setup")]
    partial class setup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LonghornAirlines.Models.Business.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityCode");

                    b.Property<string>("CityName");

                    b.Property<string>("CityState");

                    b.HasKey("CityID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Flight", b =>
                {
                    b.Property<int>("FlightID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AttendantCheckIn");

                    b.Property<string>("AttendantId");

                    b.Property<decimal>("BaseFare");

                    b.Property<bool>("Canceled");

                    b.Property<bool>("CoPilotCheckIn");

                    b.Property<string>("CoPilotId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("FlightInfoID");

                    b.Property<bool>("PilotCheckIn");

                    b.Property<string>("PilotId");

                    b.Property<bool>("hasDeparted");

                    b.HasKey("FlightID");

                    b.HasIndex("AttendantId");

                    b.HasIndex("CoPilotId");

                    b.HasIndex("FlightInfoID");

                    b.HasIndex("PilotId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.FlightInfo", b =>
                {
                    b.Property<int>("FlightInfoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BaseFare");

                    b.Property<int>("FlightNumber");

                    b.Property<string>("FlightTime");

                    b.Property<bool>("Friday");

                    b.Property<bool>("Monday");

                    b.Property<int?>("RouteID");

                    b.Property<bool>("Saturday");

                    b.Property<bool>("Sunday");

                    b.Property<bool>("Thursday");

                    b.Property<bool>("Tuesday");

                    b.Property<bool>("Wednesday");

                    b.HasKey("FlightInfoID");

                    b.HasIndex("RouteID");

                    b.ToTable("FlightInfos");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerId");

                    b.Property<int>("MilesPaid");

                    b.Property<int>("NumPassengers");

                    b.Property<bool>("ReservationComplete");

                    b.Property<int>("ReservationMethod");

                    b.Property<int>("ReservationType");

                    b.HasKey("ReservationID");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Route", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityFromCityID");

                    b.Property<int?>("CityToCityID");

                    b.Property<int>("Distance");

                    b.Property<int>("FlightTime");

                    b.HasKey("RouteID");

                    b.HasIndex("CityFromCityID");

                    b.HasIndex("CityToCityID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Ticket", b =>
                {
                    b.Property<int>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CheckedIn");

                    b.Property<string>("CustomerId");

                    b.Property<decimal>("Fare");

                    b.Property<int?>("FlightID");

                    b.Property<int?>("ReservationID");

                    b.Property<string>("Seat");

                    b.Property<bool>("UpgradeWithMilage");

                    b.HasKey("TicketID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FlightID");

                    b.HasIndex("ReservationID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Users.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AdvantageNumber");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MI");

                    b.Property<decimal>("Mileage");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SSN");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int>("UserID");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("ZIP")
                        .HasMaxLength(5);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Flight", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Users.AppUser", "Attendant")
                        .WithMany()
                        .HasForeignKey("AttendantId");

                    b.HasOne("LonghornAirlines.Models.Users.AppUser", "CoPilot")
                        .WithMany()
                        .HasForeignKey("CoPilotId");

                    b.HasOne("LonghornAirlines.Models.Business.FlightInfo", "FlightInfo")
                        .WithMany("Flights")
                        .HasForeignKey("FlightInfoID");

                    b.HasOne("LonghornAirlines.Models.Users.AppUser", "Pilot")
                        .WithMany()
                        .HasForeignKey("PilotId");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.FlightInfo", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Business.Route", "Route")
                        .WithMany("FlightInfos")
                        .HasForeignKey("RouteID");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Reservation", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Users.AppUser", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Route", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Business.City", "CityFrom")
                        .WithMany()
                        .HasForeignKey("CityFromCityID");

                    b.HasOne("LonghornAirlines.Models.Business.City", "CityTo")
                        .WithMany()
                        .HasForeignKey("CityToCityID");
                });

            modelBuilder.Entity("LonghornAirlines.Models.Business.Ticket", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Users.AppUser", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId");

                    b.HasOne("LonghornAirlines.Models.Business.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightID");

                    b.HasOne("LonghornAirlines.Models.Business.Reservation", "Reservation")
                        .WithMany("Tickets")
                        .HasForeignKey("ReservationID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LonghornAirlines.Models.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LonghornAirlines.Models.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}