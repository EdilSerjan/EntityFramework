using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Milestone2.Data;
using Milestone2.Models;

namespace Milestone2.Services.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly FitnessClubContext context;

        public RoomRepository(FitnessClubContext context)
        {
            this.context = context;
        }

        public void Add(Room room)
        {
            context.Rooms.Add(room);
        }

        public bool RoomExists(long id)
        {
            return context.Rooms.Any(e => e.Id == id);
        }

        public void Delete(long Id)
        {
            Room room = context.Rooms.Find(Id);
            context.Rooms.Remove(room);
        }

        public Task<List<Room>> GetAll()
        {
            return context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByID(long Id)
        {
            return await context.Rooms.FindAsync(Id);
        }

        public Task Save()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Room room)
        {
            context.Entry(room).State = EntityState.Modified;
        }
    }
}