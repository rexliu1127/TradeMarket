using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class ProductViewModel
    {
        public int DepartmentID { get; set; }
        public string ProductCustomizeID { get; set; }
        public string ProductName { get; set; }
        public string ProductUnitName { get; set; }
        public string ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }

        public string ImageUrl { get; set; }
        public string OnSales { get; set; }
        public string UpdateDate { get; set; }


        public ProductViewModel()
        {
            DepartmentID = 1;
            ProductCustomizeID = "";
            ProductName = "";
            ProductUnitName = "";
            ProductTypeID = "";
            ProductTypeName = "";
            ImageUrl = "";
            OnSales = "0";
            UpdateDate = DateTime.Now.ToString(Business.DATETIME_FORMAT_24H);
        }
    }
}
