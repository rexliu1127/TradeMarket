using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public partial class cart_order
    {
        public cart_order()
        {
            department_id = 1;
            order_state_id = 1;
            serial = "";
            temp_order_id = "0";

            buyer_id = 1;
            buyer_name = "";

            total = 0;

            tax_add = 0;
            tax_rate = 0;
            tax_total = 0;

            memo = "";
            update_date = DateTime.Now;
            delivery_date = DateTime.Now;
            shipment_type = "";


            contact = "";
            contact_tel = "";

            contact_address_city_o = "";
            contact_address_area_o = "";
            contact_address_street_o = "";
            contact_address_o = "";
            contact_address_zipno_o = "";
            invoice_type = "";
            invoice_title = "";
            invoice_no = "";

        }
    }
}
