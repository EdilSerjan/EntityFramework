using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;
using Milestone2.Models;
using Milestone2.Services.MembershipCards;

namespace Milestone2.Controllers
{
    public class MembershipCardsController : Controller
    {
        private readonly MembershipCardService _membershipCardService;

        public MembershipCardsController(MembershipCardService membershipCardService)
        {
            _membershipCardService = membershipCardService;
        }

        // GET: MembershipCards
        public async Task<IActionResult> Index()
        {
            return View(await _membershipCardService.GetAllMembershipCards());
        }

        // GET: MembershipCards/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _membershipCardService.GetById((long)id);
            if (membershipCard == null)
            {
                return NotFound();
            }

            return View(membershipCard);
        }

        // GET: MembershipCards/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["MemberId"] = new SelectList(await _membershipCardService.GetAllMembers(), "Id", "Name");
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
                await _membershipCardService.AddAndSave(membershipCard);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(await _membershipCardService.GetAllMembers(), "Id", "Email", membershipCard.MemberId);
            return View(membershipCard);
        }

        // GET: MembershipCards/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _membershipCardService.GetById((long)id);
            if (membershipCard == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(await _membershipCardService.GetAllMembers(), "Id", "Name", membershipCard.MemberId);
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
                    await _membershipCardService.UpdateAndSave(membershipCard);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_membershipCardService.MembershipCardExists(membershipCard.Id))
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
            ViewData["MemberId"] = new SelectList(await _membershipCardService.GetAllMembers(), "Id", "Name", membershipCard.MemberId);
            return View(membershipCard);
        }

        // GET: MembershipCards/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCard = await _membershipCardService.GetById((long)id);
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
            await _membershipCardService.DeleteAndSave(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
