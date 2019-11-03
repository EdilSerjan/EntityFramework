using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone1.Data;
using Milestone1.Models;

namespace Milestone1.Controllers
{
    public class CourseMemberController : Controller
    {
        private readonly FitnessClubContext _context;
        
        string[] days = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

        public CourseMemberController(FitnessClubContext context)
        {
            _context = context;
        }

        // GET: CourseMember
        public async Task<IActionResult> Index()
        {
            var fitnessClubContext = _context.CourseMembers.Include(c => c.course).Include(c => c.member);
            return View(await fitnessClubContext.ToListAsync());
        }

        // GET: CourseMember/Details/5
        public async Task<IActionResult> Details(long? courseId, long? memberId)
        {
            if (courseId == null || memberId == null)
            {
                return NotFound();
            }

            var courseMember = await _context.CourseMembers.FindAsync(courseId, memberId);

            if (courseMember == null)
            {
                return NotFound();
            }

            return View(courseMember);
        }

        // GET: CourseMember/Create
        public IActionResult Create()
        {
            ViewData["courseId"] = new SelectList(_context.Courses, "id", "name");
            ViewData["memberId"] = new SelectList(_context.Members, "id", "name");
            ViewData["day"] = new SelectList(days);
            return View();
        }

        // POST: CourseMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("courseId,memberId,day")] CourseMember courseMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["courseId"] = new SelectList(_context.Courses, "id", "id", courseMember.courseId);
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id", courseMember.memberId);
            return View(courseMember);
        }

        // GET: CourseMember/Edit/5
        public async Task<IActionResult> Edit(long? courseId, long? memberId)
        {
            if (courseId == null || memberId == null)
            {
                return NotFound();
            }

            var courseMember = await _context.CourseMembers.FindAsync(courseId, memberId);
            if (courseMember == null)
            {
                return NotFound();
            }
            ViewData["courseId"] = new SelectList(_context.Courses, "id", "id", courseMember.courseId);
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id", courseMember.memberId);
            ViewData["day"] = new SelectList(days);
            return View(courseMember);
        }

        // POST: CourseMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("courseId,memberId,day")] CourseMember courseMember)
        {

            if (ModelState.IsValid)
            {

                _context.Update(courseMember);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));

            }
            ViewData["courseId"] = new SelectList(_context.Courses, "id", "id", courseMember.courseId);
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id", courseMember.memberId);
            return View(courseMember);
        }

        // GET: CourseMember/Delete/5
        public async Task<IActionResult> Delete(long? courseId, long? memberId)
        {
            if (courseId == null || memberId == null)
            {
                return NotFound();
            }

            var courseMember = await _context.CourseMembers.FindAsync(courseId, memberId);
            if (courseMember == null)
            {
                return NotFound();
            }

            return View(courseMember);
        }

        // POST: CourseMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long courseId, long memberId)
        {
            var courseMember = await _context.CourseMembers.FindAsync(courseId, memberId);
            _context.CourseMembers.Remove(courseMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseMemberExists(long id)
        {
            return _context.CourseMembers.Any(e => e.courseId == id);
        }
    }
}
