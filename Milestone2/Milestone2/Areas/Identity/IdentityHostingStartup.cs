using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Milestone2.Models.Data;

[assembly: HostingStartup(typeof(Milestone2.Areas.Identity.IdentityHostingStartup))]
namespace Milestone2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlite("Filename=myIdentity.db"));

                //  services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //     .AddEntityFrameworkStores<IdentityContext>();
                services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}