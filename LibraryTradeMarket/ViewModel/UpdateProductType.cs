using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class UpdateProductType
    {
        

        private string updateID;
        private string productTypeName;
        

        public string UpdateID
        {
            get
            {
                return updateID;
            }

            set
            {
                updateID = value;
            }
        }


        public string ProductTypeName { get => productTypeName; set => productTypeName = value; }

       
    }
}
