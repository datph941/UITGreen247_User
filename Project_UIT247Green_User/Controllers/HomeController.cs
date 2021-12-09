using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            List<Product> listpronew = new List<Product>();
            listpronew = Product.ListProNew();
            this.ViewBag.listpronew = listpronew;
            return View();
        }
        public IActionResult category(int id, int pg = 1)
        {
            MenuCat();
            Email();
            List<Product> listpro = Product.ListProByCat(id);
            this.ViewBag.listpro = listpro;
            List<Image_product> listimg = Image_product.SelectImg();
            this.ViewBag.listimg = listimg;
            Category cat = new Category();
            cat = Category.FindCatByID(id);
            this.ViewBag.cat = cat;

            const int pageSize = 9;
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
            return View();
        }
        public IActionResult About_us()
        {
            MenuCat();
            Email();
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Product_detail(int idpro,int idcat)
        {
            MenuCat();
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
