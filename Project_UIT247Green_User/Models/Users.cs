using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Users
    {
        public int id { set; get; }
        public string fullname { set; get; }
        public string password { set; get; }
        public string email { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public static Users CheckLogin(string email, string pass)
        {
            Users u = new Users();
            using (var context = new DataContext())
            {
                 u = (from p in context.Users
                      where (p.email == email && p.password == pass)
                   select p).FirstOrDefault();
            }
            return u;
        }
        public static Users FindU(string email)
        {
            Users u = new Users();
            using (var context = new DataContext())
            {
                u = (from p in context.Users
                     where (p.email == email)
                     select p).FirstOrDefault();
            }
            return u;
        }
        public static int InsertU(string name, string email, string pass, string addr, string phone)
        {
            Users u = new Users();
            using (var context = new DataContext())
            {
                u = (from p in context.Users
                     where (p.email == email)
                     select p).FirstOrDefault();
                if(u!=null)
                {
                    return 0;
                }
                else
                {
                    context.Users.Add(new Users
                    {
                        fullname = name,
                        password = pass,
                        email = email,
                        address = addr,
                        phone = phone
                    });
                    return context.SaveChanges();
                }    
 
            }
        }
        public static void UpdateU(Users item)
        {
            using (var context = new DataContext())
            {
                Users user = (from p in context.Users
                              where (p.id == item.id)
                              select p).FirstOrDefault();
                if (user != null)
                {
                    user.id = item.id;
                    user.fullname = item.fullname;
                    user.password = item.password;
                    user.email = item.email;
                    user.address = item.address;
                    user.phone = item.phone;
                    context.SaveChanges();
                }
            }
        }
    }
}
