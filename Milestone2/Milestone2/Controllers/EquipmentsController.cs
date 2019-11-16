using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;
using Milestone2.Models;
using Milestone2.Services.Equipments;

namespace Milestone2.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly EquipmentService _equipmentService;

        public EquipmentsController(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            return View(await _equipmentService.GetAllEquipments());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _equipmentService.GetById((long)id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["RoomId"] = new SelectList(await _equipmentService.GetAllRooms(), "Id", "Id");
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
                await _equipmentService.AddAndSave(equipment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(await _equipmentService.GetAllRooms(), "Id", "Id", equipment.RoomId);
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _equipmentService.GetById((long)id);
            if (equipment == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(await _equipmentService.GetAllRooms(), "Id", "Id", equipment.RoomId);
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
                    await _equipmentService.UpdateAndSave(equipment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_equipmentService.EquipmentExists(equipment.Id))
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
            ViewData["RoomId"] = new SelectList(await _equipmentService.GetAllRooms(), "Id", "Id", equipment.RoomId);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _equipmentService.GetById((long)id);
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
            await _equipmentService.DeleteAndSave(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
