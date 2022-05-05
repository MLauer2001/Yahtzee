using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Yahtzee.BL.Models;
using Yahztee.API;

namespace Yahtzee.API.Test
{
    [TestClass]
    public class utScorecard
    {
        public HttpClient client { get; }

        public utScorecard()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();

        }

        [TestMethod]
        public void GetTest()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            // Call the api
            response = client.GetAsync("Scorecard").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Scorecard> scorecards = items.ToObject<List<Scorecard>>();

            Assert.IsTrue(scorecards.Count > 0);
        }

        [TestMethod]
        public void GetIdTest()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("Scorecard").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Scorecard> scorecards = items.ToObject<List<Scorecard>>();
            Scorecard scorecard = scorecards[0];

            Guid guid = scorecard.Id;
            response = client.GetAsync("Scorecard/" + guid).Result;

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void PostTest()
        {
            // Get UserId
            HttpResponseMessage responseL;
            string resultL;
            dynamic itemsL;

            responseL = client.GetAsync("User").Result;
            resultL = responseL.Content.ReadAsStringAsync().Result;
            itemsL = (JArray)JsonConvert.DeserializeObject(resultL);
            List<User> users = itemsL.ToObject<List<User>>();
            User user = users[0];


            Scorecard scorecard = new Scorecard 
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Aces = 0,
                Twos = 0,
                Threes = 0,
                Fours = 0,
                Fives = 0,
                Sixes = 0,
                ThreeOfKind = 0,
                FourOfKind = 0,
                SmStraight = 0,
                LgStraight = 0,
                FullHouse = 0,
                Chance = 0,
                Yahtzee = 0,
                Bonus = 0,
                GrandTotal = 0
            };

            bool rollback = true;

            string serialziedResponse = JsonConvert.SerializeObject(scorecard);
            var content = new StringContent(serialziedResponse);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Scorecard/" + rollback, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            result = result.Replace("\"", "");

            Guid guid = Guid.Parse(result);
            Assert.IsFalse(guid.Equals(Guid.Empty));
        }

        [TestMethod]
        public void PutTest()
        {
            // Get Id
            HttpResponseMessage responseA;
            string resultA;
            dynamic itemsA;

            responseA = client.GetAsync("Scorecard").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Scorecard> scorecards = itemsA.ToObject<List<Scorecard>>();
            Scorecard guid = scorecards[0];

            // Get UserId
            HttpResponseMessage responseL;
            string resultL;
            dynamic itemsL;

            responseL = client.GetAsync("User").Result;
            resultL = responseL.Content.ReadAsStringAsync().Result;
            itemsL = (JArray)JsonConvert.DeserializeObject(resultL);
            List<User> users = itemsL.ToObject<List<User>>();
            User user = users[0];

            Scorecard scorecard = new Scorecard
            {
                Id = guid.Id,
                UserId = user.Id,
                Aces = 0,
                Twos = 0,
                Threes = 0,
                Fours = 0,
                Fives = 0,
                Sixes = 0,
                ThreeOfKind = 0,
                FourOfKind = 0,
                SmStraight = 0,
                LgStraight = 0,
                FullHouse = 0,
                Chance = 0,
                Yahtzee = 0,
                Bonus = 0,
                GrandTotal = 0
            };

            bool rollback = true;

            string serialziedQuestion = JsonConvert.SerializeObject(scorecard);
            var content = new StringContent(serialziedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("Scorecard/" + scorecard.Id + "/" + rollback, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            result = result.Replace("\"", "");

            Assert.IsTrue(result == "1");
        }

        [TestMethod]
        public void PutUserIdTest()
        {
            // Get Id
            HttpResponseMessage responseA;
            string resultA;
            dynamic itemsA;

            responseA = client.GetAsync("Scorecard").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Scorecard> scorecards = itemsA.ToObject<List<Scorecard>>();
            Scorecard guid = scorecards[0];
            Guid userId = guid.UserId;

            //// Get UserId
            //HttpResponseMessage responseL;
            //string resultL;
            //dynamic itemsL;

            //responseL = client.GetAsync("User").Result;
            //resultL = responseL.Content.ReadAsStringAsync().Result;
            //itemsL = (JArray)JsonConvert.DeserializeObject(resultL);
            //List<User> users = itemsL.ToObject<List<User>>();
            //User user = users[0];

            Scorecard scorecard = new Scorecard
            {
                Id = guid.Id,
                UserId = userId,
                Username = guid.Username,
                Aces = 0,
                Twos = 0,
                Threes = 0,
                Fours = 0,
                Fives = 0,
                Sixes = 0,
                ThreeOfKind = 0,
                FourOfKind = 0,
                SmStraight = 0,
                LgStraight = 0,
                FullHouse = 0,
                Chance = 0,
                Yahtzee = 0,
                Bonus = 0,
                GrandTotal = 1
            };

            bool rollback = true;
            bool run = true;

            string serialziedQuestion = JsonConvert.SerializeObject(scorecard);
            var content = new StringContent(serialziedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("Scorecard/" + userId + "/" + rollback + "/" + run, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            result = result.Replace("\"", "");

            Assert.IsTrue(result == "1");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Get Id
            HttpResponseMessage responseA;
            string resultA;
            dynamic itemsA;

            responseA = client.GetAsync("Scorecard").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Scorecard> scorecards = itemsA.ToObject<List<Scorecard>>();
            Scorecard guid = scorecards[0];

            bool rollback = true;
            HttpResponseMessage response = client.DeleteAsync("Scorecard/" + guid.Id + "/" + rollback).Result;

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
