using System;
using Milestone2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Milestone2.Services.Courses
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAll();
        Task<Course> GetByID(long Id);
        void Add(Course course);
        void Delete(long Id);
        void Update(Course course);
        Task Save();
        bool CourseExists(long id);
    }
}