using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.Courses;
using Moq;
using System.Collections.Generic;
using Milestone2.Services.Members;
using Milestone2.Services.Coaches;
using Milestone2.Services.Rooms;

namespace Milestone2.UnitTests
{
    public class CourseServiceTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            var course1 = new Course() { Id = 1, Name = "test course 1", CoachId = 1, RoomId = 1};
            var course2 = new Course() { Id = 2, Name = "test course 2", CoachId = 2, RoomId = 2};
            var courses = new List<Course> { course1, course2 };

            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeCoachRepositoryMock = new Mock<ICoachRepository>(); var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeCourseRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(courses);

            var courseService = new CourseService(fakeCourseRepositoryMock.Object, fakeCoachRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            var resultCoursees = await courseService.GetAllCourses();

            Assert.Collection(resultCoursees, course =>
            {
                Assert.Equal("test course 1", course.Name);
            },
            course =>
            {
                Assert.Equal("test course 2", course.Name);
            });
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var course1 = new Course() { Id = 1, Name = "test course 1", CoachId = 1, RoomId = 1 };
            var course2 = new Course() { Id = 2, Name = "test course 2", CoachId = 2, RoomId = 2 };

            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeCoachRepositoryMock = new Mock<ICoachRepository>(); var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeCourseRepositoryMock.Setup(x => x.GetByID(1)).ReturnsAsync(course1);

            var courseService = new CourseService(fakeCourseRepositoryMock.Object, fakeCoachRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            var result = await courseService.GetById(1);

            Assert.Equal("test course 1", result.Name);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var course1 = new Course() { Id = 1, Name = "test course 1", CoachId = 1, RoomId = 1 };
            var course2 = new Course() { Id = 2, Name = "test course 2", CoachId = 2, RoomId = 2 };
            var courses = new List<Course> { course1, course2 };

            var course3 = new Course() { Id = 3, Name = "test course 3", CoachId = 3, RoomId = 3 };

            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeCoachRepositoryMock = new Mock<ICoachRepository>(); var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeCourseRepositoryMock.Setup(x => x.Add(It.IsAny<Course>())).Callback<Course>(arg => courses.Add(arg));

            var courseService = new CourseService(fakeCourseRepositoryMock.Object, fakeCoachRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            await courseService.AddAndSave(course3);

            
            Assert.Equal(3, courses.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var course1 = new Course() { Id = 1, Name = "test course 1", CoachId = 1, RoomId = 1 };
            var course2 = new Course() { Id = 2, Name = "test course 2", CoachId = 2, RoomId = 2 };
            var courses = new List<Course> { course1, course2 };

            var newCourse2 = new Course() { Id = 2, Name = "new test course 2", CoachId = 2, RoomId = 2 };

            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeCoachRepositoryMock = new Mock<ICoachRepository>(); var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeCourseRepositoryMock.Setup(x => x.Update(It.IsAny<Course>())).Callback<Course>(arg => courses[1]=arg);

            var courseService = new CourseService(fakeCourseRepositoryMock.Object, fakeCoachRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            await courseService.UpdateAndSave(newCourse2);

            Assert.Equal("new test course 2", courses[1].Name);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var course1 = new Course() { Id = 1, Name = "test course 1", CoachId = 1, RoomId = 1 };
            var course2 = new Course() { Id = 2, Name = "test course 2", CoachId = 2, RoomId = 2 };
            var courses = new List<Course> { course1, course2 };

            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeCoachRepositoryMock = new Mock<ICoachRepository>(); var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeCourseRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Callback<long>(arg => courses.RemoveAt(1));

            var courseService = new CourseService(fakeCourseRepositoryMock.Object, fakeCoachRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            await courseService.DeleteAndSave(course2.Id);

            Assert.Single(courses);
            Assert.Equal("test course 1", courses[0].Name);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeCoachRepositoryMock = new Mock<ICoachRepository>(); var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeCourseRepositoryMock.Setup(x => x.CourseExists(It.IsAny<long>())).Returns(true);

            var courseService = new CourseService(fakeCourseRepositoryMock.Object, fakeCoachRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            bool result = courseService.CourseExists(1);

            Assert.True(result);
        }

    }
}
