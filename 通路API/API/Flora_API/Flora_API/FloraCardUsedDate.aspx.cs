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
    public partial class FloraCardUsedDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            createapi();
        }

        public void createapi()
        {
            string RequestURL = Request.Url.AbsoluteUri.ToString();
            string[] SplitURL = RequestURL.Split('?');
            if (SplitURL.Length > 1)
            {
                string[] Param = SplitURL[1].Split('&');
                string Card_Type = Param[0];
                string core_code = Param[1];

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var result = string.Empty;
                //var result = string.Format("{0:yyyyMMdd}", DateTime.Now);
                //var result = serializer.Serialize();

                string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection"].ConnectionString;
                SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
                string completeConnStr = sqlcsb.ConnectionString;
                DataTable a = new DataTable();
                DataTable b = new DataTable();

                try
                {
                    using (SqlConnection _con = new SqlConnection(completeConnStr))
                    {
                        if (Card_Type == "I")//內卡號
                        {
                            SqlCommand cmd = new SqlCommand("select * from pUltralight03 where TICKET_NO='" + core_code + "'", _con);
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(a);
                            _con.Close();
                        }

                        else
                        { //外卡號
                            SqlCommand cmd2 = new SqlCommand("select * from pBadFloraCard where CARD_NO='" + core_code + "'", _con);
                            _con.Open();
                            cmd2.ExecuteNonQuery();
                            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            da2.Fill(b);
                            _con.Close();
                        }

                        if (a.Rows.Count != 0)
                        {
                            //Convert.ToDateTime(dt.Rows[i]["CREATEDT"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                            result = Convert.ToDateTime(a.Rows[0]["TK_ENTER_DT"].ToString()).ToString("yyyyMMdd");//pUltralight03入園時間

                        }
                        if (b.Rows.Count != 0)
                        {
                            result = Convert.ToDateTime(b.Rows[0]["CARD_REC_DT"].ToString()).ToString("yyyyMMdd"); //pBadFloraCard入園時間
                        }
                    }
                }
                catch (Exception e)
                {
                }
                Response.Write("{" + '"' + "Used_Date" + '"' + ":" + '"' + result + '"' +"}");            
            }
            else
            { 
            
            }         
        }
    }
}