using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Order_user_items
    {
        public int id_user_items { set; get; }
        public int id_ord { set; get; }
        public int id_pro { set; get; }
        public int quantity { set; get; }
        public double price { set; get; }
        public static int Insert(int id_ord, int id_pro, int quantity,double price)
        {
            using (var context = new DataContext())
            {
                context.Order_user_items.Add(new Order_user_items
                {
                    id_ord = id_ord,
                    id_pro = id_pro,
                    quantity = quantity,
                    price = price
                });
                return context.SaveChanges();
            }
        }
        public static void DeleteItem(int id)
        {
            using (var context = new DataContext())
            {
                var ord = (from p in context.Order_user_items
                           where (p.id_ord == id)
                           select p).ToList();
                if (ord != null)
                {
                    foreach(var item in ord)
                    {
                        context.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
        }
        public static List<Order_user_items> Select(int id)
        {
            using (var context = new DataContext())
            {
                var list = context.Order_user_items.Where(p => p.id_ord == id).ToList();
                return list;
            }
        }
    }
}
