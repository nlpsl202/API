using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomaji_API
{
    class GetTickets
    {
        public int query_type { get; set; }
        public int start_time { get; set; }
        public int end_time { get; set; }
    }

    class GetTicketsResp
    {
        public int code { get; set; }
        public string message { get; set; }
        public int total_items { get; set; }
        public IEnumerable<GetTicketsDataResp> data { get; set; }
    }

    class GetTicketsDataResp
    {
        public string bill_no { get; set; }
        public string product_name { get; set; }
        public int Price { get; set; }
        public string deal_dt { get; set; }
        public string modify_dt { get; set; }
        public string qrcode { get; set; }
        public string status { get; set; }
    }
}
