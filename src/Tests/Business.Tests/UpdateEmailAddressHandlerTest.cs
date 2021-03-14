using Business.Handlers;
using Business.Requests;
using Contracts.Utility;
using EF.SqlServer.Models;
using EntityFrameworkCore.Testing.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests
{
    [TestFixture]
    public class UpdateEmailAddressHandlerTest
    {
        [TestCase("updatedEmail@address.com", 1, 1)]
        public async Task Should_ReturnOK_When_PassUniqueEmailAddressForUser(string updatedEmailAddress, int emailAddressId, int userId)
        {
            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            emailAddressValidator.ValidateMail("updatedEmail@address.com").Returns(true);

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 1,
                Name = "MailingGroupName1",
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 2,
                Name = "MailingGroupName2",
                SystemUserId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                Value = "email1@address.com",
                MailingGroupId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                Value = "email2@address.com",
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                Value = "email3@address.com",
                MailingGroupId = 2
            });

            databaseContext.SaveChanges();

            var request = new UpdateEmailAddressRequest(emailAddressId, updatedEmailAddress);
            request.SetUserId(userId);

            var result = await new UpdateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            });
        }

        [TestCase("updatedEmail@address.com", 1, 1)]
        [TestCase("updatedEmail@address.com", 100, 1)]
        public async Task Should_ReturnNotFound_When_PassNotExistingEmailAddressForUser(string updatedEmailAddress, int emailAddressId, int userId)
        {
            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            emailAddressValidator.ValidateMail("updatedEmail@address.com").Returns(true);

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 1,
                Name = "MailingGroupName1",
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 2,
                Name = "MailingGroupName2",
                SystemUserId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                Value = "email1@address.com",
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                Value = "email2@address.com",
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                Value = "email3@address.com",
                MailingGroupId = 2
            });

            databaseContext.SaveChanges();

            var request = new UpdateEmailAddressRequest(emailAddressId, updatedEmailAddress);
            request.SetUserId(userId);

            var result = await new UpdateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            });
        }

        [TestCase("updatedEmail@address.com", 1, 1)]
        public async Task Should_ReturnBadRequest_When_PassInvalidEmailAddress(string updatedEmailAddress, int emailAddressId, int userId)
        {
            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            emailAddressValidator.ValidateMail("updatedEmail@address.com").Returns(false);

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.SaveChanges();

            var request = new UpdateEmailAddressRequest(emailAddressId, updatedEmailAddress);
            request.SetUserId(userId);

            var result = await new UpdateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
                Assert.IsTrue(result.Message.Contains("Invalid email address"));
            });
        }

        [TestCase("updatedEmail@address.com", 1, 1)]
        public async Task Should_ReturnConflict_When_PassAlreadyExistingEmailAddress(string updatedEmailAddress, int emailAddressId, int userId)
        {
            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            emailAddressValidator.ValidateMail("updatedEmail@address.com").Returns(true);

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 1,
                Name = "MailingGroupName1",
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 2,
                Name = "MailingGroupName2",
                SystemUserId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                Value = "email1@address.com",
                MailingGroupId = 2
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 2,
                Value = "email2@address.com",
                MailingGroupId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 3,
                Value = "email3@address.com",
                MailingGroupId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 4,
                Value = "updatedEmail@address.com",
                MailingGroupId = 1
            });

            databaseContext.SaveChanges();

            var request = new UpdateEmailAddressRequest(emailAddressId, updatedEmailAddress);
            request.SetUserId(userId);

            var result = await new UpdateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Conflict);
                Assert.IsTrue(result.Message.Contains("Email address already exists"));
            });
        }
    }
}
