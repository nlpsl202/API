using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flora_API
{
    public class GateCountCall
    {
        public string SPS_ID { get; set; }
        public string DEVICE_ID { get; set; }
        public string START_TIME { get; set; }
        public string END_TIME { get; set; }
    }

    public class GateCountResp
    {
        public string SPS_ID { get; set; }
        public string DEVICE_ID { get; set; }
        public string COUNT_TIME { get; set; }
        public string IN_COUNT { get; set; }
        public string OUT_COUNT { get; set; }
    }
}