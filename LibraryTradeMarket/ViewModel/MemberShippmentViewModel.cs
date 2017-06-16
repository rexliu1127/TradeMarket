using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class MemberShippmentViewModel
    {
        private string id;
        private string contactAddressCityO;
        private string contactAddressAreaO;
        private string contactAddressStreetO;
        private string contactAddressO;
        private string contactAddressZipNoO;

        public MemberShippmentViewModel()
        {
            ID = "";
            ContactAddressCityO = "";
            ContactAddressAreaO = "";
            ContactAddressStreetO = "";
            ContactAddressO = "";
            ContactAddressZipNoO = "";

        }

        public string ID { get => id; set => id = value; }
        public string ContactAddressCityO { get => contactAddressCityO; set => contactAddressCityO = value; }
        public string ContactAddressAreaO { get => contactAddressAreaO; set => contactAddressAreaO = value; }
        public string ContactAddressStreetO { get => contactAddressStreetO; set => contactAddressStreetO = value; }
        public string ContactAddressO { get => contactAddressO; set => contactAddressO = value; }
        public string ContactAddressZipNoO { get => contactAddressZipNoO; set => contactAddressZipNoO = value; }
    }
}
