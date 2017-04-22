using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;


namespace LibraryTradeMarket
{
    public class Business
    {

        public const string DATETIME_FORMAT_SHORT = "yyyy/MM/dd";
        public const string DATETIME_FORMAT_24H = "yyyy/MM/dd HH:mm:ss";
        public const string DATETIME_FORMAT_24H_MILLISECOND = "yyyy/MM/dd HH:mm:ss.FFFFFFF";
        public const string DATETIME_FORMAT_START_DATE = "yyyy/MM/dd 00:00:00";
        public const string DATETIME_FORMAT_END_DATE = "yyyy/MM/dd 23:59:59";
        public const string DATETIME_FORMAT_24H_NOSYMBOL = "yyyyMMddHHmmss";

        public void addErrorLog(string controller, string action, string contents)
        {
            TradeMarketEntities db = new TradeMarketEntities();
            error_log newErrorLog = new error_log();
            newErrorLog.action = action;            
            newErrorLog.update_date = DateTime.Now;
            newErrorLog.controller = controller;
            newErrorLog.contents = contents;
            db.Entry(newErrorLog).State = EntityState.Added;
            db.SaveChanges();
        }

        public MemberViewModel isLogin(string account, string password)
        {
            MemberViewModel memberViewModel = new MemberViewModel();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    var members = from m in db.member
                                  where m.account == account && m.password == password
                                  select m;

                    if (members != null)
                    {
                        var member = members
                        .FirstOrDefault();

                        if (member != null)
                        {
                            if (member.account == account && member.password == password)
                            {
                                //ModelState.AddModelError("", "登入成功");
                                //return admin;
                                memberViewModel.Account = member.account;
                                memberViewModel.Name = member.member_name;
                                memberViewModel.Role = member.role;
                            }
                            
                        }
                        
                    }
                    



                }

            }
            catch (Exception ex)
            {
                addErrorLog("", "isLogin", ex.Message);

                //TradeMarketEntities db = new TradeMarketEntities();
                //error_log newErrorLog = new error_log();
                //newErrorLog.action = "isLogin";
                //newErrorLog.contents = "";
                //newErrorLog.update_date = DateTime.Now;
                //newErrorLog.controller = "";
                //newErrorLog.contents = ex.Message;
                //db.Entry(newErrorLog).State = EntityState.Added;
                //db.SaveChanges();                

            }

