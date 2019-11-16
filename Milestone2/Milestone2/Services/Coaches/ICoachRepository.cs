using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;

namespace Milestone2.Services.Coaches
{
    public interface ICoachRepository
    {
        Task<List<Coach>> GetAll();
        Task<Coach> GetByID(long Id);
        void Add(Coach coach);
        void Delete(long Id);
        void Update(Coach coach);
        Task Save();
        bool CoachExists(long id);
    }
}
