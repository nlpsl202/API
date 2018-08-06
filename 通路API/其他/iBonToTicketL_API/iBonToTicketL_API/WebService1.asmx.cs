using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace iBonToTicketL_API
{
    /// <summary>
    ///WebService1 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ConnectTest()
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", "true".Length.ToString());
            Context.Response.Write("true");
            Context.Response.Flush();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TxnRead(string Products_Code, string Orders_STIME, string Orders_ETIME, int Block)
        {
            WTicketResp[] wticResp = new WTicketResp[5]  
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
                PriceTypes="早鳥票", Price=175, Order_datetime="2018/04/30 23:05:00",Order_Type="R"}
            };
            string RespMsg = new JavaScriptSerializer().Serialize(wticResp);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json; charset=utf-8";
            Context.Response.AddHeader("content-length", System.Text.Encoding.UTF8.GetBytes(RespMsg).Length.ToString());
            Context.Response.Write(RespMsg);
            Context.Response.Flush();
        }
    }
}
