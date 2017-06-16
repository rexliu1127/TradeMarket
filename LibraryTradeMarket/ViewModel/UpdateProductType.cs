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
        private string superTypeID;
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

        public string SuperTypeID { get => superTypeID; set => superTypeID = value; }
        public string ProductTypeName { get => productTypeName; set => productTypeName = value; }

    }
}
