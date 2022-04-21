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
    public class utActivation
    {
        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                List<Activation> activations = (List<Activation>)await ActivationManager.Load();
                Assert.IsTrue(activations.ToList().Count > 0);
            }).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Activation activation = new Activation();
                var task = await LobbyManager.Load();
                IEnumerable<Models.Lobby> lobbies = task;


                int results = await ActivationManager.Insert(new Activation { 
                    Id = Guid.NewGuid(), 
                    LobbyId = lobbies.LastOrDefault().Id, 
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    KeyCode = "sky1234567"
                }, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void UpdateTest()
        {

            var task = ActivationManager.Load();
            IEnumerable<Activation> activations = task.Result;
            task.Wait();
            Activation activation = activations.FirstOrDefault(c => c.KeyCode == "star123456");
            activation.KeyCode = "sky1234567";
            var results = ActivationManager.Update(activation, true);
            Assert.IsTrue(results.Result > 0);

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await ActivationManager.Load();
                IEnumerable<Activation> activations = task;
                Activation activation = activations.FirstOrDefault(c => c.KeyCode == "star123456");
                int results = await ActivationManager.Delete(activation.Id, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
