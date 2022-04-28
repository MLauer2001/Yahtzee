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
    public class utUser
    {
        public HttpClient client { get; }

        public utUser()
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
            response = client.GetAsync("User").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<User> users = items.ToObject<List<User>>();

            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void PostTest()
        {
            User user = new User 
            {
                Id = Guid.NewGuid(),
                Email = "test@gmail.com",
                FirstName = "Bob",
                LastName = "Smith",
                Username = "Test",
                Password = "Account"
            };

            bool rollback = true;

            string serialziedResponse = JsonConvert.SerializeObject(user);
            var content = new StringContent(serialziedResponse);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("User/" + rollback, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            result = result.Replace("\"", "");

            Guid guid = Guid.Parse(result);
            Assert.IsFalse(guid.Equals(Guid.Empty));
        }

        [TestMethod]
        public void PutTest()
        {
            // Get Id
            HttpResponseMessage responseU;
            string resultU;
            dynamic itemsU;

            responseU = client.GetAsync("User").Result;
            resultU = responseU.Content.ReadAsStringAsync().Result;
            itemsU = (JArray)JsonConvert.DeserializeObject(resultU);
            List<User> users = itemsU.ToObject<List<User>>();
            User guid = users[0];

            User user = new User 
            {
                Id = guid.Id,
                Email = "test@gmail.com",
                FirstName = "Bob",
                LastName = "Smith",
                Username = "Test",
                Password = "Account"
            };

            bool rollback = true;

            string serialziedQuestion = JsonConvert.SerializeObject(user);
            var content = new StringContent(serialziedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("User/" + guid.Id + "/" + rollback, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            result = result.Replace("\"", "");

            Assert.IsTrue(result == "1");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Get Id
            HttpResponseMessage responseU;
            string resultU;
            dynamic itemsU;

            responseU = client.GetAsync("User").Result;
            resultU = responseU.Content.ReadAsStringAsync().Result;
            itemsU = (JArray)JsonConvert.DeserializeObject(resultU);
            List<User> users = itemsU.ToObject<List<User>>();
            User guid = users[0];

            bool rollback = true;
            HttpResponseMessage response = client.DeleteAsync("User/" + guid.Id + "/" + rollback).Result;

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
