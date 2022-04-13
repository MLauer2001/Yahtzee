using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.PL.Test
{
    [TestClass]
    public class utUser
    {
        protected YahtzeeEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new YahtzeeEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.IsTrue(dc.tblUsers.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblUser newrow = new tblUser
            {
                Id = Guid.NewGuid(),
                Email = "a@gmail.com",
                FirstName = "Test",
                LastName = "Account",
                Username = "ATest",
                Password = "taccount"
            };

            dc.tblUsers.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Guid id = dc.tblUsers.FirstOrDefault().Id;
            tblUser existingUser = (from r in dc.tblUsers
                                                where r.Id == id
                                                select r).FirstOrDefault();

            if (existingUser != null)
            {
                existingUser.Username = "Tester";
                dc.SaveChanges();
            }

            tblUser updatedUser = (from r in dc.tblUsers
                                               where r.Id == id
                                               select r).FirstOrDefault();
            Assert.AreEqual(existingUser.Username, updatedUser.Username);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUser existingUser = (from r in dc.tblUsers
                                                where r.Id == dc.tblUsers.FirstOrDefault().Id
                                                select r).FirstOrDefault();

            if (existingUser != null)
            {
                dc.tblUsers.Remove(existingUser);
                int actual = dc.SaveChanges();
                Assert.AreNotEqual(0, actual);
            }
        }
    }
}
