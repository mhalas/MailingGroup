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
    public class RetrieveEmailAddressHandlerTest
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 0)]
        public async Task Should_ReturnOK_When_RetrievingUserMailingGroups(int userId, int counts)
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
                SystemUserId = 2,
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 4,
                SystemUserId = 3,
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 5,
                SystemUserId = 3,
            });
            databaseContext.Set<MailingGroup>().Add(new MailingGroup()
            {
                Id = 6,
                SystemUserId = 3,
            });

            databaseContext.SaveChanges();

            var request = new RetrieveMailingGroupsRequest();
            request.SetUserId(userId);

            var result = await new RetrieveMailingGroupsHandler(databaseContext).Handle(request, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
                Assert.AreEqual(result.MailingGroups.Count(), counts);
            });
        }
    }
}
