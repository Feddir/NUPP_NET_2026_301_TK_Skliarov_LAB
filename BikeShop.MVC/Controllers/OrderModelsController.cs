using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShop.Infrastructure;
using BikeShop.Infrastructure.Models;

namespace BikeShop.MVC.Controllers
{
    public class OrderModelsController : Controller
    {
        private readonly BikeShopContext _context;

        public OrderModelsController(BikeShopContext context)
        {
            _context = context;
        }

        // GET: OrderModels
        public async Task<IActionResult> Index()
        {
            var bikeShopContext = _context.Orders.Include(o => o.Bike).Include(o => o.Customer);
            return View(await bikeShopContext.ToListAsync());
        }

        // GET: OrderModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .Include(o => o.Bike)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // GET: OrderModels/Create
        public IActionResult Create()
        {
            ViewData["BikeId"] = new SelectList(_context.Bikes, "Id", "Brand");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            return View();
        }

        // POST: OrderModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,Quantity,UnitPrice,CustomerId,BikeId")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                orderModel.Id = Guid.NewGuid();
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BikeId"] = new SelectList(_context.Bikes, "Id", "Brand", orderModel.BikeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", orderModel.CustomerId);
            return View(orderModel);
        }

        // GET: OrderModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            ViewData["BikeId"] = new SelectList(_context.Bikes, "Id", "Brand", orderModel.BikeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", orderModel.CustomerId);
            return View(orderModel);
        }

        // POST: OrderModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrderDate,Quantity,UnitPrice,CustomerId,BikeId")] OrderModel orderModel)
        {
            if (id != orderModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.Id))
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
            ViewData["BikeId"] = new SelectList(_context.Bikes, "Id", "Brand", orderModel.BikeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", orderModel.CustomerId);
            return View(orderModel);
        }

        // GET: OrderModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .Include(o => o.Bike)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: OrderModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel != null)
            {
                _context.Orders.Remove(orderModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderModelExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
