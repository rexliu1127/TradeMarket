using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryTradeMarket;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using System.Data;


namespace ApiTradeMarket.Controllers
{
    public class WebApiController : ApiController
    {


        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        //[HttpPost]
        //public HttpResponseMessage validateMember([FromBody]string value)
        //{

        //    ClassApiResponseData response = new ClassApiResponseData();
        //    //ClassMemberData member = new ClassMemberData();
        //    try
        //    {

        //        MemberLoginModel model = (MemberLoginModel)JsonConvert.DeserializeObject(value, typeof(MemberLoginModel));
        //        //List<ClassValidateMemberPostData> listOfData = (List<ClassValidateMemberPostData>)JsonConvert.DeserializeObject(value, typeof(List<ClassValidateMemberPostData>));


        //        using (var db = new BillingEntities())
        //        {
        //            // Query for all blogs with names starting with B 
        //            var members = from b in db.member
        //                          where b.email == model.Email && b.password == model.Password
        //                          select b;

        //            var member = members
        //                .FirstOrDefault();


        //            if (member == null)
        //            {
        //                response.Result = "0";
        //                response.Message = "帳號或密碼錯誤";
        //            }
        //            else
        //            {
        //                if (member.email == model.Email && member.password == model.Password)
        //                {
        //                    //ModelState.AddModelError("", "登入成功");
        //                    response.Result = "1";
        //                }
        //                else
        //                {
        //                    response.Result = "0";
        //                    response.Message = "帳號或密碼錯誤";

        //                }
        //            }

        //        }



        //        //cbm.Message = "your post:" + value;
        //        //if (ConfigurationManager.AppSettings["ResponsePostContent"] == "true")
        //        //{
        //        //    response.SourceData = listOfData[0];
        //        //}


        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message
        //            //+ "\n" +
        //            //"source:\n" +
        //            //value
        //            ;
        //    }

        //    string json = JsonConvert.SerializeObject(response);
        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent(json)
        //    };

        //}
        public class Model
        {
            public string account { get; set; }
            public string password { get; set; }
        }

