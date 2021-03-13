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
    public class UpdateMailingGroupHandlerTest
    {
        [TestCase("UpdatedName", 1, 1)]
        public async Task Should_ReturnOK_When_PassUniqueNameToChangeForUser(string name, int mailingGroupId, int userId)
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
                SystemUserId = 2
            });

            databaseContext.SaveChanges();

            var request = new UpdateMailingGroupRequest(mailingGroupId, name);
            request.SetUserId(userId);

            var result = await new UpdateMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

                Assert.AreEqual(databaseContext.MailingGroups.Count(), 2);
                Assert.AreEqual(databaseContext.MailingGroups.Count(x => x.Name == name), 1);
                Assert.AreEqual(databaseContext.MailingGroups.Count(x => x.Name != name), 1);

                Assert.AreEqual(databaseContext.MailingGroups.Single(x => x.Name == name).Id, mailingGroupId);
            });
        }

        [TestCase("UpdatedName", 1, 1)]
        public async Task Should_ReturnConflict_When_PassAlreadyExistingNameForUser(string name, int mailingGroupId, int userId)
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
                SystemUserId = 2
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 3,
                Name = "UpdatedName",
                SystemUserId = 1
            });

            databaseContext.SaveChanges();

            var request = new UpdateMailingGroupRequest(mailingGroupId, name);
            request.SetUserId(userId);

            var result = await new UpdateMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Conflict);
                Assert.IsTrue(result.Message.Contains("already exists"));
            });
        }

        [TestCase("UpdatedName", 2, 1)]
        public async Task Should_ReturnConflict_When_PassNotExistingIdForUser(string name, int mailingGroupId, int userId)
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
                SystemUserId = 2
            });

            databaseContext.SaveChanges();

            var request = new UpdateMailingGroupRequest(mailingGroupId, name);
            request.SetUserId(userId);

            var result = await new UpdateMailingGroupHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            });
        }
    }
}
