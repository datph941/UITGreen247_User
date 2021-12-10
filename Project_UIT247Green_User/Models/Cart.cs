using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Cart
    {
        public int id_user{ set; get; }
        public int id_pro { set; get; }
        public int quantity { set; get; }
        public static List<Cart> FindCart(int id)
        {
            List<Cart> list = new List<Cart>();
            using (var context = new DataContext())
            {
                list = context.Cart.Where(p=> p.id_user == id).ToList();
            }
            return list;
        }
        public static void InsertCart(int id_user, int id_pro, int quan)
        {
            using (var context = new DataContext())
            {
                Cart cart = (from p in context.Cart
                             where (p.id_user == id_user && p.id_pro == id_pro)
                             select p).FirstOrDefault();
                if (cart != null)
                {
                    cart.quantity = cart.quantity + quan;                  
                }   
                else
                {
                    context.Cart.Add(new Cart
                    {
                        id_user = id_user,
                        id_pro = id_pro,
                        quantity = quan
                    });              
                }
                context.SaveChanges();

            }
        }
        public static void ChangeQuantity(int id_user, int id_pro, int quan)
        {
            using(var context = new DataContext())
            {
                Cart cart = (from p in context.Cart
                             where (p.id_user== id_user && p.id_pro == id_pro)
                                   select p).FirstOrDefault();
                if(cart!=null)
                {
                    cart.quantity = cart.quantity + quan;
                    context.SaveChanges();
                }    
            }    
        }
        public static void DeleteCart(int id_user, int id_pro)
        {
            using (var context = new DataContext())
            {
                Cart cart = (from p in context.Cart
                             where (p.id_user == id_user && p.id_pro == id_pro)
                             select p).FirstOrDefault();
                if (cart != null)
                {
                    context.Remove(cart);
                    context.SaveChanges();
                }
            }
        }
    }
}