            return memberViewModel;

        }

        public IntMessage addCart(string tempOrderID,
            string productCustomizeID,string productName,string quantity,string price,
            string unitName
            )
        {
            IntMessage bm = new IntMessage();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                   


                    temp_cart newTempCart = new temp_cart();

                    newTempCart.temp_order_id = tempOrderID;
                    newTempCart.product_customize_id = productCustomizeID;
                    newTempCart.product_name = productName;
                    newTempCart.quantity = Utility.getIntOrDefault(quantity,1);
                    newTempCart.price = Utility.getIntOrDefault(price, 0);
                    newTempCart.unit_name = unitName;
                    newTempCart.update_date = DateTime.Now;
                    //memo備用欄位先設為空

                    newTempCart.memo = "";

                    db.Entry(newTempCart).State = EntityState.Added;

                    db.SaveChanges();


                    var carts = from c in db.temp_cart
                                  where c.temp_order_id == tempOrderID 
                                  select c.temp_order_id;

                    bm.Result = carts.Count();

                }

            }
            catch (Exception ex)
            {
                addErrorLog("", "AddCart", ex.Message);
                bm.Message = ex.Message;
                //TradeMarketEntities db = new TradeMarketEntities();
                //error_log newErrorLog = new error_log();
                //newErrorLog.action = "isLogin";
                //newErrorLog.contents = "";
                //newErrorLog.update_date = DateTime.Now;
                //newErrorLog.controller = "";
                //newErrorLog.contents = ex.Message;
                //db.Entry(newErrorLog).State = EntityState.Added;
                //db.SaveChanges();                

            }

            return bm;

        }

        public List<ProductTypeViewModel> getProductType()
        {
            List<ProductTypeViewModel> list = new List<ProductTypeViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    var product_types = from t in db.product_type
                                        select new ProductTypeViewModel { ProductTypeName = t.product_type_name };



                    if (product_types != null)
                    {
                        list = product_types.ToList();
                        //return product_types.ToList();
                    }
                    //else
                    //{
                    //    return null;
                    //}

                }
            }
            catch (Exception ex)
            {

                addErrorLog("", "isLogin", ex.Message);
            }

            return list;
        }

        public List<ProductViewModel> getProductByType(string productType)
        {
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    //from c in Categories
                    //join o in Products on c.CategoryID equals o.CategoryID

                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where t.product_type_name == productType
                                   select new ProductViewModel {
                                       ProductCustomizeID =p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name
                                   };



                    if (products != null)
                    {
                        list = products.ToList();
                    }
                    else
                    {
                        list = null;
                    }

                }
            }
            catch (Exception ex)
            {

                addErrorLog("", "getProductByType", ex.Message);
            }

            return list;

        }

        public List<ProductViewModel> getProductByKeyword(string keyword)
        {
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    //from c in Categories
                    //join o in Products on c.CategoryID equals o.CategoryID

                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where t.product_type_name.Contains(keyword) || p.product_name.Contains(keyword) || p.short_product_name.Contains(keyword)
                                   select new ProductViewModel
                                   {
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name
                                   };



                    if (products != null)
                    {
                        list = products.ToList();
                    }
                    else
                    {
                        list = null;
                    }

                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getProductByKeyword", ex.Message);

            }

            return list;

        }

        public int getProductCountByTypeName(string productTypeName)
        {
            int count = 0;
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    //from c in Categories
                    //join o in Products on c.CategoryID equals o.CategoryID

                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where t.product_type_name == productTypeName
                                   select p.product_customize_id;
                                   



                    if (products != null)
                    {
                        count = products.Count();
                    }
                    //else
                    //{
                    //    list = null;
                    //}

                }
            }
            catch (Exception ex)
            {

                addErrorLog("", "getProductCountByTypeName", ex.Message);
            }

            return count;

        }

        public int getProductCountByKeyword(string keyword)
        {
            int count = 0;
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    //from c in Categories
                    //join o in Products on c.CategoryID equals o.CategoryID

                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where t.product_type_name.Contains(keyword) || p.product_name.Contains(keyword) || p.short_product_name.Contains(keyword)
                                   select p.product_customize_id
                                   ;





                    if (products != null)
                    {
                        count = products.Count();
                    }

                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getProductByKeyword", ex.Message);

            }

            return count;

        }

        public List<temp_cart> getTempCart(string tempID)
        {
            List<temp_cart> list = new List<temp_cart>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    //from c in Categories
                    //join o in Products on c.CategoryID equals o.CategoryID

                    var carts = from c in db.temp_cart
                                where c.temp_order_id == tempID
                                select c;


                    if (carts != null)
                    {
                        list = carts.ToList();
                    }
                    else
                    {
                        list = null;
                    }

                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getTempCart", ex.Message);

            }
            return list;
        }

        public ProductViewModel getOneProduct(string customizeID)
        {
            ProductViewModel result = new ProductViewModel();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where p.product_customize_id == customizeID
                                   select new ProductViewModel
                                   {
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name
                                   };

                    var product = products
                        .FirstOrDefault();


                    result = product;

                }
            }
            catch (Exception ex)
            {

                addErrorLog("", "getOneProduct", ex.Message);
            }

            return result;

        }

        public List<ProductViewModel> getProduct(int departmentID, string customizeID = "", string productName = "", string productTypeName = "", int resultCountOnly = 0)
        {
            //ProductApiResponseData response = new ProductApiResponseData();
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {


                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   select new ProductViewModel
                                   {
                                       DepartmentID = p.department_id,
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name
                                   };
                    //ere p.product_customize_id == customizeID

                    //p.department_id == departmentID                                   
                    //&&
                    //p.product_customize_id.Contains(customizeID)
                    //&&
                    //p.product_name.Contains(productName)
                    //&&
                    //t.product_type_name.Contains(productTypeName)

                    var query = products.AsQueryable();

                    departmentID = Utility.getNumericInt(departmentID.ToString());

                    if (departmentID > 0)
                    {
                        query = query.Where(table => table.DepartmentID == departmentID);

                    }

                    if (!string.IsNullOrEmpty(productTypeName))
                    {
                        query = query.Where(table => table.ProductTypeName.Contains(productTypeName));
                    }

                    if (!string.IsNullOrEmpty(customizeID))
                    {
                        query = query.Where(table => table.ProductCustomizeID.Contains(customizeID));
                    }

                    if (!string.IsNullOrEmpty(productName))
                    {
                        query = query.Where(table => table.ProductName.Contains(productName));
                    }

                    //var products = from table in query
                    //               select table;


                    var newProducts = from table in query
                                      select new ProductViewModel
                                   {
                                       DepartmentID = table.DepartmentID,
                                       ProductCustomizeID = table.ProductCustomizeID,
                                       ProductName = table.ProductName,
                                       ProductUnitName = table.ProductUnitName,
                                       ProductTypeName = table.ProductTypeName
                                   };

                    //var product = products
                    //    .FirstOrDefault();


                    list = newProducts.ToList();
                    //if (product == null)
                    //{
                    //    response.Result = "1";
                    //    response.Message = "查無資料";
                    //}
                    //else
                    //{
                    //    response.Result = "1";
                    //    response.Message = "";
                    //    response.listOfProduct = products.ToList();
                    //}
                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getProduct", ex.Message);
            }

            return list;
        }

        public int getProductCount(int departmentID, string customizeID = "", string productName = "", string productTypeName = "", int resultCountOnly = 0)
        {
            //ProductApiResponseData response = new ProductApiResponseData();
            List<ProductViewModel> list = new List<ProductViewModel>();
            int count = 0;
            try
            {
                using (var db = new TradeMarketEntities())
                {


                    var products = from p in db.product
                                   join t in db.product_type on p.product_type_id equals t.id
                                   select new ProductViewModel
                                   {
                                       DepartmentID = p.department_id,
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name
                                   };
                    //ere p.product_customize_id == customizeID

                    //p.department_id == departmentID                                   
                    //&&
                    //p.product_customize_id.Contains(customizeID)
                    //&&
                    //p.product_name.Contains(productName)
                    //&&
                    //t.product_type_name.Contains(productTypeName)

                    var query = products.AsQueryable();

                    departmentID = Utility.getNumericInt(departmentID.ToString());

                    if (departmentID > 0)
                    {
                        query = query.Where(table => table.DepartmentID == departmentID);

                    }

                    if (!string.IsNullOrEmpty(productTypeName))
                    {
                        query = query.Where(table => table.ProductTypeName.Contains(productTypeName));
                    }

                    if (!string.IsNullOrEmpty(customizeID))
                    {
                        query = query.Where(table => table.ProductCustomizeID.Contains(customizeID));
                    }

                    if (!string.IsNullOrEmpty(productName))
                    {
                        query = query.Where(table => table.ProductName.Contains(productName));
                    }

                    //var products = from table in query
                    //               select table;


                    var newProducts = from table in query
                                      select new ProductViewModel
                                      {
                                          DepartmentID = table.DepartmentID,
                                          ProductCustomizeID = table.ProductCustomizeID,
                                          ProductName = table.ProductName,
                                          ProductUnitName = table.ProductUnitName,
                                          ProductTypeName = table.ProductTypeName
                                      };

                    //var product = products
                    //    .FirstOrDefault();


                    list = newProducts.ToList();

                    count = list.Count;
                    //if (product == null)
                    //{
                    //    response.Result = "1";
                    //    response.Message = "查無資料";
                    //}
                    //else
                    //{
                    //    response.Result = "1";
                    //    response.Message = "";
                    //    response.listOfProduct = products.ToList();
                    //}
                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getProduct", ex.Message);
            }

            return count;
        }


        public BooleanMessage isCreateProduct(CreateProduct createProduct)
        {
      
            BooleanMessage bm = new BooleanMessage();

            try
            {

                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckCreateProduct(createProduct);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }

                TradeMarketEntities db = new TradeMarketEntities();
               
                //int productTypeID = 1;

                //var product_types = from t in db.product_type
                //                    where t.product_type_name == createProduct.ProductTypeName
                //                    select t;

                //if (product_types != null)
                //{
                //    var product_type = product_types
                //        .FirstOrDefault();

                //    if (product_type != null)
                //    {
                //        productTypeID = product_type.id;
                //    }
                //}

                product newProduct = new product();
                newProduct.product_type_id = Utility.getIntOrDefault(createProduct.ProductTypeID, 1);                
                newProduct.update_date = DateTime.Now;
                newProduct.department_id = Utility.getIntOrDefault(createProduct.DepartmentID, 1);
                newProduct.product_customize_id = createProduct.CustomizeID;
                newProduct.product_name = createProduct.ProductName;
                newProduct.update_member_id = Utility.getIntOrDefault(createProduct.UpdateMemberID, 1);
                db.Entry(newProduct).State = EntityState.Added;

                db.SaveChanges();

                bm.Result = true;



            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }
            return bm;
        }

        public BooleanMessage isCheckCreateProduct(CreateProduct createProduct)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(createProduct.DepartmentID))
                {
                    bm.Message += "請輸入部門編號\n";
                }

                if (String.IsNullOrEmpty(createProduct.ProductTypeID))
                {
                    bm.Message += "請輸入產品分類\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();

                    int productTypeID = Utility.getIntOrDefault(createProduct.ProductTypeID, 0);

                    int count = db.product_type.Where(o => o.id == productTypeID).Count();
                                 
                                 
                    if (count == 0)
                    {
                        bm.Message += "產品分類不存在\n";
                    }
                }

                if (String.IsNullOrEmpty(createProduct.CustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();
                    int count = db.product.Where(o=>o.product_customize_id == createProduct.CustomizeID).Count();
                    if (count > 0)
                    {
                        bm.Message += "產品代碼重覆\n";
                    }
                }

                if (String.IsNullOrEmpty(createProduct.ProductName))
                {
                    bm.Message += "請輸入產品名稱\n";
                }

                if (bm.Message == "")
                {
                    bm.Result = true;
                }
               

            }
            catch (Exception ex)
            {
                if (bm.Message == "")
                {
                    bm.Message = ex.Message;
                }
                else
                {
                    bm.Message = bm.Message + "\n" + ex.Message;
                }
            }

            return bm;
        }

        public BooleanMessage isUpdateProduct(UpdateProduct updateProduct)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                //Check Before Update
                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckUpdateProduct(updateProduct);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }


                TradeMarketEntities db = new TradeMarketEntities();

                var products = from a in db.product
                               where a.product_customize_id == updateProduct.UpdateCustomizeID
                               select a;

                var product = products.FirstOrDefault();

                if (product != null)
                {
                    //product newProduct = db.product.FirstOrDefault(o => o.product_customize_id == updateProduct.UpdateCustomizeID);
                    product.update_date = DateTime.Now;
                    product.product_customize_id = updateProduct.CustomizeID;
                    product.product_name = updateProduct.ProductName;
                    product.update_member_id = Utility.getIntOrDefault(updateProduct.UpdateMemberID, 1);
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    bm.Result = true;
                }
                else
                {
                    bm.Message = "該代碼查無資料";
                }

            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isUpdateProduct", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCheckUpdateProduct(UpdateProduct updateProduct)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(updateProduct.DepartmentID))
                {
                    bm.Message += "請輸入部門編號\n";
                }

                if (String.IsNullOrEmpty(updateProduct.UpdateCustomizeID))
                {
                    bm.Message += "請輸入欲更新產品的代碼\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();
                    //var count = (from t in db.product
                    //             where t.product_customize_id == updateProduct.UpdateCustomizeID
                    //             select t).Count();
                    int count = db.product.Where(o => o.product_customize_id == updateProduct.CustomizeID).Count();
                    if (count == 0)
                    {
                        bm.Message += "查無欲更新產品的代碼\n";
                    }

                }

                if (String.IsNullOrEmpty(updateProduct.ProductTypeID))
                {
                    bm.Message += "請輸入產品分類\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();

                    int productTypeID = Utility.getIntOrDefault(updateProduct.ProductTypeID, 0);

                    int count = db.product_type.Where(o => o.id == productTypeID).Count();


                    if (count == 0)
                    {
                        bm.Message += "產品分類不存在\n";
                    }
                }

                if (String.IsNullOrEmpty(updateProduct.CustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();
                    //var count = (from t in db.product
                    //             where t.product_customize_id == updateProduct.CustomizeID
                    //             select t).Count();


                    //if (count > 0)
                    //{
                    //    bm.Message += "產品代碼重覆\n";
                    //}

                    int count = db.product.Where(o => o.product_customize_id == updateProduct.CustomizeID).Count();
                    if (count > 0)
                    {
                        bm.Message += "產品代碼重覆\n";
                    }

                }

                if (String.IsNullOrEmpty(updateProduct.ProductName))
                {
                    bm.Message += "請輸入產品名稱\n";
                }





                if (bm.Message == "")
                {
                    bm.Result = true;
                }


            }
            catch (Exception ex)
            {
                if (bm.Message == "")
                {
                    bm.Message = ex.Message;
                }
                else
                {
                    bm.Message = bm.Message + "\n" + ex.Message;
                }
            }

            return bm;
        }

        public BooleanMessage isDeleteProduct(string deleteCustomizeID)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {

                //Check Before Delete

                TradeMarketEntities db = new TradeMarketEntities();

                var products = from a in db.product
                               where a.product_customize_id == deleteCustomizeID
                               select a;

                var product = products.FirstOrDefault();

                if (product != null)
                {
                    product deleteProduct = db.product.FirstOrDefault(o=>o.product_customize_id==deleteCustomizeID);
                    db.product.Remove(deleteProduct);
                    db.SaveChanges();

                    bm.Result = true;
                }


            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isDeleteProduct", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCreateProductType(CreateProductType createProductType)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {

                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckCreateProductType(createProductType);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }

                TradeMarketEntities db = new TradeMarketEntities();

                

                product_type newProductType = new product_type();
                newProductType.product_type_name = createProductType.ProductTypeName;                
                db.Entry(newProductType).State = EntityState.Added;

                db.SaveChanges();

                bm.Result = true;



            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }
            return bm;
        }

        public BooleanMessage isCheckCreateProductType(CreateProductType createProductType)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(createProductType.ProductTypeName))
                {
                    bm.Message += "請輸入產品分類\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();

                    int count = db.product_type.Where(o => o.product_type_name == createProductType.ProductTypeName).Count();
                    if (count > 0)
                    {
                        bm.Message += "產品分類重覆\n";
                    }

                }


                if (bm.Message == "")
                {
                    bm.Result = true;
                }


            }
            catch (Exception ex)
            {
                if (bm.Message == "")
                {
                    bm.Message = ex.Message;
                }
                else
                {
                    bm.Message = bm.Message + "\n" + ex.Message;
                }
            }

            return bm;
        }

        public BooleanMessage isUpdateProductType(UpdateProductType updateProductType)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                //Check Before Update
                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckUpdateProductType(updateProductType);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }


                TradeMarketEntities db = new TradeMarketEntities();

                int updateID = Utility.getIntOrDefault(updateProductType.UpdateID, 0);

                var product_types = from a in db.product_type
                               where a.id == updateID
                               select a;

                var product_type = product_types.FirstOrDefault();

                if (product_type != null)
                {
                    product_type newProductType = db.product_type.FirstOrDefault(o => o.id == updateID);
                    
                    newProductType.product_type_name = updateProductType.ProductTypeName;
                    
                    db.Entry(product_type).State = EntityState.Modified;
                    db.SaveChanges();

                    bm.Result = true;
                }
                else
                {
                    bm.Message = "該編號查無資料";
                }

            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isUpdateProductType", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCheckUpdateProductType(UpdateProductType updateProductType)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(updateProductType.UpdateID))
                {
                    bm.Message += "請輸入欲修改的分類編號\n";
                }

                if (String.IsNullOrEmpty(updateProductType.ProductTypeName))
                {
                    bm.Message += "請輸入產品分類\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();


                    int updateID = Utility.getIntOrDefault(updateProductType.UpdateID, 0);

                    int count = db.product_type.Where(o => o.product_type_name == updateProductType.ProductTypeName && o.id != updateID).Count();
                    if (count >0)
                    {
                        bm.Message += "產品分類重覆\n";
                    }

                }


                if (bm.Message == "")
                {
                    bm.Result = true;
                }


            }
            catch (Exception ex)
            {
                if (bm.Message == "")
                {
                    bm.Message = ex.Message;
                }
                else
                {
                    bm.Message = bm.Message + "\n" + ex.Message;
                }
            }

            return bm;
        }

        public BooleanMessage isDeleteProductType(string deleteID)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {

                //Check Before Delete

                TradeMarketEntities db = new TradeMarketEntities();

                int newDeleteID = 0;
                newDeleteID = Utility.getIntOrDefault(deleteID, 0);

                var products = from a in db.product_type
                               where a.id == newDeleteID
                               select a;

                var product = products.FirstOrDefault();

                if (product != null)
                {
                    product_type deleteProductType = db.product_type.FirstOrDefault(o => o.id == newDeleteID);
                    db.product_type.Remove(deleteProductType);
                    db.SaveChanges();

                    bm.Result = true;
                }


            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isDeleteProductType", ex.Message);
            }

            return bm;
        }

        //get product type

        //get product by type

        //get product by keyword

        //isUpdateOnSales

        //getBuyerOrder

        //getSellerOrder

        //getSellerOrderGroupByDate

        //getSellerOrderByBuyer

        //getSellerOrderGroupByBuyer

        //isCheckOut

        //getOrder

    }
}
