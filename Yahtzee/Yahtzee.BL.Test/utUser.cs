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
    public class utUser
    {
        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                List<User> users = (List<User>)await UserManager.Load();
                Assert.IsTrue(users.ToList().Count > 0);
            }).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                User user = new User();


                int results = await UserManager.Insert(new User { Id = Guid.NewGuid(), FirstName = "Test", LastName = "Name", Email = "2@gmail.com", Username = "Testing", Password = "Tested" }, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void UpdateTest()
        {
            var task = UserManager.Load();
            IEnumerable<User> users = task.Result;
            task.Wait();
            User user = users.FirstOrDefault(c => c.Username == "JSmith");
            user.Username = "Updated";
            var results = UserManager.Update(user, true);
            Assert.IsTrue(results.Result > 0);

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await UserManager.Load();
                IEnumerable<User> users = task;
                User user = users.FirstOrDefault(c => c.Username == "JSmith");
                int results = await UserManager.Delete(user.Id, true);
                Assert.IsTrue(results > 0);

            });
        }

        [TestMethod()]
        public void LoginTestSuccess()
        {
            Assert.IsTrue(UserManager.Login(new Models.User("MLauer", "lauerm")));
        }
    }
}
