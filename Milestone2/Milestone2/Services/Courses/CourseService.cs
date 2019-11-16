using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone2.Models;
using Milestone2.Services.Coaches;
using Milestone2.Services.Rooms;

namespace Milestone2.Services.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly ICoachRepository _coachRepo;
        private readonly IRoomRepository _roomRepo;

        public CourseService(ICourseRepository courseRepo, ICoachRepository coachRepo, IRoomRepository roomRepo)
        {
            _courseRepo = courseRepo;
            _coachRepo = coachRepo;
            _roomRepo = roomRepo;

        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepo.GetAll();
        }

        public async Task<List<Coach>> GetAllCoaches()
        {
            return await _coachRepo.GetAll();
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomRepo.GetAll();
        }

        public async Task<Course> GetById(long Id)
        {
            return await _courseRepo.GetByID(Id);
        }

        public async Task AddAndSave(Course course)
        {
            _courseRepo.Add(course);
            await _courseRepo.Save();
        }

        public async Task UpdateAndSave(Course course)
        {
            _courseRepo.Update(course);
            await _courseRepo.Save();
        }

        public async Task DeleteAndSave(long Id)
        {
            _courseRepo.Delete(Id);
            await _courseRepo.Save();
        }

        public bool CourseExists(long id)
        {
            return _courseRepo.CourseExists(id);
        }

       
    }
}
