using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace Gomaji_API
{
    public partial class Form1 : Form
    {
        static string StartupPath = System.Windows.Forms.Application.StartupPath;
        private IniFile ini;
        private WriteLog writeLog;
        public Form1()
        {
            InitializeComponent();
            timeinitial();
            label3.Text = "";
            label10.Visible = false;
            ini = new IniFile(StartupPath + "\\Setup.ini");
            writeLog = new WriteLog();
        }

        private void timeinitial()
        {
            GetTickets_timer.Interval = 300000;
            GetTickets_API_Stop_btn.Enabled = false;
        }

        private bool GetDataDB(string TK_QRCODE, string TK_ORDERNO)
        {
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            SqlConnection _con = new SqlConnection(completeConnStr);
            DataTable objDT = new DataTable();
            string sql = string.Format(@"select top 1 * from cTicketWhitelist where TK_QRCODE='{0}' and TK_ORDERNO='{1}' order by CREATEDT desc", TK_QRCODE, TK_ORDERNO);
            _con.Open();
            SqlCommand cmd = new SqlCommand(sql, _con);
            SqlDataAdapter objDR = new SqlDataAdapter(cmd);
            objDR.Fill(objDT);
            _con.Close();

            if (objDT.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region GetTickets_API
        private void GetTickets_API_Start_btn_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked)
            {
                MessageBox.Show("請勾選查詢類別");
                return;
            }
            label3.Text = "批次執行中......";
            GetTickets_timer.Start();
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            GetTickets_API_Start_btn.Enabled = false;
            GetTickets_API_Stop_btn.Enabled = true;
            GetTickets_timer_Tick(null, null);
        }

        private void GetTickets_timer_Tick(object sender, EventArgs e)
        {
            DateTime dt=DateTime.Now;
            if (File.Exists(StartupPath + "\\Setup.ini"))
            {
                dt = DateTime.Parse(ini.IniReadValue("Setup", "LastTime"));
            }
            GetTickets ticket = new GetTickets
            {
                query_type = 2,
                start_time = Convert.ToInt32(dt.AddMinutes(-8).AddHours(-8).Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds),
                end_time = Convert.ToInt32(dt.AddMinutes(-2).AddHours(-8).Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds)
                //start_time = Convert.ToInt32(DateTime.Now.AddMinutes(-8).AddHours (-8).Subtract(new DateTime(1970, 1, 1,0,0,0)).TotalSeconds),
                //end_time = Convert.ToInt32(DateTime.Now.AddMinutes(-2).AddHours(-8).Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds)
            };
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["URL"].ToString() + "query_type=" + ticket.query_type + "&start_time=" + ticket.start_time + "&end_time=" + ticket.end_time);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                string sql = "";
                string TK_STATUS = "";
                try
                {
                    GetTicketsResp deserialized = JsonConvert.DeserializeObject<GetTicketsResp>(result);
                    if (deserialized.code == 1 && deserialized.total_items > 0)
                    {
                        using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                        {
                            _con.Open();
                            foreach (GetTicketsDataResp resp in deserialized.data)
                            {
                                if (!GetDataDB(resp.qrcode, resp.bill_no))
                                {
                                    if (resp.status == "Y")
                                    {
                                        TK_STATUS = "1";
                                    }
                                    else if (resp.status == "U")
                                    {
                                        TK_STATUS = "2";
                                    }
                                    else if (resp.status == "X")
                                    {
                                        TK_STATUS = "X";
                                    }
                                    sql = string.Format(@"insert into cTicketWhitelist
                                                        (TK_QRCODE
                                                        ,TK_ORDERNO
                                                        ,TK_PRICETYPES
                                                        ,TK_PRICE
                                                        ,TK_ORDERDT
                                                        ,TK_PLACE
                                                        ,TK_STATUS
                                                        ,TK_USED_DT
                                                        ,TK_END_DT
                                                        ,TK_FEEDBACK
                                                        ,TK_FEEDBACKMEMO
                                                        ,CREATEDT
                                                        ,CREATEID
                                                        ,MODIFYDT
                                                        ,MODIFYID) 
                                                         values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                                                        , resp.qrcode
                                                        , resp.bill_no
                                                        , resp.product_name.Substring(resp.product_name.IndexOf("】") + 1, resp.product_name.IndexOf("單") - resp.product_name.IndexOf("】")-1) + "票"
                                                        , Convert.ToInt32(resp.Price)
                                                        , resp.deal_dt
                                                        , "gomaji"
                                                        , TK_STATUS //1:售出  2:已使用  X:作廢
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                                        , "admin"
                                                        , null
                                                        , null);
                                    SqlCommand cmd = new SqlCommand(sql, _con);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    if (resp.status == "X")
                                    {
                                        sql = string.Format(@"update cTicketWhitelist
                                                              set TK_ORDERDT='{0}',
                                                                  TK_STATUS='{1}',
                                                                  MODIFYDT='{2}',
                                                                  MODIFYID='{3}'
                                                              where TK_QRCODE='{4}'
                                                              and TK_ORDERNO='{5}'
                                                              and TK_STATUS<>'X'"
                                                                , resp.modify_dt
                                                                , resp.status
                                                                , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                                                , "udn"
                                                                , resp.qrcode
                                                                , resp.bill_no);
                                        SqlCommand cmd = new SqlCommand(sql, _con);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    ini.IniWriteValue("Setup", "LastTime", String.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now));//儲存最後一次排程的時間
                    writeLog.Write_Log("GetTickets " + ticket.start_time + " ~ " + ticket.end_time + ": ErrCode = " + deserialized.code + " , RowCount = " + deserialized.total_items);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void GetTickets_API_Stop_btn_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            label3.Text = "批次已停止";
            GetTickets_timer.Stop();
            GetTickets_API_Start_btn.Enabled = true;
            GetTickets_API_Stop_btn.Enabled = false;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dtS = DateTime.Parse(textBox1.Text.Trim() + " 00:00:00");
            DateTime dtE = DateTime.Parse(textBox2.Text.Trim() + " 00:00:00");
            int days = new TimeSpan(dtE.Ticks - dtS.Ticks).Days;
            GetTickets ticket = new GetTickets();
            for (int i = 0; i <= days; i++)
            {
                ticket.query_type = 2;
                ticket.start_time = Convert.ToInt32(DateTime.Parse(String.Format("{0:yyyy/MM/dd}", dtS) + " 00:00:00").AddHours(-8).Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
                ticket.end_time = Convert.ToInt32(DateTime.Parse(String.Format("{0:yyyy/MM/dd}", dtS) + " 23:59:59").AddHours(-8).Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["URL"].ToString() + "query_type=" + ticket.query_type + "&start_time=" + ticket.start_time + "&end_time=" + ticket.end_time);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    string sql = "";
                    string TK_STATUS = "";
                    try
                    {
                        GetTicketsResp deserialized = JsonConvert.DeserializeObject<GetTicketsResp>(result);
                        if (deserialized.code == 1 && deserialized.total_items > 0)
                        {
                            using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                            {
                                _con.Open();
                                foreach (GetTicketsDataResp resp in deserialized.data)
                                {
                                    resp.product_name = resp.product_name.Substring(resp.product_name.IndexOf("】") + 1, resp.product_name.IndexOf("單") - resp.product_name.IndexOf("】") - 1) + "票";
                                    if (resp.product_name.Equals("花博預售優惠票"))
                                        resp.product_name = "預售優惠票";
                                    else if (resp.product_name.Equals("花博預售票"))
                                        resp.product_name = "預售全票";
                                    if (!GetDataDB(resp.qrcode, resp.bill_no))
                                    {
                                        if (resp.status == "Y")
                                        {
                                            TK_STATUS = "1";
                                        }
                                        else if (resp.status == "U")
                                        {
                                            TK_STATUS = "2";
                                        }
                                        else if (resp.status == "X")
                                        {
                                            TK_STATUS = "X";
                                        }
                                        sql = string.Format(@"insert into cTicketWhitelist
                                                        (TK_QRCODE
                                                        ,TK_ORDERNO
                                                        ,TK_PRICETYPES
                                                        ,TK_PRICE
                                                        ,TK_ORDERDT
                                                        ,TK_PLACE
                                                        ,TK_STATUS
                                                        ,TK_USED_DT
                                                        ,TK_END_DT
                                                        ,TK_FEEDBACK
                                                        ,TK_FEEDBACKMEMO
                                                        ,CREATEDT
                                                        ,CREATEID
                                                        ,MODIFYDT
                                                        ,MODIFYID) 
                                                         values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                                                            , resp.qrcode
                                                            , resp.bill_no
                                                            , resp.product_name
                                                            , Convert.ToInt32(resp.Price)
                                                            , resp.deal_dt
                                                            , "gomaji"
                                                            , TK_STATUS //1:售出  2:已使用  X:作廢
                                                            , ""
                                                            , ""
                                                            , ""
                                                            , ""
                                                            , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                                            , "admin"
                                                            , null
                                                            , null);
                                        SqlCommand cmd = new SqlCommand(sql, _con);
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        if (resp.status == "X")
                                        {
                                            sql = string.Format(@"update cTicketWhitelist
                                                              set TK_ORDERDT='{0}',
                                                                  TK_STATUS='{1}',
                                                                  MODIFYDT='{2}',
                                                                  MODIFYID='{3}'
                                                              where TK_QRCODE='{4}'
                                                              and TK_ORDERNO='{5}'
                                                              and TK_STATUS<>'X'"
                                                                    , resp.modify_dt
                                                                    , resp.status
                                                                    , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                                                    , "udn"
                                                                    , resp.qrcode
                                                                    , resp.bill_no);
                                            SqlCommand cmd = new SqlCommand(sql, _con);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                        dtS = dtS.AddDays(1);
                        writeLog.Write_Log("GetTickets " + ticket.start_time + " ~ " + ticket.end_time + ": ErrCode = " + deserialized.code + " , RowCount = " + deserialized.total_items);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
            }
            label10.Visible = true;
            ini.IniWriteValue("Setup", "LastTime", String.Format("{0:yyyy/MM/dd}", dtS) + " 00:00:00");//儲存最後一次排程的時間
        }
    }
}
