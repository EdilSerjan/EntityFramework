using Milestone2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milestone2.Data;


namespace Milestone2.Services.Members
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepo;

        public MemberService(IMemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }

        public async Task<List<Member>> GetAll()
        {
            return await _memberRepo.GetAll();
        }

        public async Task<Member> GetById(long Id)
        {
            return await _memberRepo.GetByID(Id);
        }

        public async Task AddAndSave(Member member)
        {
            _memberRepo.Add(member);
            await _memberRepo.Save();
        }

        public async Task UpdateAndSave(Member member)
        {
            _memberRepo.Update(member);
            await _memberRepo.Save();
        }

        public async Task DeleteAndSave(long Id)
        {
            _memberRepo.Delete(Id);
            await _memberRepo.Save();
        }

        public bool MemberExists(long id)
        {
            return _memberRepo.MemberExists(id);
        }

        public bool VerifyEmail(string email)
        {
            return true;
        }
    }
}
