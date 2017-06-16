using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryTradeMarket;


namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BooleanMessage bm = new BooleanMessage();
            Business b = new Business();
            //b.getProductChildType();

            b.getProductTypePaging();

            b.getFavoriteProduct("3");

            b.getProductByProductTypeName("水果");

            return;

            CreateProduct cp = new CreateProduct();
            cp.DepartmentID = "1";
            cp.ProductCustomizeID = "0009";
            cp.ProductName = "3斤袋";
            cp.ProductTypeID = "7";
            cp.UpdateMemberID = "1";

            bm = b.isCreateProduct(cp);

            UpdateProduct up = new UpdateProduct();

            up.UpdateProductCustomizeID = "00044";

            up.DepartmentID = "1";
            up.ProductCustomizeID = "000456";
            up.ProductName = "高山高麗菜";
            up.ProductTypeID = "7";
            up.UpdateMemberID = "1";

            bm = b.isUpdateProduct(up);

            return;

            b.getProductPaging(1, currentPage: 2);


            var x = b.isLogin("buyer", LibraryTradeMarket.Utility.getSecretCode("1"));
            
            if (x != null)
            {
                MessageBox.Show(x.Account);
            }
            else
            {
                MessageBox.Show("error");
            }

            return;

            DeleteProductOnSales dons = new DeleteProductOnSales();
            dons.DepartmentID = "2";
            dons.ProductCustomizeID = "00011";
            b.isDeleteProductOnSales(dons);


            CreateProductOnSales ons = new CreateProductOnSales();
            ons.ProductCustomizeID = "00011";
            ons.UpdateMemberID = "2";
            ons.DepartmentID = "2";

            b.isCreateProductOnSales(ons);


            CreateCartOrder cco = new CreateCartOrder();

            cco.BuyerID = "1";
            cco.BuyerName = "neil";
            cco.Contact = "qq";
            cco.ContactAddressCityO = "taipei";
            cco.ContactAddressAreaO = "dai an";
            cco.ContactAddressStreetO = "rose";
            cco.ContactAddressO = "236 5-4 1F";
            cco.ContactAddressZipNoO = "116";
            cco.ContactTel = "0911222333";
            cco.DeliveryDate = "2017/5/20";
            cco.SellerID = "83";
            cco.SellerName = "mount";
            cco.InvoiceNo = "23443322";
            cco.InvoiceTitle = "Ntu";
            cco.InvoiceType = "a";
            cco.ShipmentType = "ubike";
            cco.TempOrderID = "8c586bc7821c42f48b8c02af21cdd783";

            b.isTempOrderToCartOrder(cco);
           

            return;

            CreateTempCart ctc = new CreateTempCart();
            ctc.TempOrderID = "0515";
            ctc.ProductCustomizeID = "0001";
            ctc.ProductName = "白菜";
            ctc.Quantity = "5";
            ctc.Price = "18";
            ctc.ProductUnitName = "PCS";

            b.isCreateTempCart(ctc);

            UpdateTempCart utc = new UpdateTempCart();
            utc.ProductCustomizeID = "0002";
            utc.ProductName = "白菜2";
            utc.Quantity = "50";
            utc.Price = "180";
            utc.ProductUnitName = "單位";
            utc.UpdateID = "55";
            

            b.isUpdateTempCart(utc);

            return;

            b.getProduct(1);

           

            CreateProductType cpt = new CreateProductType();
            cpt.ProductTypeName = "進口類";
            

            bm = b.isCreateProductType(cpt);

            UpdateProductType upt = new UpdateProductType();
            //test
            upt.UpdateID = "5";

            upt.ProductTypeName = "進口";
            

            bm = b.isUpdateProductType(upt);

            //bm = b.isUpdateProduct("0001", "00011", "特級香蕉", "");

            DeleteProductType dpt = new DeleteProductType();
            dpt.DeleteProductTypeID = "6";

            bm = b.isDeleteProductType(dpt);



           

            //bm = b.isUpdateProduct("0001", "00011", "特級香蕉", "");

            DeleteProduct del = new DeleteProduct();
            del.DeleteProductCustomizeID = "000111";

            bm = b.isDeleteProduct(del);

            ProductViewModel p = new ProductViewModel();
            p = b.getOneProduct("0001");

            List<ProductViewModel> lp = new List<ProductViewModel>();
            //lp = b.getProduct(1, "00", "", "菜", 0);

            b.getTempCart("fd0531a65184448fabd22d4881bb58e6");
            b.addCart("q234", "0001", "bb", "10", "0", "PCS");

           

            return;

            //ClassSqlclient cdb = new ClassSqlclient();
            //cdb.ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TradeMarket;Persist Security Info=True;User ID=rd;Password=allone";
            //cdb.isOpen();

            
            

            if (x != null)
            {
                MessageBox.Show(x.Account);
            }
            else
            {
                MessageBox.Show("error");
            }


            //List<ProductTypeViewModel> l = new List<ProductTypeViewModel>();
            //l = b.getProductType();

            //List<ProductViewModel> lp = new List<ProductViewModel>();

            //lp = b.getProductByType("水果");


            //lp = b.getProductByKeyword("蕉");

        }
    }
}
