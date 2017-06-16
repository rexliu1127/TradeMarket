using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class CreateCartOrder
    {
        private string tempOrderID="";
        private string sellerID="";
        private string sellerName="";
        private string buyerID="";
        private string buyerName="";
        private string deliveryDate="";
        private string shipmentType="";
        private string contact = "";
        private string contactTel = "";
        private string contactAddressCityO = "";
        private string contactAddressAreaO = "";
        private string contactAddressStreetO = "";
        private string contactAddressO = "";
        private string contactAddressZipNoO = "";
        private string invoiceType = "";
        private string invoiceTitle = "";
        private string invoiceNo = "";

        public string TempOrderID { get => tempOrderID; set => tempOrderID = value; }
        public string SellerID { get => sellerID; set => sellerID = value; }
        public string SellerName { get => sellerName; set => sellerName = value; }
        public string BuyerID { get => buyerID; set => buyerID = value; }
        public string BuyerName { get => buyerName; set => buyerName = value; }
        public string DeliveryDate { get => deliveryDate; set => deliveryDate = value; }
        public string ShipmentType { get => shipmentType; set => shipmentType = value; }
        public string Contact { get => contact; set => contact = value; }
        public string ContactTel { get => contactTel; set => contactTel = value; }
        public string ContactAddressCityO { get => contactAddressCityO; set => contactAddressCityO = value; }
        public string ContactAddressAreaO { get => contactAddressAreaO; set => contactAddressAreaO = value; }
        public string ContactAddressStreetO { get => contactAddressStreetO; set => contactAddressStreetO = value; }
        public string ContactAddressO { get => contactAddressO; set => contactAddressO = value; }
        public string ContactAddressZipNoO { get => contactAddressZipNoO; set => contactAddressZipNoO = value; }
        public string InvoiceType { get => invoiceType; set => invoiceType = value; }
        public string InvoiceTitle { get => invoiceTitle; set => invoiceTitle = value; }
        public string InvoiceNo { get => invoiceNo; set => invoiceNo = value; }
    }
}
