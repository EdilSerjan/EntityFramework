using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;
using Milestone2.Services.Courses;
using Milestone2.Services.Members;

namespace Milestone2.Services.CourseMembers
{
    public class CourseMemberService
    {
        private readonly ICourseMemberRepository _courseMemberRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IMemberRepository _memberRepo;

        public CourseMemberService(ICourseMemberRepository courseMemberRepo, ICourseRepository courseRepo, IMemberRepository memberRepo)
        {
            _courseMemberRepo = courseMemberRepo;
            _courseRepo = courseRepo;
            _memberRepo = memberRepo;

        }

        public async Task<List<CourseMember>> GetAllCourseMembers()
        {
            return await _courseMemberRepo.GetAll();
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepo.GetAll();
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _memberRepo.GetAll();
        }

        public async Task<CourseMember> GetById(long CourseId, long MemberId)
        {
            return await _courseMemberRepo.GetByID(CourseId, MemberId);
        }

        public async Task AddAndSave(CourseMember courseMember)
        {
            _courseMemberRepo.Add(courseMember);
            await _courseMemberRepo.Save();
        }

        public async Task UpdateAndSave(CourseMember courseMember)
        {
            _courseMemberRepo.Update(courseMember);
            await _courseMemberRepo.Save();
        }

        public async Task DeleteAndSave(long CourseId, long MemberId)
        {
            _courseMemberRepo.Delete(CourseId, MemberId);
            await _courseMemberRepo.Save();
        }

        public bool CourseMemberExists(long CourseId, long MemberId)
        {
            return _courseMemberRepo.CourseMemberExists(CourseId, MemberId);
        }

       
    }
}
