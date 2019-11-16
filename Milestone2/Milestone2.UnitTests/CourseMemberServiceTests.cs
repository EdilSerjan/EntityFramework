using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.CourseMembers;
using Moq;
using System.Collections.Generic;
using Milestone2.Services.Members;
using Milestone2.Services.Courses;

namespace Milestone2.UnitTests
{
    public class CourseMemberServiceTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            var courseMember1 = new CourseMember() { CourseId = 1, MemberId = 1, Day = "Monday"};
            var courseMember2 = new CourseMember() { CourseId = 2, MemberId = 2, Day = "Tuesday"};
            var courseMembers = new List<CourseMember> { courseMember1, courseMember2 };

            var fakeCourseMemberRepositoryMock = new Mock<ICourseMemberRepository>();
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeCourseMemberRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(courseMembers);

            var courseMemberService = new CourseMemberService(fakeCourseMemberRepositoryMock.Object, fakeCourseRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            var resultCourseMemberes = await courseMemberService.GetAllCourseMembers();

            Assert.Collection(resultCourseMemberes, courseMember =>
            {
                Assert.Equal("Monday", courseMember.Day);
            },
            courseMember =>
            {
                Assert.Equal("Tuesday", courseMember.Day);
            });
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var courseMember1 = new CourseMember() { CourseId = 1, MemberId = 1, Day = "Monday" };
            var courseMember2 = new CourseMember() { CourseId = 2, MemberId = 2, Day = "Tuesday" };

            var fakeCourseMemberRepositoryMock = new Mock<ICourseMemberRepository>();
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>(); var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeCourseMemberRepositoryMock.Setup(x => x.GetByID(1,1)).ReturnsAsync(courseMember1);

            var courseMemberService = new CourseMemberService(fakeCourseMemberRepositoryMock.Object, fakeCourseRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            var result = await courseMemberService.GetById(1,1);

            Assert.Equal("Monday", result.Day);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var courseMember1 = new CourseMember() { CourseId = 1, MemberId = 1, Day = "Monday" };
            var courseMember2 = new CourseMember() { CourseId = 2, MemberId = 2, Day = "Tuesday" };
            var courseMembers = new List<CourseMember> { courseMember1, courseMember2 };

            var courseMember3 = new CourseMember() { CourseId = 3, MemberId = 3, Day = "Wednesday" };

            var fakeCourseMemberRepositoryMock = new Mock<ICourseMemberRepository>();
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>(); var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeCourseMemberRepositoryMock.Setup(x => x.Add(It.IsAny<CourseMember>())).Callback<CourseMember>(arg => courseMembers.Add(arg));

            var courseMemberService = new CourseMemberService(fakeCourseMemberRepositoryMock.Object, fakeCourseRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            await courseMemberService.AddAndSave(courseMember3);

            
            Assert.Equal(3, courseMembers.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var courseMember1 = new CourseMember() { CourseId = 1, MemberId = 1, Day = "Monday" };
            var courseMember2 = new CourseMember() { CourseId = 2, MemberId = 2, Day = "Tuesday" };
            var courseMembers = new List<CourseMember> { courseMember1, courseMember2 };

            var newCourseMember2 = new CourseMember() { CourseId = 2, MemberId = 2, Day = "Wednesday" };

            var fakeCourseMemberRepositoryMock = new Mock<ICourseMemberRepository>();
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>(); var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeCourseMemberRepositoryMock.Setup(x => x.Update(It.IsAny<CourseMember>())).Callback<CourseMember>(arg => courseMembers[1]=arg);

            var courseMemberService = new CourseMemberService(fakeCourseMemberRepositoryMock.Object, fakeCourseRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            await courseMemberService.UpdateAndSave(newCourseMember2);

            Assert.Equal("Wednesday", courseMembers[1].Day);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var courseMember1 = new CourseMember() { CourseId = 1, MemberId = 1, Day = "Monday" };
            var courseMember2 = new CourseMember() { CourseId = 2, MemberId = 2, Day = "Tuesday" };
            var courseMembers = new List<CourseMember> { courseMember1, courseMember2 };

            var fakeCourseMemberRepositoryMock = new Mock<ICourseMemberRepository>();
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>(); var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeCourseMemberRepositoryMock.Setup(x => x.Delete(It.IsAny<long>(), It.IsAny<long>())).Callback( () => courseMembers.RemoveAt(1));

            var courseMemberService = new CourseMemberService(fakeCourseMemberRepositoryMock.Object, fakeCourseRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            await courseMemberService.DeleteAndSave(courseMember2.CourseId, courseMember2.MemberId);

            Assert.Single(courseMembers);
            Assert.Equal("Monday", courseMembers[0].Day);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeCourseMemberRepositoryMock = new Mock<ICourseMemberRepository>();
            var fakeCourseRepositoryMock = new Mock<ICourseRepository>(); var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeCourseMemberRepositoryMock.Setup(x => x.CourseMemberExists(It.IsAny<long>(), It.IsAny<long>())).Returns(true);

            var courseMemberService = new CourseMemberService(fakeCourseMemberRepositoryMock.Object, fakeCourseRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            bool result = courseMemberService.CourseMemberExists(1,1);

            Assert.True(result);
        }

    }
}
