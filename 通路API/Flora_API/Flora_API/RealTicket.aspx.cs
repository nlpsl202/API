using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flora_API
{
    public partial class RealTicket : System.Web.UI.Page
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
                try
                {
                    string[] Param = SplitURL[1].Split('&');
                    string Channel = Param[0].ToLower();
                    string TICKET_NO = Param[1];
                    string TYPE = Param[2];

                    var result = string.Empty;

                    string myConnStr = ConfigurationManager.ConnectionStrings["TWFETConnection"].ConnectionString;
                    SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
                    string completeConnStr = sqlcsb.ConnectionString;
                    DataTable a = new DataTable();
                    DataTable b = new DataTable();

                    using (SqlConnection _con = new SqlConnection(completeConnStr))
                    {
                        SqlCommand cmd = new SqlCommand("select * from cTicketPool where TICKET_NO='" + TICKET_NO + "'", _con);
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

                        SqlCommand cmd3 = new SqlCommand("update cTicketPool set TK_STATUS='X' where TICKET_NO='" + TICKET_NO + "'", _con);

                        if (b.Rows.Count != 0)
                        {
                            if (a.Rows.Count != 0)
                            {
                                if (TYPE.Equals("Q"))
                                {
                                    if (a.Rows[0]["TK_STATUS"].ToString().Equals("1"))
                                    {
                                        result = "C";//未使用
                                    }
                                    else if (a.Rows[0]["TK_STATUS"].ToString().Equals("2"))
                                    {
                                        result = "U";//已使用
                                    }
                                    else if (a.Rows[0]["TK_STATUS"].ToString().Equals("X"))
                                    {
                                        result = "X";//註銷
                                    }
                                }
                                else if (TYPE.Equals("R"))
                                {
                                    _con.Open();
                                    cmd3.ExecuteNonQuery();
                                    _con.Close();
                                    result = "Y";//退票成功
                                }
                            }
                            else
                            {
                                result = "N";//無此票號
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('查無此通路')</script>");
                        }
                    }
                    Response.Write("{" + "\"message\":\"" + result + "\"}");
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}