using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Sub_news
    {
        public int id_sub { set; get; }
        public string email { set; get; }

        public static int Insert(string email)
        {
            using (var context = new DataContext())
            {
                Sub_news sub = null;
                sub = context.Sub_news.Where(p => p.email == email).FirstOrDefault();
                if(sub==null)
                {
                    context.Sub_news.Add(new Sub_news
                    {
                        email = email
                    });
                    return context.SaveChanges();
                }    
                else
                {
                    return 0;
                }    
            }
        }
    }
}
