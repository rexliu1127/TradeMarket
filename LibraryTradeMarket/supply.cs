//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryTradeMarket
{
    using System;
    using System.Collections.Generic;
    
    public partial class supply
    {
        public int id { get; set; }
        public int department_id { get; set; }
        public int supply_type_id { get; set; }
        public string supply_customize_id { get; set; }
        public string supply_name { get; set; }
        public string nickname { get; set; }
        public string unified_business_no { get; set; }
        public string owner { get; set; }
        public string owner_mobile { get; set; }
        public string contact { get; set; }
        public string contact_mobile { get; set; }
        public string tel_o { get; set; }
        public string tel_h { get; set; }
        public string fax { get; set; }
        public string tel_other { get; set; }
        public string contact_address_city_o { get; set; }
        public string contact_address_area_o { get; set; }
        public string contact_address_street_o { get; set; }
        public string contact_address_o { get; set; }
        public string contact_address_zipno_o { get; set; }
        public string contact_address_city_h { get; set; }
        public string contact_address_area_h { get; set; }
        public string contact_address_street_h { get; set; }
        public string contact_address_h { get; set; }
        public string contact_address_zipno_h { get; set; }
        public string bank_name { get; set; }
        public string receiver { get; set; }
        public string account { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string memo { get; set; }
        public System.DateTime update_date { get; set; }
        public int currency_id { get; set; }
        public Nullable<int> update_user_id { get; set; }
        public decimal tax_rate { get; set; }
        public int tax_add { get; set; }
        public int payment_term_id { get; set; }
    }
}