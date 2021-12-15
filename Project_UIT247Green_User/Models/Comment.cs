using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Comment
    {
        public int id_cmt { set; get; }
        public string name { set; get; }
        public int id_pro { set; get; }
        public int rate { set; get; }
        public string cmt_detail { set; get; }

        public static int Insert(string name, int id_pro, int rate, string cmt)
        {
            using (var context = new DataContext())
            {
                context.Comment.Add(new Comment
                {
                    name = name,
                    id_pro = id_pro,
                    rate = rate,
                    cmt_detail = cmt
                }) ;
                return context.SaveChanges();
            }
        }
        public static List<Comment> FindCmtByID(int id)
        {
            using (var context = new DataContext())
            {
               var cmt = (from p in context.Comment
                                   where (p.id_pro == id)
                                   select p).ToList();
                return cmt;
            }

        }
        public static int SumRate(int id_pro)
        {
            using (var context = new DataContext())
            {
                List<Comment> cmt = context.Comment.Where(p=>p.id_pro==id_pro).ToList();
                int sum = 0;
                foreach(var item in cmt)
                {
                    sum += item.rate;
                }
                sum = sum / cmt.Count;
                return sum;
            }
        }
    }
}
