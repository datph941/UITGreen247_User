using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Customer
    {
        public int id_cus { set; get; }
        public string name_cus { set; get; }
        public string email { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public static int Insert(string name, string email, string addr, string phone)
        {
            Customer u = new Customer();
            using (var context = new DataContext())
            {
                context.Customer.Add(new Customer
                {
                    name_cus = name,
                    email = email,
                    address = addr,
                    phone = phone
                });
                return context.SaveChanges();
            }
        }
        public static Customer SelectNew()
        {
            using (var context = new DataContext())
            {
                Customer cus = context.Customer.OrderBy(p=>p.id_cus).Last();
                return cus;
            }
        }
    }
}
