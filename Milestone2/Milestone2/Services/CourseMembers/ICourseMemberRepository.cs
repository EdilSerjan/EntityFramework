using System;
using Milestone2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Milestone2.Services.CourseMembers
{
    public interface ICourseMemberRepository
    {
        Task<List<CourseMember>> GetAll();
        Task<CourseMember> GetByID(long CourseId, long MemberId);
        void Add(CourseMember courseMember);
        void Delete(long CourseId, long MemberId);
        void Update(CourseMember courseMember);
        Task Save();
        bool CourseMemberExists(long CourseId, long MemberId);
    }
}