using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.Coaches;
using Moq;
using System.Collections.Generic;

namespace Milestone2.UnitTests
{
    public class CoachServiceTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            var coach1 = new Coach() { Id = 1, Name = "test coach 1", Email = "test1@test.com" };
            var coach2 = new Coach() { Id = 2, Name = "test coach 2", Email = "test2@test.com" };
            var coaches = new List<Coach> { coach1, coach2 };

            var fakeRepositoryMock = new Mock<ICoachRepository>();

            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(coaches);

            var coachService = new CoachService(fakeRepositoryMock.Object);

            var resultCoaches = await coachService.GetAll();

            Assert.Collection(resultCoaches, coach =>
            {
                Assert.Equal("test coach 1", coach.Name);
            },
            coach =>
            {
                Assert.Equal("test coach 2", coach.Name);
            });
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var coach1 = new Coach() { Id = 1, Name = "test coach 1", Email = "test1@test.com" };
            var coach2 = new Coach() { Id = 2, Name = "test coach 2", Email = "test2@test.com" };

            var fakeRepositoryMock = new Mock<ICoachRepository>();

            fakeRepositoryMock.Setup(x => x.GetByID(1)).ReturnsAsync(coach1);

            var coachService = new CoachService(fakeRepositoryMock.Object);

            var result = await coachService.GetById(1);

            Assert.Equal("test coach 1", result.Name);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var coach1 = new Coach() { Id = 1, Name = "test coach 1", Email = "test1@test.com" };
            var coach2 = new Coach() { Id = 2, Name = "test coach 2", Email = "test2@test.com" };
            var coaches = new List<Coach> { coach1, coach2 };

            var coach3 = new Coach() { Id = 3, Name = "test coach 3", Email = "test3@test.com" };

            var fakeRepositoryMock = new Mock<ICoachRepository>();

            fakeRepositoryMock.Setup(x => x.Add(It.IsAny<Coach>())).Callback<Coach>(arg => coaches.Add(arg));

            var coachService = new CoachService(fakeRepositoryMock.Object);

            await coachService.AddAndSave(coach3);

            
            Assert.Equal(3, coaches.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var coach1 = new Coach() { Id = 1, Name = "test coach 1", Email = "test1@test.com" };
            var coach2 = new Coach() { Id = 2, Name = "test coach 2", Email = "test2@test.com" };
            var coaches = new List<Coach> { coach1, coach2 };

            var newCoach2 = new Coach() { Id = 2, Name = "new test coach 2", Email = "test2@test.com" };

            var fakeRepositoryMock = new Mock<ICoachRepository>();

            fakeRepositoryMock.Setup(x => x.Update(It.IsAny<Coach>())).Callback<Coach>(arg => coaches[1]=arg);

            var coachService = new CoachService(fakeRepositoryMock.Object);

            await coachService.UpdateAndSave(newCoach2);

            Assert.Equal("new test coach 2", coaches[1].Name);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var coach1 = new Coach() { Id = 1, Name = "test coach 1", Email = "test1@test.com" };
            var coach2 = new Coach() { Id = 2, Name = "test coach 2", Email = "test2@test.com" };
            var coaches = new List<Coach> { coach1, coach2 };

            var fakeRepositoryMock = new Mock<ICoachRepository>();

            fakeRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Callback<long>(arg => coaches.RemoveAt(1));

            var coachService = new CoachService(fakeRepositoryMock.Object);

            await coachService.DeleteAndSave(coach2.Id);

            Assert.Single(coaches);
            Assert.Equal("test1@test.com", coaches[0].Email);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeRepositoryMock = new Mock<ICoachRepository>();

            fakeRepositoryMock.Setup(x => x.CoachExists(It.IsAny<long>())).Returns(true);

            var coachService = new CoachService(fakeRepositoryMock.Object);

            bool result = coachService.CoachExists(1);

            Assert.True(result);
        }

    }
}
