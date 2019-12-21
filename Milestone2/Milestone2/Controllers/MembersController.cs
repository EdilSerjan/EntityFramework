using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milestone2.Areas.Identity.Pages.Account;
using Milestone2.Models;
using Milestone2.Services.Members;

namespace Milestone2.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly MemberService _memberService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        [TempData]
        public string Count { get; set; }

        public MembersController(MemberService memberService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _memberService = memberService;
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEmail(string email)
        {
            if (_memberService.VerifyEmail(email))
            {
                return Json($"Email {email} is already in use.");
            }

            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyName(string name)
        {
            if (_memberService.VerifyName(name))
            {
                return Json($"Name {name} is already in use.");
            }

            return Json(true);
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("count")))
            {
                HttpContext.Session.SetString("count", "1");
            }

            int count = Int32.Parse(HttpContext.Session.GetString("count"));
            count += 1;
            HttpContext.Session.SetString("count", count.ToString());

            Count = $"You have visited this page {HttpContext.Session.GetString("count")} times";
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            return View(await _memberService.GetAll());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetById((long)id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                await _memberService.AddAndSave(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
                var user = await _userManager.GetUserAsync(User);
                var email = user.Email;
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _memberService.GetById((long)id);
                if (member == null)
                {
                    return NotFound();
                }


                if (member.Email.Equals(email) || User.IsInRole("Admin"))
                    return View(member);
                return NotFound();

        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Email")] Member member)
        {
            Console.WriteLine("1111111111111111111111");
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("222222222222222222");

                try
                {
                    await _memberService.UpdateAndSave(member);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_memberService.MemberExists(member.Id))
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
            Console.WriteLine("3333333333333");
            return RedirectToAction(nameof(Index));
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetById((long)id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _memberService.DeleteAndSave(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
