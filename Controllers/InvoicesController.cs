using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Controllers
{
    [Authorize (Roles = "Admin")]
    public class InvoicesController : Controller
    {
        private readonly Garage2Context _context;

        public InvoicesController(Garage2Context context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewData["IssueDateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "issuedate_desc" : "";
            ViewData["TotalAmountSortParm"] = sortOrder == "totalamount" ? "totalamount_desc" : "totalamount";

            var invoices = _context.Invoice.AsQueryable();

            switch (sortOrder)
            {
                case "issuedate_desc":
                    invoices = invoices.OrderByDescending(i => i.IssueDate);
                    break;
                case "totalamount":
                    invoices = invoices.OrderBy(i => i.TotalAmount);
                    break;
                case "totalamount_desc":
                    invoices = invoices.OrderByDescending(i => i.TotalAmount);
                    break;
                default:
                    invoices = invoices.OrderBy(i => i.IssueDate);
                    break;
            }

            invoices = invoices.Include(i => i.Car).Include(c => c.Car.Customer);
            
            decimal totalInvoicesSum = invoices.Sum(i => i.TotalAmount);
            ViewBag.TotalInvoicesSum = totalInvoicesSum;

            return View(await invoices.ToListAsync());
        }


        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Car)
                .Include(i => i.Car.Customer)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
        // GET: Invoices/Create
        public IActionResult Create()
        {

            ViewData["Cars"] = new SelectList(_context.Car, "CarID", "LicensePlate");
            //ViewData["Customers"] = new SelectList(_context.Customer, "CustomerId", "Name");
            return View();
        }






        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,CarID,IssueDate,TotalAmount,Details")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var cars = _context.Car.Select(c => new SelectListItem
            {
                Value = c.CarID.ToString(),
                Text = c.LicensePlate
            }).ToList();

            ViewData["Cars"] = new SelectList(cars, "Value", "Text");

            return View(invoice);
        }


        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            //para eliminar los puntos cada 3 digitos

            string totalAmountString = new string(invoice.TotalAmount.ToString().Where(c => char.IsDigit(c) || c == '.').ToArray());

            // reemplaza la coma por un punto en el TotalAmount
            invoice.TotalAmount = (decimal.Parse(invoice.TotalAmount.ToString("F2"), CultureInfo.InvariantCulture))/100;

            ViewData["FormattedInvoiceDate"] = invoice.IssueDate.ToString("yyyy-MM-ddTHH:mm");

            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "LicensePlate", invoice.CarID);
            //ViewData["CarDetails"] = _context.Car.FirstOrDefault(c => c.CarID == invoice.CarID)?.SomeProperty;

            return View(invoice);
        }


        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,CarID,IssueDate,TotalAmount,Details")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "CarID", invoice.CarID);
            //ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", invoice.Car.CustomerId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Car)
                //.Include(i => i.Car.Customer)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoice == null)
            {
                return Problem("Entity set 'Garage2Context.Invoice'  is null.");
            }
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoice.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
          return (_context.Invoice?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
