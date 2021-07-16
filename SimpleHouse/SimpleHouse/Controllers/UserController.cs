using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleHouse.Context;
using SimpleHouse.Models;

namespace SimpleHouse.Controllers
{
    public class UserController : Controller
    {
        private DataContext dataContext = new DataContext();
        // GET: User
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(dataContext.Users.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = dataContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = dataContext.Users.FirstOrDefault(s => s.UserName == user.UserName);
                if (check == null)
                {
                    // chua co tk nay
                    user.Password = GetMD5(user.Password);
                    dataContext.Users.Add(user);
                    dataContext.Configuration.ValidateOnSaveEnabled = false;
                    dataContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                }
            }
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] tgData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < tgData.Length; i++)
            {
                hashString += tgData[i].ToString("x2");
            }
            return hashString;
        }

        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = dataContext.Users.Where(s => s.UserName.Equals(username) && s.Password.Equals(f_password)).ToList();
                if (data.Count > 0)
                {
                    // login thanh cong
                    var u = data.FirstOrDefault();
                    FormsAuthentication.SetAuthCookie(u.UserName, true);
                    return View("User_Layout");
                }
            }
            return View();
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = dataContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = dataContext.Users.Find(id);
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
