using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
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
        public IActionResult Ins(string name, string email, string phone, string add1,string add2, string district, string zone, string password)
        {
            string addr = add1 + ", " + add2 + ", " + district + ", " + zone;
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
