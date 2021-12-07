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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult category(int id)
        {
            List<Category> listCat = new List<Category>();
            List<Category> list= new List<Category>();
            listCat = Category.FindCatFather();
            list = Category.FindCatChild();
            this.ViewBag.list = list;
            List<Product> listpro = Product.ListProByCat(id);
            this.ViewBag.listpro = listpro;
            List<Image> listimg = Image.SelectImg();
            this.ViewBag.listimg = listimg;
            return View(listCat);
        }
        public IActionResult categoryPro(int id_child)
        {
            return View("category");
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About_us()
        {
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
        public IActionResult Product_detail()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
