using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public partial class cart_order_detail
    {
        public cart_order_detail()
        {
            order_id = 1;
            state = 0;
            product_customize_id = "";
            product_name = "";
            quantity = 0;
            price = 0;
            unit_name = "";
            

            total = 0;

            tax_add = 0;
            tax_rate = 0;
            tax_total = 0;

            memo = "";
            update_date = DateTime.Now;
            

        }
    }
}
