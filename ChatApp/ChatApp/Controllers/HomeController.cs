using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
  
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ChangeLanguage(string lan)
        {
            if (lan !=null )
            {
               
                CultureInfo oci = new CultureInfo(lan);
                System.Threading.Thread.CurrentThread.CurrentCulture = oci;
                System.Threading.Thread.CurrentThread.CurrentUICulture = oci;

                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = lan;
                Response.Cookies.Add(cookie);

            }



            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}