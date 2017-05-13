using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class UpdateProductUnit
    {
        

        private string updateID;
        private string productUnitName;
        

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


        public string ProductUnitName { get => productUnitName; set => productUnitName = value; }

       
    }
}
