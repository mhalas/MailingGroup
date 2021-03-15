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
    public class RetrieveSingleEmailAddressHandlerTest
    {
        [TestCase(1, 1)]
        [TestCase(4, 2)]
        public async Task Should_ReturnOK_When_RetrievingEmailAddress(int emailAddressId, int userId)
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
                MailingGroupId = 1,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                MailingGroupId = 1,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                MailingGroupId = 2,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                MailingGroupId = 2,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveSingleEmailAddressRequest(emailAddressId);
            request.SetUserId(userId);

            var result = await new RetrieveSingleEmailAddressHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.IsNotNull(result.EmailAddress);
                Assert.AreEqual(result.EmailAddress.Id, emailAddressId);
            });
        }
        [TestCase(1, 2)]
        [TestCase(100, 2)]
        [TestCase(4, 1)]
        [TestCase(200, 1)]
        public async Task Should_ReturnNotFound_When_RetrievingNotExistingEmailAddressForUser(int emailAddressId, int userId)
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
                MailingGroupId = 1,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                MailingGroupId = 1,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                MailingGroupId = 2,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                MailingGroupId = 2,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveSingleEmailAddressRequest(emailAddressId);
            request.SetUserId(userId);

            var result = await new RetrieveSingleEmailAddressHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
                Assert.IsNull(result.EmailAddress);
            });
        }
    }
}
