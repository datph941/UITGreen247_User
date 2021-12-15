using Microsoft.AspNetCore.Mvc;
using Project_UIT247Green_User.Models;
using System.Collections.Generic;

namespace Project_UIT247Green_User.Controllers
{
    public class OrderController : Controller
    {
        public void DataCart()
        {
            double total = 0;
            Product pro = new Product();
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
        public IActionResult Order_history()
        {
            Email();
            MenuCat();
            DataCart();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            List<Orders_user> list = Orders_user.Select(u.id);
            this.ViewBag.listord = list;
            return View();
        }
        public IActionResult Order_information(int id)
        {
            Email();
            MenuCat();
            DataCart();
            string key = "email";
            var cookie = Request.Cookies[key];
            Users u = Users.FindU(cookie);
            Orders_user ord = Orders_user.SelectOne(id);
            this.ViewBag.ord = ord;
            var listitem = Order_user_items.Select(id);
            this.ViewBag.listitem = listitem;
            List<Order_status> liststatus = Order_status.SelectStatus(id);
            this.ViewBag.liststatus = liststatus;
            return View();
        }
        public IActionResult Return(int id_ord)
        {
            Order_user_items.DeleteItem(id_ord);
            Order_status.DeleteStatus(id_ord);
            Orders_user.DeleteOrder(id_ord);
            return RedirectToAction("order_history");
        }
    }
}
