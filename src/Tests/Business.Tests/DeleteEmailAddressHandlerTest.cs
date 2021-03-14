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
    public class DeleteEmailAddressHandlerTest
    {
        [TestCase(1, 1)]
        public async Task Should_ReturnOK_When_DeleteEmailAddress(int emailAddressIdToDelete, int userId)
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

            var request = new DeleteEmailAddressRequest(emailAddressIdToDelete);
            request.SetUserId(userId);

            var result = await new DeleteEmailAddressHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.IsTrue(!databaseContext.EmailAddress.Any(x => x.Id == emailAddressIdToDelete));
            });
        }

        [TestCase(0, 1)]
        public async Task Should_ReturnBadRequest_When_NotPassingEmailAddress(int emailAddressIdToDelete, int userId)
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

            var request = new DeleteEmailAddressRequest(emailAddressIdToDelete);
            request.SetUserId(userId);

            var result = await new DeleteEmailAddressHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
            });
        }

        [TestCase(4, 1)]
        [TestCase(100, 1)]
        public async Task Should_ReturnNotFound_When_PassNotExistsEmailAddressIdForUser(int emailAddressIdToDelete, int userId)
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

            var request = new DeleteEmailAddressRequest(emailAddressIdToDelete);
            request.SetUserId(userId);

            var result = await new DeleteEmailAddressHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            });
        }
    }
}
