using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryTradeMarket;

using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace TradeMarket.Controllers
{
    public class ProductController : Controller
    {

        // GET: BackOffice
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct(string productTypeID = "", string productCustomizeID = "", string productName = "", string productUnitName = "")
        {

            BooleanMessage bm = new BooleanMessage();
            try
            {
                

                CreateProduct createProduct = new CreateProduct();


                createProduct.ProductTypeID = productTypeID;
                createProduct.ProductCustomizeID = productCustomizeID;
                createProduct.ProductName = productName;
                createProduct.ProductUnitName = productUnitName;




                bm = await isCreateProduct(createProduct);

                


            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }

            if (bm.Result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }


        }

        public async Task<BooleanMessage> isCreateProduct(CreateProduct createProduct)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiHost);

                var result = await client.PostAsJsonAsync("/api/WebApi/isCreateProduct", createProduct);
                string resultJson = await result.Content.ReadAsStringAsync();

                bm = (BooleanMessage)JsonConvert.DeserializeObject(resultJson, typeof(BooleanMessage));

                

            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }

            return bm;

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProduct(string updateProductCustomizeID = "", string productTypeID = "", string productCustomizeID = "", string productName = "", string productUnitName="", string updateMemberID = "")
        {

            BooleanMessage bm = new BooleanMessage();
            try
            {


                UpdateProduct updateProduct = new UpdateProduct();

                updateProduct.UpdateProductCustomizeID = updateProductCustomizeID;
                updateProduct.ProductTypeID = productTypeID;
                updateProduct.ProductCustomizeID = productCustomizeID;
                updateProduct.ProductName = productName;
                updateProduct.ProductUnitName = productUnitName;
                updateProduct.UpdateMemberID = updateMemberID;



                bm = await isUpdateProduct(updateProduct);




            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }

            if (bm.Result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }


        }

        public async Task<BooleanMessage> isUpdateProduct(UpdateProduct updateProduct)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiHost);

                var result = await client.PostAsJsonAsync("/api/WebApi/isUpdateProduct", updateProduct);
                string resultJson = await result.Content.ReadAsStringAsync();

                bm = (BooleanMessage)JsonConvert.DeserializeObject(resultJson, typeof(BooleanMessage));



            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }

            return bm;

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProduct(string deleteProductCustomizeID = "")
        {

            BooleanMessage bm = new BooleanMessage();
            try
            {

                DeleteProduct deleteProduct = new DeleteProduct();
                deleteProduct.DeleteProductCustomizeID = deleteProductCustomizeID;
                bm = await isDeleteProduct(deleteProduct);

            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }

            if (bm.Result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }


        }

        public async Task<BooleanMessage> isDeleteProduct(DeleteProduct deleteProduct)
        {

            BooleanMessage bm = new BooleanMessage();

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiHost);

                var result = await client.PostAsJsonAsync("/api/WebApi/isDeleteProduct", deleteProduct);
                string resultJson = await result.Content.ReadAsStringAsync();

                bm = (BooleanMessage)JsonConvert.DeserializeObject(resultJson, typeof(BooleanMessage));



            }
            catch (Exception ex)
            {

                bm.Message = ex.Message;
            }

            return bm;

        }



        //[HttpPost]
        //public ActionResult CreateProduct(string productTypeID = "", string productCustomizeID = "", string productName = "", string productUnitName="")
        //{

        //    Business business = new Business();
        //    BooleanMessage bm = new BooleanMessage();
        //    try
        //    {

        //        CreateProduct createProduct = new CreateProduct();


        //        createProduct.ProductTypeID = productTypeID;
        //        createProduct.ProductCustomizeID = productCustomizeID;
        //        createProduct.ProductName = productName;
        //        createProduct.ProductUnitName = productUnitName;

        //        bm = business.isCreateProduct(createProduct);

        //        if (bm.Result == true)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}

        //[HttpPost]
        //public ActionResult UpdateProduct(string updateCustomizeID ="", string productTypeID="", string productCustomizeID = "", string productName = "",string updateMemberID="")
        //{

        //    Business business = new Business();
        //    BooleanMessage bm = new BooleanMessage();
        //    try
        //    {

        //        UpdateProduct updateProduct = new UpdateProduct();
        //        updateProduct.UpdateCustomizeID = updateCustomizeID;
        //        updateProduct.ProductTypeID = productTypeID;
        //        updateProduct.ProductCustomizeID = productCustomizeID;
        //        updateProduct.ProductName = productName;
        //        updateProduct.UpdateCustomizeID = updateMemberID;

        //        bm = business.isUpdateProduct(updateProduct);

        //        if (bm.Result == true)
        //        {
        //            return RedirectToAction("Product");
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}

        //[HttpPost]
        //public ActionResult DeleteProduct(string deleteCustomizeID = "")
        //{
        //    Business business = new Business();
        //    BooleanMessage bm = new BooleanMessage();
        //    try
        //    {
        //        bm = business.isDeleteProduct(deleteCustomizeID);

        //        if (bm.Result == true)
        //        {
        //            return RedirectToAction("Product");
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}
    }
}