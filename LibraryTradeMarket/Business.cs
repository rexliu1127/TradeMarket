using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Data.Entity.Validation;

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

        public const int DEFAULT_DEPARTMENT_ID = 1;

        
        private const string KEY1 = "0531";
        private const string KEY2 = "2017";

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
                                memberViewModel.ID = member.id.ToString();
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
                addErrorLog("Business", "isLogin", ex.Message+"ac:"+account+"pw:"+password );

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

        public IntMessage isCreateTempCart(CreateTempCart createTempCart)
        {
            IntMessage bm = new IntMessage();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    temp_cart newTempCart = new temp_cart();

                    newTempCart.temp_order_id = createTempCart.TempOrderID;
                    newTempCart.product_customize_id = createTempCart.ProductCustomizeID;
                    newTempCart.product_name = createTempCart.ProductName;
                    newTempCart.quantity = Utility.getIntOrDefault(createTempCart.Quantity, 1);
                    newTempCart.price = Utility.getIntOrDefault(createTempCart.Price, 0);
                    newTempCart.unit_name = createTempCart.ProductUnitName;
                    newTempCart.update_date = DateTime.Now;
                    //memo備用欄位先設為空

                    newTempCart.memo = "";

                    db.Entry(newTempCart).State = EntityState.Added;

                    db.SaveChanges();


                    var carts = from c in db.temp_cart
                                where c.temp_order_id == createTempCart.TempOrderID
                                select c.temp_order_id;

                    bm.Result = carts.Count();

                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "isCreateTempCart", ex.Message);
                bm.Message = ex.Message;
            }

            return bm;

        }

        public BooleanMessage isUpdateTempCart(UpdateTempCart updateTempCart)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                //Check Before Update
                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckUpdateTempCart(updateTempCart);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }


                TradeMarketEntities db = new TradeMarketEntities();

                int updateID = Utility.getIntOrDefault(updateTempCart.UpdateID, 0);

                var temp_carts = from a in db.temp_cart
                                    where a.id == updateID
                                    select a;

                var temp_cart = temp_carts.FirstOrDefault();

                if (temp_cart != null)
                {
                    temp_cart newTempCart = db.temp_cart.FirstOrDefault(o => o.id == updateID);

                   
                    newTempCart.product_customize_id = updateTempCart.ProductCustomizeID;
                    newTempCart.product_name = updateTempCart.ProductName;
                    newTempCart.quantity = Utility.getIntOrDefault(updateTempCart.Quantity, 1);
                    newTempCart.price = Utility.getIntOrDefault(updateTempCart.Price, 0);
                    newTempCart.unit_name = updateTempCart.ProductUnitName;
                    newTempCart.update_date = DateTime.Now;


                    db.Entry(temp_cart).State = EntityState.Modified;
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
                addErrorLog("", "isUpdateTempCart", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCheckUpdateTempCart(UpdateTempCart updateTempCart)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(updateTempCart.UpdateID))
                {
                    bm.Message += "請輸入欲修改的編號\n";
                }

                if (String.IsNullOrEmpty(updateTempCart.ProductCustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
                }

                if (String.IsNullOrEmpty(updateTempCart.ProductName))
                {
                    bm.Message += "請輸入產品名稱\n";
                }

                if (String.IsNullOrEmpty(updateTempCart.Quantity))
                {
                    bm.Message += "請輸入數量\n";
                }

                if (String.IsNullOrEmpty(updateTempCart.Price))
                {
                    bm.Message += "請輸入價格\n";
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

        public BooleanMessage isDeleteTempCart(DeleteTempCart deleteTempCartObj)
        {
            BooleanMessage bm = new BooleanMessage();
            try
            {
                TradeMarketEntities db = new TradeMarketEntities();

                int newDeleteID = 0;
                newDeleteID = Utility.getIntOrDefault(deleteTempCartObj.DeleteID, 0);

                var tempCarts = from a in db.temp_cart
                               where a.id == newDeleteID
                               select a;

                var tempCart = tempCarts.FirstOrDefault();

                if (tempCart != null)
                {
                    temp_cart deleteTempCart = db.temp_cart.FirstOrDefault(o => o.id == newDeleteID);
                    db.temp_cart.Remove(deleteTempCart);
                    db.SaveChanges();

                    bm.Result = true;
                }

            }
            catch (Exception ex)
            {
                addErrorLog("", "isDeleteTempCart", ex.Message);
                bm.Message = ex.Message;
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
                                        join t2 in db.product_type on t.id equals t2.super_type_id
                                        select new ProductTypeViewModel {
                                            SuperTypeID = t2.super_type_id.ToString(),
                                            SuperTypeName = t.product_type_name,
                                            ProductTypeID = t.id.ToString(),                                            
                                            ProductTypeName = t2.product_type_name };



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

                addErrorLog("", "getProductType", ex.Message);
            }

            return list;
        }

        public List<ProductTypeViewModel> getProductSuperType()
        {
            List<ProductTypeViewModel> list = new List<ProductTypeViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    var product_types = from t in db.product_type
                                        where t.super_type_id == 0
                                        select new ProductTypeViewModel
                                        {
                                            SuperTypeID = t.super_type_id.ToString(),
                                            SuperTypeName = t.product_type_name,
                                            ProductTypeID = t.id.ToString(),
                                            ProductTypeName = t.product_type_name
                                        };



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

                addErrorLog("", "getProductSuperType", ex.Message);
            }

            return list;
        }

        //public List<ProductTypeViewModel> getProductChildType()
        //{
        //    List<ProductTypeViewModel> list = new List<ProductTypeViewModel>();
        //    try
        //    {
        //        using (var db = new TradeMarketEntities())
        //        {
        //            var product_types = from t in db.product_type
        //                                join t2 in db.product_type on t.id equals t2.super_type_id                                        
        //                                select new ProductTypeViewModel
        //                                {
        //                                    SuperTypeID = t.super_type_id.ToString(),
        //                                    SuperTypeName = t.product_type_name,
        //                                    ProductTypeID = t.id.ToString(),
        //                                    ProductTypeName = t2.product_type_name
        //                                };



        //            if (product_types != null)
        //            {
        //                list = product_types.ToList();
        //                //return product_types.ToList();
        //            }
        //            //else
        //            //{
        //            //    return null;
        //            //}

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        addErrorLog("", "getProductChildType", ex.Message);
        //    }

        //    return list;
        //}

        public List<ProductViewModel> getProductByProductTypeName(string productType)
        {
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                    //from c in Categories
                    //join o in Products on c.CategoryID equals o.CategoryID

                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where t.product_type_name == productType
                                   select new ProductViewModel {
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeID = p.product_type_id.ToString(),
                                       ProductTypeName = t.product_type_name,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
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

                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where t.product_type_name.Contains(keyword) || p.product_name.Contains(keyword) || p.short_product_name.Contains(keyword)
                                   select new ProductViewModel
                                   {
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeID = p.product_type_id.ToString(),
                                       ProductTypeName = t.product_type_name,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
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
                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   where p.product_customize_id == customizeID
                                   select new ProductViewModel
                                   {
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
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

        public List<ProductViewModel> getProduct(int departmentID, string customizeID = "", string productName = "", string productTypeName = "")
        {
            //ProductApiResponseData response = new ProductApiResponseData();
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {


                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   select new ProductViewModel
                                   {
                                       DepartmentID = p.department_id,
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeID = p.product_type_id.ToString(),
                                       ProductTypeName = t.product_type_name,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
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
                                        ProductTypeID = table.ProductTypeID,
                                        ProductTypeName = table.ProductTypeName,
                                        UpdateDate = table.UpdateDate
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

        //currentPage = 1;
        //    totalPage = 1;
        //    totalCount = 0;
        //    showCount = 10;
        public ProductPagingViewModel getProductPaging(int departmentID, string customizeID = "", 
            string productName = "", string productTypeName = "",string sortColumn= "UpdateDate" ,string orderBy="ASC", 
            int currentPage=1, int showCount=10)
        {
            //ProductApiResponseData response = new ProductApiResponseData();
            //List<ProductViewModel> list = new List<ProductViewModel>();


            ProductPagingViewModel productPaging = new ProductPagingViewModel();

            try
            {
                using (var db = new TradeMarketEntities())
                {


                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id                                   
                                   select new ProductViewModel
                                   {
                                       DepartmentID = p.department_id,
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeID = p.product_type_id.ToString(),
                                       ProductTypeName = t.product_type_name,
                                       ImageUrl = p.image_url,                                       
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
                                   };

                    var li = products.ToList();

                    var query = products.AsQueryable();

                    departmentID = Utility.getNumericInt(departmentID.ToString());

                    if (departmentID > 0)
                    {
                        query = query.Where(table => table.DepartmentID == departmentID);

                    }

                    if (!string.IsNullOrEmpty(productTypeName) && productTypeName!="全部")
                    {
                        query = query.Where(table => table.ProductTypeName.Contains(productTypeName));
                    }

                    if (!string.IsNullOrEmpty(customizeID))
                    {
                        query = query.Where(table => table.ProductCustomizeID.Contains(customizeID));
                    }

                    if (!string.IsNullOrEmpty(productName) && productName!="*")
                    {
                        query = query.Where(table => table.ProductName.Contains(productName));
                    }


                    var newProducts = from table in query
                                      select new ProductViewModel
                                      {
                                          DepartmentID = table.DepartmentID,
                                          ProductCustomizeID = table.ProductCustomizeID,
                                          ProductName = table.ProductName,
                                          ProductUnitName = table.ProductUnitName,
                                          ProductTypeID = table.ProductTypeID,
                                          ProductTypeName = table.ProductTypeName,
                                          ImageUrl = table.ImageUrl,
                                          OnSales = "0",
                                          UpdateDate = table.UpdateDate
                                          
                                      };


                    var salesProduct = from o in db.product_on_sales
                                       where o.department_id == departmentID
                                       select o.product_customize_id;

                    List<string> listOfSalesProduct = new List<string>();
                    listOfSalesProduct = salesProduct.ToList();


                    List<ProductViewModel> listOfNewProduct = new List<ProductViewModel>();
                    listOfNewProduct = newProducts.ToList();

                    foreach (string productCustomizeID in listOfSalesProduct)
                    {

                        foreach (var item in listOfNewProduct.Where(w => w.ProductCustomizeID == productCustomizeID && w.DepartmentID == departmentID))
                        {
                            item.OnSales = "1";                            
                        }                       
                    }
                    

                    var newProductsQueryable = listOfNewProduct.AsQueryable();


                    int outCurrentPage =currentPage;
                    int outTotalPage=1 ;
                    int outShowCount=10 ;
                    int outTotalCount=0 ;

                    bool orderASC=true;

                    if (orderBy.ToUpper() == "DESC")
                    {
                        orderASC = false;
                    }
                    
                    switch (sortColumn)
                    {
                        case "ProductTypeName":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductTypeName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "ProductCustomizeID":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductCustomizeID, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "ProductName":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "UpdateDate":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.UpdateDate, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "OnSales":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.OnSales, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        default:
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductCustomizeID, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                    }

                    


                    productPaging.ListOfProduct = newProducts.ToList();
                    productPaging.CurrentPage = outCurrentPage;
                    productPaging.TotalPage = outTotalPage;
                    productPaging.ShowCount = outShowCount;
                    productPaging.TotalCount = outTotalCount;

                    //list = newProducts.ToList();
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

            return productPaging;
        }


        public ProductTypePagingViewModel getProductTypePaging(
    string superTypeName = "", string productTypeName = "", string sortColumn = "ProductTypeName", string orderBy = "ASC",
    int currentPage = 1, int showCount = 10)
        {


            ProductTypePagingViewModel productTypePaging = new ProductTypePagingViewModel();

            try
            {
                using (var db = new TradeMarketEntities())
                {


                    var product_types =

                    from e in db.product_type
                    join s in db.product_type
                    on e.super_type_id equals s.id into subGrp
                    from s in subGrp.DefaultIfEmpty()
                    select new
                    {
                        ProductTypeID = e.id.ToString(),
                        ProductTypeName = e.product_type_name,
                        SuperTypeID = s.super_type_id.ToString(),
                        SuperTypeName = s.product_type_name,
                        //e.DeptId,
                        //e.UserId,
                        //e.UserName,
                        //Substitute = s.UserName ?? "NA"
                    };

                    //from t in db.product_type
                    //join t2 in db.product_type on t.id equals t2.super_type_id into tmp
                    //from r in tmp.DefaultIfEmpty()
                    //select new ProductTypeViewModel
                    //{
                    //    ProductTypeID = t.id.ToString(),
                    //    ProductTypeName = t.product_type_name,
                    //    SuperTypeID = r.super_type_id.ToString(),
                    //    SuperTypeName = r.product_type_name,
                    //};

//from m in db.product_type
//join o in db.product_type on m.id equals o.super_type_id into ps
//from t in ps.DefaultIfEmpty()
//select new ProductTypeViewModel
//{
//    ProductTypeID = t.id.ToString(),
//    ProductTypeName = t.product_type_name,
//    SuperTypeID = t.super_type_id.ToString(),
//    SuperTypeName = t.product_type_name

//};

//var product_types = from t in db.product_type
//                    join t2 in db.product_type on t.id equals t2.super_type_id
//                    select new ProductTypeViewModel
//                    {
//                        SuperTypeID = t.super_type_id.ToString(),
//                        SuperTypeName = t.product_type_name,
//                        ProductTypeID = t.id.ToString(),
//                        ProductTypeName = t2.product_type_name
//                    };


                    var li = product_types.ToList();

                    var query = product_types.AsQueryable();

                    

                    if (!string.IsNullOrEmpty(productTypeName))
                    {
                        query = query.Where(table => table.ProductTypeName.Contains(productTypeName));
                    }

                    if (!string.IsNullOrEmpty(superTypeName))
                    {
                        query = query.Where(table => table.SuperTypeName.Contains(superTypeName));
                    }




                    var newProducts = from table in query
                                      select new ProductTypeViewModel
                                      {
                                          SuperTypeID = table.SuperTypeID,
                                          SuperTypeName = table.SuperTypeName,
                                          ProductTypeID = table.ProductTypeID,
                                          ProductTypeName = table.ProductTypeName,
                                          

                                      };
                    List<ProductTypeViewModel> listOfNewProduct = new List<ProductTypeViewModel>();
                    listOfNewProduct = newProducts.ToList();


                    

                    var newProductsQueryable = listOfNewProduct.AsQueryable();


                    int outCurrentPage = currentPage;
                    int outTotalPage = 1;
                    int outShowCount = 10;
                    int outTotalCount = 0;

                    bool orderASC = true;

                    if (orderBy.ToUpper() == "DESC")
                    {
                        orderASC = false;
                    }

                    switch (sortColumn)
                    {
                        case "ProductTypeName":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductTypeName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "SuperTypeName":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.SuperTypeName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;                        
                        default:
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductTypeName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                    }




                    productTypePaging.ListOfProductType = newProducts.ToList();
                    productTypePaging.CurrentPage = outCurrentPage;
                    productTypePaging.TotalPage = outTotalPage;
                    productTypePaging.ShowCount = outShowCount;
                    productTypePaging.TotalCount = outTotalCount;


                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getProductTypePaging", ex.Message);
            }

            return productTypePaging;
        }

        public ProductPagingViewModel getProductByeProductTypeNamePaging(int departmentID, string productTypeName = "", string sortColumn = "UpdateDate", string orderBy = "ASC",
           int currentPage = 1, int showCount = 10)
        {

            ProductPagingViewModel productPaging = new ProductPagingViewModel();

            try
            {
                using (var db = new TradeMarketEntities())
                {


                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   select new ProductViewModel
                                   {
                                       DepartmentID = p.department_id,
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeID = p.product_type_id.ToString(),
                                       ProductTypeName = t.product_type_name,
                                       ImageUrl = p.image_url,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
                                   };

                    var li = products.ToList();

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

                    


                    var newProducts = from table in query
                                      select new ProductViewModel
                                      {
                                          DepartmentID = table.DepartmentID,
                                          ProductCustomizeID = table.ProductCustomizeID,
                                          ProductName = table.ProductName,
                                          ProductUnitName = table.ProductUnitName,
                                          ProductTypeID = table.ProductTypeID,
                                          ProductTypeName = table.ProductTypeName,
                                          ImageUrl = table.ImageUrl,
                                          OnSales = "0",
                                          UpdateDate = table.UpdateDate

                                      };


                    var salesProduct = from o in db.product_on_sales
                                       where o.department_id == departmentID
                                       select o.product_customize_id;

                    List<string> listOfSalesProduct = new List<string>();
                    listOfSalesProduct = salesProduct.ToList();


                    List<ProductViewModel> listOfNewProduct = new List<ProductViewModel>();
                    listOfNewProduct = newProducts.ToList();

                    foreach (string productCustomizeID in listOfSalesProduct)
                    {

                        foreach (var item in listOfNewProduct.Where(w => w.ProductCustomizeID == productCustomizeID && w.DepartmentID == departmentID))
                        {
                            item.OnSales = "1";
                        }
                    }


                    var newProductsQueryable = listOfNewProduct.AsQueryable();


                    int outCurrentPage = currentPage;
                    int outTotalPage = 1;
                    int outShowCount = 10;
                    int outTotalCount = 0;

                    bool orderASC = true;

                    if (orderBy.ToUpper() == "DESC")
                    {
                        orderASC = false;
                    }

                    switch (sortColumn)
                    {
                        case "ProductTypeName":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductTypeName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "ProductCustomizeID":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductCustomizeID, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "ProductName":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductName, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "UpdateDate":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.UpdateDate, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        case "OnSales":
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.OnSales, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                        default:
                            newProducts = PagedResult(newProductsQueryable, currentPage, showCount, o => o.ProductCustomizeID, orderASC, out outCurrentPage, out outTotalPage, out outShowCount, out outTotalCount);
                            break;
                    }




                    productPaging.ListOfProduct = newProducts.ToList();
                    productPaging.CurrentPage = outCurrentPage;
                    productPaging.TotalPage = outTotalPage;
                    productPaging.ShowCount = outShowCount;
                    productPaging.TotalCount = outTotalCount;

                    
                }
            }
            catch (Exception ex)
            {
                addErrorLog("", "getProduct", ex.Message);
            }

            return productPaging;
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


                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   select new ProductViewModel
                                   {
                                       DepartmentID = p.department_id,
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeName = t.product_type_name,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
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
                                          ProductTypeName = table.ProductTypeName,
                                          UpdateDate = table.UpdateDate
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
                addErrorLog("", "getProductCount", ex.Message);
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
                newProduct.product_customize_id = createProduct.ProductCustomizeID;
                newProduct.product_name = createProduct.ProductName;
                newProduct.image_url = createProduct.ImageUrl;
                newProduct.update_member_id = Utility.getIntOrDefault(createProduct.UpdateMemberID, 1);
                db.Entry(newProduct).State = EntityState.Added;

                db.SaveChanges();

                bm.Result = true;



            }
            catch (DbEntityValidationException ex)
            {
                string err0 = "";
                string err1 = "";

                foreach (var err in ex.EntityValidationErrors)
                {
                   err0 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        err.Entry.Entity.GetType().Name, err.Entry.State);
                    foreach (var errProperty in err.ValidationErrors)
                    {
                        err1 = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                        errProperty.PropertyName, errProperty.ErrorMessage);
                    }
                }

                addErrorLog("DbEntityValidationException", "isCreateProduct", err0+err1);

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

                if (String.IsNullOrEmpty(createProduct.ProductCustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();
                    int count = db.product.Where(o=>o.product_customize_id == createProduct.ProductCustomizeID).Count();
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
                               where a.product_customize_id == updateProduct.UpdateProductCustomizeID
                               select a;

                var product = products.FirstOrDefault();

                if (product != null)
                {
                    //product newProduct = db.product.FirstOrDefault(o => o.product_customize_id == updateProduct.UpdateCustomizeID);
                    product.update_date = DateTime.Now;
                    product.product_type_id = Utility.getIntOrDefault(updateProduct.ProductTypeID, 1);
                    product.product_customize_id = updateProduct.ProductCustomizeID;
                    product.product_name = updateProduct.ProductName;
                    product.update_member_id = Utility.getIntOrDefault(updateProduct.UpdateMemberID, 1);                    
                    product.display_unit = updateProduct.ProductUnitName;
                    product.image_url = updateProduct.ImageUrl;
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

                if (String.IsNullOrEmpty(updateProduct.ProductCustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
                }

                if (String.IsNullOrEmpty(updateProduct.UpdateProductCustomizeID))
                {
                    bm.Message += "請輸入欲更新產品的代碼\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();
                    //var count = (from t in db.product
                    //             where t.product_customize_id == updateProduct.UpdateCustomizeID
                    //             select t).Count();

                    if (updateProduct.ProductCustomizeID != updateProduct.UpdateProductCustomizeID)
                    {
                        int countRepeat = db.product.Where(o => o.product_customize_id == updateProduct.ProductCustomizeID).Count();
                        if (countRepeat > 0)
                        {
                            bm.Message += "產品代碼重覆:" + updateProduct.UpdateProductCustomizeID + ","+ updateProduct.ProductCustomizeID + "\n";
                        }
                    }
                    else
                    {
                        int count = db.product.Where(o => o.product_customize_id == updateProduct.ProductCustomizeID).Count();
                        if (count == 0)
                        {
                            bm.Message += "查無欲更新產品的代碼\n";
                        }
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

                //if (String.IsNullOrEmpty(updateProduct.ProductCustomizeID))
                //{
                //    bm.Message += "請輸入產品代碼\n";
                //}
                //else
                //{
                //    TradeMarketEntities db = new TradeMarketEntities();
                //    //var count = (from t in db.product
                //    //             where t.product_customize_id == updateProduct.CustomizeID
                //    //             select t).Count();


                //    //if (count > 0)
                //    //{
                //    //    bm.Message += "產品代碼重覆\n";
                //    //}

                //    int count = db.product.Where(o => o.product_customize_id == updateProduct.ProductCustomizeID).Count();
                //    if (count > 0)
                //    {
                //        bm.Message += "產品代碼重覆\n";
                //    }

                //}

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

        public BooleanMessage isDeleteProduct(DeleteProduct deleteProductObj)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                TradeMarketEntities db = new TradeMarketEntities();

                var products = from a in db.product
                               where a.product_customize_id == deleteProductObj.DeleteProductCustomizeID
                               select a;

                var product = products.FirstOrDefault();

                if (product != null)
                {
                    product deleteProduct = db.product.FirstOrDefault(o=>o.product_customize_id== deleteProductObj.DeleteProductCustomizeID);
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
                newProductType.super_type_id = Utility.getIntOrDefault(createProductType.SuperTypeID, 1);
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


                    newProductType.super_type_id = Utility.getIntOrDefault(updateProductType.SuperTypeID, 1);
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

        public BooleanMessage isDeleteProductType(DeleteProductType deleteProductTypeObj)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                TradeMarketEntities db = new TradeMarketEntities();

                int newDeleteID = 0;
                newDeleteID = Utility.getIntOrDefault(deleteProductTypeObj.DeleteProductTypeID, 0);

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



        public BooleanMessage isCreateProductUnit(CreateProductUnit createProductUnit)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {

                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckCreateProductUnit(createProductUnit);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }

                TradeMarketEntities db = new TradeMarketEntities();



                product_unit newProductUnit = new product_unit();
                newProductUnit.unit_name = createProductUnit.ProductUnitName;
                db.Entry(newProductUnit).State = EntityState.Added;

                db.SaveChanges();

                bm.Result = true;



            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }
            return bm;
        }

        public BooleanMessage isCheckCreateProductUnit(CreateProductUnit createProductUnit)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(createProductUnit.ProductUnitName))
                {
                    bm.Message += "請輸入產品單位\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();

                    int count = db.product_unit.Where(o => o.unit_name == createProductUnit.ProductUnitName).Count();
                    if (count > 0)
                    {
                        bm.Message += "產品單位重覆\n";
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

        public BooleanMessage isUpdateProductUnit(UpdateProductUnit updateProductUnit)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                //Check Before Update
                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckUpdateProductUnit(updateProductUnit);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }


                TradeMarketEntities db = new TradeMarketEntities();

                int updateID = Utility.getIntOrDefault(updateProductUnit.UpdateID, 0);

                var product_units = from a in db.product_unit
                                    where a.id == updateID
                                    select a;

                var product_unit = product_units.FirstOrDefault();

                if (product_unit != null)
                {
                    product_unit newProductUnit = db.product_unit.FirstOrDefault(o => o.id == updateID);

                    newProductUnit.unit_name = updateProductUnit.ProductUnitName;

                    db.Entry(product_unit).State = EntityState.Modified;
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
                addErrorLog("", "isUpdateProductUnit", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCheckUpdateProductUnit(UpdateProductUnit updateProductUnit)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(updateProductUnit.UpdateID))
                {
                    bm.Message += "請輸入欲修改的單位編號\n";
                }

                if (String.IsNullOrEmpty(updateProductUnit.ProductUnitName))
                {
                    bm.Message += "請輸入產品單位\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();


                    int updateID = Utility.getIntOrDefault(updateProductUnit.UpdateID, 0);

                    int count = db.product_unit.Where(o => o.unit_name == updateProductUnit.ProductUnitName && o.id != updateID).Count();
                    if (count > 0)
                    {
                        bm.Message += "產品單位重覆\n";
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

        public BooleanMessage isDeleteProductUnit(DeleteProductUnit deleteProductUnitObj)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                TradeMarketEntities db = new TradeMarketEntities();

                int newDeleteID = 0;
                newDeleteID = Utility.getIntOrDefault(deleteProductUnitObj.DeleteProductUnitID, 0);

                var products = from a in db.product_unit
                               where a.id == newDeleteID
                               select a;

                var product = products.FirstOrDefault();

                if (product != null)
                {
                    product_unit deleteProductUnit = db.product_unit.FirstOrDefault(o => o.id == newDeleteID);
                    db.product_unit.Remove(deleteProductUnit);
                    db.SaveChanges();

                    bm.Result = true;
                }


            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isDeleteProductUnit", ex.Message);
            }

            return bm;
        }

   
        /// <summary>
        /// Pages the specified query.
        /// </summary>
        /// <typeparam name="T">Generic Type Object</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The Object query where paging needs to be applied.</param>
        /// <param name="pageNum">The page number.</param>
        /// <param name="showCount">Size of the page.</param>
        /// <param name="orderByProperty">The order by property.</param>
        /// <param name="isAscendingOrder">if set to <c>true</c> [is ascending order].</param>
        /// <param name="rowsCount">The total rows count.</param>
        /// <returns></returns>
        private static IQueryable<T> PagedResult<T, TResult>(IQueryable<T> query, int currentPage, int showCount,
                        Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder,out int outCurrentPage, out int outTotalPage, out int outShowCount, out int outTotalCount)
        {
            if (showCount <= 0) showCount = 20;

            //Total result count

            outCurrentPage = currentPage;

            outTotalCount = query.Count();

            outTotalPage = (int)Math.Ceiling((decimal)outTotalCount / showCount);

            outShowCount = showCount;

            //If page number should be > 0 else set to first page
            //if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            //Calculate nunber of rows to skip on pagesize
            int excludedRows = (currentPage - 1) * showCount;

            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);

            //Skip the required rows for the current page and take the next records of pagesize count
            return query.Skip(excludedRows).Take(showCount);
        }


        public BooleanMessage isTempOrderToCartOrder(CreateCartOrder cartOrder)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                TradeMarketEntities db = new TradeMarketEntities();

                

                var temp_carts = from a in db.temp_cart
                                    where a.temp_order_id == cartOrder.TempOrderID
                                    select a;


                if (temp_carts != null)
                {

                    var temp_cart = temp_carts.FirstOrDefault();

                    cart_order newCartOrder = new cart_order();

                    newCartOrder.department_id = DEFAULT_DEPARTMENT_ID;
                    newCartOrder.temp_order_id = cartOrder.TempOrderID;
                    newCartOrder.serial = DateTime.Now.ToString(DATETIME_FORMAT_24H_NOSYMBOL);
                    newCartOrder.buyer_id = Utility.getIntOrDefault(cartOrder.BuyerID, 1);
                    newCartOrder.buyer_name = cartOrder.BuyerName;

                    //total

                    newCartOrder.invoice_no = cartOrder.InvoiceNo;
                    newCartOrder.invoice_type = cartOrder.InvoiceType;
                    newCartOrder.invoice_title = cartOrder.InvoiceTitle;
                  
                    newCartOrder.update_date = DateTime.Now;


                    //memo備用欄位先設為空

                    newCartOrder.memo = "";

                    db.Entry(newCartOrder).State = EntityState.Added;

                    db.SaveChanges();

                    

                    foreach (temp_cart cart in temp_carts)
                    {
                        TradeMarketEntities dbDetail = new TradeMarketEntities();
                        cart_order_detail newCartOrderDetail = new cart_order_detail();

                        newCartOrderDetail.order_id = newCartOrder.id;

                        newCartOrderDetail.product_customize_id = cart.product_customize_id;
                        newCartOrderDetail.product_name = cart.product_name;
                        newCartOrderDetail.quantity = cart.quantity;
                        newCartOrderDetail.price = cart.price;
                        newCartOrderDetail.unit_name = cart.unit_name;
                        newCartOrderDetail.total = cart.total;
                        newCartOrderDetail.tax_add = cart.tax_add;
                        newCartOrderDetail.tax_rate = cart.tax_rate;
                        newCartOrderDetail.tax_total = cart.tax_total;
                        newCartOrderDetail.memo = cart.memo;

                        newCartOrderDetail.update_date = DateTime.Now;
                        //memo備用欄位先設為空



                        dbDetail.Entry(newCartOrderDetail).State = EntityState.Added;

                        dbDetail.SaveChanges();
                    }
                }

                

            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isDeleteProductUnit", ex.Message);
            }

            return bm;

        }

        public BooleanMessage isCheckCreateProductOnSales(CreateProductOnSales createProductOnSales)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(createProductOnSales.ProductCustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
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

        public BooleanMessage isCreateProductOnSales(CreateProductOnSales productOnSales)
        {
            BooleanMessage bm = new BooleanMessage();
            try
            {

                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckCreateProductOnSales(productOnSales);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }



                TradeMarketEntities db = new TradeMarketEntities();


                int departmentID = Utility.getIntOrDefault(productOnSales.DepartmentID, DEFAULT_DEPARTMENT_ID);
                int updateMemberID =Utility.getIntOrDefault(productOnSales.UpdateMemberID, 1);

                var onsales = from a in db.product_on_sales
                              where a.product_customize_id == productOnSales.ProductCustomizeID && a.department_id == departmentID
                              select a;

                var oneOnsales = onsales.FirstOrDefault();

                if (oneOnsales != null)
                {
                    TradeMarketEntities dbDelete = new TradeMarketEntities();
                    product_on_sales deleteProductOnSales = dbDelete.product_on_sales.FirstOrDefault(o => o.id == oneOnsales.id);
                    dbDelete.product_on_sales.Remove(deleteProductOnSales);
                    dbDelete.SaveChanges();
                }

                product_on_sales newProductOnSales = new product_on_sales();

                newProductOnSales.product_customize_id = productOnSales.ProductCustomizeID;
                newProductOnSales.department_id = departmentID;
                newProductOnSales.update_member_id = updateMemberID;
                newProductOnSales.update_date = DateTime.Now;

                db.Entry(newProductOnSales).State = EntityState.Added;

                db.SaveChanges();

                bm.Result = true;

            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isProductOnSales", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCheckDeleteProductOnSales(DeleteProductOnSales deleteProductOnSales)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {


                if (String.IsNullOrEmpty(deleteProductOnSales.ProductCustomizeID))
                {
                    bm.Message += "請輸入產品代碼\n";
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

        public BooleanMessage isDeleteProductOnSales(DeleteProductOnSales productOnSales)
        {
            BooleanMessage bm = new BooleanMessage();
            try
            {

                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckDeleteProductOnSales(productOnSales);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }



                TradeMarketEntities db = new TradeMarketEntities();


                int departmentID = Utility.getIntOrDefault(productOnSales.DepartmentID, DEFAULT_DEPARTMENT_ID);                

                db.product_on_sales.RemoveRange(db.product_on_sales.Where(x => x.product_customize_id == productOnSales.ProductCustomizeID && x.department_id == departmentID));

                db.SaveChanges();

                bm.Result = true;

            }
            catch (Exception ex)
            {
                bm.Message = ex.Message;
                addErrorLog("", "isDeleteProductOnSales", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCreateMember(CreateMember createMember)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {

                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckCreateMember(createMember);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }

                TradeMarketEntities db = new TradeMarketEntities();

                

                member newMember = new member();

                newMember.role = Utility.getIntOrDefault(createMember.Role, 1);
                newMember.member_type_id = Utility.getIntOrDefault(createMember.MemberTypeID, 1);
                newMember.member_name = createMember.MemberName;

                newMember.account = createMember.Account;                
                newMember.password = createMember.Password;

                newMember.email = createMember.Email;
                newMember.mobile = createMember.Mobile;
                newMember.tel = createMember.Tel;
                newMember.fax = createMember.Fax;
                newMember.state = Utility.getIntOrDefault(createMember.State, 0);
                newMember.contact_address_city_o = createMember.ContactAddressCityO;
                newMember.contact_address_area_o = createMember.ContactAddressAreaO;
                newMember.contact_address_street_o = createMember.ContactAddressStreetO;
                newMember.contact_address_o = createMember.ContactAddressO;
                newMember.contact_address_zipno_o = createMember.ContactAddressZipNoO;


                newMember.update_member_id = Utility.getIntOrDefault(createMember.UpdateMemberID, 1);
                
                newMember.update_date = DateTime.Now;

                db.Entry(newMember).State = EntityState.Added;

                db.SaveChanges();

                bm.Result = true;



            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }
            return bm;
        }

        public BooleanMessage isCheckCreateMember(CreateMember createMember)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {



                if (String.IsNullOrEmpty(createMember.MemberTypeID))
                {
                    bm.Message += "請輸入帳號分類\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();

                    int memberTypeID = Utility.getIntOrDefault(createMember.MemberTypeID, 0);

                    int count = db.member_type.Where(o => o.id == memberTypeID).Count();


                    if (count == 0)
                    {
                        bm.Message += "帳號分類不存在\n";
                    }
                }

                if (String.IsNullOrEmpty(createMember.Account))
                {
                    bm.Message += "請輸入帳號\n";
                }
                else
                {
                    TradeMarketEntities db = new TradeMarketEntities();
                    int count = db.member.Where(o => o.account == createMember.Account).Count();
                    if (count > 0)
                    {
                        bm.Message += "帳號重覆\n";
                    }
                }

                if (String.IsNullOrEmpty(createMember.Password))
                {
                    bm.Message += "請輸入密碼\n";
                }

                if (String.IsNullOrEmpty(createMember.MemberName))
                {
                    bm.Message += "請輸入帳號名稱\n";
                }


                //if (String.IsNullOrEmpty(createMember.MemberName))
                //{
                //    bm.Message += "請輸入產品名稱\n";
                //}

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

        public BooleanMessage isUpdateMember(UpdateMember updateMember)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                //Check Before Update
                BooleanMessage bmCheck = new BooleanMessage();

                bmCheck = isCheckUpdateMember(updateMember);

                if (bmCheck.Result == false)
                {
                    bm = bmCheck;
                    return bm;
                }


                TradeMarketEntities db = new TradeMarketEntities();

                int updateID = Utility.getIntOrDefault(updateMember.UpdateTargetMemberID, 0);

                var product_units = from a in db.product_unit
                                    where a.id == updateID
                                    select a;

                var product_unit = product_units.FirstOrDefault();

                if (product_unit != null)
                {
                    product_unit newMember = db.product_unit.FirstOrDefault(o => o.id == updateID);

                    newMember.unit_name = updateMember.MemberName;

                    db.Entry(product_unit).State = EntityState.Modified;
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
                addErrorLog("", "isUpdateMember", ex.Message);
            }

            return bm;
        }

        public BooleanMessage isCheckUpdateMember(UpdateMember updateMember)
        {
            BooleanMessage bm = new BooleanMessage();

            try
            {
                if (String.IsNullOrEmpty(updateMember.UpdateTargetMemberID))
                {
                    bm.Message += "請輸入欲修改的編號\n";
                }

                if (String.IsNullOrEmpty(updateMember.MemberName))
                {
                    bm.Message += "請輸入會員名稱\n";
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

        public MemberShippmentViewModel getOneMemberShipmentInfo(string memberID)
        {
            MemberShippmentViewModel memberShippmentInfo = new MemberShippmentViewModel();
            try
            {
                using (var db = new TradeMarketEntities())
                {

                    int id = Utility.getIntOrDefault(memberID, 0);

                    var members = from m in db.member
                                  where m.id == id
                                  select m;

                    if (members != null)
                    {
                        var member = members
                        .FirstOrDefault();

                        if (member != null)
                        {
                            memberShippmentInfo.ID = member.id.ToString();
                            memberShippmentInfo.ContactAddressCityO = member.contact_address_city_o;
                            memberShippmentInfo.ContactAddressAreaO = member.contact_address_area_o;
                            memberShippmentInfo.ContactAddressStreetO = member.contact_address_street_o;                            
                            memberShippmentInfo.ContactAddressO = member.contact_address_o;
                            memberShippmentInfo.ContactAddressZipNoO = member.contact_address_zipno_o;
                        }

                    }




                }

            }
            catch (Exception ex)
            {
                addErrorLog("Business", "getMemberShipmentInfo", ex.Message);

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

            return memberShippmentInfo;

        }

        public List<ProductViewModel> getFavoriteProduct(string memberID)
        {
            List<ProductViewModel> list = new List<ProductViewModel>();
            try
            {
                using (var db = new TradeMarketEntities())
                {
                   
                    var products = from p in db.product.AsEnumerable()
                                   join t in db.product_type on p.product_type_id equals t.id
                                   join m in db.my_favorite on p.id equals m.product_id
                                   select new ProductViewModel
                                   {
                                       ProductCustomizeID = p.product_customize_id,
                                       ProductName = p.product_name,
                                       ProductUnitName = p.display_unit,
                                       ProductTypeID = p.product_type_id.ToString(),
                                       ProductTypeName = t.product_type_name,
                                       UpdateDate = p.update_date.ToString(DATETIME_FORMAT_24H)
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

                addErrorLog("", "getFavoriteProduct", ex.Message);
            }

            return list;

        }
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
