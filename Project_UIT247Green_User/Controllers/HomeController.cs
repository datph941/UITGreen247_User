using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_UIT247Green_User.Helpers;
using Project_UIT247Green_User.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public void Email ()
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
                if(u!=null)
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
                           if(arrcart[i]!="")
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
        public IActionResult Index()
        {
            MenuCat();
            Email();
            DataCart();
            List<Product> listpronew = new List<Product>();
            listpronew = Product.ListProNew();
            this.ViewBag.listpronew = listpronew;
            return View();
        }
        public IActionResult category(int id, int type=0, int pg = 1)
        {
            MenuCat();
            Email();
            DataCart();
            List<Product> listpro = Product.ListProByCat(id,type);
            List<Image_product> listimg = Image_product.SelectImg();
            this.ViewBag.listimg = listimg;
            Category cat = new Category();
            cat = Category.FindCatByID(id);
            this.ViewBag.cat = cat;
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = listpro.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listpro.Skip(recSkip).Take(pager.pageSize).ToList();
            this.ViewBag.Pager = pager;
            this.ViewBag.data = data;
            return View();
        }
        public IActionResult Search(int id, string search, int type = 0, int pg = 1)
        {
            MenuCat();
            Email();
            DataCart();
            List<Product> listpro = new List<Product>();
            List<Product> listpro1 = new List<Product>();
            List<Category> list = Category.search(id);
            foreach(var item in list)
            {
                listpro1 = Product.ListProByName(item.id_cat, search);
                foreach(var item1 in listpro1)
                {
                    listpro.Add(item1);
                }    
            }
            if (type == 0)
            {
                listpro = listpro.ToList();
    
            }
            else if (type == 1)
            {
                listpro = listpro.OrderBy(p => p.name_pro).ToList();
            }
            else if (type == 2)
            {
                listpro = listpro.OrderByDescending(p => p.name_pro).ToList();
            }
            else if (type == 3)
            {
                listpro = listpro.OrderBy(p => p.price * (p.sale_rate + 100) / 100 * (100 - p.discount) / 100).ToList();
            }
            else if (type == 4)
            {
                listpro = listpro.OrderByDescending(p => p.price * (p.sale_rate + 100) / 100 * (100 - p.discount) / 100).ToList();
            }
            this.ViewBag.id_cat = id;
            this.ViewBag.search = search;
            List<Image_product> listimg = Image_product.SelectImg();
            this.ViewBag.listimg = listimg;
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = listpro.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listpro.Skip(recSkip).Take(pager.pageSize).ToList();
            this.ViewBag.Pager = pager;
            this.ViewBag.data = data;
            return View();
        }
        public IActionResult Contact()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult QuickView(int id_pro, int id_cat)
        {
            MenuCat();
            Email();
            DataCart();
            Product pro = Product.FindProByID(id_pro);
            Category cat = Category.FindCatByID(id_cat);
            ViewBag.pro = pro;
            ViewBag.cat = cat;
            return View();
        }
        public IActionResult About_us()
        {
            MenuCat();
            Email();
            DataCart();
            return View();
        }
        public IActionResult Cmt_Product(int id_pro, string name, int rating, string text, string type="Normal")
        {
            Comment.Insert(name, id_pro, rating, text);
            int rate = Comment.SumRate(id_pro);
            Product.UpdatePro(id_pro, rate);
            return RedirectToAction("product_detail");
        }
        public IActionResult Subscribe(string email, string type = "Normal")
        {
            int check = Sub_news.Insert(email);
            string body = System.IO.File.ReadAllText(@"wwwroot\email_register.txt");
            MailUtils.SendGmail("uitgreen247@gmail.com", email, "Đăng kí nhận tin tức từ UITGreen247", body, "uitgreen247@gmail.com", "LucasPhan94");
            if (type == "ajax")
            {
                return Json(new
                {
                    check = check
                });
            }
            return RedirectToAction("index");
        }
        public IActionResult Product_detail(int idpro,int idcat)
        {
            MenuCat();
            Email();
            DataCart();
            Product pro = new Product();
            pro = Product.FindProByID(idpro);
            Category cat = Category.FindCatByID(idcat);
            List<Image_product> listimg = new List<Image_product>();
            listimg = Image_product.FindImgByIDPro(idpro);
            this.ViewBag.pro = pro;
            this.ViewBag.cat = cat;
            this.ViewBag.listimg = listimg;
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
