using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryTradeMarket
{
    public class ApiResponseData
    {

        private string _Result;
        private string _Message;
        private string _Source;
        public ApiResponseData()
        {
            _Result = "0";
            _Message = "";
            _Source = "";
        }
        public string Result
        {
            get
            {
                string returnValue;
                returnValue = _Result;
                return returnValue;
            }
            set
            {
                _Result = value;
            }
        }

        //連線字串屬性
        public string Message
        {
            get
            {
                string returnValue;
                returnValue = _Message;
                return returnValue;
            }
            set
            {
                _Message = value;
            }
        }

        public string Source
        {
            get
            {
                string returnValue;
                returnValue = _Source;
                return returnValue;
            }
            set
            {
                _Source = value;
            }
        }

    }
}