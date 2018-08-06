using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flora_API
{
    public class TicketUsedDataCall
    {
        public string Channel { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
    }

    public class TicketUsedDataResp
    {
        public string QR_CODE { get; set; }
        public string Used_TIME { get; set; }
    }
}