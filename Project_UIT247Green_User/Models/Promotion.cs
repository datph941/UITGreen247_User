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

        public static Promotion selectbyname(string name)
        {
            using (var context = new DataContext())
            {
                var pro = context.Promotion;
                Promotion pro1 = (from p in pro
                                   where (p.name_promotion == name)
                                   select p).FirstOrDefault();
                return pro1;
            }
        }
    }
}
