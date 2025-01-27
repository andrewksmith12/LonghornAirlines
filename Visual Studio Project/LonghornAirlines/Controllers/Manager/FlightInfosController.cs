﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LonghornAirlines.DAL;
using LonghornAirlines.Models.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace LonghornAirlines.Controllers
{
    [Authorize(Roles = "Manager")]
    public class FlightInfosController : Controller
    {
        private readonly AppDbContext _context;

        public FlightInfosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FlightInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlightInfos.Include(fi => fi.Route).ThenInclude(fi => fi.CityFrom).Include(fi => fi.Route).ThenInclude(fi => fi.CityTo).ToListAsync());
        }

        // GET: FlightInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightInfo = await _context.FlightInfos.Include(fi => fi.Flights).Include(fi => fi.Route).ThenInclude(fi => fi.CityFrom)
                .Include(fi => fi.Route).ThenInclude(fi => fi.CityTo)
                .Include(fi => fi.Flights).ThenInclude(fi => fi.Pilot)
                .Include(fi => fi.Flights).ThenInclude(fi => fi.CoPilot)
                .Include(fi => fi.Flights).ThenInclude(fi => fi.Attendant)
                .FirstOrDefaultAsync(m => m.FlightInfoID == id);
            if (flightInfo == null)
            {
                return NotFound();
            }

            return View(flightInfo);
        }

        // GET: FlightInfoes/Create
        public IActionResult Create()
        {
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        // POST: FlightInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightNumber,FlightTime,BaseFare")] FlightInfo flightInfo, int DepartCity, int ArriveCity)
        {
            Route route = _context.Routes.Include(m => m.CityFrom).Include(m => m.CityTo).FirstOrDefault(m => m.CityFrom.CityID == DepartCity && m.CityTo.CityID == ArriveCity);
            if (_context.FlightInfos.Where(f => f.FlightNumber == flightInfo.FlightNumber).Count() != 0)
            {
                ViewBag.AllCities = GetAllCities();
                String error = "Flight Number Already Exists. Please select a different Flight Number.";
                ViewBag.error = error;
                return View("Create", flightInfo);
            }
            if (route == null)
            {
                ViewBag.AllCities = GetAllCities();
                String error = "Route Not Found, Please Check your Departure & Arrival Cities";
                ViewBag.Error = error;
                return View("Create", flightInfo);
            };
            if (ModelState.IsValid)
            {
               
                flightInfo.Route = route;
                _context.FlightInfos.Add(flightInfo);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Edit", new { id = flightInfo.FlightInfoID });
            }
            return View(flightInfo);
        }

        // GET: FlightInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightInfo = await _context.FlightInfos.Include(f => f.Route).ThenInclude(f => f.CityFrom).Include(f => f.Route).ThenInclude(f => f.CityTo).FirstOrDefaultAsync(f => f.FlightInfoID == id);
            if (flightInfo == null)
            {
                return NotFound();
            }
            return View(flightInfo);
        }

        // POST: FlightInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightInfoID,FlightTime,BaseFare,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday")] FlightInfo flightInfo)
        {
            if (id != flightInfo.FlightInfoID)
            {
                return NotFound();
            }
            FlightInfo dbFlightInfo = _context.FlightInfos.FirstOrDefault(m => m.FlightInfoID == flightInfo.FlightInfoID);
            // Add Bools
            Boolean sundayBooladd = false;
            Boolean mondayBooladd = false;
            Boolean tuesdayBooladd = false;
            Boolean wednesdayBooladd = false;
            Boolean thursdayBooladd = false;
            Boolean fridayBooladd = false;
            Boolean saturdayBooladd = false;
            // RM Bools
            Boolean sundayBoolrm = false;
            Boolean mondayBoolrm = false;
            Boolean tuesdayBoolrm = false;
            Boolean wednesdayBoolrm = false;
            Boolean thursdayBoolrm = false;
            Boolean fridayBoolrm = false;
            Boolean saturdayBoolrm = false;
            // Update Fields
            

            // Set Booleans
            if (dbFlightInfo.Sunday != flightInfo.Sunday)
            {
                if (flightInfo.Sunday == true)
                {
                    sundayBooladd = true;
                }
                else
                {
                    sundayBoolrm = true;
                }
                dbFlightInfo.Sunday = flightInfo.Sunday;
            }
            if (dbFlightInfo.Monday != flightInfo.Monday)
            {
                if (flightInfo.Monday == true)
                {
                    mondayBooladd = true;
                }
                else
                {
                    mondayBoolrm = true;
                }
                dbFlightInfo.Monday = flightInfo.Monday;

            }
            if (dbFlightInfo.Tuesday != flightInfo.Tuesday)
            {
                if (flightInfo.Tuesday == true)
                {
                    tuesdayBooladd = true;
                }
                else
                {
                    tuesdayBoolrm = true;
                }
                dbFlightInfo.Tuesday = flightInfo.Tuesday;

            }
            if (dbFlightInfo.Wednesday != flightInfo.Wednesday)
            {
                if (flightInfo.Wednesday == true)
                {
                    wednesdayBooladd = true;
                }
                else
                {
                    wednesdayBoolrm = true;
                }
                dbFlightInfo.Wednesday = flightInfo.Wednesday;

            }
            if (dbFlightInfo.Thursday != flightInfo.Thursday)
            {
                if (flightInfo.Thursday == true)
                {
                    thursdayBooladd = true;
                }
                else
                {
                    thursdayBoolrm = true;
                }
                dbFlightInfo.Thursday = flightInfo.Thursday;

            }
            if (dbFlightInfo.Friday != flightInfo.Friday)
            {
                if (flightInfo.Friday == true)
                {
                    fridayBooladd = true;
                }
                else
                {
                    fridayBoolrm = true;
                }
                dbFlightInfo.Friday = flightInfo.Friday;

            }
            if (dbFlightInfo.Saturday != flightInfo.Saturday)
            {
                if (flightInfo.Saturday == true)
                {
                    saturdayBooladd = true;
                }
                else
                {
                    saturdayBoolrm = true;
                }
                dbFlightInfo.Saturday = flightInfo.Saturday;

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbFlightInfo);
                    _context.SaveChanges();
                    FlightInfo flightInfoforAdd = _context.FlightInfos.Include(f => f.Flights).FirstOrDefault(f => f.FlightInfoID == flightInfo.FlightInfoID);
                    Utilities.AddFlight.addBools(_context, flightInfoforAdd, sundayBooladd, mondayBooladd, tuesdayBooladd, wednesdayBooladd, thursdayBooladd, fridayBooladd, saturdayBooladd);
                    Utilities.RemoveFlight.removeBools(_context, flightInfo, sundayBoolrm, mondayBoolrm, tuesdayBoolrm, wednesdayBoolrm, thursdayBoolrm, fridayBoolrm, saturdayBoolrm);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightInfoExists(flightInfo.FlightInfoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flightInfo);
        }

        // GET: FlightInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightInfo = await _context.FlightInfos
                .FirstOrDefaultAsync(m => m.FlightInfoID == id);
            if (flightInfo == null)
            {
                return NotFound();
            }

            return View(flightInfo);
        }

        // POST: FlightInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightInfo = await _context.FlightInfos.FindAsync(id);
            _context.FlightInfos.Remove(flightInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightInfoExists(int id)
        {
            return _context.FlightInfos.Any(e => e.FlightInfoID == id);
        }

        public SelectList GetAllCities()
        {
            List<City> cityList = _context.Cities.ToList();

            return new SelectList(cityList.OrderBy(c => c.CityID), "CityID", "CityName");
        }

    }
}
