using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Helpers;
using Project_UIT247Green_User.Models;
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
        public IActionResult Checkout()
        {
            MenuCat();
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
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
        public IActionResult Buy(int id)
        {
            //ProductModel productModel = new ProductModel();       

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>(); //mảng các item

                cart.Add(new Item { Product = Product.FindProByID(id), Quantity = 1 });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
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
