using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Controllers
{
    public class CartUserController : Controller
    {
        public void Email()
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            this.ViewBag.email = cookie;
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
            double total = 0;
            Product pro = new Product();
            MenuCat();
            Email();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            List<Cart> list = Cart.FindCart(u.id);
            this.ViewBag.cart = list;
            foreach (var item in list)
            {
                pro = Product.FindProByID(item.id_pro);
                double price_new = (pro.price * (100 + pro.sale_rate) / 100 * ((100 - pro.discount) / 100)) * item.quantity;
                total = total + price_new;
            }
            this.ViewBag.total = total;
            return View();
        }
        public IActionResult Buy(int id, int SoLuong, string type = "Normal")
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            Cart.InsertCart(u.id, id, SoLuong);
            return RedirectToAction("index");
        }
        public IActionResult Plus(int idpro)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            Cart.ChangeQuantity(u.id, idpro, 1);
            return RedirectToAction("index");
        }
        public IActionResult Minus(int idpro)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            Cart.ChangeQuantity(u.id, idpro, -1);
            return RedirectToAction("index");
        }
        public IActionResult Remove(int idpro)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            Cart.DeleteCart(u.id, idpro);
            return RedirectToAction("index");
        }
        public IActionResult UpdateAdd(string addr1, string addr2, string city, string zone,string telephone)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            string addr = addr1 + "," + addr2 + "," + city + "," + zone;
            Users.UpdateAdd(u.id, addr, telephone);
            return RedirectToAction("checkout");
        }
        public IActionResult Checkout()
        {
            MenuCat();
            Email();
            double total = 0;
            Product pro = new Product();
            int ship = 15000;
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
            int addr = u.address.IndexOf("TP.Hồ Chí Minh");
            if(addr>0)
            {
                ViewBag.ship = ship;
            }    
            else
            {
                ship = 30000;
                ViewBag.ship = ship;
            }    
            List<Cart> list = Cart.FindCart(u.id);
            this.ViewBag.cart = list;
            foreach (var item in list)
            {
                pro = Product.FindProByID(item.id_pro);
                double price_new = (pro.price * (100 + pro.sale_rate) / 100 * ((100 - pro.discount) / 100)) * item.quantity;
                total = total + price_new;
            }
            this.ViewBag.total = total;
            return View();
        }
        public IActionResult Payment(int ship, int pay, string coupon, string comments)
        {
            int ship1 = 15000;
            double total = 0;
            int id_promo = 1;
            Product pro = new Product();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            int addr = u.address.IndexOf("TP.Hồ Chí Minh");
            if (addr < 0)
            {
                ship1 = 30000;
            }
            if (coupon != null)
            {
                id_promo = Promotion.selectbyname(coupon).id_promotion;
            }
            List<Cart> list = Cart.FindCart(u.id);
            foreach(var item in list)
            {
                pro = Product.FindProByID(item.id_pro);
                double price_new = (pro.price * (100 + pro.sale_rate) / 100 * ((100 - pro.discount) / 100))*item.quantity;
                total = total + price_new;
            }
            Orders_user.Insert(u.id, id_promo, ship1, comments, ship, pay, total);
            int id_ord = Orders_user.SelectNew().id_ord;
            foreach (var item in list)
            {
                Order_user_items.Insert(id_ord, item.id_pro, item.quantity);
            }
            Cart.DeleteAll(u.id);
            return RedirectToAction("index");
        }
    }
}
