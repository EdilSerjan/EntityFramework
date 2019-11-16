using System;
using Milestone2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Milestone2.Services.Equipments
{
    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAll();
        Task<Equipment> GetByID(long Id);
        void Add(Equipment equipment);
        void Delete(long Id);
        void Update(Equipment equipment);
        Task Save();
        bool EquipmentExists(long id);
    }
}