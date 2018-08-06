using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/tic/{Products_Code:string}/{Orders_STIME:string}/{Orders_ETIME:string}")]
    public class TicController : ApiController
    {
        WTicketResp[] wticResp = new WTicketResp[]  
        { 
            new WTicketResp{Orders_No="o01031800001", QR_Code="C09002B00AZQ5U-1616305202E",   
            PriceTypes="早鳥票", Price=175, Order_datetime="2018/03/01 10:05:00",Order_Type="B"},
            new WTicketResp{Orders_No="o01031800003", QR_Code="C09002B00AZQ5U-1616305202F",   
            PriceTypes="早鳥票", Price=175, Order_datetime="2018/04/18 10:05:00",Order_Type="R"},
            new WTicketResp{Orders_No="o01041800001", QR_Code="C09002B00AZQ5U-1616305202G",   
            PriceTypes="早鳥票", Price=175, Order_datetime="2018/04/30 23:05:00",Order_Type="B"},
            new WTicketResp{Orders_No="o01041800005", QR_Code="C09002B00AZQ5U-1616305203G",   
            PriceTypes="早鳥票", Price=175, Order_datetime="2018/04/30 23:05:00",Order_Type="B"},
            new WTicketResp{Orders_No="o01041800006", QR_Code="C09002B00AZQ5U-1616305204G",   
            PriceTypes="早鳥票", Price=175, Order_datetime="2018/04/30 23:05:00",Order_Type="B"}
        };
        WTicketResp[] wticResp2 = new WTicketResp[]  
        { 
            new WTicketResp{Orders_No="o01031111111", QR_Code="C09002B00AZQ5U-16163052099",   
            PriceTypes="早鳥票", Price=175, Order_datetime="2018/03/01 10:05:00",Order_Type="R"}
        };
        public IEnumerable<WTicketResp> GetallTickets(string Products_Code, string Orders_STIME, string Orders_ETIME = null)
        {
            //string dt = Orders_STIME;
            //for (int i = 0; i < wtic.Length; i++)
            //{
            //    string dt2 = wtic[i].Order_datetime;
            //}
            //string dt2 = wtic[0].Order_datetime;
            //int result = string.Compare(dt,dt2);
            //if (result < 0)
            //{
            //    return wtic;
            //}
            return wticResp;
        }

        public IHttpActionResult GetTicket(string odr_no)
        {
            var record = wticResp.FirstOrDefault(x => x.Orders_No == odr_no);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        public IEnumerable<WTicketResp> Post(WTicket ticket)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            if (ticket.Block == 1)
            {
                return wticResp;
            }
            else
            {
                return wticResp2;
            }
        }
    }
}
