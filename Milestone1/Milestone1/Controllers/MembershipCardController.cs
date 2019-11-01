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
    public class MembershipCardController : Controller
    {
        private readonly FitnessClubContext _context;

        public MembershipCardController(FitnessClubContext context)
        {
            _context = context;
        }

        // GET: MembershipCard
        public async Task<IActionResult> Index()
        {
            var fitnessClubContext = _context.MembershipCards.Include(m => m.member);
            return View(await fitnessClubContext.ToListAsync());
        }

        // GET: MembershipCard/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _context.MembershipCards
                .Include(m => m.member)
                .FirstOrDefaultAsync(m => m.id == id);
            if (membershipCard == null)
            {
                return NotFound();
            }

            return View(membershipCard);
        }

        // GET: MembershipCard/Create
        public IActionResult Create()
        {
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id");
            return View();
        }

        // POST: MembershipCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,createdAt,memberId")] MembershipCard membershipCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id", membershipCard.memberId);
            return View(membershipCard);
        }

        // GET: MembershipCard/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _context.MembershipCards.FindAsync(id);
            if (membershipCard == null)
            {
                return NotFound();
            }
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id", membershipCard.memberId);
            return View(membershipCard);
        }

        // POST: MembershipCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id,createdAt,memberId")] MembershipCard membershipCard)
        {
            if (id != membershipCard.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipCardExists(membershipCard.id))
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
            ViewData["memberId"] = new SelectList(_context.Members, "id", "id", membershipCard.memberId);
            return View(membershipCard);
        }

        // GET: MembershipCard/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _context.MembershipCards
                .Include(m => m.member)
                .FirstOrDefaultAsync(m => m.id == id);
            if (membershipCard == null)
            {
                return NotFound();
            }

            return View(membershipCard);
        }

        // POST: MembershipCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var membershipCard = await _context.MembershipCards.FindAsync(id);
            _context.MembershipCards.Remove(membershipCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipCardExists(long id)
        {
            return _context.MembershipCards.Any(e => e.id == id);
        }
    }
}
