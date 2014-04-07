using BM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using NLog.Common;
namespace BM.Web.Settings
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Data.Entity.Database.SetInitializer(new DbInitializer());
            using (DbContext db = new DbContext())
            {
                db.Database.Initialize(true);
            }
            string nlogPath = Server.MapPath("nlog-web.log");
            InternalLogger.LogFile = nlogPath;
            InternalLogger.LogLevel = NLog.LogLevel.Trace;
            //ControllerBuilder.Current.SetControllerFactory(new ErrorHandlingControllerFactory());

            //// Register custom NLog Layout renderers
            //LayoutRendererFactory.AddLayoutRenderer("utc_date", typeof(UtcDateRenderer));
            //LayoutRendererFactory.AddLayoutRenderer("web_variables", typeof(WebVariablesRenderer));
        }
    }
}
