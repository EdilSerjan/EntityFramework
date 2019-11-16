using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.MembershipCards
{
    public class MembershipCardRepository : IMembershipCardRepository
    {
        private readonly FitnessClubContext context;

        public MembershipCardRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(MembershipCard membershipCard)
        {
            context.MembershipCards.Add(membershipCard);
        }

        public bool MembershipCardExists(long id)
        {
            return context.MembershipCards.Any(e => e.Id == id);
        }

        public void Delete(long Id)
        {
            MembershipCard membershipCard = context.MembershipCards.Find(Id);
            context.MembershipCards.Remove(membershipCard);
        }

        public Task<List<MembershipCard>> GetAll()
        {
            return context.MembershipCards.Include(m => m.Member).ToListAsync();
        }

        public async Task<MembershipCard> GetByID(long Id)
        {
            return await context.MembershipCards.Include(m => m.Member).FirstOrDefaultAsync(m => m.Id == Id);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(MembershipCard membershipCard)
        {
            context.Entry(membershipCard).State = EntityState.Modified;
        }
    }
}