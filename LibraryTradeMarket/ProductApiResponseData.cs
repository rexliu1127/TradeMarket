using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryTradeMarket
{
    public class ProductApiResponseData : ApiResponseData
    {

        public List<ProductViewModel> ListOfProduct { get; set; }
       
        public ProductApiResponseData()
        {
            ListOfProduct = new List<ProductViewModel>();
        }       

    }
}