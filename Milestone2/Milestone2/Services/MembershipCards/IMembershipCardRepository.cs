using System;
using Milestone2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Milestone2.Services.MembershipCards
{
    public interface IMembershipCardRepository
    {
        Task<List<MembershipCard>> GetAll();
        Task<MembershipCard> GetByID(long Id);
        void Add(MembershipCard membershipCard);
        void Delete(long Id);
        void Update(MembershipCard membershipCard);
        Task Save();
        bool MembershipCardExists(long id);
    }
}