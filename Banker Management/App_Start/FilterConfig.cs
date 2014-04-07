using System.Web;
using System.Web.Mvc;

namespace BM.Web.Settings
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new BM.Web.Filters.CustomActionAttribute());
        }
    }
}
