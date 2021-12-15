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
        public static Category FindCatByName(string name)
        {
            Category cat = new Category();
            using (var context = new DataContext())
            {
                cat = (from p in context.Category
                           where (p.name_cat == name)
                           select p).FirstOrDefault();
            }
            return cat;
        }
        public static List<Category> search(int id_parent)
        {
            List<Category> list = new List<Category>();
            using (var context = new DataContext())
            {
               if(id_parent==0)
                {
                    list = context.Category.ToList();                   
                }   
               else
                {
                    list = (from p in context.Category
                            where (p.id_parent == id_parent)
                            select p).ToList();
                }    
            }
            return list;
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
        public static List<Category> FindCatChildByID(int id)
        {
            List<Category> listCat = new List<Category>();
            using (var context = new DataContext())
            {
                var cat = context.Category;
                listCat = (from p in cat
                           where (p.id_parent ==id)
                           select p).ToList();
            }
            return listCat;
        }
        public static Category FindCatByID(int id)
        {
            using (var context = new DataContext())
            {
                var cate = context.Category;
                Category cat = (from p in cate
                                   where (p.id_cat == id)
                                   select p).FirstOrDefault();
                return cat;
            }

        }
    }
    
}
