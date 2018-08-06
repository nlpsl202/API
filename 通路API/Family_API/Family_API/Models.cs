using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_API
{
    class GetTickets
    {
        public string TR_TYPE { get; set; }
        public string PASS_CODE { get; set; }
        public string PRODUCTS_CODE { get; set; }
        public string ORDERS_STIME { get; set; }
        public string ORDERS_ETIME { get; set; }
    }

    class GetTicketsResp
    {
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
        public IEnumerable<GetTicketsDataResp> TICKETS { get; set; }
    }

    class GetTicketsDataResp
    {
        public string PRODUCTS_CODE { get; set; }
        public string ORDERS_NO { get; set; }
        public string QR_CODE { get; set; }
        public string PRICE_NAME { get; set; }
        public string PRICE { get; set; }
        public string GAME_NO { get; set; }
        public string SECTION_NAME { get; set; }
        public string SEAT { get; set; }
        public string ORDER_DATETIME { get; set; }
        public string ORDER_TYPE { get; set; }
    }

    class VerifyTickets
    {
        public string TR_TYPE { get; set; }
        public string PASS_CODE { get; set; }
        public string QR_CODE { get; set; }
        public string QR_TIME { get; set; }
    }

    class VerifyTicketsResp
    {
        public string PRODUCTS_CODE { get; set; }
        public string ORDERS_NO { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
    }
}
