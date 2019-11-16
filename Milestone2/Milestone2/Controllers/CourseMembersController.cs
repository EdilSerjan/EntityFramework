using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;
using Milestone2.Models;
using Milestone2.Services.CourseMembers;

namespace Milestone2.Controllers
{
    public class CourseMembersController : Controller
    {
        private readonly CourseMemberService _courseMemberService;

        string[] Days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public CourseMembersController(CourseMemberService courseMemberService)
        {
            _courseMemberService = courseMemberService;
        }

        // GET: CourseMembers
        public async Task<IActionResult> Index()
        {
            return View(await _courseMemberService.GetAllCourseMembers());
        }

        // GET: CourseMembers/Details/5
        public async Task<IActionResult> Details(long? CourseId, long? MemberId)
        {
            if (CourseId == null || MemberId == null)
            {
                return NotFound();
            }

            var courseMember = await _courseMemberService.GetById((long)CourseId, (long)MemberId);
            if (courseMember == null)
            {
                return NotFound();
            }

            return View(courseMember);
        }

        // GET: CourseMembers/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CourseId"] = new SelectList(await _courseMemberService.GetAllCourses(), "Id", "Name");
            ViewData["MemberId"] = new SelectList(await _courseMemberService.GetAllMembers(), "Id", "Name");
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
                await _courseMemberService.AddAndSave(courseMember);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(await _courseMemberService.GetAllCourses(), "Id", "Name", courseMember.CourseId);
            ViewData["MemberId"] = new SelectList(await _courseMemberService.GetAllMembers(), "Id", "Email", courseMember.MemberId);
            return View(courseMember);
        }

        // GET: CourseMembers/Edit/5
        public async Task<IActionResult> Edit(long? CourseId, long? MemberId)
        {
            if (CourseId == null || MemberId == null)
            {
                return NotFound();
            }

            var courseMember = await _courseMemberService.GetById((long)CourseId, (long)MemberId);
            if (courseMember == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(await _courseMemberService.GetAllCourses(), "Id", "Name", courseMember.CourseId);
            ViewData["MemberId"] = new SelectList(await _courseMemberService.GetAllMembers(), "Id", "Name", courseMember.MemberId);
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
                    await _courseMemberService.UpdateAndSave(courseMember);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_courseMemberService.CourseMemberExists(courseMember.CourseId, courseMember.MemberId))
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
            ViewData["CourseId"] = new SelectList(await _courseMemberService.GetAllCourses(), "Id", "Name", courseMember.CourseId);
            ViewData["MemberId"] = new SelectList(await _courseMemberService.GetAllMembers(), "Id", "Email", courseMember.MemberId);
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

            var courseMember = await _courseMemberService.GetById((long)CourseId, (long)MemberId);

            if (courseMember == null)
            {
                return NotFound();
            }

            return View(courseMember);
        }

        // POST: CourseMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long CourseId, long MemberId)
        {
            await _courseMemberService.DeleteAndSave(CourseId, MemberId);
            return RedirectToAction(nameof(Index));
        }

    }
}
