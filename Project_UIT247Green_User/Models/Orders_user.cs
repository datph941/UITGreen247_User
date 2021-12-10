using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Orders_user
    {
        public int id_ord { set; get; }
        public int id_user { set; get; }
        public int id_promotion { set; get; }
        public double ship { set; get; }
        public int shipmethod { set; get; }
        public int paymethod { set; get; }
        public int status { set; get; }
        public string note { set; get; }
        public double price_sum { set; get; }
        public static int Insert(int id, int id_promotion, double ship, string note, int shipmethod, int paymethod, double pricesum)
        {
            using (var context = new DataContext())
            {
                context.Orders_user.Add(new Orders_user
                {
                    id_user = id,
                    id_promotion = id_promotion,
                    ship = ship,
                    shipmethod = shipmethod,
                    paymethod = paymethod,
                    status = 0,
                    note = note,
                    price_sum = pricesum + ship
                });
                return context.SaveChanges();
            }
        }
        public static Orders_user SelectNew()
        {
            using (var context = new DataContext())
            {
                Orders_user ord = context.Orders_user.Last();
                return ord;
            }
        }
    }
}
