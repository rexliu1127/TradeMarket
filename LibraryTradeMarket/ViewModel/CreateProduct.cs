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
        private string productCustomizeID;
        private string productName;
        private string productUnitName;
        private string updateMemberID;

        private const string DEFAULT_DEPARTMENT_ID= "1";

        public CreateProduct()
        {
            departmentID = DEFAULT_DEPARTMENT_ID;
        }

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
                    departmentID = DEFAULT_DEPARTMENT_ID;
                }
                else
                {
                    departmentID = value;
                }
                
            }
        }
        public string ProductTypeID { get => productTypeID; set => productTypeID = value; }
        public string ProductCustomizeID { get => productCustomizeID; set => productCustomizeID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string ProductUnitName { get => productUnitName; set => productUnitName = value; }

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
