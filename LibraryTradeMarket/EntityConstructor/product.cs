using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public partial class product
    {
        public product()
        {
            department_id = 1;
            product_customize_id = "";
            product_name = "";
            product_type_id = 1;
            short_product_name = "";
            english_name = "";
            brand = "";
            specification1 = "";
            specification2 = "";
            specification3 = "";
            length = 0;
            width = 0;
            height = 0;
            barcode1 = "";
            reference_price1 = 0;
            reference_price2 = 0;
            reference_price3 = 0;
            unit_type = 1;
            display_unit = "PCS";
            image_url = "";
            tax_type = 0;
            sales_type = 0;
            update_date = DateTime.Now;
            update_member_id = 1;
            expiration_date = "";
            expiration_days = 1;
            expiration_alert_percent = 0;
            memo = "";
        }
    }
}
