using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;
using Milestone2.Services.Rooms;

namespace Milestone2.Services.Equipments
{
    public class EquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepo;
        private readonly IRoomRepository _roomRepo;

        public EquipmentService(IEquipmentRepository equipmentRepo, IRoomRepository roomRepo)
        {
            _equipmentRepo = equipmentRepo;
            _roomRepo = roomRepo;

        }

        public async Task<List<Equipment>> GetAllEquipments()
        {
            return await _equipmentRepo.GetAll();
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomRepo.GetAll();
        }

        public async Task<Equipment> GetById(long Id)
        {
            return await _equipmentRepo.GetByID(Id);
        }

        public async Task AddAndSave(Equipment equipment)
        {
            _equipmentRepo.Add(equipment);
            await _equipmentRepo.Save();
        }

        public async Task UpdateAndSave(Equipment equipment)
        {
            _equipmentRepo.Update(equipment);
            await _equipmentRepo.Save();
        }

        public async Task DeleteAndSave(long Id)
        {
            _equipmentRepo.Delete(Id);
            await _equipmentRepo.Save();
        }

        public bool EquipmentExists(long id)
        {
            return _equipmentRepo.EquipmentExists(id);
        }

       
    }
}
