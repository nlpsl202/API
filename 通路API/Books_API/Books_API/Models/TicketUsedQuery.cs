using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Books_API.Models
{
    public class TicketUsedQuery
    {
        public string Channel { get; set; }
        public string QR_CODE { get; set; }
    }
}