using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Milestone2.Data;
using Milestone2.Hubs;
using Milestone2.Services.Coaches;
using Milestone2.Services.CourseMembers;
using Milestone2.Services.Courses;
using Milestone2.Services.Equipments;
using Milestone2.Services.Members;
using Milestone2.Services.MembershipCards;
using Milestone2.Services.Rooms;

namespace Milestone2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddSignalR();
            services.AddScoped<MemberService>();
            services.AddScoped<IMemberRepository, MemberRepository>();

            services.AddScoped<CoachService>();
            services.AddScoped<ICoachRepository, CoachRepository>();

            services.AddScoped<RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<EquipmentService>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();

            services.AddScoped<MembershipCardService>();
            services.AddScoped<IMembershipCardRepository, MembershipCardRepository>();

            services.AddScoped<CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddScoped<CourseMemberService>();
            services.AddScoped<ICourseMemberRepository, CourseMemberRepository>();

            services.AddDbContext<FitnessClubContext>(options =>
            {
                options.UseSqlite("Filename=myDB.db");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapHub<myHub>("/myHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
