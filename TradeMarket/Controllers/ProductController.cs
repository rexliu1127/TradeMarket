using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryTradeMarket;
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
        public ActionResult EditProduct(string updateCustomizeID ="", string productTypeID="", string customizeID = "", string productName = "",string updateMemberID="")
        {

            Business business = new Business();
            BooleanMessage bm = new BooleanMessage();
            try
            {

                UpdateProduct updateProduct = new UpdateProduct();
                updateProduct.UpdateCustomizeID = updateCustomizeID;
                updateProduct.ProductTypeID = productTypeID;
                updateProduct.CustomizeID = customizeID;
                updateProduct.ProductName = productName;
                updateProduct.UpdateCustomizeID = updateMemberID;

                bm = business.isUpdateProduct(updateProduct);

                if (bm.Result == true)
                {
                    return RedirectToAction("Product");
                }
                else
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        [HttpPost]
        public ActionResult DeleteProduct(string deleteCustomizeID = "")
        {
            Business business = new Business();
            BooleanMessage bm = new BooleanMessage();
            try
            {
                bm = business.isDeleteProduct(deleteCustomizeID);

                if (bm.Result == true)
                {
                    return RedirectToAction("Product");
                }
                else
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}