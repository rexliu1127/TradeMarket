using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public partial class member
    {
        public member()
        {
            role = 1;
            
            member_type_id = 1;
            member_name = "";
            account = "";
            password = "";
            email = "";
            mobile = "";
            tel = "";
            fax = "";
            state = 0;
            contact_address_city_o = "";
            contact_address_area_o = "";
            contact_address_street_o = "";
            contact_address_o = "";
            contact_address_zipno_o = "";

            update_date = DateTime.Now;
            update_member_id = 1;
            
        }
    }
}
