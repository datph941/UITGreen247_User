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
        public int status { set; get; }
        public string note { set; get; }
        public double price_sum { set; get; }
    }
}
