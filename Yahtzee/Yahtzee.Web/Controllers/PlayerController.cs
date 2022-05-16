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
using Yahtzee.BL;
using Yahtzee.BL.Models;
using Yahtzee.Web.Models;
using Yahztee.Web.Extensions;

namespace Yahtzee.Web.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Login(string returnUrl)
        {
            TempData["returnurl"] = returnUrl;
            return View();
        }

        public IActionResult Logout()
        {
            SetUser(null);
            return View(nameof(Login));
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                if (UserManager.Login(user))
                {
                    SetUser(user);
                    if (TempData["returnurl"] != null)
                        return Redirect(TempData["returnurl"].ToString());
                    else
                    {
                        ViewBag.Message = "You are now logged in.";
                    }
                }
                return View("~/Views/Home/Index.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);
            if (user != null)
            {
                HttpContext.Session.SetObject("username", "Welcome " + user.Username);
            }
            else
            {
                HttpContext.Session.SetObject("username", "");
            }
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new ApiClient();
            return client;
        }

        public async Task<ActionResult> Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();

                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("Scorecard").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Scorecard> scorecards = items.ToObject<List<Scorecard>>();

                return View(nameof(Index), scorecards);
            }
            else
            {
                return RedirectToAction("Login", "Player", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("Scorecard/" + id).Result;

            string result = response.Content.ReadAsStringAsync().Result;
            Scorecard scorecard = JsonConvert.DeserializeObject<Scorecard>(result);

            return View("Details", scorecard);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                HttpClient client = InitializeClient();
                string serialziedResponse = JsonConvert.SerializeObject(user);
                var content = new StringContent(serialziedResponse);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("User/" + false, content).Result;

                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
