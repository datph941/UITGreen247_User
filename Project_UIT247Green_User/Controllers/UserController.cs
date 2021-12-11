using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Helpers;
using Project_UIT247Green_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Controllers
{
    public class UserController : Controller
    {
        public void DataCart()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100));
            }
            else
            {
                ViewBag.cart = null;
                ViewBag.total = 0;
            }
        }
        public void MenuCat()
        {
            List<Category> listCat = new List<Category>();
            List<Category> list = new List<Category>();
            listCat = Category.FindCatFather();
            list = Category.FindCatChild();
            this.ViewBag.list = list;
            this.ViewBag.listCat = listCat;
        }
        public IActionResult Login()
        {
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult Register()
        {
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult MyAccount()
        {
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult Password()
        {
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult Wishlist()
        {
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult CheckLogin(string email, string password)
        {
            Users u = Users.CheckLogin(email, password);
            if(u!=null)
            {
                string key = "email";
                string value = u.email;
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Append(key, value, cookie);
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.check = "Sai tên tài khoản hoặc mật khẩu";
                return View("login");
            }    
           
        }
        public IActionResult Ins(string name, string email, string phone, string add1,string add2, string city, string zone, string password)
        {
            string addr = add1 + ", " + add2 + ", " + city + ", " + zone;
            int check = Users.InsertU(name, email, password, addr, phone);
            if (check > 0)
            {
                ViewBag.check = "Đăng kí thành công";
                return View("login");
            }
            else
            {
                ViewBag.check = "Đăng kí không thành công, email đã tồn tại";
                return View("register");
            }    
        }
        public IActionResult Logout()
        {
            string key = "email";
            string value = "";
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append(key, value, cookie);
            return View("login");
        }
    }
}
