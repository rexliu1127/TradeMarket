using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class MemberViewModel
    {
        public string ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public MemberViewModel()
        {
            ID = "";
            Account = "";
            Name = "";
            Role = 1;
        }
    }
}
