using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly FitnessClubContext _context;

        public EquipmentsController(FitnessClubContext context)
        {
            _context = context;
        }

        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            var fitnessClubContext = _context.Equipments.Include(e => e.Room);
            return View(await fitnessClubContext.ToListAsync());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,RoomId")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", equipment.RoomId);
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", equipment.RoomId);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Price,RoomId")] Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", equipment.RoomId);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(long id)
        {
            return _context.Equipments.Any(e => e.Id == id);
        }
    }
}
