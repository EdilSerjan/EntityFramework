using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;
using Milestone2.Services.Members;
using Milestone2.Services.Rooms;

namespace Milestone2.Services.MembershipCards
{
    public class MembershipCardService
    {
        private readonly IMembershipCardRepository _membershipCardRepo;
        private readonly IMemberRepository _memberRepo;

        public MembershipCardService(IMembershipCardRepository membershipCardRepo, IMemberRepository memberRepo)
        {
            _membershipCardRepo = membershipCardRepo;
            _memberRepo = memberRepo;

        }

        public async Task<List<MembershipCard>> GetAllMembershipCards()
        {
            return await _membershipCardRepo.GetAll();
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _memberRepo.GetAll();
        }

        public async Task<MembershipCard> GetById(long Id)
        {
            return await _membershipCardRepo.GetByID(Id);
        }

        public async Task AddAndSave(MembershipCard membershipCard)
        {
            _membershipCardRepo.Add(membershipCard);
            await _membershipCardRepo.Save();
        }

        public async Task UpdateAndSave(MembershipCard membershipCard)
        {
            _membershipCardRepo.Update(membershipCard);
            await _membershipCardRepo.Save();
        }

        public async Task DeleteAndSave(long Id)
        {
            _membershipCardRepo.Delete(Id);
            await _membershipCardRepo.Save();
        }

        public bool MembershipCardExists(long id)
        {
            return _membershipCardRepo.MembershipCardExists(id);
        }

       
    }
}
