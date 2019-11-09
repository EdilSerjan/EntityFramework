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
    public class CourseMembersController : Controller
    {
        private readonly FitnessClubContext _context;

        string[] Days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public CourseMembersController(FitnessClubContext context)
        {
            _context = context;
        }

        // GET: CourseMembers
        public async Task<IActionResult> Index()
        {
            var fitnessClubContext = _context.CourseMembers.Include(c => c.Course).Include(c => c.Member);
            return View(await fitnessClubContext.ToListAsync());
        }

        // GET: CourseMembers/Details/5
        public async Task<IActionResult> Details(long? CourseId, long? MemberId)
        {
            if (CourseId == null || MemberId == null)
            {
                return NotFound();
            }

            var courseMember = await _context.CourseMembers
                .Include(c => c.Course)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.CourseId == CourseId && m.MemberId == MemberId);
            if (courseMember == null)
            {
                return NotFound();
            }

            return View(courseMember);
        }

        // GET: CourseMembers/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name");
            ViewData["Days"] = new SelectList(Days);
            return View();
        }

        // POST: CourseMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,MemberId,Day")] CourseMember courseMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseMember.CourseId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email", courseMember.MemberId);
            return View(courseMember);
        }

        // GET: CourseMembers/Edit/5
        public async Task<IActionResult> Edit(long? CourseId, long? MemberId)
        {
            if (CourseId == null || MemberId == null)
            {
                return NotFound();
            }

            var courseMember = await _context.CourseMembers.FindAsync(CourseId, MemberId);
            if (courseMember == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseMember.CourseId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", courseMember.MemberId);
            ViewData["Days"] = new SelectList(Days);
            return View(courseMember);
        }

        // POST: CourseMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long CourseId, long MemberId, [Bind("CourseId,MemberId,Day")] CourseMember courseMember)
        {
            if (CourseId != courseMember.CourseId || MemberId != courseMember.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseMemberExists(courseMember.CourseId, courseMember.MemberId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseMember.CourseId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email", courseMember.MemberId);
            ViewData["Days"] = new SelectList(Days);
            return View(courseMember);
        }

        // GET: CourseMembers/Delete/5
        public async Task<IActionResult> Delete(long? CourseId, long? MemberId)
        {
            if (CourseId == null || MemberId == null)
            {
                return NotFound();
            }

            var courseMember = await _context.CourseMembers
                .Include(c => c.Course)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.CourseId == CourseId && m.MemberId == MemberId);
            if (courseMember == null)
            {
                return NotFound();
            }

            return View(courseMember);
        }

        // POST: CourseMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? CourseId, long? MemberId)
        {
            var courseMember = await _context.CourseMembers.FindAsync(CourseId, MemberId);
            _context.CourseMembers.Remove(courseMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseMemberExists(long CourseId, long MemberId)
        {
            return _context.CourseMembers.Any(e => e.CourseId == CourseId && e.MemberId == MemberId);
        }
    }
}
