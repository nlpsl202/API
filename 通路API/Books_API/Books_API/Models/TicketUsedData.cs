using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Books_API.Models
{
    public class TicketUsedData
    {
        public string Channel { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
    }
}