using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryTradeMarket
{
    public class ProductApiResponseData : ApiResponseData
    {

        public List<ProductViewModel> listOfProduct { get; set; }
       
        public ProductApiResponseData()
        {
            listOfProduct = new List<ProductViewModel>();
        }       

    }
}