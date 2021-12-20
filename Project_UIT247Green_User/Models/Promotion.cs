using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Promotion
    {
        public int id_promotion { set; get; }
        public string name_promotion { set; get; }
        public double discount { set; get; }
        public DateTime date { set; get; }
        public static Promotion selectbyname(string name)
        {
            using (var context = new DataContext())
            {
                var pro = context.Promotion;
                Promotion pro1 = (from p in pro
                                   where (p.name_promotion == name)
                                   select p).FirstOrDefault();
                Promotion pro2 = (from p in pro
                                  where (p.id_promotion == 1)
                                  select p).FirstOrDefault();
                int result = DateTime.Compare(pro1.date, DateTime.Now);
                if (result>0)
                {
                    return pro1;
                }    
                else
                {
                    return pro2;
                }    
               
            }
        }
        public static Promotion selectbyid(int id)
        {
            using (var context = new DataContext())
            {
                var pro = context.Promotion;
                Promotion pro1 = (from p in pro
                                  where (p.id_promotion == id)
                                  select p).FirstOrDefault();
                return pro1;
            }
        }
    }
}
