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
    public class CreateEmailAddressesHandlerTest
    {
        [TestCase("email1@address.com", 1)]
        [TestCase("email1@address.com;email2@address.com;email3@address.com", 1)]
        public async Task Should_ReturnCreated_When_PassUniqueEmailAddressForUser(string emailAddresses, int mailingGroupId)
        {
            var emailAddressesList = emailAddresses.Split(";");

            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            emailAddressValidator.ValidateMail("email1@address.com").Returns(true);
            emailAddressValidator.ValidateMail("email2@address.com").Returns(true);
            emailAddressValidator.ValidateMail("email3@address.com").Returns(true);

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

            var request = new CreateEmailAddressRequest(mailingGroupId, emailAddressesList);

            var result = await new CreateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
                Assert.AreEqual(databaseContext.EmailAddress.Where(x => emailAddressesList.Contains(x.Value) && x.MailingGroupId == mailingGroupId).Count(), emailAddressesList.Count());
            });
        }

        [TestCase("email1@address.com", 1)]
        [TestCase("email1@address.com;email2@address.com;email3@address.com", 1)]
        public async Task Should_ReturnBadRequest_When_PassInvalidEmailAddress(string addresses, int mailingGroupId)
        {
            var addressesList = addresses.Split(";");

            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();

            var request = new CreateEmailAddressRequest(mailingGroupId, addressesList);
            var result = await new CreateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
                Assert.IsTrue(result.Message.Contains("Invalid email addresses"));
            });
        }

        [TestCase("email2@address.com", 1)]
        [TestCase("email1@address.com;email2@address.com;email3@address.com", 1)]
        public async Task Should_ReturnConflict_When_PassAlreadyAddedEmailAddress(string addresses, int mailingGroupId)
        {
            var addressesList = addresses.Split(";");

            var cancellationToken = new CancellationToken();

            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();

            emailAddressValidator.ValidateMail("email1@address.com").Returns(true);
            emailAddressValidator.ValidateMail("email2@address.com").Returns(true);
            emailAddressValidator.ValidateMail("email3@address.com").Returns(true);

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 1,
                Name = "MailingGroupName1",
                SystemUserId = 1
            });
            databaseContext.Set<EmailAddress>().Add(new EmailAddress()
            {
                Id = 1,
                Value = "email2@address.com",
                MailingGroupId = 1
                
            });
            databaseContext.SaveChanges();


            var request = new CreateEmailAddressRequest(mailingGroupId, addressesList);
            var result = await new CreateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Conflict);
                Assert.IsTrue(result.Message.Contains("Addresses already added"));
            });
        }

        [TestCase("", 1)]
        [TestCase("email1@address.com;email2@address.com;email3@address.com", 0)]
        public async Task Should_ReturnBadRequest_When_NotPassingRequiredParameters(string addresses, int mailingGroupId)
        {
            var addressesList = addresses.Split(";");

            var cancellationToken = new CancellationToken();
            var emailAddressValidator = Substitute.For<IEmailAddressValidatorUtility>();
            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();

            var request = new CreateEmailAddressRequest(mailingGroupId, addressesList);
            var result = await new CreateEmailAddressHandler(databaseContext, emailAddressValidator).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
                Assert.AreEqual(result.Message, "Required Addresses and MailingGroupId.");
            });
        }
    }
}
