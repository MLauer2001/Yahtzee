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
    public class utLobby
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
            Assert.IsTrue(dc.tblLobbies.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblLobby newrow = new tblLobby
            {
                Id = Guid.NewGuid(),
                LobbyName = "Test",
                MaxPlayer = 3
            };

            dc.tblLobbies.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Guid id = dc.tblLobbies.FirstOrDefault().Id;
            tblLobby existingLobby = (from r in dc.tblLobbies
                                                where r.Id == id
                                                select r).FirstOrDefault();

            if (existingLobby != null)
            {
                existingLobby.LobbyName = "Test";
                dc.SaveChanges();
            }

            tblLobby updatedLobby = (from r in dc.tblLobbies
                                               where r.Id == id
                                               select r).FirstOrDefault();
            Assert.AreEqual(existingLobby.LobbyName, updatedLobby.LobbyName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblLobby existingLobby = (from r in dc.tblLobbies
                                                where r.LobbyName == "HelloWorld"
                                                select r).FirstOrDefault();

            if (existingLobby != null)
            {
                dc.tblLobbies.Remove(existingLobby);
                int actual = dc.SaveChanges();
                Assert.AreNotEqual(0, actual);
            }
        }
    }
}
