using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace TradeMarket.Controllers
{
    public class BackOfficeController : Controller
    {
       

        // GET: BackOffice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();            
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}