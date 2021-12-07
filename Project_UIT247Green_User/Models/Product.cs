using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Product
    {
        public int id_pro { set; get; }
        public string name_pro { set; get; }
        public int id_img { set; get; }
        public int id_cat { set; get; }
        public double price { set; get; }
        public int quantity { set; get; }
        public string origin { set; get; }
        public string status { set; get; }
        public string type { set; get; }
        public double discount { set; get; }
        public double sale_rate { set; get; }
        public static List<Product> ListProByCat(int id)
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                var product = context.Product.Where(p => p.id_cat==id).ToList();
                listPro = product;
            }
            return listPro;
        }
        public static Product FindProByID(int id)
        {
            using (var context = new DataContext())
            {
                var pro = context.Product;
                Product product = (from p in pro
                                   where (p.id_pro == id)
                                   select p).FirstOrDefault();
                return product;
            }

        }
    }
}
