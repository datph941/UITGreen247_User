using Microsoft.AspNetCore.Http;
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
            List<Item> cart = new List<Item>();
            Product pro = new Product();
            string cartcookie = Request.Cookies["cart"];
            if (cartcookie == null)
            {
                string value = "";
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Append("cart", value, cookie);
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
                if (pro.id_promotion == 1)
                {
                    nofi = "Mã đã hết hạn sử dụng";
                }
                else
                {
                    string discount = String.Format("{0:0,0 vnđ}", pro.discount);
                    nofi = pro.name_promotion + " khả dụng được giảm " + discount;
                    discount1 = pro.discount;
                }
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
            List<Item> cart = new List<Item>();
            List<Item> cartcheckout = new List<Item>();
            Product pro = new Product();
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
                        if (pro.quantity>0)
                        {
                            cartcheckout.Add(item);
                        }    
                    }

                }
            }
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.cartcheckout = cartcheckout;
                ViewBag.totalcheckout = cartcheckout.Sum(item => item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100));
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
                    if(pro.quantity>0)
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
            double price = 0;
            foreach(var item in cart)
            {
                price = item.Quantity * item.Product.price * (100 + item.Product.sale_rate) / 100 * ((100 - item.Product.discount) / 100);
                Order_items.Insert(id_ord,item.Product.id_pro, item.Quantity,price);
            }
            UpdateCart(cart1);
            if(check>0)
            {
                ViewBag.nofi = "Đặt hàng thành công";
            }    
            return RedirectToAction("index");
        }
        public IActionResult Minus(int id, int SoLuong = -1)
        {
            string cartcookie = Request.Cookies["cart"];
            if (cartcookie == null || cartcookie.Equals(""))
            {
                cartcookie = id + "," + 1;
            }
            else
            {
                int check = isExist(id);
                if (check == -1)
                {
                    cartcookie = cartcookie + "|" + id + "," + SoLuong;
                }
                else
                {
                    string[] arrcart = cartcookie.Split("|");
                    string cart = "";
                    for (int i = 0; i < arrcart.Length; i++)
                    {
                        string[] arritem = arrcart[i].Split(",");
                        int id_pro = Convert.ToInt32(arritem[0]);
                        int quantity = Convert.ToInt32(arritem[1]);
                        if (id_pro == id)
                        {
                            if(quantity>1)
                            {
                                quantity += SoLuong;
                            }       
                            arritem[1] = quantity.ToString();
                        }
                        string item1 = id_pro + "," + quantity;
                        if (i == 0)
                        {
                            cart = cart + item1;
                        }
                        else
                        {
                            cart = cart + "|" + item1;
                        }
                    }
                    cartcookie = cart;
                }
            }
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("cart", cartcookie, cookie);
            return RedirectToAction("Index");
        }
        public IActionResult Buy(int id, int SoLuong=1)
        {
            string cartcookie = Request.Cookies["cart"];
            if (cartcookie.Equals(""))
            {
                cartcookie = id + "," + 1;
            }
            else
            {
                int check = isExist(id);
                if (check == -1)
                {
                    cartcookie = cartcookie + "|" + id + "," + SoLuong;
                }
                else
                {
                    string[] arrcart = cartcookie.Split("|");
                    string cart = "";
                    for (int i = 0; i < arrcart.Length; i++)
                    {
                        if (arrcart[i] != null)
                        {
                            string[] arritem = arrcart[i].Split(",");
                            int id_pro = Convert.ToInt32(arritem[0]);
                            int quantity = Convert.ToInt32(arritem[1]);
                            if (id_pro == id)
                            {
                                quantity += SoLuong;
                                arritem[1] = quantity.ToString();

                            }
                            string item1 = id_pro + "," + quantity;
                            if (i == 0)
                            {
                                cart = cart + item1;
                            }
                            else
                            {
                                cart = cart + "|" + item1;
                            }
                        }    
                    }
                    cartcookie = cart;
                }
            }
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("cart", cartcookie, cookie);
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            string cartcookie = Request.Cookies["cart"];
            if (cartcookie.Equals(""))
            {
                return 0;
            }
            else
            {
                string[] arrcart = cartcookie.Split("|");
                for (int i = 0; i < arrcart.Length; i++)
                {
                    if (!arrcart[i].Equals(""))
                    {
                        string[] arritem = arrcart[i].Split(",");
                        int id_pro = Convert.ToInt32(arritem[0]);
                        int quantity = Convert.ToInt32(arritem[1]);
                        if (id_pro == id)
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
        }
        public void UpdateCart(List<Item> cart)
        {
            string cartnull = "";
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("cart", cartnull, cookie);
            foreach (var item in cart)
            {
                Buy(item.Product.id_pro, item.Quantity);
            }    
            
        }
        public IActionResult Remove(int id)
        {
            string cartcookie = Request.Cookies["cart"];
            string[] arrcart = cartcookie.Split("|");
            string cart = "";
            for (int i = 0; i < arrcart.Length; i++)
            {
               if(!arrcart[i].Equals(""))
                {
                    string[] arritem = arrcart[i].Split(",");
                    int id_pro = Convert.ToInt32(arritem[0]);
                    int quantity = Convert.ToInt32(arritem[1]);
                    if (id_pro == id)
                    {

                    }
                    else
                    {
                        string item1 = id_pro + "," + quantity;
                        if (i == 0)
                        {
                            cart = item1;
                        }
                        else
                        {
                            cart = cart + "|" + item1;
                        }
                    }
                }    
            }
            cartcookie = cart;
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("cart", cartcookie, cookie);
            return RedirectToAction("index");
        }
    }
}
