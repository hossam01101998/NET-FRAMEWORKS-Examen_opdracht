using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models;
using static NuGet.Packaging.PackagingConstants;
using Microsoft.AspNetCore.Authorization;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly Garage2Context _context;

        public AppointmentsController(Garage2Context context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index(string statusFilter, string search, DateTime? dateFilter)
        {
            var appointments = _context.Appointment.Include(a => a.Car).AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                appointments = appointments.Where(a => a.Status == statusFilter);
            }

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();

                appointments = appointments.Where(a =>
                    a.RequiredService.ToLower().Contains(search) ||
                    a.Car.LicensePlate.ToLower().Contains(search)
                );
            }

            if (dateFilter.HasValue)
            {
                appointments = appointments.Where(a => a.AppointmentDate.Date == dateFilter.Value.Date);
            }

            // ordenar por fecha
            appointments = appointments.OrderBy(a => a.AppointmentDate);

            var appointmentList = await appointments.ToListAsync();

            return View(appointmentList);
        }



        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Car)
                    .ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }
                        
            return View(appointment);
        }


        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.Cars = new SelectList(_context.Car, "CarID", "LicensePlate");

            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,CarID,AppointmentDate,RequiredService,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CarID"] = new SelectList(_context.Car, "CarID", "CarID", appointment.CarID);
            ViewBag.Cars = new SelectList(_context.Car, "CarID", "LicensePlate");

            return View(appointment);

        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.Cars = new SelectList(_context.Car.ToList(), "CarID", "LicensePlate");
            ViewData["FormattedAppointmentDate"] = appointment.AppointmentDate.ToString("yyyy-MM-ddTHH:mm");

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,CarID,AppointmentDate,RequiredService,Status")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "CarID", appointment.CarID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Car)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'Garage2Context.Appointment'  is null.");
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
