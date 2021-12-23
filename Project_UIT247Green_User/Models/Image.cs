using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Image
    {
        public int id_img { set; get; }
        public string link { set; get; }
        public static List<Image> SelectImg()
        {
            List<Image> listImg = new List<Image>();
            using (var context = new DataContext())
            {
                listImg = context.Image.ToList();
            }
            return listImg;
        }
        public static Image SelectImgByID(int id)
        {
            Image Img = new Image();
            using (var context = new DataContext())
            {
                Img = context.Image.Where(p => p.id_img == id).FirstOrDefault();
            }
            return Img;
        }
    }

}
