using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone2.Areas.Identity.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                IdentityUser admin = new IdentityUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.com"
                };

                IdentityResult result = userManager.CreateAsync(admin, "Admin.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("lisa@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Lisa",
                    Email = "lisa@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "User.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("yedil@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Yedil",
                    Email = "yedil@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "User.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("coach@coach.com").Result == null)
            {
                IdentityUser coach = new IdentityUser
                {
                    UserName = "Coach",
                    Email = "coach@coach.com"
                };

                IdentityResult result = userManager.CreateAsync(coach, "Coach.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(coach, "Coach").Wait();
                }
            }
        }
    }
}
