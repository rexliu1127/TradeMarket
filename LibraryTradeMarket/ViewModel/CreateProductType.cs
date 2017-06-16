using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class CreateProductType
    {

        private string superTypeID;
        private string productTypeName;        
        
        public string ProductTypeName { get => productTypeName; set => productTypeName = value; }
        public string SuperTypeID { get => superTypeID; set => superTypeID = value; }
    }
}
