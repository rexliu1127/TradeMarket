using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class ProductTypeViewModel
    {
        public string SuperTypeID { get; set; }
        public string SuperTypeName { get; set; }

        public string ProductTypeID { get; set; }        
        public string ProductTypeName { get; set; }

        public ProductTypeViewModel()
        {
            SuperTypeID = "";
            SuperTypeName = "";
            ProductTypeID = "";           
            ProductTypeName = "";            
        }
    }
}
