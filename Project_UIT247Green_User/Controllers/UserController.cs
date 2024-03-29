﻿using Microsoft.AspNetCore.Http;
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
            string key = "email";
            var cookie = Request.Cookies[key];
            double total = 0;
            Product pro = new Product();
            if (cookie != null)
            {
                Users u = Users.FindU(cookie);
                if (u != null)
                {
                    List<Cart> list = Cart.FindCart(u.id);
                    List<Item> listitem = new List<Item>();
                    foreach (var item in list)
                    {
                        pro = Product.FindProByID(item.id_pro);
                        Item item1 = new Item(pro, item.quantity);
                        listitem.Add(item1);
                        double price_new = (pro.price * (100 + pro.sale_rate) / 100 * ((100 - pro.discount) / 100)) * item.quantity;
                        total = total + price_new;
                    }
                    this.ViewBag.cart = listitem;
                    this.ViewBag.total = total;
                }
            }
            else
            {
                List<Item> cart = new List<Item>();
                string cartcookie = Request.Cookies["cart"];
                if (cartcookie == null)
                {
                    string value = "";
                    CookieOptions cookie1 = new CookieOptions();
                    cookie1.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("cart", value, cookie1);
                }
                else
                {
                    if (!cartcookie.Equals(""))
                    {
                        string[] arrcart = cartcookie.Split("|");
                        for (int i = 0; i < arrcart.Length; i++)
                        {
                            if (arrcart[i] != "")
                            {
                                string[] arritem = arrcart[i].Split(",");
                                int id_pro = Convert.ToInt32(arritem[0]);
                                int quantity = Convert.ToInt32(arritem[1]);
                                pro = Product.FindProByID(id_pro);
                                Item item = new Item(pro, quantity);
                                cart.Add(item);
                            }
                        }
                    }
                }
                if (cart != null)
                {
                    ViewBag.cart = cart;
                    ViewBag.total = ViewBag.total = cart.Sum(item => item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100)); ;
                }
                else
                {
                    ViewBag.cart = null;
                    ViewBag.total = 0;
                }
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
        public void Email()
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            this.ViewBag.email = cookie;
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
            Email();
            MenuCat();
            DataCart();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            string add = u.address;
            string[] arr = add.Split(',');
            string add1 = arr[0];
            string add2 = arr[1];
            string district = arr[2];
            string city = arr[3];
            this.ViewBag.add1 = add1;
            this.ViewBag.add2 = add2;
            this.ViewBag.district = district;
            this.ViewBag.city = city;
            return View();
        }
        public IActionResult Password()
        {
            Email();
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult wishlist()
        {
            Email();
            MenuCat();
            DataCart();
            return View();
        }
        public IActionResult UpdateAcc(string addr1, string addr2, string city, string zone, string phone, string name)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            string addr = addr1 + "," + addr2 + "," + city + "," + zone;
            Users.UpdateAdd(u.id, name, addr, phone);
            return RedirectToAction("myaccount");
        }
        public IActionResult ChangePass(string newpassword)
        {
            Email();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            int check = Users.ChangePass(u.email, newpassword);
            string change = "";
            if (check > 0)
            {
                change = "Thay đổi mật khẩu thành công";
            }
            else
            {
                change = "Thay đổi mật khẩu thất bại";
            }
            return View("password",change);
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
            string addr = add1 + "," + add2 + "," + district + "," + zone;
            int check = Users.InsertU(name, email, password, addr, phone);
            if (check > 0)
            {
                ViewBag.check = "Đăng kí thành công";
                Users u = Users.selectnew();
                List<Item> cart = new List<Item>();
                List<Item> cart1 = new List<Item>();
                string cartcookie = Request.Cookies["cart"];
                if (cartcookie == null)
                {
                    string value = "";
                    CookieOptions cookie1 = new CookieOptions();
                    cookie1.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("cart", value, cookie1);
                }
                if (!cartcookie.Equals(""))
                {
                    string[] arrcart = cartcookie.Split("|");
                    for (int i = 0; i < arrcart.Length; i++)
                    {
                        string[] arritem = arrcart[i].Split(",");
                        int idpro = Convert.ToInt32(arritem[0]);
                        int quantity = Convert.ToInt32(arritem[1]);
                        Product pro = Product.FindProByID(idpro);
                        if (pro.quantity > 0)
                        {
                            Item item = new Item(pro, quantity);
                            cart.Add(item);
                        }
                        else
                        {
                            Item item = new Item(pro, quantity);
                            cart1.Add(item);
                        }
                    }
                }
                foreach(var item in cart)
                {
                    Cart.InsertCart(u.id, item.Product.id_pro, item.Quantity);
                }
                string cartnull = "";
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Append("cart", cartnull, cookie);
                string body = System.IO.File.ReadAllText(@"wwwroot\email_register.txt");              
                MailUtils.SendGmail("uitgreen247@gmail.com", email, "Đăng kí thành viên UITGreen247", body, "uitgreen247@gmail.com", "LucasPhan94");
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
        public IActionResult AddWishList(int id_pro, string type ="Normal")
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            int check = Wishlist.InsertWishList(u.id, id_pro);
            if (type == "ajax")
            {
                return Json(new
                {
                    check = check
                });
            }
            return RedirectToAction("index", "home");
        }
        public IActionResult DeleteWishList(int id)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            Wishlist.Delete(u.id, id);
            return RedirectToAction("wishlist");
        }
     
    }
}
