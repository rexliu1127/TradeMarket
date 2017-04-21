using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryTradeMarket
{
    public class LoginApiResponseData : ApiResponseData
    {

        public MemberViewModel MemberViewModel { get; set; }
       
        public LoginApiResponseData()
        {
            MemberViewModel = new MemberViewModel();
        }       

    }
}