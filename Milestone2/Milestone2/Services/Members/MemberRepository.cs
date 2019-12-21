using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.Members
{
    public class MemberRepository : IMemberRepository
    {
        private readonly FitnessClubContext context;

        public MemberRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(Member member)
        {
            context.Members.Add(member);
        }

        public void Delete(long Id)
        {
            Member member = context.Members.Find(Id);
            context.Members.Remove(member);
        }

        public Task<List<Member>> GetAll()
        {
            return context.Members.ToListAsync();
        }

        public async Task<Member> GetByID(long Id)
        {
            return await context.Members.FindAsync(Id);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Member member)
        {
            context.Entry(member).State = EntityState.Modified;
        }

        public bool MemberExists(long id)
        {
            return context.Members.Any(e => e.Id == id);
        }

        public bool MemberEmailExists(string email)
        {
            if (context.Members.Any(m => m.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        public bool MemberNameExists(string name)
        {
            if (context.Members.Any(m => m.Name.ToLower() == name.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
