using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiTradeMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string userCookieTempID = "";

            HttpCookie loadCookie = Request.Cookies["AlloneECShop"];

            //Console.Write(loadCookie.Values["tempID"]);

            if (loadCookie == null)
            {

                string tempID = LibraryTradeMarket.Utility.getUniqueID();

                HttpCookie saveCookie = new HttpCookie("AlloneECShop");
                saveCookie.Values.Add("tempID", tempID);
                Response.Cookies.Add(saveCookie);
                userCookieTempID = tempID;

            }
            else
            {
                if (loadCookie.Values["tempID"] != null)
                {
                    userCookieTempID = loadCookie.Values["tempID"];
                }

            }

            ViewBag.Title = "Home Page";

            return View();
        }


    }
}
