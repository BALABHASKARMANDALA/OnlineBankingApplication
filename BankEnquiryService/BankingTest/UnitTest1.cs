using BankEnquiryService;
using BankEnquiryService.Models;
using BankEnquiryService.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BankingTest
{
    public class Tests
    {
        List<BankDetails> bankdetails = new List<BankDetails>();
        IQueryable<BankDetails> bankdata;
        Mock<DbSet<BankDetails>> mockSet;
        Mock<BankDetailDbContext> bankcontextmock;

        [SetUp]
        public void Setup()
        {
            bankdetails = new List<BankDetails>()
            {
                new BankDetails{UserId = 101, AccountNo = 201 , UserName = "abc", Balance = 25000},
                new BankDetails{UserId = 102, AccountNo = 202 , UserName = "xyz", Balance = 35000},
                new BankDetails{UserId = 103, AccountNo = 203 , UserName = "pqr", Balance = 30000},
            };
            bankdata = bankdetails.AsQueryable();
            mockSet = new Mock<DbSet<BankDetails>>();
            mockSet.As<IQueryable<BankDetails>>().Setup(m => m.Provider).Returns(bankdata.Provider);
            mockSet.As<IQueryable<BankDetails>>().Setup(m => m.Expression).Returns(bankdata.Expression);
            mockSet.As<IQueryable<BankDetails>>().Setup(m => m.ElementType).Returns(bankdata.ElementType);
            mockSet.As<IQueryable<BankDetails>>().Setup(m => m.GetEnumerator()).Returns(bankdata.GetEnumerator());
            var p = new DbContextOptions<BankDetailDbContext>();
            bankcontextmock = new Mock<BankDetailDbContext>(p);
            bankcontextmock.Setup(x => x.BankDetail).Returns(mockSet.Object);
        }

        [Test]
        public void GetAllTest()
        {
            var bankrepo = new BankDetailsRepository(bankcontextmock.Object);
            var banklist = bankrepo.GetAll();
            Assert.AreEqual(3, banklist.Count());
        }

        [Test]
        public void GetAccountDetailsTest()
        {
            var bankrepo = new BankDetailsRepository(bankcontextmock.Object);
            var bankobj = bankrepo.GetAccountDetails(101);
            Assert.IsNotNull(bankobj);
        }

        [Test]
        public void GetAccountDetailsFail()
        {
            var bankrepo = new BankDetailsRepository(bankcontextmock.Object);
            var bankobj = bankrepo.GetAccountDetails(105);
            Assert.IsNull(bankobj);
        }
    }
}