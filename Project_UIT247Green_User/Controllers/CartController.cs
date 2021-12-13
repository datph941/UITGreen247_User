using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Helpers;
using Project_UIT247Green_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_UIT247Green_User.Controllers
{
    public class CartController : Controller
    {
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
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            if(cart!=null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100));             
            }    
            else
            {
                ViewBag.cart = null;
                ViewBag.total = 0;
            }
            return View();
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
        public IActionResult Checkout()
        {
            MenuCat();
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            List<Item> list = new List<Item>();
            if (cart != null)
            {
                foreach(var item in cart)
                {
                    if(item.Product.quantity>0)
                    {
                        list.Add(item);
                    }    
                }    
                ViewBag.cart = list;
                ViewBag.total = cart.Sum(item => item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100));
            }
            else
            {
                ViewBag.cart = null;
                ViewBag.total = 0;
            }
            return View();
        }
        public IActionResult Payment(string firstname, string email,string telephone,string addr1, string addr2, string city, string zone,string coupon, string comments, int pay)
        {
            string address = addr1 + ", " + addr2 + ", " + city + ", " + zone;
            int id_pro = 1;
            double ship = 15000;
            Promotion pro1 = new Promotion();
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            List<Item> list = new List<Item>();
            foreach (var item in cart)
            {
                if (item.Product.quantity > 0)
                {
                    list.Add(item);
                }
            }
            double total = cart.Sum(item => item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100));
            if (!zone.Equals("TP.Hồ Chí Minh"))
            {
                ship = 30000;
            }
            if (coupon != null)
            {
                pro1 = Promotion.selectbyname(coupon);
                if(pro1 != null)
                {
                    id_pro = pro1.id_promotion;
                }    
                else
                {
                    id_pro = 1;
                }    
            }
            Customer.Insert(firstname, email, address, telephone);
            Customer cus = Customer.SelectNew();
            
            int check = Orders.Insert(cus.id_cus, id_pro, pay, ship, comments, total);
            int id_ord = Orders.SelectNew().id_ord;
            foreach(var item in list)
            {
                Order_items.Insert(id_ord,item.Product.id_pro, item.Quantity);
                cart.Remove(item);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("index");
        }
        public IActionResult Plus(int idpro)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(idpro);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("index");
        }
        public IActionResult Minus(int idpro)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(idpro);
            if (index != -1 && cart[index].Quantity!=1)
            {
                cart[index].Quantity--;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);        
            return RedirectToAction("index");
        }
        public IActionResult Buy(int id, int SoLuong, string type = "Normal")
        {      

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>(); //mảng các item

                cart.Add(new Item { Product = Product.FindProByID(id), Quantity = SoLuong });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity+=SoLuong;
                }
                else
                {
                    cart.Add(new Item { Product = Product.FindProByID(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.id_pro == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("index");
        }
    }
}
