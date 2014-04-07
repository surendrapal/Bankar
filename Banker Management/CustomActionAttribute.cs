using BM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BM.Web.Filters
{
    public class CustomActionAttribute : System.Web.Mvc.FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            // filterContext.Controller.ViewBag.OnActionExecuted = "IActionFilter.OnActionExecuted filter called";
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(GlobalVariables.CompanyId))
            {
                string areaName = Convert.ToString(filterContext.RequestContext.RouteData.Values["Area"]);
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                //if (string.IsNullOrEmpty(areaName))
                //    if (areaName != "Settings")
                //    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Company",
                            action = "Index",
                            area = "Settings"
                        }));
                    //}
            }
        }
    }
}