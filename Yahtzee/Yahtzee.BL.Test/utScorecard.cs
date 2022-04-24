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
    public class utScorecard
    {
        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                List<Scorecard> scorecards = (List<Scorecard>)await ScorecardManager.Load();
                Assert.IsTrue(scorecards.ToList().Count > 0);
            }).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Scorecard scorecard = new Scorecard();


                int results = await ScorecardManager.Insert(new Scorecard { 
                    Id = Guid.NewGuid(), 
                    Aces = 0,
                    Twos = 0,
                    Threes = 0,
                    Fours = 0,
                    Fives = 0,
                    Sixes = 0,
                    Bonus = 0,
                    ThreeOfKind = 0,
                    FourOfKind = 0,
                    FullHouse = 0,
                    SmStraight = 0,
                    LgStraight = 0,
                    Yahtzee = 0,
                    Chance = 0,
                    GrandTotal = 0
                }, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void UpdateTest()
        {

            var task = ScorecardManager.Load();
            IEnumerable<Scorecard> scorecards = task.Result;
            task.Wait();
            Scorecard scorecard = scorecards.FirstOrDefault(c => c.GrandTotal == 239);
            scorecard.GrandTotal = 100;
            var results = ScorecardManager.Update(scorecard, true);
            Assert.IsTrue(results.Result > 0);

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await ScorecardManager.Load();
                IEnumerable<Scorecard> scorecards = task;
                Scorecard scorecard = scorecards.FirstOrDefault(c => c.GrandTotal == 239);
                int results = await ScorecardManager.Delete(scorecard.Id, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
