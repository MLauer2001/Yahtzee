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
    public class utLobby
    {
        public HttpClient client { get; }

        public utLobby()
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
            response = client.GetAsync("Lobby").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Lobby> lobbies = items.ToObject<List<Lobby>>();

            Assert.IsTrue(lobbies.Count > 0);
        }

        [TestMethod]
        public void PostTest()
        {
            Lobby lobby = new Lobby
            {
                Id = Guid.NewGuid(),
                LobbyName = "Test",
                MaxPlayers = 3
            };

            bool rollback = true;

            string serialziedResponse = JsonConvert.SerializeObject(lobby);
            var content = new StringContent(serialziedResponse);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Lobby/" + rollback, content).Result;
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

            responseA = client.GetAsync("Lobby").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Lobby> lobbies = itemsA.ToObject<List<Lobby>>();
            Lobby guid = lobbies[0];

            Lobby lobby = new Lobby 
            {
                Id = guid.Id,
                LobbyName = "Test",
                MaxPlayers = 3
            };

            bool rollback = true;

            string serialziedQuestion = JsonConvert.SerializeObject(lobby);
            var content = new StringContent(serialziedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("Lobby/" + guid.Id + "/" + rollback, content).Result;
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

            responseA = client.GetAsync("Lobby").Result;
            resultA = responseA.Content.ReadAsStringAsync().Result;
            itemsA = (JArray)JsonConvert.DeserializeObject(resultA);
            List<Lobby> lobbies = itemsA.ToObject<List<Lobby>>();
            Lobby guid = lobbies[0];

            bool rollback = true;
            HttpResponseMessage response = client.DeleteAsync("Lobby/" + guid.Id + "/" + rollback).Result;

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
