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
    public class TicketUsedQueryController : ApiController
    {
        // POST: api/Book
        public TicketUsedQueryResp Post(TicketUsedQuery ticketUsedQuery)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection2"].ConnectionString;
            DataTable a = new DataTable();
            string TK_ENTER_DT = "";
            TicketUsedQueryResp ticketUsedQueryResp=new TicketUsedQueryResp();
            try
            {
                using (SqlConnection _con = new SqlConnection(myConnStr))
                {
                    SqlCommand cmd = new SqlCommand("select * from pUltralight03 where QRCODE='" + ticketUsedQuery.QR_CODE + "'", _con);
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(a);
                    _con.Close();
                    TK_ENTER_DT = a.Rows[0]["TK_ENTER_DT"].ToString();
                    if (a.Rows.Count != 0)
                    {
                        if (TK_ENTER_DT == "")//pUltralight03入園時間
                        {
                            ticketUsedQueryResp.Already_Used = "N";//未使用
                        }
                        else if (TK_ENTER_DT != "")
                        {
                            ticketUsedQueryResp.Already_Used = "Y";//已使用
                        }
                        else
                        {
                            ticketUsedQueryResp.Already_Used = "X";//註銷
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return ticketUsedQueryResp;
        }
    }
}
