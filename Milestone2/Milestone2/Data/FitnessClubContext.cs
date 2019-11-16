using Milestone2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Milestone2.Data
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
            // One to One
            modelBuilder
                .Entity<MembershipCard>()
                .HasOne(m => m.Member)
                .WithOne(m => m.MembershipCard);

            modelBuilder
                .Entity<Course>()
                .HasOne(p => p.Coach)
                .WithOne(c => c.Course);

            // One to Many
            modelBuilder
                .Entity<Course>()
                .HasOne(p => p.Room)
                .WithMany(r => r.Courses);

            modelBuilder
                .Entity<Equipment>()
                .HasOne(e => e.Room)
                .WithMany(r => r.Equipments);

            //Many to Many
            modelBuilder.Entity<CourseMember>().HasKey(sc => new { sc.CourseId, sc.MemberId });

            modelBuilder.Entity<CourseMember>()
                .HasOne(sc => sc.Course)
                .WithMany(p => p.CourseMembers);

            modelBuilder.Entity<CourseMember>()
                .HasOne(sc => sc.Member)
                .WithMany(p => p.CourseMembers);


            modelBuilder.Entity<Coach>().HasData(
                new Coach
                {
                    Id = 1,
                    Name = "Tony",
                    Email = "tony@gmail.com"
                },
                new Coach
                {
                    Id = 2,
                    Name = "Mike",
                    Email = "mike@gmail.com"
                }
                );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    Name = "Yedil",
                    Email = "yedil@gmail.com"
                },
                new Member
                {
                    Id = 2,
                    Name = "Lisa",
                    Email = "lisa@gmail.com"
                }
                );
            modelBuilder.Entity<MembershipCard>().HasData(
                new MembershipCard
                {
                    CreatedAt = DateTime.Now,
                    Id = 1,
                    MemberId = 1

                },
                new MembershipCard
                {
                    CreatedAt = DateTime.Now,
                    Id = 2,
                    MemberId = 2

                }
                ); ;
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Capcity = 20
                },
                new Room
                {
                    Id = 2,
                    Capcity = 30
                }
                );
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Yoga ball",
                    Price = 2000,
                    RoomId = 2
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Dumbell",
                    Price = 5000,
                    RoomId = 1
                });
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Yoga",
                    CoachId = 2,
                    RoomId = 2

                },
                new Course
                {
                    Id = 2,
                    Name = "Upper Body Workout",
                    CoachId = 1,
                    RoomId = 1

                }); ;
            modelBuilder.Entity<CourseMember>().HasData(
                new CourseMember
                {
                    CourseId = 2,
                    MemberId = 1,
                    Day = "Monday"
                },
                new CourseMember
                {
                    CourseId = 1,
                    MemberId = 2,
                    Day = "Tuesday"
                });
        }
    }
}
