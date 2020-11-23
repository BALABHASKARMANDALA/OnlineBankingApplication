using AuthService;
using AuthService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AuthTest
{
    public class Tests
    {
        List<UserDetails> user = new List<UserDetails>();
        IQueryable<UserDetails> userdata;
        Mock<DbSet<UserDetails>> mockSet;
        Mock<UserDetailsDbContext> usercontextmock;

        [SetUp]
        public void Setup()
        {
            user = new List<UserDetails>()
            {
                new UserDetails{Userid=1,Username="bala",Password="bala@123"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<UserDetails>>();
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<UserDetailsDbContext>();
            usercontextmock = new Mock<UserDetailsDbContext>(p);
            usercontextmock.Setup(x => x.User).Returns(mockSet.Object);

        }

        [Test]
        public void LoginTest()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new AuthController(usercontextmock.Object, config.Object);
            var auth = controller.Login(new UserDetails { Userid = 1, Username = "bala", Password = "bala@123" }) as OkObjectResult;

            Assert.AreEqual(200, auth.StatusCode);
        }

        [Test]
        public void LoginTestFail()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new AuthController(usercontextmock.Object, config.Object);
            var auth = controller.Login(new UserDetails { Userid = 1, Username = "bala", Password = "bala123" }) as OkObjectResult;

            Assert.IsNull(auth);
        }
    }
}