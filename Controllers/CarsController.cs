using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly Garage2Context _context;

        public CarsController(Garage2Context context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string search, string sortOrder)
        {
            var cars = _context.Car.Include(c => c.Customer).AsQueryable();

            ViewData["MakeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "make_desc" : "";
            ViewData["ModelSortParm"] = sortOrder == "model" ? "model_desc" : "model";
            ViewData["LicensePlateSortParm"] = sortOrder == "licenseplate" ? "licenseplate_desc" : "licenseplate";

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                cars = cars
                    .Where(c =>
                        c.LicensePlate.ToLower().Contains(search) ||
                        c.ChassisNumber.ToLower().Contains(search) ||
                        c.Make.ToLower().Contains(search) ||
                        c.Model.ToLower().Contains(search) ||
                        c.Customer.Name.ToLower().Contains(search));
            }

            cars = cars.OrderBy(c => c.Make).ThenBy(c => c.Model).ThenBy(c => c.LicensePlate);

            if (sortOrder == "make_desc")
            {
                cars = cars.OrderByDescending(c => c.Make);
            }
            else if (sortOrder == "model")
            {
                cars = cars.OrderBy(c => c.Model);
            }
            else if (sortOrder == "model_desc")
            {
                cars = cars.OrderByDescending(c => c.Model);
            }
            else if (sortOrder == "licenseplate")
            {
                cars = cars.OrderBy(c => c.LicensePlate);
            }
            else if (sortOrder == "licenseplate_desc")
            {
                cars = cars.OrderByDescending(c => c.LicensePlate);
            }

            return View(await cars.ToListAsync());
        }




        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
            ViewBag.CustomerList = new SelectList(_context.Customer, "CustomerId", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarID,CustomerId,Make,Model,LicensePlate,ChassisNumber")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.Customer = _context.Customer.FirstOrDefault(c => c.CustomerId == car.CustomerId);


                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("Errores de validación en el modelo:");
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", car.CustomerId);
            ViewBag.CustomerList = new SelectList(_context.Customer, "CustomerId", "Name", car.CustomerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", car.CustomerId);
            ViewBag.CustomerList = new SelectList(_context.Customer, "CustomerId", "Name", car.CustomerId);

            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarID,CustomerId,Make,Model,LicensePlate,ChassisNumber")] Car car)
        {
            if (id != car.CarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                car.Customer = _context.Customer.FirstOrDefault(c => c.CustomerId == car.CustomerId);

                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarID))
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

            ViewBag.CustomerList = new SelectList(_context.Customer, "CustomerId", "Name", car.CustomerId);

            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var car = await _context.Car
                    .Include(c => c.Orders)
                    .FirstOrDefaultAsync(c => c.CarID == id);

                if (car == null)
                {
                    return NotFound();
                }

                _context.Order.RemoveRange(car.Orders);

                _context.Car.Remove(car);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ViewData["ErrorMessage"] = "You cannot delete this car because there are orders or invoices for this car.";
                    return View("Error");
                }

                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        private bool CarExists(int id)
        {
          return (_context.Car?.Any(e => e.CarID == id)).GetValueOrDefault();
        }
    }
}
