using FundTransferService;
using FundTransferService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using FundTransferService.Repository;

namespace TransferTest
{
    public class Tests
    {
        List<TransactionDetails> transaction = new List<TransactionDetails>();
        IQueryable<TransactionDetails> transactiondata;
        Mock<DbSet<TransactionDetails>> mockSet;
        Mock<TransactionDetailDbContext> transactioncontextmock;
        [SetUp]
        public void Setup()
        {
            transaction = new List<TransactionDetails>()
            {
                new TransactionDetails{TransactionId = 11, UserId = 10, FromAccount = 101, ToAccount = 102, Amount = 5000},
                 new TransactionDetails{TransactionId = 12, UserId = 11, FromAccount = 150, ToAccount = 100, Amount = 10000}
            };
            transactiondata = transaction.AsQueryable();
            mockSet = new Mock<DbSet<TransactionDetails>>();
            mockSet.As<IQueryable<TransactionDetails>>().Setup(m => m.Provider).Returns(transactiondata.Provider);
            mockSet.As<IQueryable<TransactionDetails>>().Setup(m => m.Expression).Returns(transactiondata.Expression);
            mockSet.As<IQueryable<TransactionDetails>>().Setup(m => m.ElementType).Returns(transactiondata.ElementType);
            mockSet.As<IQueryable<TransactionDetails>>().Setup(m => m.GetEnumerator()).Returns(transactiondata.GetEnumerator());
            var p = new DbContextOptions<TransactionDetailDbContext>();
            transactioncontextmock = new Mock<TransactionDetailDbContext>(p);
            transactioncontextmock.Setup(x => x.TransactionDetail).Returns(mockSet.Object);
        }

        [Test]
        public void GetByIdTest()
        {
            var transactionrepo = new TransactionDetailRepository(transactioncontextmock.Object);
            var transactionlist = transactionrepo.GetById(11);
            Assert.AreEqual(1, transactionlist.Count());
        }

        [Test]
        public void TransferTest()
        {
            var transactionrepo = new TransactionDetailRepository(transactioncontextmock.Object);
            var transactionobj = transactionrepo.Transfer(new TransactionDetails { TransactionId = 3, UserId = 10, FromAccount = 101, ToAccount = 102, Amount = 5000 });
            Assert.IsNotNull(transactionobj);
        }
    }
}