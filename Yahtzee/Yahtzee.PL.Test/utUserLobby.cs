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
    public class utUserLobby
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
            Assert.IsTrue(dc.tblUserLobbies.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblUserLobby newrow = new tblUserLobby
            {
                Id = Guid.NewGuid(),
                LobbyId = dc.tblLobbies.FirstOrDefault().Id,
                ScorecardId = dc.tblScorecards.FirstOrDefault().Id,
                UserId = dc.tblUsers.FirstOrDefault().Id
            };

            dc.tblUserLobbies.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Guid id = dc.tblUserLobbies.FirstOrDefault().Id;
            tblUserLobby existingUserLobby = (from r in dc.tblUserLobbies
                                      where r.Id == id
                                      select r).FirstOrDefault();

            if (existingUserLobby != null)
            {
                existingUserLobby.LobbyId = Guid.NewGuid();
                dc.SaveChanges();
            }

            tblUserLobby updatedUserLobby = (from r in dc.tblUserLobbies
                                     where r.Id == id
                                     select r).FirstOrDefault();
            Assert.AreEqual(existingUserLobby.LobbyId, updatedUserLobby.LobbyId);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUserLobby existingUserLobby = (from r in dc.tblUserLobbies
                                      where r.Id == dc.tblUserLobbies.FirstOrDefault().Id
                                      select r).FirstOrDefault();

            if (existingUserLobby != null)
            {
                dc.tblUserLobbies.Remove(existingUserLobby);
                int actual = dc.SaveChanges();
                Assert.AreNotEqual(0, actual);
            }
        }
    }
}
