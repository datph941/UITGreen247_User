using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Order_status
    {
        public int id { set; get; }
        public int id_ord { set; get; }
        public string status { set; get; }
        public DateTime date { set; get; }
        public string address { set; get; }
        public static int Insert(int id_ord, string status, string address)
        {
            using (var context = new DataContext())
            {
                context.Order_status.Add(new Order_status
                {
                    id_ord = id_ord,
                    status = status,
                    date = DateTime.Now,
                    address = address
                });
                return context.SaveChanges();
            }
        }
        public static List<Order_status> SelectStatus(int id)
        {
            using (var context = new DataContext())
            {
                List<Order_status> list = context.Order_status.Where(p => p.id_ord == id).ToList();
                return list;
            }
        }
        public static void DeleteStatus(int id)
        {
            using (var context = new DataContext())
            {
                var ord = (from p in context.Order_status
                           where (p.id_ord == id)
                           select p).ToList();
                if (ord != null)
                {
                    foreach (var item in ord)
                    {
                        context.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
