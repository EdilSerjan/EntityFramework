using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.Equipments;
using Moq;
using System.Collections.Generic;
using Milestone2.Services.Rooms;

namespace Milestone2.UnitTests
{
    public class EquipmentServiceTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            var equipment1 = new Equipment() { Id = 1, Name = "test equipment 1", Price = 20000, RoomId = 1};
            var equipment2 = new Equipment() { Id = 2, Name = "test equipment 2", Price = 30000, RoomId = 2 };
            var equipments = new List<Equipment> { equipment1, equipment2 };

            var fakeEquipmentRepositoryMock = new Mock<IEquipmentRepository>();
            var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeEquipmentRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(equipments);

            var equipmentService = new EquipmentService(fakeEquipmentRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            var resultEquipmentes = await equipmentService.GetAllEquipments();

            Assert.Collection(resultEquipmentes, equipment =>
            {
                Assert.Equal(20000, equipment.Price);
            },
            equipment =>
            {
                Assert.Equal(30000, equipment.Price);
            });
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var equipment1 = new Equipment() { Id = 1, Name = "test equipment 1", Price = 20000, RoomId = 1 };
            var equipment2 = new Equipment() { Id = 2, Name = "test equipment 2", Price = 30000, RoomId = 2 };

            var fakeEquipmentRepositoryMock = new Mock<IEquipmentRepository>();
            var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeEquipmentRepositoryMock.Setup(x => x.GetByID(1)).ReturnsAsync(equipment1);

            var equipmentService = new EquipmentService(fakeEquipmentRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            var result = await equipmentService.GetById(1);

            Assert.Equal(20000, result.Price);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var equipment1 = new Equipment() { Id = 1, Name = "test equipment 1", Price = 20000, RoomId = 1 };
            var equipment2 = new Equipment() { Id = 2, Name = "test equipment 2", Price = 30000, RoomId = 2 };
            var equipments = new List<Equipment> { equipment1, equipment2 };

            var equipment3 = new Equipment() { Id = 2, Name = "test equipment 3", Price = 40000, RoomId = 2 };

            var fakeEquipmentRepositoryMock = new Mock<IEquipmentRepository>();
            var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeEquipmentRepositoryMock.Setup(x => x.Add(It.IsAny<Equipment>())).Callback<Equipment>(arg => equipments.Add(arg));

            var equipmentService = new EquipmentService(fakeEquipmentRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            await equipmentService.AddAndSave(equipment3);

            
            Assert.Equal(3, equipments.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var equipment1 = new Equipment() { Id = 1, Name = "test equipment 1", Price = 20000, RoomId = 1 };
            var equipment2 = new Equipment() { Id = 2, Name = "test equipment 2", Price = 30000, RoomId = 2 };
            var equipments = new List<Equipment> { equipment1, equipment2 };

            var newEquipment2 = new Equipment() { Id = 2, Name = "test equipment 2", Price = 40000, RoomId = 2 };

            var fakeEquipmentRepositoryMock = new Mock<IEquipmentRepository>();
            var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeEquipmentRepositoryMock.Setup(x => x.Update(It.IsAny<Equipment>())).Callback<Equipment>(arg => equipments[1]=arg);

            var equipmentService = new EquipmentService(fakeEquipmentRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            await equipmentService.UpdateAndSave(newEquipment2);

            Assert.Equal(40000, equipments[1].Price);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var equipment1 = new Equipment() { Id = 1, Name = "test equipment 1", Price = 20000, RoomId = 1 };
            var equipment2 = new Equipment() { Id = 2, Name = "test equipment 2", Price = 30000, RoomId = 2 };
            var equipments = new List<Equipment> { equipment1, equipment2 };

            var fakeEquipmentRepositoryMock = new Mock<IEquipmentRepository>();
            var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeEquipmentRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Callback<long>(arg => equipments.RemoveAt(1));

            var equipmentService = new EquipmentService(fakeEquipmentRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            await equipmentService.DeleteAndSave(equipment2.Id);

            Assert.Single(equipments);
            Assert.Equal(20000, equipments[0].Price);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeEquipmentRepositoryMock = new Mock<IEquipmentRepository>();
            var fakeRoomRepositoryMock = new Mock<IRoomRepository>();

            fakeEquipmentRepositoryMock.Setup(x => x.EquipmentExists(It.IsAny<long>())).Returns(true);

            var equipmentService = new EquipmentService(fakeEquipmentRepositoryMock.Object, fakeRoomRepositoryMock.Object);

            bool result = equipmentService.EquipmentExists(1);

            Assert.True(result);
        }

    }
}
