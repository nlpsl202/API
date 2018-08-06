using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace iBonTicketCount_API
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
            Context.Response.Flush();
            Context.Response.Write("true");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTicketCount(string jsonSting)
        {
            WTicketResp ticketResp = new WTicketResp { TotalCount = 1000, BlockCount = 4 };
            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", new JavaScriptSerializer().Serialize(ticketResp).Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(new JavaScriptSerializer().Serialize(ticketResp));
            return new JavaScriptSerializer().Serialize(ticketResp);
        }
    }
}
