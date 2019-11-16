using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;

namespace Milestone2.Services.Coaches
{
    public class CoachService
    {
        private readonly ICoachRepository _coachRepo;

        public CoachService(ICoachRepository coachRepo)
        {
            _coachRepo = coachRepo;
        }

        public async Task<List<Coach>> GetAll()
        {
            return await _coachRepo.GetAll();
        }

        public async Task<Coach> GetById(long Id)
        {
            return await _coachRepo.GetByID(Id);
        }

        public async Task AddAndSave(Coach coach)
        {
            _coachRepo.Add(coach);
            await _coachRepo.Save();
        }

        public async Task UpdateAndSave(Coach coach)
        {
            _coachRepo.Update(coach);
            await _coachRepo.Save();
        }

        public async Task DeleteAndSave(long Id)
        {
            _coachRepo.Delete(Id);
            await _coachRepo.Save();
        }

        public bool CoachExists(long id)
        {
            return _coachRepo.CoachExists(id);
        }

    }
}
