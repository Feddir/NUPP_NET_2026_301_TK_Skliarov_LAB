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
    public class BikeModelsController : Controller
    {
        private readonly BikeShopContext _context;

        public BikeModelsController(BikeShopContext context)
        {
            _context = context;
        }

        // GET: BikeModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bikes.ToListAsync());
        }

        // GET: BikeModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeModel = await _context.Bikes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikeModel == null)
            {
                return NotFound();
            }

            return View(bikeModel);
        }

        // GET: BikeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BikeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,FrameSize,WheelSize,Price")] BikeModel bikeModel)
        {
            if (ModelState.IsValid)
            {
                bikeModel.Id = Guid.NewGuid();
                _context.Add(bikeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikeModel);
        }

        // GET: BikeModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeModel = await _context.Bikes.FindAsync(id);
            if (bikeModel == null)
            {
                return NotFound();
            }
            return View(bikeModel);
        }

        // POST: BikeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Brand,FrameSize,WheelSize,Price")] BikeModel bikeModel)
        {
            if (id != bikeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeModelExists(bikeModel.Id))
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
            return View(bikeModel);
        }

        // GET: BikeModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeModel = await _context.Bikes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikeModel == null)
            {
                return NotFound();
            }

            return View(bikeModel);
        }

        // POST: BikeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bikeModel = await _context.Bikes.FindAsync(id);
            if (bikeModel != null)
            {
                _context.Bikes.Remove(bikeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikeModelExists(Guid id)
        {
            return _context.Bikes.Any(e => e.Id == id);
        }
    }
}
