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
    public class RetrieveEmailAddressesHandlerTest
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 0)]
        public async Task Should_ReturnOK_When_RetrievingUserEmailAddresses(int userId, int counts)
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
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 3,
                SystemUserId = 3,
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 4,
                SystemUserId = 4,
            });

            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                MailingGroupId = 1,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                MailingGroupId = 2,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                MailingGroupId = 2,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                MailingGroupId = 3,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 5,
                MailingGroupId = 3,
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 6,
                MailingGroupId = 3,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveEmailAddressesRequest(null);
            request.SetUserId(userId);

            var result = await new RetrieveEmailAddressesHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.AreEqual(result.EmailAddresses.Count(), counts);
            });
        }

        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 3)]
        public async Task Should_ReturnOK_When_RetrievingUserEmailAddressesFilterByMailingGroupId(int userId, int mailingGroupId, int counts)
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
                SystemUserId = 1,
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
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 5,
                MailingGroupId = 2,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveEmailAddressesRequest(mailingGroupId);
            request.SetUserId(userId);

            var result = await new RetrieveEmailAddressesHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.AreEqual(result.EmailAddresses.Count(), counts);
            });
        }
    }
}
