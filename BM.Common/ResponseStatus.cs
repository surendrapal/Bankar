using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Common
{
    public class ResponseStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class SelectObject
    {
        public string value { get; set; }
        public string caption { get; set; }
    }
}
