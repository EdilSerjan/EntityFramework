using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;

namespace Milestone2.Services.Members
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAll();
        Task<Member> GetByID(long Id);
        void Add(Member member);
        void Delete(long Id);
        void Update(Member member);
        Task Save();
        bool MemberExists(long id);
        bool MemberEmailExists(string email);
    }
}
