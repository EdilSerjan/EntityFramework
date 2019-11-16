using System;
using Milestone2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Milestone2.Services.Rooms
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAll();
        Task<Room> GetByID(long Id);
        void Add(Room room);
        void Delete(long Id);
        void Update(Room room);
        Task Save();
        bool RoomExists(long id);
    }
}