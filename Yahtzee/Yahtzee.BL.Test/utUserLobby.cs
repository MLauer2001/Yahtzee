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
    public class utUserLobby
    {
        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                List<UserLobby> userLobbies = (List<UserLobby>)await UserLobbyManager.Load();
                Assert.IsTrue(userLobbies.ToList().Count > 0);
            }).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                UserLobby userLobby = new UserLobby();
                var task = await LobbyManager.Load();
                var task2 = await ScorecardManager.Load();
                var task3 = await UserManager.Load();
                IEnumerable<Models.Lobby> lobbies = task;
                IEnumerable<Models.Scorecard> scorecards = task2;
                IEnumerable<Models.User> users = task3;


                int results = await UserLobbyManager.Insert(new UserLobby
                {
                    Id = Guid.NewGuid(),
                    LobbyId = lobbies.LastOrDefault().Id,
                    ScorecardId = scorecards.LastOrDefault().Id,
                    UserId = users.LastOrDefault().Id
                }, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void UpdateTest()
        {
            var task = UserLobbyManager.Load();
            IEnumerable<UserLobby> userLobbies = task.Result;
            task.Wait();
            UserLobby userLobby = userLobbies.FirstOrDefault();
            userLobby.UserId = Guid.NewGuid();
            var results = UserLobbyManager.Update(userLobby, true);
            Assert.IsTrue(results.Result > 0);

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await UserLobbyManager.Load();
                var task2 = ScorecardManager.Load();
                IEnumerable<UserLobby> userLobbies = task;
                IEnumerable<Scorecard> scorecards = task2.Result;
                UserLobby userLobby = userLobbies.FirstOrDefault(c => c.ScorecardId == scorecards.LastOrDefault().Id);
                int results = await UserLobbyManager.Delete(userLobby.Id, true);
                Assert.IsTrue(results > 0);
            });
        }
    }
}
