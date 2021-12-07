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
        public static List<Category> FindCatFather()
        {
            List<Category> listCat = new List<Category>();
            using (var context = new DataContext())
            {
                var cat = context.Category;
                listCat = (from p in cat
                           where (p.id_parent == 0)
                           select p).ToList();
            }
            return listCat;
        }
        public static List<Category> FindCatChild()
        {
            List<Category> listCat = new List<Category>();
            using (var context = new DataContext())
            {
                var cat = context.Category;
                listCat = (from p in cat
                           where (p.id_parent > 0)
                           select p).ToList();
            }
            return listCat;
        }
    }
    
}
