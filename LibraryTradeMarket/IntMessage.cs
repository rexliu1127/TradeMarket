using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class IntMessage
    {
        public int Result{ get; set; }
        public string Message { get; set; }

        public IntMessage()
        {
            Result = 0;
            Message = "";

        }
    }
}
