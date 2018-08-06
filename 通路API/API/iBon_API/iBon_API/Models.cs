using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBon_API
{
    class GetTicketData
    {
        public string Products_Code { get; set; }
        public string Orders_STIME { get; set; }
        public string Orders_ETIME { get; set; }
        public int Block { get; set; }
    }

    class GetTicketDataResp
    {
        public string QR_Code { get; set; }
        public string Orders_No { get; set; }
        public string PriceTypes { get; set; }
        public int Price { get; set; }
        public string Order_datetime { get; set; }
        public string Order_Type { get; set; }
    }

    class CheckTicketData
    {
        public string QR_code { get; set; }
        public string QR_time { get; set; }
    }

    class CheckTicketDataResp
    {
        public string status { get; set; }
        public string message { get; set; }
        public string priceType { get; set; }
        public string priceAreas { get; set; }
        public string seat { get; set; }
    }

    class GetTicketCount
    {
        public string Products_Code { get; set; }
        public string Orders_STIME { get; set; }
        public string Orders_ETIME { get; set; }
    }

    class GetTicketCountResp
    {
        public int TotalCount { get; set; }
        public int BlockCount { get; set; }
    }
}
