using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.Coaches
{
    public class CoachRepository : ICoachRepository
    {
        private readonly FitnessClubContext context;

        public CoachRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(Coach coach)
        {
            context.Coaches.Add(coach);
        }

        public bool CoachExists(long id)
        {
            return context.Coaches.Any(e => e.Id == id);
        }

        public void Delete(long Id)
        {
            Coach coach = context.Coaches.Find(Id);
            context.Coaches.Remove(coach);
        }

        public Task<List<Coach>> GetAll()
        {
            return context.Coaches.ToListAsync();
        }

        public async Task<Coach> GetByID(long Id)
        {
            return await context.Coaches.FindAsync(Id);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Coach coach)
        {
            context.Entry(coach).State = EntityState.Modified;
        }
    }
}