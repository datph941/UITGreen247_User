using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Order_items
    {
        public int id_ord { set; get; }
        public int id_pro { set; get; }
        public int quantity { set; get; }
        public static int Insert(int id_ord, int id_pro, int quantity)
        {
            using (var context = new DataContext())
            {
                context.Order_items.Add(new Order_items
                {
                    id_ord = id_ord,
                    id_pro = id_pro,
                    quantity = quantity
                });
                return context.SaveChanges();
             }
        }
    }
}
