using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.BL.Models;

namespace Yahtzee.BL.Test
{
    [TestClass]
    public class utLobby
    {
        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                List<Lobby> lobbies = (List<Lobby>)await LobbyManager.Load();
                Assert.IsTrue(lobbies.ToList().Count > 0);
            }).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Lobby lobby = new Lobby();


                int results = await LobbyManager.Insert(new Lobby { Id = Guid.NewGuid(), LobbyName = "Test", MaxPlayers = 2 }, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void UpdateTest()
        {

            var task = LobbyManager.Load();
            IEnumerable<Lobby> lobbies = task.Result;
            task.Wait();
            Lobby lobby = lobbies.FirstOrDefault(c => c.LobbyName == "TestGame");
            lobby.LobbyName = "Updated Test";
            var results = LobbyManager.Update(lobby, true);
            Assert.IsTrue(results.Result > 0);

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await LobbyManager.Load();
                IEnumerable<Lobby> lobbies = task;
                Lobby lobby = lobbies.FirstOrDefault(c => c.LobbyName == "TestGame");
                int results = await LobbyManager.Delete(lobby.Id, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
