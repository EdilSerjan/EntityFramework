using Milestone1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Milestone1.Data
{
    public class FitnessClubContext : DbContext
    {
        public FitnessClubContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<MembershipCard> MembershipCards { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<CourseMember> CourseMembers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MembershipCard>()
                .HasOne(m => m.member)
                .WithOne(m => m.membershipCard);
             
            modelBuilder
                .Entity<Course>()
                .HasOne(p => p.coach)
                .WithOne(c => c.program);

            modelBuilder
                .Entity<Course>()
                .HasOne(p => p.room)
                .WithMany(r => r.courses);

            modelBuilder
                .Entity<Equipment>()
                .HasOne(e => e.room)
                .WithMany(r => r.equipments);

            modelBuilder.Entity<CourseMember>().HasKey(sc => new { sc.courseId, sc.memberId });

            modelBuilder.Entity<CourseMember>()
                .HasOne(sc => sc.course)
                .WithMany(p => p.CourseMembers);

            modelBuilder.Entity<CourseMember>()
                .HasOne(sc => sc.member)
                .WithMany(p => p.CourseMembers);


            modelBuilder.Entity<Coach>().HasData(
                new Coach
                {
                    id = 1,
                    name = "Tony",
                    tel = "+13625623"
                },
                new Coach
                {
                    id = 2,
                    name = "Mike",
                    tel = "+73473827"
                }
                );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    id = 1,
                    name = "Yedil",
                    telephone = "+77771234567"
                },
                new Member
                {
                    id = 2,
                    name = "Lisa",
                    telephone = "+21233523343"
                }
                );
            modelBuilder.Entity<MembershipCard>().HasData(
                new MembershipCard
                {
                    id = 1,
                    memberId = 1,
                    createdAt = DateTime.Now

                },
                new MembershipCard
                {
                    id = 2,
                    memberId = 2,
                    createdAt = DateTime.Now
                }
                );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    id = 1,
                    capcity = 20
                },
                new Room
                {
                    id = 2,
                    capcity = 30
                }
                );
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    id = 1,
                    name = "Yoga ball",
                    price = 2000,
                    roomId = 2
                },
                new Equipment
                {
                    id = 2,
                    name = "Dumbell",
                    price = 5000,
                    roomId = 1
                });
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    id = 1,
                    name = "Yoga",
                    coachId = 2,
                    roomId = 2
                },
                new Course
                {
                    id = 2,
                    name = "Upper Body Workout",
                    coachId = 1,
                    roomId = 1

                });
            modelBuilder.Entity<CourseMember>().HasData(
                new CourseMember
                {
                    courseId = 2,
                    memberId = 1,
                    day = "Monday"
                },
                new CourseMember
                {
                    courseId = 1,
                    memberId = 2,
                    day = "Thursday"
                });
        }
    }
}
