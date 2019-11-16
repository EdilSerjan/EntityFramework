using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.CourseMembers
{
    public class CourseMemberRepository : ICourseMemberRepository
    {
        private readonly FitnessClubContext context;

        public CourseMemberRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(CourseMember courseMember)
        {
            context.CourseMembers.Add(courseMember);
        }

        public bool CourseMemberExists(long CourseId, long MemberId)
        {
            return context.CourseMembers.Any(e => e.CourseId == CourseId && e.MemberId == MemberId);
        }

        public void Delete(long CourseId, long MemberId)
        {
            CourseMember courseMember = context.CourseMembers.Find(CourseId, MemberId);
            context.CourseMembers.Remove(courseMember);
        }

        public Task<List<CourseMember>> GetAll()
        {
            return context.CourseMembers.Include(c => c.Course).Include(c => c.Member).ToListAsync();
        }

        public async Task<CourseMember> GetByID(long CourseId, long MemberId)
        {
            return await context.CourseMembers.Include(c => c.Course).Include(c => c.Member).FirstOrDefaultAsync(e => e.CourseId == CourseId && e.MemberId == MemberId);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(CourseMember courseMember)
        {
            context.Entry(courseMember).State = EntityState.Modified;
        }
    }
}