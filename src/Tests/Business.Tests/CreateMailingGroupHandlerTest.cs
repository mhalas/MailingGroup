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
    public class CreateMailingGroupHandlerTest
    {
        [TestCase("MailingGroupName1", 2)]
        [TestCase("MailingGroupName2", 2)]
        [TestCase("MailingGroupName3", 2)]
        public async Task Should_ReturnCreated_When_PassUniqueGroupNameForUser(string name, int userId)
        {
            var cancellationToken = new CancellationToken();

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
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 3,
                Name = "MailingGroupName3",
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 4,
                Name = "MailingGroupName4",
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 5,
                Name = "MailingGroupName5",
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 6,
                Name = "MailingGroupName6",
                SystemUserId = 2
            });

            databaseContext.SaveChanges();

            var request = new CreateMailingGroupRequest(name);
            request.SetUserId(userId);

            var result = await new CreateMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            });
        }

        [TestCase("MailingGroupName1", 2)]
        [TestCase("MailingGroupName2", 2)]
        [TestCase("MailingGroupName3", 2)]
        public async Task Should_ReturnConflict_When_PassAlreadyAddedGroupNameForUser(string name, int userId)
        {
            var cancellationToken = new CancellationToken();

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
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 3,
                Name = "MailingGroupName3",
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 4,
                Name = "MailingGroupName1",
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 5,
                Name = "MailingGroupName2",
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 6,
                Name = "MailingGroupName3",
                SystemUserId = 2
            });

            databaseContext.SaveChanges();

            var request = new CreateMailingGroupRequest(name);
            request.SetUserId(userId);

            var result = await new CreateMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Conflict);
            });
        }

        [TestCase("")]
        public async Task Should_ReturnBadRequest_When_PassNullName(string name)
        {
            var cancellationToken = new CancellationToken();

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
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 3,
                Name = "MailingGroupName3",
                SystemUserId = 1
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 4,
                Name = "MailingGroupName1",
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 5,
                Name = "MailingGroupName2",
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 6,
                Name = "MailingGroupName3",
                SystemUserId = 2
            });

            databaseContext.SaveChanges();

            var request = new CreateMailingGroupRequest(name);

            var result = await new CreateMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
            });
        }
    }
}
