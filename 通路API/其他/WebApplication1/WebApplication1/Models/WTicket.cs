using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class WTicket
    {
        public string Products_Code { get; set; }
        public string Orders_STIME { get; set; }
        public string Orders_ETIME { get; set; }
        public int Block { get; set; }
    }
}