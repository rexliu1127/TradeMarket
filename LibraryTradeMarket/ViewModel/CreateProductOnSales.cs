using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class CreateProductOnSales
    {
        
        private string departmentID;
        private string productCustomizeID;
        private string updateDate;
        private string updateMemberID;

        public string DepartmentID { get => departmentID; set => departmentID = value; }
        public string ProductCustomizeID { get => productCustomizeID; set => productCustomizeID = value; }
        public string UpdateDate { get => updateDate; set => updateDate = value; }
        public string UpdateMemberID { get => updateMemberID; set => updateMemberID = value; }
    }
}
