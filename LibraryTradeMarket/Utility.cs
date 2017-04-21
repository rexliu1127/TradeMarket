using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace LibraryTradeMarket
{
    public class Utility
    {
        //public 

        public static string getByteToString(byte[] buff)
        {
            string binary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                binary += buff[i].ToString("X2"); // hex format
            }
            return (binary);
        }

        public static string getSecretCode(string strInput)
        {
            string strReturn = "";
            try
            {
                string SECRET = "1@3$";

                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                byte[] keyByte = encoding.GetBytes(SECRET);


                HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);


                byte[] messageBytes = encoding.GetBytes(strInput);


                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

                strReturn = getByteToString(hashmessage);

                
            }
            catch (Exception ex)
            {

                Business b = new Business();
                b.addErrorLog("", "getSecretCode",ex.Message);
            }

            return strReturn;

        }

        public static string getUniqueID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static bool isNumeric(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static decimal getNumericDecimal(String strText)
        {
            try
            {
                if (String.IsNullOrEmpty(strText))
                {
                    return 0;
                }
                else
                {
                    if (isNumeric(strText))
                    {
                        return decimal.Parse(strText);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static double getNumericDoubleDefaultOne(String strText)
        {
            try
            {
                if (String.IsNullOrEmpty(strText))
                {
                    return 1;
                }
                else
                {
                    if (isNumeric(strText))
                    {
                        return Double.Parse(strText);
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception)
            {
                return 1;
            }

        }

        public static double getNumericDouble(String strText)
        {
            try
            {
                if (String.IsNullOrEmpty(strText))
                {
                    return 0;
                }
                else
                {
                    if (isNumeric(strText))
                    {
                        return Double.Parse(strText);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Business business = new Business();
                business.addErrorLog("Utility", "getNumericDouble",ex.Message);
                return 0;
            }

        }

        public static int getNumericInt(string strText)
        {
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(strText))
                {
                    return 0;
                }
                else
                {
                    if (isNumeric(strText))
                    {
                        if (int.TryParse(strText, out result))
                        {
                            return result;
                        }
                        else
                        {

                            return Convert.ToInt16(Math.Round(double.Parse(strText)));
                        }

                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static long getNumericLong(string strText)
        {
            long result = 0;
            try
            {
                if (string.IsNullOrEmpty(strText))
                {
                    return 0;
                }
                else
                {
                    if (isNumeric(strText))
                    {
                        if (long.TryParse(strText, out result))
                        {
                            return result;
                        }
                        else
                        {

                            return Convert.ToInt64(Math.Round(double.Parse(strText)));
                        }

                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static DateTime getDateOrDefault(string strDate, DateTime defaultDate)
        {
            DateTime resultDate;
            try
            {
                resultDate = DateTime.Parse(strDate);
            }
            catch (Exception)
            {
                resultDate = defaultDate;
                //throw;
            }

            return resultDate;
        }

        public static int getIntOrDefault(string strInt, int defaultInt)
        {
            int resultInt;
            try
            {
                resultInt = int.Parse(strInt);
            }
            catch (Exception)
            {
                resultInt = defaultInt;
                //throw;
            }

            return resultInt;
        }

    }
}
