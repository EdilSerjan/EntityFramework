using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.Equipments
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly FitnessClubContext context;

        public EquipmentRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(Equipment equipment)
        {
            context.Equipments.Add(equipment);
        }

        public bool EquipmentExists(long id)
        {
            return context.Equipments.Any(e => e.Id == id);
        }

        public void Delete(long Id)
        {
            Equipment equipment = context.Equipments.Find(Id);
            context.Equipments.Remove(equipment);
        }

        public Task<List<Equipment>> GetAll()
        {
            return context.Equipments.Include(e => e.Room).ToListAsync();
        }

        public async Task<Equipment> GetByID(long Id)
        {
            return await context.Equipments.Include(e => e.Room).FirstOrDefaultAsync(m => m.Id == Id);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Equipment equipment)
        {
            context.Entry(equipment).State = EntityState.Modified;
        }
    }
}