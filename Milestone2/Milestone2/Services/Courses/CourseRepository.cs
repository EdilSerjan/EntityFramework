using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.Courses
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FitnessClubContext context;

        public CourseRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(Course course)
        {
            context.Courses.Add(course);
        }

        public bool CourseExists(long id)
        {
            return context.Courses.Any(e => e.Id == id);
        }

        public void Delete(long Id)
        {
            Course course = context.Courses.Find(Id);
            context.Courses.Remove(course);
        }

        public Task<List<Course>> GetAll()
        {
            return context.Courses.Include(c => c.Coach).Include(c => c.Room).ToListAsync();
        }

        public async Task<Course> GetByID(long Id)
        {
            return await context.Courses.Include(c => c.Coach).Include(c => c.Room).FirstOrDefaultAsync(m => m.Id == Id);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Course course)
        {
            context.Entry(course).State = EntityState.Modified;
        }
    }
}