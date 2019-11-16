using System;
using Xunit;
using Milestone2.Models;
using System.Threading.Tasks;
using Milestone2.Services.MembershipCards;
using Moq;
using System.Collections.Generic;
using Milestone2.Services.Members;

namespace Milestone2.UnitTests
{
    public class MembershipCardServiceTests
    {
        static string dateString1 = "5/1/2008 8:30:52 AM";
        static DateTime date1 = DateTime.Parse(dateString1,
                          System.Globalization.CultureInfo.InvariantCulture);
        static string dateString2 = "5/2/2008 8:30:52 AM";
        static DateTime date2 = DateTime.Parse(dateString2,
                  System.Globalization.CultureInfo.InvariantCulture);
        static string dateString3 = "5/3/2008 8:30:52 AM";
        static DateTime date3 = DateTime.Parse(dateString3,
                  System.Globalization.CultureInfo.InvariantCulture);
        [Fact]
        public async Task GetAllTest()
        {
            var membershipCard1 = new MembershipCard() { Id = 1, CreatedAt = date1, MemberId = 1};
            var membershipCard2 = new MembershipCard() { Id = 2, CreatedAt = date2, MemberId = 2 };
            var membershipCards = new List<MembershipCard> { membershipCard1, membershipCard2 };

            var fakeMembershipCardRepositoryMock = new Mock<IMembershipCardRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeMembershipCardRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(membershipCards);

            var membershipCardService = new MembershipCardService(fakeMembershipCardRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            var resultMembershipCardes = await membershipCardService.GetAllMembershipCards();

            Assert.Collection(resultMembershipCardes, membershipCard =>
            {
                Assert.Equal(date1, membershipCard.CreatedAt);
            },
            membershipCard =>
            {
                Assert.Equal(date2, membershipCard.CreatedAt);
            });
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var membershipCard1 = new MembershipCard() { Id = 1, CreatedAt = date1, MemberId = 1 };
            var membershipCard2 = new MembershipCard() { Id = 2, CreatedAt = date2, MemberId = 2 };

            var fakeMembershipCardRepositoryMock = new Mock<IMembershipCardRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeMembershipCardRepositoryMock.Setup(x => x.GetByID(1)).ReturnsAsync(membershipCard1);

            var membershipCardService = new MembershipCardService(fakeMembershipCardRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            var result = await membershipCardService.GetById(1);

            Assert.Equal(date1, result.CreatedAt);
        }

        [Fact]
        public async Task AddAndSaveTest()
        {
            var membershipCard1 = new MembershipCard() { Id = 1, CreatedAt = date1, MemberId = 1 };
            var membershipCard2 = new MembershipCard() { Id = 2, CreatedAt = date2, MemberId = 2 };
            var membershipCards = new List<MembershipCard> { membershipCard1, membershipCard2 };

            var membershipCard3 = new MembershipCard() { Id = 2, CreatedAt = date3, MemberId = 2 };

            var fakeMembershipCardRepositoryMock = new Mock<IMembershipCardRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeMembershipCardRepositoryMock.Setup(x => x.Add(It.IsAny<MembershipCard>())).Callback<MembershipCard>(arg => membershipCards.Add(arg));

            var membershipCardService = new MembershipCardService(fakeMembershipCardRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            await membershipCardService.AddAndSave(membershipCard3);

            
            Assert.Equal(3, membershipCards.Count);
        }

        [Fact]
        public async Task UpdateAndSaveTest()
        {
            var membershipCard1 = new MembershipCard() { Id = 1, CreatedAt = date1, MemberId = 1 };
            var membershipCard2 = new MembershipCard() { Id = 2, CreatedAt = date2, MemberId = 2 };
            var membershipCards = new List<MembershipCard> { membershipCard1, membershipCard2 };

            var newMembershipCard2 = new MembershipCard() { Id = 2, CreatedAt = date3, MemberId = 2 };

            var fakeMembershipCardRepositoryMock = new Mock<IMembershipCardRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeMembershipCardRepositoryMock.Setup(x => x.Update(It.IsAny<MembershipCard>())).Callback<MembershipCard>(arg => membershipCards[1]=arg);

            var membershipCardService = new MembershipCardService(fakeMembershipCardRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            await membershipCardService.UpdateAndSave(newMembershipCard2);

            Assert.Equal(date3, membershipCards[1].CreatedAt);
        }

        [Fact]
        public async Task DeleteAndSaveTest()
        {
            var membershipCard1 = new MembershipCard() { Id = 1, CreatedAt = date1, MemberId = 1 };
            var membershipCard2 = new MembershipCard() { Id = 2, CreatedAt = date2, MemberId = 2 };
            var membershipCards = new List<MembershipCard> { membershipCard1, membershipCard2 };

            var fakeMembershipCardRepositoryMock = new Mock<IMembershipCardRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeMembershipCardRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Callback<long>(arg => membershipCards.RemoveAt(1));

            var membershipCardService = new MembershipCardService(fakeMembershipCardRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            await membershipCardService.DeleteAndSave(membershipCard2.Id);

            Assert.Single(membershipCards);
            Assert.Equal(date1, membershipCards[0].CreatedAt);
        }

        [Fact]
        public void ExistsTest()
        {
            var fakeMembershipCardRepositoryMock = new Mock<IMembershipCardRepository>();
            var fakeMemberRepositoryMock = new Mock<IMemberRepository>();

            fakeMembershipCardRepositoryMock.Setup(x => x.MembershipCardExists(It.IsAny<long>())).Returns(true);

            var membershipCardService = new MembershipCardService(fakeMembershipCardRepositoryMock.Object, fakeMemberRepositoryMock.Object);

            bool result = membershipCardService.MembershipCardExists(1);

            Assert.True(result);
        }

    }
}
