using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udn_API
{
    class GetTickets
    {
        public string ProductsCode { get; set; }
        public string OrdersSTime{ get; set; }
        public string OrdersETime{ get; set; }
    }

    class GetTicketsResp
    {
        public string ErrCode { get; set; }
        public int RowCount { get; set; }
        public IEnumerable<GetTicketsDataResp> Data { get; set; }
    }

    class GetTicketsDataResp
    {
        public string OrdersNo { get; set; }
        public string QRCode { get; set; }
        public string PriceTypes { get; set; }
        public string Price { get; set; }
        public string OrderDatetime { get; set; }
        public string OrderType { get; set; }
    }

    class VerifyTickets
    {
        public string QRCode { get; set; }
        public string QRTime { get; set; }
    }

    class VerifyTicketsResp
    {
        public string ErrCode { get; set; }
        public IEnumerable<VerifyTicketsDataResp> Data { get; set; }
    }

    class VerifyTicketsDataResp
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string QRCode { get; set; }
        public string PriceTypes { get; set; }
        public string PriceAreas { get; set; }
        public string Seat { get; set; }
    }
}
