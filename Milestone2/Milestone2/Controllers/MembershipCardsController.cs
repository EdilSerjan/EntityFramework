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
    public class MembershipCardsController : Controller
    {
        private readonly FitnessClubContext _context;

        public MembershipCardsController(FitnessClubContext context)
        {
            _context = context;
        }

        // GET: MembershipCards
        public async Task<IActionResult> Index()
        {
            var fitnessClubContext = _context.MembershipCards.Include(m => m.Member);
            return View(await fitnessClubContext.ToListAsync());
        }

        // GET: MembershipCards/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _context.MembershipCards
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipCard == null)
            {
                return NotFound();
            }

            return View(membershipCard);
        }

        // GET: MembershipCards/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name");
            return View();
        }

        // POST: MembershipCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedAt,MemberId")] MembershipCard membershipCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email", membershipCard.MemberId);
            return View(membershipCard);
        }

        // GET: MembershipCards/Edit/5
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", membershipCard.MemberId);
            return View(membershipCard);
        }

        // POST: MembershipCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CreatedAt,MemberId")] MembershipCard membershipCard)
        {
            if (id != membershipCard.Id)
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
                    if (!MembershipCardExists(membershipCard.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", membershipCard.MemberId);
            return View(membershipCard);
        }

        // GET: MembershipCards/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _context.MembershipCards
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipCard == null)
            {
                return NotFound();
            }

            return View(membershipCard);
        }

        // POST: MembershipCards/Delete/5
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
            return _context.MembershipCards.Any(e => e.Id == id);
        }
    }
}
