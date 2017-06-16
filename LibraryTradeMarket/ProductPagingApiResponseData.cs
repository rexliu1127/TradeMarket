using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryTradeMarket
{
    public class ProductPagingApiResponseData : ApiResponseData
    {

        public ProductPagingViewModel Data { get; set; }
       
        public ProductPagingApiResponseData()
        {
            Data = new ProductPagingViewModel();
        }       

    }
}