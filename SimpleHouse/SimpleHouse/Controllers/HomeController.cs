using SimpleHouse.Context;
using SimpleHouse.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleHouse.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            ViewBag.CategoryId = 0;
            var products = db.Products.ToList();
            var categories = db.Categories.ToList();
            dynamic data = new ExpandoObject();
            data.Products = products;
            data.Categories = categories;
            return View("User_Layout",data);
        }
        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddToCart(int? id, int? qty)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                // them vao gio hang
                CartItem item = new CartItem(product, (int)qty);
                // lay gio hang tu Session
                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    Customer customer = new Customer("Nguyễn Văn An", "0987654321", "Số 8 Tôn Thất Thuyết");
                    cart = new Cart();
                    cart.Customer = customer;
                }
                cart.AddToCart(item);
                Session["cart"] = cart;// theem session
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult RemoveItem(int? id)
        {
            try
            {

                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    return HttpNotFound();
                }
                cart.RemoveItem((int)id);
                Session["cart"] = cart;// theem session
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Cart");
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            if (ModelState.IsValid)
            {
                var cart = (Cart)Session["cart"];
                order.GrandTotal = cart.GrandTotal;
                order.CreatedAt = DateTime.Now;
                order.Status = 1;
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cart.CartItems)
                {
                    OrderItem orderItem = new OrderItem() { OrderID = order.Id, ProductID = item.Product.Id, Qty = item.Quantity, Price = item.Product.Price };
                    db.OrderItems.Add(orderItem);
                }
                db.SaveChanges();
                Session["cart"] = null;// xoa gio hang
            }
            return View("CheckOutSuccess",order);
        }

        public ActionResult CheckOutSuccess()
        {
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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Users.Where(s => s.UserName.Equals(username) && s.Password.Equals(f_password)).ToList();
                if (data.Count > 0)
                {
                    // login thanh cong
                    var u = data.FirstOrDefault();
                    FormsAuthentication.SetAuthCookie(u.UserName, true);
                    if (u.UserName.Trim() == "admin")
                    {
                        return RedirectToAction("Index","Product");
                    }
                    else
                    {
                        return View("CheckOut");

                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.UserName == user.UserName);
                if (check == null)
                {
                    // chua co tk nay
                    user.Password = GetMD5(user.Password);
                    db.Users.Add(user);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "UserName already exists";
                }
            }
            return View();
        }

    }
}