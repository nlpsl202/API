using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class QrcodeController : ApiController
    {
        QTicketResp[] qTicketResp = new QTicketResp[2];

        public IEnumerable<QTicketResp> GetallTickets(string QR_CODE, string QR_TIME)
        {
            qTicketResp[0] = new QTicketResp
            {
                status = "S000",
                message = "成功",
                priceType = "成人票",
                priceAreas = "搖滾區",
                seat = "紅3區-3排-2號"
            };
            qTicketResp[1] = new QTicketResp
            {
                status = "S001",
                message = "失敗",
                priceType = "兒童票",
                priceAreas = "安寧區",
                seat = "綠5區-6排-8號"
            };
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(qTicketResp);
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
            return qTicketResp;
        }

        public IHttpActionResult GetTicket(string message)
        {
            var record = qTicketResp.FirstOrDefault(x => x.message == message);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // POST
        public IEnumerable<QTicketResp> Post(QTicket[] qtic)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            QTicketResp[] qTicketResp = new QTicketResp[qtic.Length];
            if (response.IsSuccessStatusCode)
            {
                for (int i = 0; i < qTicketResp.Length; i++)
                {
                    qTicketResp[i] = new QTicketResp
                    {
                        TK_QRCODE = qtic[i].TK_QRCODE,
                        TK_USED_DT = qtic[i].TK_USED_DT,
                        status = "S000",
                        message = "成功",
                        priceType = "",
                        priceAreas = "",
                        seat = ""
                    };
                }
            }
            return qTicketResp;
        }
    }
}
