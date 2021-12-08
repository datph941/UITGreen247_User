using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Comment
    {
        public int id_cmt { set; get; }
        public int id_user { set; get; }
        public int id_pro { set; get; }
        public string cmt_detail { set; get; }
    }
}
