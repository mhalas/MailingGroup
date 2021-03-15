using Business.Handlers;
using Business.Requests;
using EF.SqlServer.Models;
using EntityFrameworkCore.Testing.NSubstitute;
using NUnit.Framework;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests
{
    [TestFixture]
    class RetrieveSingleMailingGroupHandlerTest
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task Should_ReturnOK_When_RetrievingMailingGroup(int mailingGroupId, int userId)
        {
            var cancellationToken = new CancellationToken();

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 1,
                SystemUserId = 1,
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 2,
                SystemUserId = 2,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveSingleMailingGroupRequest(mailingGroupId);
            request.SetUserId(userId);

            var result = await new RetrieveSingleMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.IsNotNull(result.MailingGroup);
                Assert.AreEqual(result.MailingGroup.Id, mailingGroupId);
            });
        }

        [TestCase(4, 1)]
        [TestCase(200, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 2)]
        public async Task Should_ReturnNotFound_When_RetrievingNotExistingMailingGroupForUser(int mailingGroupId, int userId)
        {
            var cancellationToken = new CancellationToken();

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 1,
                SystemUserId = 1,
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 2,
                SystemUserId = 2,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveSingleMailingGroupRequest(mailingGroupId);
            request.SetUserId(userId);

            var result = await new RetrieveSingleMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
                Assert.IsNull(result.MailingGroup);
            });
        }
    }
}
