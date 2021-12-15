using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Controllers
{
    public class BlogController : Controller
    {
        public void Email()
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            this.ViewBag.email = cookie;
        }
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
        public IActionResult Index()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult Blog_detail_1()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult Blog_detail_2()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult Blog_detail_3()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult Blog_detail_4()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult Blog_detail_5()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
    }
}
