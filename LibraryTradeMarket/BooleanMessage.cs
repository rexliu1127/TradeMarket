using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class BooleanMessage
    {
        public bool Result{ get; set; }
        public string Message { get; set; }

        public BooleanMessage()
        {
            Result = false;
            Message = "";

        }
    }
}
