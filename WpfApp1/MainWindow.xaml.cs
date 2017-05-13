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

            Business b = new Business();


            b.getProduct(1);

            return;

            BooleanMessage bm = new BooleanMessage();

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



            CreateProduct cp = new CreateProduct();
            cp.DepartmentID = "1";
            cp.ProductCustomizeID = "0009";
            cp.ProductName = "3斤袋";
            cp.ProductTypeID = "雜類";
            cp.UpdateMemberID = "1";

            bm = b.isCreateProduct(cp);

            UpdateProduct up = new UpdateProduct();

            up.UpdateProductCustomizeID = "0004";

            up.DepartmentID = "1";
            up.ProductCustomizeID = "00045";
            up.ProductName = "高山高麗菜";
            up.ProductTypeID = "雜類";
            up.UpdateMemberID = "1";

            bm = b.isUpdateProduct(up);

            //bm = b.isUpdateProduct("0001", "00011", "特級香蕉", "");

            DeleteProduct del = new DeleteProduct();
            del.DeleteProductCustomizeID = "000111";

            bm = b.isDeleteProduct(del);

            ProductViewModel p = new ProductViewModel();
            p = b.getOneProduct("0001");

            List<ProductViewModel> lp = new List<ProductViewModel>();
            lp = b.getProduct(1, "00", "", "菜", 0);

            b.getTempCart("fd0531a65184448fabd22d4881bb58e6");
            b.addCart("q234", "0001", "bb", "10", "0", "PCS");

            var x = b.isLogin("admin", LibraryTradeMarket.Utility.getSecretCode("1"));
            if (x != null)
            {
                MessageBox.Show(x.Account);
            }
            else
            {
                MessageBox.Show("error");
            }

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
