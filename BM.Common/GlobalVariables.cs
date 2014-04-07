using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BM.Common
{
    public static class GlobalVariables
    {
        // readonly variable
        public static string CompanyName
        {
            get
            {
                return "Ansu Tech"; //HttpContext.Current.Application["CompanyName"] as string;
            }
            set
            {
                HttpContext.Current.Application["CompanyName"] = value;
            }
        }

        // read-write variable
        public static string CompanyId
        {
            get
            {
                return "5f426a46-c1e6-487c-8cb2-86915da5fed2";// Guid.NewGuid().ToString();// HttpContext.Current.Application["CompanyId"] as string;
            }
            set
            {
                HttpContext.Current.Application["CompanyId"] = value;
            }
        }
    }


}
