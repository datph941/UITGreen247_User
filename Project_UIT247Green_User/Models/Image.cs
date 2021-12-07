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
        public static List<Image> SelectImgByID()
        {
            List<Image> listImg = new List<Image>();
            using (var context = new DataContext())
            {
                listImg = context.Image.ToList();
            }
            return listImg;
        }
    }

}
