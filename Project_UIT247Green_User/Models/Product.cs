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
        public int id_cat { set; get; }
        public double price { set; get; }
        public int quantity { set; get; }
        public string origin { set; get; }
        public string status { set; get; }
        public string type { set; get; }
        public double discount { set; get; }
        public int rate { set; get; }
        public double sale_rate { set; get; }
        public static List<Product> ListPro()
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                listPro = context.Product.ToList();               
            }
            return listPro;
        }
        public static List<Product> ListProByCat(int id)
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                var product = context.Product.Where(p => p.id_cat==id && p.quantity>0).ToList();
                listPro = product;
            }
            return listPro;
        }
        public static List<Product> ListProByCat(int id,int type)
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                if(type==0)
                {
                    var product = context.Product.Where(p => p.id_cat == id && p.quantity > 0).ToList();
                    listPro = product;
                }    
                else if(type==1)
                {
                    var product = context.Product.Where(p => p.id_cat == id && p.quantity > 0).OrderBy(p=> p.name_pro).ToList();
                    listPro = product;
                }
                else if (type == 2)
                {
                    var product = context.Product.Where(p => p.id_cat == id && p.quantity > 0).OrderByDescending(p => p.name_pro).ToList();
                    listPro = product;
                }
                else if (type == 3)
                {
                    var product = context.Product.Where(p => p.id_cat == id && p.quantity > 0).OrderBy(p => p.price*(p.sale_rate+100)/100*(100-p.discount)/100).ToList();
                    listPro = product;
                }
                else if (type == 4)
                {
                    var product = context.Product.Where(p => p.id_cat == id && p.quantity > 0).OrderByDescending(p => p.price * (p.sale_rate + 100) / 100 * (100 - p.discount) / 100).ToList();
                    listPro = product;
                }
            }
            return listPro;
        }
        public static List<Product> ListProByCatSearch(int id, int type,string search)
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                if (type == 0)
                {
                    var product = context.Product.Where(p => p.name_pro.Contains(search) && p.id_cat == id && p.quantity > 0).ToList();
                    listPro = product;
                }
                else if (type == 1)
                {
                    var product = context.Product.Where(p => p.name_pro.Contains(search) && p.id_cat == id && p.quantity > 0).OrderBy(p => p.name_pro).ToList();
                    listPro = product;
                }
                else if (type == 2)
                {
                    var product = context.Product.Where(p => p.name_pro.Contains(search) && p.id_cat == id && p.quantity > 0).OrderByDescending(p => p.name_pro).ToList();
                    listPro = product;
                }
                else if (type == 4)
                {
                    var product = context.Product.Where(p => p.name_pro.Contains(search) && p.id_cat == id && p.quantity > 0).OrderBy(p => p.price * (p.sale_rate + 100) / 100 * (100 - p.discount) / 100).ToList();
                    listPro = product;
                }
                else if (type == 5)
                {
                    var product = context.Product.Where(p => p.name_pro.Contains(search) && p.id_cat == id && p.quantity > 0).OrderByDescending(p => p.price * (p.sale_rate + 100) / 100 * (100 - p.discount) / 100).ToList();
                    listPro = product;
                }
            }
            return listPro;
        }
        public static List<Product> ListProByName(int id_cat,string name)
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                var product = context.Product.Where(p => p.name_pro.Contains(name) && p.quantity > 0 && p.id_cat==id_cat).ToList();
                listPro = product;
            }
            return listPro;
        }
        public static List<Product> ListProNew()
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                var product = context.Product.Where(p=> p.quantity > 0).OrderByDescending(s => s.id_pro).Take(8).ToList();
                listPro = product;
            }
            return listPro;
        }
        public static List<Product> ListProBest(int sl)
        {
            List<Product> list = new List<Product>();
            using (var context = new DataContext())
            {
                var q = (from h in context.Order_user_items
                         group h by new { h.id_pro } into hh
                         select new
                         {
                             hh.Key.id_pro,
                             Score = hh.Sum(s => s.quantity)
                         }).OrderByDescending(i => i.Score);
                foreach(var item in q)
                {
                    Product pro = FindProByID(item.id_pro);
                    list.Add(pro);
                }
                list = list.Where(p => p.quantity > 0).Take(sl).ToList();
                return list;
            }          
        }
        public static List<Product> ListProCat(int id)
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                var product = context.Product.Where(p => p.id_cat == id && p.quantity > 0).Take(3).ToList();
                listPro = product;
            }
            return listPro;
        }
        public static List<Product> ListProSale()
        {
            List<Product> listPro = new List<Product>();
            using (var context = new DataContext())
            {
                var product = context.Product.Where(p=>p.discount>0 && p.quantity > 0).OrderByDescending(p=>p.discount).Take(8).ToList();
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
        public static void UpdatePro(int id_pro, int rate)
        {
            using (var context = new DataContext())
            {
                var product = context.Product;
                Product pro_duct = (from p in product
                                    where (p.id_pro == id_pro)
                                    select p).FirstOrDefault();

                if (pro_duct != null)
                {
                    pro_duct.rate = rate;
                }
                context.SaveChanges();

            }
        }
    }
}
