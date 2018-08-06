using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class QTicketResp
    {
        public string TK_QRCODE { get; set; }
        public string TK_USED_DT { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string priceType { get; set; }
        public string priceAreas { get; set; }
        public string seat { get; set; }
    }
}