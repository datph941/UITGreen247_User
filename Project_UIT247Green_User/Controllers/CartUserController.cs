using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Helpers;
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
        public IActionResult CheckPromotion(string coupon, string type = "Normal")
        {
            Promotion pro = new Promotion();
            pro = Promotion.selectbyname(coupon);
            string nofi = "";
            double discount1 = 0;
            if (pro != null)
            {
                string discount = String.Format("{0:0,0 vnđ}", pro.discount);
                nofi = pro.name_promotion + " khả dụng được giảm " + discount;
                discount1 = pro.discount;
            }
            else
            {
                nofi = "Mã khuyến mãi không khả dụng";
            }
            if (type == "ajax")
            {
                return Json(new
                {
                    nofi = nofi,
                    discount = discount1
                });
            }
            return RedirectToAction("checkout");
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
        public IActionResult UpdateAdd(string addr1, string addr2, string city, string zone,string telephone,string firstname)
        {
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            string addr = addr1 + "," + addr2 + "," + city + "," + zone;
            Users.UpdateAdd(u.id, firstname, addr, telephone);
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
            List<Item> listitem = new List<Item>();
            List<Item> listitemcheckout = new List<Item>();
            foreach (var item in list)
            {
                pro = Product.FindProByID(item.id_pro);
                Item item1 = new Item(pro, item.quantity);
                double price_new = (pro.price * (100 + pro.sale_rate) / 100 * ((100 - pro.discount) / 100)) * item.quantity;
                listitem.Add(item1);
                total = total + price_new;
                if (pro.quantity>0)
                {                 
                    listitemcheckout.Add(item1);                 
                }    
            }    
            this.ViewBag.cart = listitem;
            this.ViewBag.cartcheckout = listitemcheckout;
            this.ViewBag.total = total;
            return View();
        }
        public IActionResult Payment(int pay, string coupon, string comments)
        {
            double ship = 15000;
            double total = 0;
            double discount = 0;
            int id_promo = 1;
            Product pro = new Product();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            int addr = u.address.IndexOf("TP.Hồ Chí Minh");
            if (addr < 0)
            {
                ship = 30000;
            }
            if (coupon != null)
            {
                id_promo = Promotion.selectbyname(coupon).id_promotion;
                discount = Promotion.selectbyname(coupon).discount;
            }
            List<Cart> list = Cart.FindCart(u.id);
            List<Item> listitem = new List<Item>();
            foreach (var item in list)
            {
                pro = Product.FindProByID(item.id_pro);
                if (pro.quantity > 0)
                {
                    Item item1 = new Item(pro, item.quantity);
                    listitem.Add(item1);
                    double price_new = (pro.price * (100 + pro.sale_rate) / 100 * ((100 - pro.discount) / 100)) * item.quantity;
                    total = total + price_new;
                }
            }
            Orders_user.Insert(u.id, id_promo, ship, comments, pay, total-discount);
            int id_ord = Orders_user.SelectNew().id_ord;
            foreach (var item in listitem)
            {
                double price = item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100);
                Order_user_items.Insert(id_ord, item.Product.id_pro, item.Quantity,price);
            }
            Order_status.Insert(id_ord, "Đã đặt hàng", u.address);
            Cart.DeleteAll(u.id,listitem);
            return RedirectToAction("index");
        }
    }
}
