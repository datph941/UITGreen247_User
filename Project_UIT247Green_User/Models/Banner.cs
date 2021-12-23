using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Banner
    {
        public int id_banner { set; get; }
        public string description { set; get; }
        public int id_img { set; get; }

        public static Banner Selectone(string type)
        {
            Banner bn = new Banner();
            using (var context = new DataContext())
            {
                bn = context.Banner.Where(p => p.description==type).FirstOrDefault();
            }
            return bn;
        }
    }
}
