using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RCW.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Yahtzee.BL.Models;
using Yahtzee.Web.Models;
using Yahtzee.Web.ViewModels;

namespace Yahtzee.Web.Controllers
{
    public class GameController : Controller
    {
        private static HttpClient InitializeClient()
        {
            HttpClient client = new ApiClient();
            return client;
        }

        // GET: LobbyController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();

                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("Lobby").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Lobby> lobbies = items.ToObject<List<Lobby>>();

                return View(nameof(Index), lobbies);
            }
            else
            {
                return RedirectToAction("Login", "Player", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: LobbyController/Details/5
        public ActionResult Join(Guid id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.GetAsync("Lobby/" + id).Result;

                string result = response.Content.ReadAsStringAsync().Result;
                Lobby lobby = JsonConvert.DeserializeObject<Lobby>(result);

                GameVM gameVM = new GameVM();
                string results = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(results);

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
                    GrandTotal = 50
                };

                bool rollback = false;
                string serialziedResponse = JsonConvert.SerializeObject(scorecard);
                var content = new StringContent(serialziedResponse);
                HttpResponseMessage responses = client.PostAsync("Scorecard/" + rollback, content).Result;

                gameVM.Scorecard = scorecard;
                gameVM.User = user;
                gameVM.Lobby = lobby;

                return View("Join", gameVM);
            }
            else
            {
                return RedirectToAction("Login", "Player", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: LobbyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LobbyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lobby lobby)
        {
            try
            {
                lobby.Id = Guid.NewGuid();
                HttpClient client = InitializeClient();
                string serialziedResponse = JsonConvert.SerializeObject(lobby);
                var content = new StringContent(serialziedResponse);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Lobby/" + false, content).Result;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LobbyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LobbyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LobbyController/Delete/5
        public ActionResult Delete(Guid id)
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("Lobby/" + id).Result;

            string result = response.Content.ReadAsStringAsync().Result;
            Lobby lobby = JsonConvert.DeserializeObject<Lobby>(result);
            return View(lobby);
        }

        // POST: LobbyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Lobby/" + id + "/" + false).Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
