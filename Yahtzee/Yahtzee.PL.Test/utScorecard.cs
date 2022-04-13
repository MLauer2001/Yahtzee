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
    public class utScorecard
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
            Assert.IsTrue(dc.tblScorecards.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblScorecard newrow = new tblScorecard
            {
                Id = Guid.NewGuid(),
                UserId = dc.tblUsers.FirstOrDefault().Id,
                Aces = false,
                Twos = false,
                Threes = false,
                Fours = false,
                Fives = false,
                Sixes = false,
                Bonus = false,
                ThreeofKind = false,
                FourofKind = false,
                FullHouse = false,
                SmStraight = false,
                LgStraight = false,
                Yahtzee = false,
                Chance = false,
                GrandTotal = 0
            };

            dc.tblScorecards.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Guid id = dc.tblScorecards.FirstOrDefault().Id;
            tblScorecard existingScorecard = (from r in dc.tblScorecards
                                                where r.Id == id
                                                select r).FirstOrDefault();

            if (existingScorecard != null)
            {
                existingScorecard.Aces = false;
                dc.SaveChanges();
            }

            tblScorecard updatedScorecard = (from r in dc.tblScorecards
                                               where r.Id == id
                                               select r).FirstOrDefault();
            Assert.AreEqual(existingScorecard.Aces, updatedScorecard.Aces);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblScorecard existingScorecard = (from r in dc.tblScorecards
                                                where r.Id == dc.tblScorecards.FirstOrDefault().Id
                                                select r).FirstOrDefault();

            if (existingScorecard != null)
            {
                dc.tblScorecards.Remove(existingScorecard);
                int actual = dc.SaveChanges();
                Assert.AreNotEqual(0, actual);
            }
        }
    }
}
