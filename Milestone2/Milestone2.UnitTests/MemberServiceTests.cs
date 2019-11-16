using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.Members;
using Moq;
using System.Collections.Generic;

namespace Milestone2.UnitTests
{
    public class MemberServiceTests
    {
        [Fact]
        public async Task GetMembersTest()
        {
            var member1 = new Member() { Id = 1, Name = "test member 1", Email = "test1@test.com" };
            var member2 = new Member() { Id = 2, Name = "test member 2", Email = "test2@test.com" };
            var members = new List<Member> { member1, member2 };

            var fakeRepositoryMock = new Mock<IMemberRepository>();

            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(members);

            var memberService = new MemberService(fakeRepositoryMock.Object);

            var resultMembers = await memberService.GetAll();

            Assert.Collection(resultMembers, member =>
            {
                Assert.Equal("test member 1", member.Name);
            },
            member =>
            {
                Assert.Equal("test member 2", member.Name);
            });
        }

        [Fact]
        public async Task GetMemberByIdTest()
        {
            var member1 = new Member() { Id = 1, Name = "test member 1", Email = "test1@test.com" };
            var member2 = new Member() { Id = 1, Name = "test member 1 updated", Email = "test2@test.com" };

            var fakeRepositoryMock = new Mock<IMemberRepository>();

            fakeRepositoryMock.Setup(x => x.GetByID(1)).ReturnsAsync(member1);

            var memberService = new MemberService(fakeRepositoryMock.Object);

            var resultMember = await memberService.GetById(1);

            Assert.Equal("test member 1", resultMember.Name);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var member1 = new Member() { Id = 1, Name = "test member 1", Email = "test1@test.com" };
            var member2 = new Member() { Id = 2, Name = "test member 2", Email = "test2@test.com" };
            var members = new List<Member> { member1, member2 };

            var member3 = new Member() { Id = 3, Name = "test member 3", Email = "test3@test.com" };

            var fakeRepositoryMock = new Mock<IMemberRepository>();

            fakeRepositoryMock.Setup(x => x.Add(It.IsAny<Member>())).Callback<Member>(arg => members.Add(arg));

            var memberService = new MemberService(fakeRepositoryMock.Object);

            await memberService.AddAndSave(member3);

            
            Assert.Equal(3, members.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var member1 = new Member() { Id = 1, Name = "test member 1", Email = "test1@test.com" };
            var member2 = new Member() { Id = 2, Name = "test member 2", Email = "test2@test.com" };
            var members = new List<Member> { member1, member2 };

            var newMember2 = new Member() { Id = 2, Name = "new test member 2", Email = "test2@test.com" };

            var fakeRepositoryMock = new Mock<IMemberRepository>();

            fakeRepositoryMock.Setup(x => x.Update(It.IsAny<Member>())).Callback<Member>(arg => members[1]=arg);

            var memberService = new MemberService(fakeRepositoryMock.Object);

            await memberService.UpdateAndSave(newMember2);

            Assert.Equal("new test member 2", members[1].Name);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var member1 = new Member() { Id = 1, Name = "test member 1", Email = "test1@test.com" };
            var member2 = new Member() { Id = 2, Name = "test member 2", Email = "test2@test.com" };
            var members = new List<Member>{ member1,member2};

            var fakeRepositoryMock = new Mock<IMemberRepository>();

            fakeRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Callback<long>(arg => members.RemoveAt(1));

            var memberService = new MemberService(fakeRepositoryMock.Object);

            await memberService.DeleteAndSave(member2.Id);

            Assert.Single(members);
            Assert.Equal("test1@test.com", members[0].Email);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeRepositoryMock = new Mock<IMemberRepository>();

            fakeRepositoryMock.Setup(x => x.MemberExists(It.IsAny<long>())).Returns(true);

            var memberService = new MemberService(fakeRepositoryMock.Object);

            bool result = memberService.MemberExists(1);

            Assert.True(result);
        }

    }
}
