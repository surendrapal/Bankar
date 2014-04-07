﻿using LowercaseDashedRouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BM.Web.Settings
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.Add(new LowercaseDashedRoute("{controller}/{action}/{id}",
            //    new RouteValueDictionary(
            //        new { controller = "Home", action = "Index", id = UrlParameter.Optional }),
            //        new DashedRouteHandler()
            //    )
            //);

            routes.MapRoute(
                "default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            MvcSiteMapProvider.Web.Mvc.XmlSiteMapController.RegisterRoutes(routes);
        }
    }
}
