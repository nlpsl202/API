using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Flora_API
{
    public partial class TicketUsedQuery : System.Web.UI.Page
    {
        private WriteLog writeLog = new WriteLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WebConfigurationManager.AppSettings["IpAddress"].ToString().IndexOf(Request.ServerVariables["REMOTE_ADDR"].ToString()) > -1)
            {
                createapi();
            }
            else
            {
                Response.Write("<script>alert('IP權限不符！')</script>");
                writeLog.Write_Log(Request.ServerVariables["REMOTE_ADDR"].ToString()+"TicketUsedQuery IP權限不符！");
            }
        }

        public void createapi()
        {
            string RequestURL = Request.Url.AbsoluteUri.ToString();
            string[] SplitURL = RequestURL.Split('?');
            if (SplitURL.Length > 1)
            {
                string[] Param = SplitURL[1].Split('&');
                string Channel = Param[0].ToLower();
                string QR_CODE = Param[1];
                
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var result = string.Empty;
                //var result = string.Format("{0:yyyyMMdd}", DateTime.Now);
                //var result = serializer.Serialize();

                string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection"].ConnectionString;
                SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
                string completeConnStr = sqlcsb.ConnectionString;
                string TK_STATUS = "";
                DataTable a = new DataTable();
                DataTable b = new DataTable();

                try
                {
                    using (SqlConnection _con = new SqlConnection(completeConnStr))
                    {
                        SqlCommand cmd = new SqlCommand("select * from cTicketWhitelist where TK_QRCODE='" + QR_CODE + "' and TK_PLACE='" + Channel + "'", _con);
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(a);
                        _con.Close();

                        SqlCommand cmd2 = new SqlCommand("select * from oChannel where CHL_NAME='" + Channel + "'", _con);
                        _con.Open();
                        cmd2.ExecuteNonQuery();
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        da2.Fill(b);
                        _con.Close();

                        if (b.Rows.Count != 0)
                        {
                            if (a.Rows.Count != 0)
                            {
                                TK_STATUS = a.Rows[0]["TK_STATUS"].ToString();
                                if (TK_STATUS.Equals("1"))//pUltralight03入園時間
                                {
                                    result = "N";//未使用
                                }
                                else if (TK_STATUS.Equals("2"))
                                {
                                    result = "Y";//已使用
                                }
                                else
                                {
                                    result = "X";//註銷
                                }
                            }
                            else
                            {
                                result = "U";//無此條碼
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('查無此通路')</script>");
                        }
                    }
                }
                catch (Exception e)
                {
                }
                Response.Write("{" + '"' + "Already_Used" + '"' + ":" + '"' + result + '"' + "}");
                writeLog.Write_Log("TicketUsedQuery " + Channel + "&" + QR_CODE + "  " + "{" + '"' + "Already_Used" + '"' + ":" + '"' + result + '"' + "}");
            }
            else
            {

            }
        }
        //[System.Web.Services.WebMethod]
        //public static string UsedGen(string Channel,string QR_CODE)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    var result = string.Empty;
        //    //var result = string.Format("{0:yyyyMMdd}", DateTime.Now);
        //    //var result = serializer.Serialize();

        //    string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection"].ConnectionString;
        //    SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
        //    string completeConnStr = sqlcsb.ConnectionString;
        //    string TK_ENTER_DT = "";
        //    DataTable a = new DataTable();

        //    try
        //    {
        //        using (SqlConnection _con = new SqlConnection(completeConnStr))
        //        {
        //            SqlCommand cmd = new SqlCommand("select * from pUltralight03 where QRCODE='" + QR_CODE + "'", _con);
        //            _con.Open();
        //            cmd.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(a);
        //            _con.Close();
        //            TK_ENTER_DT = a.Rows[0]["TK_ENTER_DT"].ToString();
        //            if (a.Rows.Count != 0)
        //            {
        //                if (TK_ENTER_DT == "")//pUltralight03入園時間
        //                {
        //                    result = "N";//未使用
        //                }
        //                else if (TK_ENTER_DT != "")
        //                {
        //                    result = "Y";//已使用
        //                }
        //                else
        //                {
        //                    result = "X";//註銷
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    return result;
        //}
    }
}