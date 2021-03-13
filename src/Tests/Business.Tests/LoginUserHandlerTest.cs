using Business.Handlers;
using Business.Requests;
using Contracts.Utility;
using EF.SqlServer.Models;
using EntityFrameworkCore.Testing.NSubstitute;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class LoginUserHandlerTest
    {
        [TestCase("TestUser", "UserPassword")]
        [TestCase("TestUser2", "UserPassword2")]
        [TestCase("TestUser3", "UserPassword3")]
        public async Task Should_ReturnToken_When_PassCorrectParameters(string username, string password)
        {
            var cancellationToken = new CancellationToken();

            var jwtSecurityTokenHandler = Substitute.For<JwtSecurityTokenHandler>();
            var userPasswordUtility = Substitute.For<IUserPasswordUtility>();
            //userPasswordUtility.When(x => x.IsPasswordCorrect(default, default, default)).DoNotCallBase();
            var salt = Encoding.ASCII.GetBytes("UserPasswordTest");
            userPasswordUtility.IsPasswordCorrect(password, salt, password).Returns(true);
            
            var configuration = Substitute.For<IConfiguration>();
            configuration["Jwt:Issuer"] = "TestIssuer";
            configuration["Jwt:Secret"] = "TestSecret";
            

            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();

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

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 4,
                Password = password,
                Username = username,
                Salt = salt
            });

            databaseContext.SaveChanges();

            var entity = new LoginUserRequest(username, password);

            var handler = new LoginUserHandler(databaseContext, userPasswordUtility, configuration, jwtSecurityTokenHandler);
            var result = await handler.Handle(entity, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            });
        }

        [TestCase("TestUser", "UserPassword")]
        [TestCase("TestUser2", "UserPassword2")]
        [TestCase("TestUser3", "UserPassword3")]
        public async Task Should_ReturnNotFound_When_PassUnknownUsername(string username, string password)
        {
            var cancellationToken = new CancellationToken();

            var jwtSecurityTokenHandler = Substitute.For<JwtSecurityTokenHandler>();
            var userPasswordUtility = Substitute.For<IUserPasswordUtility>();

            var configuration = Substitute.For<IConfiguration>();
            configuration["Jwt:Issuer"] = "TestIssuer";
            configuration["Jwt:Secret"] = "TestSecret";


            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();

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

            var entity = new LoginUserRequest(username, password);

            var handler = new LoginUserHandler(databaseContext, userPasswordUtility, configuration, jwtSecurityTokenHandler);
            var result = await handler.Handle(entity, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            });
        }

        [TestCase("TestUser", "UserPassword")]
        [TestCase("TestUser2", "UserPassword2")]
        [TestCase("TestUser3", "UserPassword3")]
        public async Task Should_ReturnUnauthorized_When_PassBadPassword(string username, string password)
        {
            var cancellationToken = new CancellationToken();

            var jwtSecurityTokenHandler = Substitute.For<JwtSecurityTokenHandler>();
            var userPasswordUtility = Substitute.For<IUserPasswordUtility>();
            userPasswordUtility.IsPasswordCorrect(password, Encoding.ASCII.GetBytes("UserPasswordTest"), password).Returns(false);

            var configuration = Substitute.For<IConfiguration>();
            configuration["Jwt:Issuer"] = "TestIssuer";
            configuration["Jwt:Secret"] = "TestSecret";


            var databaseContext = Create.MockedDbContextFor<DatabaseContext>();

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

            databaseContext.Set<SystemUser>().Add(new SystemUser()
            {
                Id = 4,
                Password = password,
                Username = username,
                Salt = Encoding.ASCII.GetBytes("correctPasswordForTest")
            });

            databaseContext.SaveChanges();

            var entity = new LoginUserRequest(username, password);

            var handler = new LoginUserHandler(databaseContext, userPasswordUtility, configuration, jwtSecurityTokenHandler);
            var result = await handler.Handle(entity, cancellationToken);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.Success);
                Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized);
            });
        }
    }
}