        public class AddCardModel
        {
            public string tempOrderID { get; set; }
            public string productCustomizeID { get; set; }
            public string productName { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string unitName { get; set; }

        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage validateMember(Model model)
        {
            //string result = "";
            LoginApiResponseData response = new LoginApiResponseData();
            Business business = new Business();
            
            try
            {

                //MemberViewModel model = (MemberViewModel)JsonConvert.DeserializeObject(value, typeof(MemberViewModel));
                MemberViewModel member = business.isLogin(model.account, model.password);
                if (member.Account != "")
                {
                    response.MemberViewModel = member;
                    response.Result = "1";
                }
            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "isLoginValidate", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }

            string result = JsonConvert.SerializeObject(response);

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductType()
        {
            string result = "";
            Business business = new Business();
            try
            {
                
                List<ProductTypeViewModel> list = new List<ProductTypeViewModel>();

                list = business.getProductType();

                result = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductType", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductByProductTypeName(string productTypeName)
        {
            string result = "";
            Business business = new Business();
            try
            {

                List<ProductViewModel> list = new List<ProductViewModel>();

                list = business.getProductByType(productTypeName);

                result = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductByType", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }
        
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductCountByProductTypeName(string productTypeName = "")
        {
            string result = "";
            Business business = new Business();
            try
            {
                

                Int32 count = business.getProductCountByTypeName(productTypeName);

                result = count.ToString();
            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductCountByType", ex.Message);
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }


        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductByKeyword(string keyword)
        {
            string result = "";
            Business business = new Business();
            try
            {

                List<ProductViewModel> list = new List<ProductViewModel>();

                list = business.getProductByKeyword(keyword);

                result = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductByKeyword", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductCountByKeyword(string keyword)
        {
            string result = "";
            Business business = new Business();
            try
            {

                

                Int32 count = business.getProductCountByKeyword(keyword);

                result = count.ToString();

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductCountByKeyword", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }


        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductByProductTypeNameAndPaging(string productTypeName = "", int currentPage = 0, int pageSize = 12)
        {
            string result = "";
            Business business = new Business();
            try
            {
                List<ProductViewModel> list = new List<ProductViewModel>();

                //List<ProductViewModel> pagedList = new List<ProductViewModel>();

                list = business.getProductByType(productTypeName);

               
                var queryResultPage = list
                  .Skip(pageSize * currentPage)
                  .Take(pageSize);

                //pagedList = list.Skip(currentPage * pageSize).Take(pageSize).ToList();

                if (queryResultPage!=null)
                {                  
                    result = JsonConvert.SerializeObject(queryResultPage.ToList(), Formatting.Indented);
                }



            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductByProductTypeNameAndPaging", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }


        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage addCart(AddCardModel model)
        {
            //string result = "";
            ApiResponseData response = new ApiResponseData();
            Business business = new Business();
            IntMessage im = new IntMessage();
            try
            {

                //MemberViewModel model = (MemberViewModel)JsonConvert.DeserializeObject(value, typeof(MemberViewModel));
                im = business.addCart(model.tempOrderID, model.productCustomizeID
                    , model.productName, model.quantity, model.price, model.unitName);

                response.Result = im.Result.ToString();

                //if (im.Result >0)
                //{
                //    response.Result = im.Result;
                //}
            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "addCart", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }

            string result = JsonConvert.SerializeObject(response);

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getTempCart(string tempID)
        {
            string result = "";
            Business business = new Business();
            try
            {
                List<temp_cart> list = new List<temp_cart>();

                //List<ProductViewModel> pagedList = new List<ProductViewModel>();

                list = business.getTempCart(tempID);

               

                //pagedList = list.Skip(currentPage * pageSize).Take(pageSize).ToList();

                if (list != null)
                {
                    result = JsonConvert.SerializeObject(list, Formatting.Indented);
                }



            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getTempCart", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getOneProduct(string customizeID)
        {
            //ClassApiResponseData response = new ClassApiResponseData();
            Business business = new Business();
            ProductViewModel product = new ProductViewModel();
            string result = "";

            try
            {
                product = business.getOneProduct(customizeID);               
                result = JsonConvert.SerializeObject(product, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getOneProduct", ex.Message);
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductCount(int departmentID = 1, string customizeID = "", string productName = "", string productTypeName = "")
        {
            string result = "";
            Business business = new Business();
            try
            {



                Int32 count = business.getProductCount(departmentID, customizeID, productName, productTypeName);

                result = count.ToString();

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductCount", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }


        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProduct(int departmentID=1, string customizeID = "", string productName = "", string productTypeName="", int resultCountOnly = 0)
        {
            Business business = new Business();
            ProductApiResponseData response = new ProductApiResponseData();            
            string result = resultCountOnly != 0 ? "0" : "";
            try
            {
                response.listOfProduct = business.getProduct(departmentID, customizeID, productName, productTypeName);
                response.Result = "1";
                result = JsonConvert.SerializeObject(response, Formatting.Indented);
            }
            catch (Exception ex)
            {                
                response.Message = ex.Message;
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage isCreateProduct(CreateProduct product)
        {
            //string result = "";
            BooleanMessage bm = new BooleanMessage();
            
            Business business = new Business();

            try
            {

                bm = business.isCreateProduct(product);
                
            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "isCreateProduct", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }

            string result = JsonConvert.SerializeObject(bm);

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage isUpdateProduct(UpdateProduct product)
        {
            //string result = "";
            BooleanMessage bm = new BooleanMessage();

            Business business = new Business();

            try
            {

                bm = business.isUpdateProduct(product);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "isUpdateProduct", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }

            string result = JsonConvert.SerializeObject(bm);

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage isDeleteProduct(string deleteCustomizeID)
        {
            //string result = "";
            BooleanMessage bm = new BooleanMessage();

            Business business = new Business();

            try
            {

                bm = business.isDeleteProduct(deleteCustomizeID);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "isDeleteProduct", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }

            string result = JsonConvert.SerializeObject(bm);

            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

    }
}
