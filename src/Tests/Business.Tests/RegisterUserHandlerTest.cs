using Business.Handlers;
using Business.Requests;
using Contracts.Utility;
using EF.SqlServer.Models;
using EntityFrameworkCore.Testing.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class RegisterUserHandlerTest
    {
        [TestCase("TestUser", "UserPassword")]
        [TestCase("TestUser2", "UserPassword2")]
        [TestCase("TestUser3", "UserPassword3")]
        public async Task Should_ReturnCreated_When_PassUniqueUsername(string username, string password)
        {
            var cancellationToken = new CancellationToken();
            
            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            var userPasswordUtility = Substitute.For<IUserPasswordUtility>();
            var saltGeneratorUtility = Substitute.For<ISaltGeneratorUtility>();

            saltGeneratorUtility.Generate().Returns(Encoding.ASCII.GetBytes("UserPasswordTest"));
            userPasswordUtility.HashPassword(password, Encoding.ASCII.GetBytes("UserPasswordTest")).Returns("HashedPassword");

            databaseContext.Set<SystemUser>().Add(new SystemUser() 
            { 
                Id = 1,
                Password = "Test1",
                Username = "username1",
                Salt = Encoding.ASCII.GetBytes("password1")
            });

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 2,
                Password = "Test2",
                Username = "username2",
                Salt = Encoding.ASCII.GetBytes("password2")
            });

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 3,
                Password = "Test3",
                Username = "username3",
                Salt = Encoding.ASCII.GetBytes("password3")
            });

            databaseContext.SaveChanges();

            var entity = new RegisterUserRequest(username, password);

            var handler = new RegisterUserHandler(databaseContext, userPasswordUtility, saltGeneratorUtility);
            var result = await handler.Handle(entity, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            });
        }

        [TestCase("", "")]
        public async Task Should_ReturnBadRequest_When_PassingParametersAreEmpty(string username, string password)
        {
            var cancellationToken = new CancellationToken();

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            var userPasswordUtility = Substitute.For<IUserPasswordUtility>();
            var saltGeneratorUtility = Substitute.For<ISaltGeneratorUtility>();

            saltGeneratorUtility.Generate().Returns(Encoding.ASCII.GetBytes("UserPasswordTest"));
            userPasswordUtility.HashPassword(password, Encoding.ASCII.GetBytes("UserPasswordTest")).Returns("HashedPassword");

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 1,
                Password = "Test1",
                Username = "username1",
                Salt = Encoding.ASCII.GetBytes("password1")
            });

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 2,
                Password = "Test2",
                Username = "username2",
                Salt = Encoding.ASCII.GetBytes("password2")
            });

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 3,
                Password = "Test3",
                Username = "username3",
                Salt = Encoding.ASCII.GetBytes("password3")
            });

            databaseContext.SaveChanges();

            var entity = new RegisterUserRequest(username, password);

            var handler = new RegisterUserHandler(databaseContext, userPasswordUtility, saltGeneratorUtility);
            var result = await handler.Handle(entity, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
            });
        }

        [TestCase("username1", "test")]
        [TestCase("username2", "test")]
        [TestCase("username3", "test")]
        public async Task Should_ReturnConflict_When_PassExistingUsername(string username, string password)
        {
            var cancellationToken = new CancellationToken();

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();
            var userPasswordUtility = Substitute.For<IUserPasswordUtility>();
            var saltGeneratorUtility = Substitute.For<ISaltGeneratorUtility>();

            saltGeneratorUtility.Generate().Returns(Encoding.ASCII.GetBytes("UserPasswordTest"));
            userPasswordUtility.HashPassword(password, Encoding.ASCII.GetBytes("UserPasswordTest")).Returns("HashedPassword");

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 1,
                Password = "Test1",
                Username = "username1",
                Salt = Encoding.ASCII.GetBytes("password1")
            });

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 2,
                Password = "Test2",
                Username = "username2",
                Salt = Encoding.ASCII.GetBytes("password2")
            });

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 3,
                Password = "Test3",
                Username = "username3",
                Salt = Encoding.ASCII.GetBytes("password3")
            });

            databaseContext.SaveChanges();

            var entity = new RegisterUserRequest(username, password);

            var handler = new RegisterUserHandler(databaseContext, userPasswordUtility, saltGeneratorUtility);
            var result = await handler.Handle(entity, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Conflict);
            });
        }
    }
}
