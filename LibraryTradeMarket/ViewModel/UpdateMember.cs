using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class UpdateMember
    {

        private string updateTargetMemberID;

        private string role;
        private string memberTypeID;
        private string memberName;
        private string account;
        private string password;
        private string email;
        private string mobile;

        private string tel;
        private string fax;
        private string state;
        private string contactAddressCityO;

        private string contactAddressAreaO;
        private string contactAddressStreetO;
        private string contactAddressO;
        private string contactAddressZipNoO;

        private string updateDate;
        private string updateMemberID;


        
        

        public UpdateMember()
        {
            memberTypeID = "1";
            role = "1";
            UpdateDate = DateTime.Now.ToString(Business.DATETIME_FORMAT_24H);
        }

        public string Role { get => role; set => role = value; }
        public string MemberTypeID { get => memberTypeID; set => memberTypeID = value; }
        public string MemberName { get => memberName; set => memberName = value; }
        public string Account { get => account; set => account = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Fax { get => fax; set => fax = value; }
        public string State { get => state; set => state = value; }
        public string ContactAddressCityO { get => contactAddressCityO; set => contactAddressCityO = value; }
        public string ContactAddressAreaO { get => contactAddressAreaO; set => contactAddressAreaO = value; }
        public string ContactAddressStreetO { get => contactAddressStreetO; set => contactAddressStreetO = value; }
        public string ContactAddressO { get => contactAddressO; set => contactAddressO = value; }
        public string ContactAddressZipNoO { get => contactAddressZipNoO; set => contactAddressZipNoO = value; }
        public string UpdateDate { get => updateDate; set => updateDate = value; }
        public string UpdateMemberID { get => updateMemberID; set => updateMemberID = value; }
        public string UpdateTargetMemberID { get => updateTargetMemberID; set => updateTargetMemberID = value; }
    }
}
