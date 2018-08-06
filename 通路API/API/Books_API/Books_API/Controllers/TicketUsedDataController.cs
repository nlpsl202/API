using Books_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Books_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TicketUsedDataController : ApiController
    {
        // POST: api/Book
        public IEnumerable<TicketUsedDataResp> Post(TicketUsedData ticketUsedData)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection2"].ConnectionString;
            string Start_Time;
            string End_Time;
            DataTable a = new DataTable();
            TicketUsedDataResp[] bookResp=new TicketUsedDataResp[0];
            try
            {
                SqlConnection _con = new SqlConnection(myConnStr);
                Start_Time = ticketUsedData.Start_Time;
                End_Time = ticketUsedData.End_Time;
                SqlCommand cmd = new SqlCommand("select * from pUltralight03 where CREATEDT between '" + Start_Time + "' and '" + End_Time + "'", _con);
                _con.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(a);
                _con.Close();
                if (a.Rows.Count != 0)
                {
                    bookResp = new TicketUsedDataResp[a.Rows.Count];
                    for (int i = 0; i < bookResp.Length; i++)
                    {
                        bookResp[i] = new TicketUsedDataResp
                        {
                            QR_CODE = a.Rows[i]["QRCODE"].ToString(),
                            Used_TIME = Convert.ToDateTime(a.Rows[i]["TK_ENTER_DT"].ToString()).ToString("yyyy/MM/dd HH:mm:ss")
                        };
                    }
                }
            }
            catch (Exception e)
            {
            }
            return bookResp;
        }
    }
}
