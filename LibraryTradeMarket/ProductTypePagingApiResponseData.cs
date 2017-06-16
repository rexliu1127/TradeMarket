using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryTradeMarket
{
    public class ProductTypePagingApiResponseData : ApiResponseData
    {

        public ProductTypePagingViewModel Data { get; set; }
       
        public ProductTypePagingApiResponseData()
        {
            Data = new ProductTypePagingViewModel();
        }       

    }
}