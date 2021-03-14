using Business.Handlers;
using Business.Requests;
using EF.SqlServer.Models;
using EntityFrameworkCore.Testing.NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests
{
    [TestFixture]
    public class DeleteMailingGroupHandlerTest
    {
        [TestCase(1, 1)]
        public async Task Should_ReturnOK_When_DeleteMailingGroup(int mailingGroupIdToDelete, int userId)
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
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                MailingGroupId = 1

            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                MailingGroupId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                MailingGroupId = 2
            });

            databaseContext.SaveChanges();

            var request = new DeleteMailingGroupRequest(mailingGroupIdToDelete);
            request.SetUserId(userId);

            var result = await new DeleteMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.IsFalse(databaseContext.EmailAddress.Any(x => x.MailingGroupId == mailingGroupIdToDelete));
                Assert.IsFalse(databaseContext.MailingGroups.Any(x => x.Id == mailingGroupIdToDelete));
            });
        }

        [TestCase(0, 1)]
        public async Task Should_ReturnBadRequest_When_PasingNullMailingGroupId(int mailingGroupIdToDelete, int userId)
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
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                MailingGroupId = 1

            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                MailingGroupId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                MailingGroupId = 2
            });

            databaseContext.SaveChanges();

            var request = new DeleteMailingGroupRequest(mailingGroupIdToDelete);
            request.SetUserId(userId);

            var result = await new DeleteMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
            });
        }

        [TestCase(2, 1)]
        [TestCase(100, 1)]
        public async Task Should_ReturnNotFound_When_PassingNotExistsIdForUser(int mailingGroupIdToDelete, int userId)
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
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                MailingGroupId = 1

            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                MailingGroupId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                MailingGroupId = 2
            });

            databaseContext.SaveChanges();

            var request = new DeleteMailingGroupRequest(mailingGroupIdToDelete);
            request.SetUserId(userId);

            var result = await new DeleteMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            });
        }
    }
}
