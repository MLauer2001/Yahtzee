using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Yahtzee.PL.Test
{
    [TestClass]
    public class utActivation
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
            Assert.IsTrue(dc.tblActivations.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblActivation newrow = new tblActivation
            {
                Id = Guid.NewGuid(),
                LobbyId = dc.tblLobbies.FirstOrDefault().Id,
                KeyCode = "fire123456",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            dc.tblActivations.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Guid id = dc.tblActivations.FirstOrDefault().Id;
            tblActivation existingActivation = (from r in dc.tblActivations
                                                where r.Id == id
                                                select r).FirstOrDefault();

            if (existingActivation != null)
            {
                existingActivation.EndDate = DateTime.Now;
                dc.SaveChanges();
            }

            tblActivation updatedActivation = (from r in dc.tblActivations
                                               where r.Id == id
                                               select r).FirstOrDefault();
            Assert.AreEqual(existingActivation.EndDate, updatedActivation.EndDate);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblActivation existingActivation = (from r in dc.tblActivations
                                                where r.LobbyId == dc.tblLobbies.FirstOrDefault().Id
                                                select r).FirstOrDefault();

            if (existingActivation != null)
            {
                dc.tblActivations.Remove(existingActivation);
                int actual = dc.SaveChanges();
                Assert.AreNotEqual(0, actual);
            }
        }

        [TestMethod]
        public void GetActivations()
        {
            var results = dc.Set<spGetActivationsResult>().FromSqlRaw("exec spGetActivations").ToList();

            Assert.IsTrue(results.Count > 0);
        }
    }
}
