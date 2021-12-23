using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Suggest_product
    {
        public int id { set; get; }
        public int id_pro { set; get; }
        public int id_sug { set; get; }

        public static List<Suggest_product> FindSugByID(int id)
        {
            using (var context = new DataContext())
            {
                List<Suggest_product> sug = (from p in context.Suggest_product
                           where (p.id_pro == id)
                           select p).Take(6).ToList();
                return sug;
            }

        }
    }
}
