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
                Aces = 0,
                Twos = 0,
                Threes = 0,
                Fours = 0,
                Fives = 0,
                Sixes = 0,
                Bonus = 0,
                ThreeofKind = 0,
                FourofKind = 0,
                FullHouse = 0,
                SmStraight = 0,
                LgStraight = 0,
                Yahtzee = 0,
                Chance = 0,
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
                existingScorecard.Aces = 5;
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
