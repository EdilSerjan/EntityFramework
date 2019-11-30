using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;
using Milestone2.Models;
using Milestone2.Services.Courses;

namespace Milestone2.Controllers
{

    [Authorize]
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _courseService.GetAllCourses());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetById((long)id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CoachId"] = new SelectList(await _courseService.GetAllCoaches(), "Id", "Name");
            ViewData["RoomId"] = new SelectList(await _courseService.GetAllRooms(), "Id", "Id");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CoachId,RoomId")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.AddAndSave(course);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachId"] = new SelectList(await _courseService.GetAllCoaches(), "Id", "Name", course.CoachId);
            ViewData["RoomId"] = new SelectList(await _courseService.GetAllRooms(), "Id", "Id", course.RoomId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetById((long)id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CoachId"] = new SelectList(await _courseService.GetAllCoaches(), "Id", "Name", course.CoachId);
            ViewData["RoomId"] = new SelectList(await _courseService.GetAllRooms(), "Id", "Id", course.RoomId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,CoachId,RoomId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.UpdateAndSave(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_courseService.CourseExists(course.Id))
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
            ViewData["CoachId"] = new SelectList(await _courseService.GetAllCoaches(), "Id", "Name", course.CoachId);
            ViewData["RoomId"] = new SelectList(await _courseService.GetAllRooms(), "Id", "Id", course.RoomId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetById((long)id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _courseService.DeleteAndSave(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
