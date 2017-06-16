using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class UpdateTempCart
    {
        

        private string updateID;
        private string productCustomizeID;
        private string productName;
        private string quantity;
        private string price;
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

        public string ProductCustomizeID { get => productCustomizeID; set => productCustomizeID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string Quantity { get => quantity; set => quantity = value; }
        public string Price { get => price; set => price = value; }
        public string ProductUnitName { get => productUnitName; set => productUnitName = value; }



        



    }
}
