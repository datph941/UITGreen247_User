using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Image_product
    {
        public int id_img { get; set; }
        public int id_pro { get; set; }
        public string link { get; set; }
        public static List<Image_product> SelectImg()
        {
            List<Image_product> listImg = new List<Image_product>();
            using (var context = new DataContext())
            {
                listImg = context.Image_product.ToList();
            }
            return listImg;
        }
        public static List<Image_product> FindImgByIDPro(int id)
        {
            using (var context = new DataContext())
            {
                var listimg = context.Image_product;
                List<Image_product> list = (from p in listimg
                                where (p.id_pro == id)
                                select p).ToList();
                return list;
            }

        }
    }


}
