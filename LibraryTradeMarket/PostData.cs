
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryTradeMarket
{
    public class PostData
    {

        private string mask;
        private string data;


        public string Mask { get => mask; set => mask = value; }
        public string Data { get => data; set => data = value; }

        public PostData()
        {
            mask = "";
        }

    }
}