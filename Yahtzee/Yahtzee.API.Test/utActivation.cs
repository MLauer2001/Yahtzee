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
    public class utActivation
    {
        public HttpClient client { get; }

        public utActivation()
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
            response = client.GetAsync("Activation").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Activation> activations = items.ToObject<List<Activation>>();

            Assert.IsTrue(activations.Count > 0);
        }

        [TestMethod]
        public void PostTest()
        {
            // Get LobbyId
            HttpResponseMessage responseL;
            string resultL;
            dynamic itemsL;

            responseL = client.GetAsync("Lobby").Result;
            resultL = responseL.Content.ReadAsStringAsync().Result;
            itemsL = (JArray)JsonConvert.DeserializeObject(resultL);
            List<Lobby> lobbies = itemsL.ToObject<List<Lobby>>();
            Lobby lobby = lobbies[0];


            Activation activation = new Activation 
            {
                Id = Guid.NewGuid(),
                LobbyId = lobby.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                KeyCode = "sky1234567"
            };

            bool rollback = true;

            string serialziedResponse = JsonConvert.SerializeObject(activation);
            var content = new StringContent(serialziedResponse);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Activation/" + rollback, content).Result;
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

            responseA = client.GetAsync("Activation").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Activation> activations = itemsA.ToObject<List<Activation>>();
            Activation guid = activations[0];

            // Get LobbyId
            HttpResponseMessage responseL;
            string resultL;
            dynamic itemsL;

            responseL = client.GetAsync("Lobby").Result;
            resultL = responseL.Content.ReadAsStringAsync().Result;
            itemsL = (JArray)JsonConvert.DeserializeObject(resultL);
            List<Lobby> lobbies = itemsL.ToObject<List<Lobby>>();
            Lobby lobby = lobbies[0];

            Activation activation = new Activation 
            {
                Id = guid.Id,
                LobbyId = lobby.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                KeyCode = "sky1234567"
            };

            bool rollback = true;

            string serialziedQuestion = JsonConvert.SerializeObject(activation);
            var content = new StringContent(serialziedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("Activation/" + guid.Id + "/" + rollback, content).Result;
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

            responseA = client.GetAsync("Activation").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Activation> activations = itemsA.ToObject<List<Activation>>();
            Activation guid = activations[0];

            bool rollback = true;
            HttpResponseMessage response = client.DeleteAsync("Activation/" + guid.Id + "/" + rollback).Result;

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
