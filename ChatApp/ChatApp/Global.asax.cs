using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChatApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Application_BeginRequest(object sender,EventArgs e)
        {

           
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];

            if (cookie!=null && cookie.Value!=null)
            {
                CultureInfo oci = new CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentCulture = oci;
                System.Threading.Thread.CurrentThread.CurrentUICulture = oci;

            }
            else
            {
                CultureInfo oci = new CultureInfo("fa-IR");
                System.Threading.Thread.CurrentThread.CurrentCulture = oci;
                System.Threading.Thread.CurrentThread.CurrentUICulture = oci;

            }
        }
    }
}
