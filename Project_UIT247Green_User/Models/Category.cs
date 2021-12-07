using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Category
    {
        public int id_cat { set; get; }

        public string name_cat { set; get; }

        public int id_parent { set; get; }
    }
}
