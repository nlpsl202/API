using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flora_API
{
    public partial class TicketUsedData : System.Web.UI.Page
    {
        private WriteLog writeLog = new WriteLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            string a = Server.MapPath(".");
            if (WebConfigurationManager.AppSettings["IpAddress"].ToString().IndexOf(Request.ServerVariables["REMOTE_ADDR"].ToString()) > -1)
            {
                createapi();
            }
            else
            {
                Response.Write("<script>alert('IP權限不符！')</script>");
                writeLog.Write_Log(Request.ServerVariables["REMOTE_ADDR"].ToString()+"TicketUsedData IP權限不符！");
            }
        }

        public void createapi()
        {
            string RequestURL = Request.Url.AbsoluteUri.ToString();
            string[] SplitURL = RequestURL.Split('?');
            
            if (SplitURL.Length > 1)
            {
                string a = System.Web.HttpUtility.UrlDecode(SplitURL[1]);
                TicketUsedDataCall deserialized = JsonConvert.DeserializeObject<TicketUsedDataCall>(a);
                DataTable DT = new DataTable();
                GetDataDB(ref DT, deserialized.Channel.ToLower(), deserialized.Start_Time, deserialized.End_Time);
                TicketUsedDataResp[] ticket = new TicketUsedDataResp[DT.Rows.Count];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    ticket[i] = new TicketUsedDataResp
                    {
                        QR_CODE = DT.Rows[i]["TK_QRCODE"].ToString(),
                        Used_TIME = DT.Rows[i]["TK_USED_DT"].ToString(),
                    };
                }
                Response.Write(new JavaScriptSerializer().Serialize(ticket));
                writeLog.Write_Log("TicketUsedData "+a +"  "+ new JavaScriptSerializer().Serialize(ticket));
            //    string[] Param = SplitURL[1].Split('&');
            //    string Channel = Param[0].ToLower();
            //    string QR_CODE = Param[1];

            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    var result = string.Empty;
            //    //var result = string.Format("{0:yyyyMMdd}", DateTime.Now);
            //    //var result = serializer.Serialize();

            //    string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection"].ConnectionString;
            //    SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            //    string completeConnStr = sqlcsb.ConnectionString;
            //    string TK_ENTER_DT = "";
            //    DataTable a = new DataTable();
            //    DataTable b = new DataTable();

            //    try
            //    {
            //        using (SqlConnection _con = new SqlConnection(completeConnStr))
            //        {
            //            //SqlCommand cmd = new SqlCommand("select * from pUltralight03 where QRCODE='" + QR_CODE + "'", _con);
            //            SqlCommand cmd = new SqlCommand("select * from cTicketWhitelist where TK_QRCODE='" + QR_CODE + "' and TK_PLACE='" + Channel + "'", _con);
            //            _con.Open();
            //            cmd.ExecuteNonQuery();
            //            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //            da.Fill(a);
            //            _con.Close();

            //            SqlCommand cmd2 = new SqlCommand("select * from oChannel where CHL_NAME='" + Channel + "'", _con);
            //            _con.Open();
            //            cmd2.ExecuteNonQuery();
            //            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            //            da2.Fill(b);
            //            _con.Close();

            //            SqlCommand cmd3 = new SqlCommand("update cTicketWhitelist set TK_STATUS='X' where TK_PLACE='" + Channel + "' and TK_QRCODE='" + QR_CODE + "'", _con);

            //            if (b.Rows.Count != 0)
            //            {
            //                if (a.Rows.Count != 0)
            //                {
            //                    if (a.Rows[0]["TK_STATUS"].ToString().Equals("1"))
            //                    {
            //                        _con.Open();
            //                        cmd3.ExecuteNonQuery();
            //                        _con.Close();
            //                        result = "Y";//未使用
            //                        //TK_ENTER_DT = a.Rows[0]["TK_USED_DT"].ToString();
            //                        //if (TK_ENTER_DT == "")//pUltralight03入園時間
            //                        //{

            //                        //}
            //                    }
            //                    else if (a.Rows[0]["TK_STATUS"].ToString().Equals("2"))
            //                    {
            //                        result = "U";//已使用
            //                    }
            //                    else if (a.Rows[0]["TK_STATUS"].ToString().Equals("X"))
            //                    {
            //                        result = "X";//註銷
            //                    }
            //                }
            //                else
            //                {
            //                    result = "N";//無此條碼
            //                }
            //            }
            //            else
            //            {
            //                Response.Write("<script>alert('查無此通路')</script>");
            //            }
            //        }
            //    }

            //    catch (Exception e)
            //    {
            //    }
            //    Response.Write("{" + "\"message\":\"" + result + "\"}");
            //}
            //else
            //{
            }
        }

        private void GetDataDB(ref DataTable result, string TK_PLACE,string Start_Time, string End_Time)
        {
            string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            SqlConnection _con = new SqlConnection(completeConnStr);
            DataTable objDT = new DataTable();
            string sql = "";
            _con.Open();
            sql = string.Format(@"select TK_QRCODE,SUBSTRING(TK_USED_DT,1,4)+'/'+SUBSTRING(TK_USED_DT,5,2)+'/'+SUBSTRING(TK_USED_DT,7,2)+' '+SUBSTRING(TK_USED_DT,9,2)+':'+SUBSTRING(TK_USED_DT,11,2)+':'+SUBSTRING(TK_USED_DT,13,2) as TK_USED_DT from cTicketWhitelist where TK_PLACE='" + TK_PLACE + "' and TK_STATUS='2' and convert(datetime,SUBSTRING(TK_USED_DT,1,4)+'/'+SUBSTRING(TK_USED_DT,5,2)+'/'+SUBSTRING(TK_USED_DT,7,2)+' '+SUBSTRING(TK_USED_DT,9,2)+':'+SUBSTRING(TK_USED_DT,11,2)+':'+SUBSTRING(TK_USED_DT,13,2)) between '" + Start_Time + "' and '" + End_Time + "'");
            SqlCommand cmd = new SqlCommand(sql, _con);
            cmd.ExecuteNonQuery();
            _con.Close();
            SqlDataAdapter objDR = new SqlDataAdapter(cmd);
            objDR.Fill(objDT);
            result = objDT;
        }
    }
}