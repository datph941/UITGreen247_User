using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Province
    {
        public int id { set; get; }
        public string name_province { set; get; }
        public string _code { set; get; }

        public static List<Province> Select()
        {
            using (var context = new DataContext())
            {
                var list = context.Province.OrderBy(s => s.name_province).ToList();
                return list;
            }
        }
    }
}
