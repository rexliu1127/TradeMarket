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
        [HttpPost]
        public ActionResult EditProduct(string updateCustomizeID ="", string customizeID = "", string productName = "",string updateMemberID="")
        {

            Business business = new Business();
            BooleanMessage bm = new BooleanMessage();
            try
            {
                bm = business.isUpdateProduct(updateCustomizeID, customizeID, productName, updateMemberID);

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