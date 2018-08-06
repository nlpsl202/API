using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class WTicketResp
    {
        public string QR_Code { get; set; }
        public string Orders_No { get; set; }
        public string PriceTypes { get; set; }
        public int Price { get; set; }
        public string Order_datetime { get; set; }
        public string Order_Type { get; set; }
    }
}