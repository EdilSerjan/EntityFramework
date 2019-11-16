using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;

namespace Milestone2.Services.Rooms
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepo;

        public RoomService(IRoomRepository roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public async Task<List<Room>> GetAll()
        {
            return await _roomRepo.GetAll();
        }

        public async Task<Room> GetById(long Id)
        {
            return await _roomRepo.GetByID(Id);
        }

        public async Task AddAndSave(Room room)
        {
            _roomRepo.Add(room);
            await _roomRepo.Save();
        }

        public async Task UpdateAndSave(Room room)
        {
            _roomRepo.Update(room);
            await _roomRepo.Save();
        }

        public async Task DeleteAndSave(long Id)
        {
            _roomRepo.Delete(Id);
            await _roomRepo.Save();
        }

        public bool RoomExists(long id)
        {
            return _roomRepo.RoomExists(id);
        }

    }
}
