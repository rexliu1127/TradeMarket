using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class CreateProduct
    {
        private string departmentID;
        private string productTypeID;
        private string customizeID;
        private string productName;
        private string updateMemberID;

        public string DepartmentID
        {
            get
            {
                return departmentID;
            }

            set
            {
                if (Utility.getIntOrDefault(DepartmentID, 0) <= 0)
                {
                    departmentID = "1";
                }
                else
                {
                    departmentID = value;
                }
                
            }
        }
        public string ProductTypeID { get => productTypeID; set => productTypeID = value; }
        public string CustomizeID { get => customizeID; set => customizeID = value; }
        public string ProductName { get => productName; set => productName = value; }

        public string UpdateMemberID
        {
            get
            {
                return updateMemberID;
            }

            set
            {
                if (Utility.getIntOrDefault(UpdateMemberID, 0) <= 0)
                {
                    updateMemberID = "1";
                }
                else
                {
                    updateMemberID = value;
                }
            }
        }
    }
}
