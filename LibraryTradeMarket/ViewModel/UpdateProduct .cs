using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class UpdateProduct
    {
        

        private string updateProductCustomizeID;
        private string departmentID;
        private string productTypeID;
        private string customizeID;
        private string productName;
        private string productUnitName;
        private string imageUrl;
        private string updateMemberID;

        public UpdateProduct()
        {            
            customizeID="";
            productName="";
            productUnitName="";
            imageUrl="";
            
    }


        public string UpdateProductCustomizeID
        {
            get
            {
                return updateProductCustomizeID;
            }

            set
            {
                updateProductCustomizeID = value;
            }
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
                    departmentID = "1";
                }
                else
                {
                    departmentID = value;
                }

            }
        }

        
        public string ProductTypeID
        {
            get
            {
                return productTypeID;
            }

            set
            {
                productTypeID = value;
            }
        }

        public string ProductCustomizeID { get => customizeID; set => customizeID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string ProductUnitName { get => productUnitName; set => productUnitName = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }

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
