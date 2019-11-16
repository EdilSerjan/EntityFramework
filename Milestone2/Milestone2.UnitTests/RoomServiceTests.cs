using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.Rooms;
using Moq;
using System.Collections.Generic;

namespace Milestone2.UnitTests
{
    public class RoomServiceTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            var room1 = new Room() { Id = 1, Capcity = 10};
            var room2 = new Room() { Id = 2, Capcity = 20 };
            var rooms = new List<Room> { room1, room2 };

            var fakeRepositoryMock = new Mock<IRoomRepository>();

            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(rooms);

            var roomService = new RoomService(fakeRepositoryMock.Object);

            var resultRoomes = await roomService.GetAll();

            Assert.Collection(resultRoomes, room =>
            {
                Assert.Equal(10, room.Capcity);
            },
            room =>
            {
                Assert.Equal(20, room.Capcity);
            });
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var room1 = new Room() { Id = 1, Capcity = 10};
            var room2 = new Room() { Id = 2, Capcity = 20 };

            var fakeRepositoryMock = new Mock<IRoomRepository>();

            fakeRepositoryMock.Setup(x => x.GetByID(1)).ReturnsAsync(room1);

            var roomService = new RoomService(fakeRepositoryMock.Object);

            var result = await roomService.GetById(1);

            Assert.Equal(10, result.Capcity);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var room1 = new Room() { Id = 1, Capcity = 10};
            var room2 = new Room() { Id = 2, Capcity = 20 };
            var rooms = new List<Room> { room1, room2 };

            var room3 = new Room() { Id = 3, Capcity = 30 };

            var fakeRepositoryMock = new Mock<IRoomRepository>();

            fakeRepositoryMock.Setup(x => x.Add(It.IsAny<Room>())).Callback<Room>(arg => rooms.Add(arg));

            var roomService = new RoomService(fakeRepositoryMock.Object);

            await roomService.AddAndSave(room3);

            
            Assert.Equal(3, rooms.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var room1 = new Room() { Id = 1, Capcity = 10};
            var room2 = new Room() { Id = 2, Capcity = 20 };
            var rooms = new List<Room> { room1, room2 };

            var newRoom2 = new Room() { Id = 2, Capcity = 25 };

            var fakeRepositoryMock = new Mock<IRoomRepository>();

            fakeRepositoryMock.Setup(x => x.Update(It.IsAny<Room>())).Callback<Room>(arg => rooms[1]=arg);

            var roomService = new RoomService(fakeRepositoryMock.Object);

            await roomService.UpdateAndSave(newRoom2);

            Assert.Equal(25, rooms[1].Capcity);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var room1 = new Room() { Id = 1, Capcity = 10};
            var room2 = new Room() { Id = 2, Capcity = 20 };
            var rooms = new List<Room> { room1, room2 };

            var fakeRepositoryMock = new Mock<IRoomRepository>();

            fakeRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Callback<long>(arg => rooms.RemoveAt(1));

            var roomService = new RoomService(fakeRepositoryMock.Object);

            await roomService.DeleteAndSave(room2.Id);

            Assert.Single(rooms);
            Assert.Equal(10, rooms[0].Capcity);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeRepositoryMock = new Mock<IRoomRepository>();

            fakeRepositoryMock.Setup(x => x.RoomExists(It.IsAny<long>())).Returns(true);

            var roomService = new RoomService(fakeRepositoryMock.Object);

            bool result = roomService.RoomExists(1);

            Assert.True(result);
        }

    }
}
