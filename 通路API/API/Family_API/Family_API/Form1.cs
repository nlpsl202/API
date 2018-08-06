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

namespace Family_API
{
    public partial class Form1 : Form
    {
        static string StartupPath = System.Windows.Forms.Application.StartupPath;
        IniFile ini;
        public Form1()
        {
            InitializeComponent();
            timeinitial();
            label3.Text = "";
            label5.Text = "";
            ini = new IniFile(StartupPath + "\\Setup.ini");
        }

        private void timeinitial()
        {
            GetTickets_timer.Interval = 300000;
            VerifyTickets_timer.Interval = 300000;
            GetTickets_API_Stop_btn.Enabled = false;
            VerifyTickets_API_Stop_btn.Enabled = false;
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

        private void GetDataDB(ref DataTable result)
        {
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            SqlConnection _con = new SqlConnection(completeConnStr);
            DataTable objDT = new DataTable();
            string sql = "";
            _con.Open();
            sql = string.Format(@"select TK_QRCODE,TK_USED_DT from cTicketWhitelist where TK_STATUS = '2' and TK_FEEDBACK<>'Y' and TK_PLACE='family'");
            SqlCommand cmd = new SqlCommand(sql, _con);
            SqlDataAdapter objDR = new SqlDataAdapter(cmd);
            objDR.Fill(objDT);
            _con.Close();
            result = objDT;
        }

        #region GetTickets_API 取得票券API
        private void GetTickets_API_Start_btn_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                MessageBox.Show("請勾選商品代碼");
                return;
            }
            label3.Text = "批次執行中......";
            GetTickets_timer.Start();
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            GetTickets_API_Start_btn.Enabled = false;
            GetTickets_API_Stop_btn.Enabled = true;
            GetTickets_timer_Tick(null,null);
        }

        private void GetTickets_timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (File.Exists(StartupPath + "\\Setup.ini"))
            {
                dt = DateTime.Parse(ini.IniReadValue("Setup", "LastTime"));
            }
            string Products_Code = string.Empty;
            if (checkBox1.Checked)
            {
                if (Products_Code != string.Empty)
                {
                    Products_Code +="|" + checkBox1.Text;
                }
                else
                {
                    Products_Code +=checkBox1.Text;
                }
            }
            if (checkBox2.Checked)
            {
                if (Products_Code != string.Empty)
                {
                    Products_Code += "|" + checkBox2.Text;
                }
                else
                {
                    Products_Code += checkBox2.Text;
                }
            }
            if (checkBox3.Checked)
            {
                if (Products_Code != string.Empty)
                {
                    Products_Code += "|" + checkBox3.Text;
                }
                else
                {
                    Products_Code += checkBox3.Text;
                }
            }

            GetTickets ticket = new GetTickets
            {
                TR_TYPE = "001",
                PASS_CODE = "23740512" + "962001",
                PRODUCTS_CODE = Products_Code,
                ORDERS_STIME = String.Format("{0:yyyy/MM/dd HH:mm:ss}", dt.AddMinutes(-6)),
                ORDERS_ETIME = String.Format("{0:yyyy/MM/dd HH:mm:ss}", dt)
            };
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["GetTicketsURL"].ToString());
            //httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(new JavaScriptSerializer().Serialize(ticket));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                label6.Text = result;
                string sql = "";
                try
                {
                    GetTicketsResp deserialized = JsonConvert.DeserializeObject<GetTicketsResp>(result);
                    if (deserialized.STATUS == "00")
                    {
                        using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                        {
                            _con.Open();
                            string TK_STATUS = string.Empty;
                            foreach (GetTicketsDataResp resp in deserialized.TICKETS)
                            {
                                if (!GetDataDB(resp.QR_CODE, resp.ORDERS_NO))
                                {
                                    if (resp.ORDER_TYPE == "R")
                                    {
                                        TK_STATUS = "X";
                                    }
                                    else
                                    {
                                        TK_STATUS = "1";
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
                                                            , resp.QR_CODE
                                                            , resp.ORDERS_NO
                                                            , resp.PRICE_NAME
                                                            , Convert.ToInt32(resp.PRICE)
                                                            , resp.ORDER_DATETIME
                                                            , "family"
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
                                    if (resp.ORDER_TYPE == "R")
                                    {
                                        TK_STATUS = "X";
                                        sql = string.Format(@"update cTicketWhitelist
                                                              set TK_ORDERDT='{0}',
                                                                  TK_STATUS='{1}',
                                                                  MODIFYDT='{2}',
                                                                  MODIFYID='{3}'
                                                              where TK_QRCODE='{4}'
                                                              and TK_ORDERNO='{5}'
                                                              and TK_STATUS<>'X'"
                                                              , resp.ORDER_DATETIME
                                                              , TK_STATUS
                                                              , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                                              , "family"
                                                              , resp.QR_CODE
                                                              , resp.ORDERS_NO);
                                        SqlCommand cmd = new SqlCommand(sql, _con);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    ini.IniWriteValue("Setup", "LastTime", String.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now));//儲存最後一次排程的時間
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

        #region VerifyTickets_API 票券核銷API
        private void VerifyTickets_API_Start_btn_Click(object sender, EventArgs e)
        {
            label5.Text = "批次執行中......";
            VerifyTickets_timer.Start();
            VerifyTickets_API_Start_btn.Enabled = false;
            VerifyTickets_API_Stop_btn.Enabled = true;
            VerifyTickets_timer_Tick(null, null);
        }

        private void VerifyTickets_timer_Tick(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            GetDataDB(ref DT);
            VerifyTickets[] ticket = new VerifyTickets[DT.Rows.Count];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ticket[i] = new VerifyTickets
                {
                    TR_TYPE = "001",
                    PASS_CODE = "23740512" + "962001",
                    QR_CODE = DT.Rows[i]["TK_QRCODE"].ToString(),
                    QR_TIME = DT.Rows[i]["TK_USED_DT"].ToString().Substring(0, 4) + "/" + DT.Rows[i]["TK_USED_DT"].ToString().Substring(4, 2) + "/" + DT.Rows[i]["TK_USED_DT"].ToString().Substring(6, 2) + " " + DT.Rows[i]["TK_USED_DT"].ToString().Substring(8, 2) + ":" + DT.Rows[i]["TK_USED_DT"].ToString().Substring(10, 2) + ":" + DT.Rows[i]["TK_USED_DT"].ToString().Substring(12, 2)
                };
            }
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["VerifyTicketsURL"].ToString());
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(new JavaScriptSerializer().Serialize(ticket));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                label7.Text = result;
                string sql = "";
                try
                {
                    IEnumerable<VerifyTicketsResp> deserialized = JsonConvert.DeserializeObject<IEnumerable<VerifyTicketsResp>>(result);
                    using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                    {
                        _con.Open();
                        string TK_FEEDBACKMEMO = string.Empty;
                        string TK_FEEDBACK = string.Empty;
                        foreach (VerifyTicketsResp resp in deserialized)
                        {
                            TK_FEEDBACKMEMO = resp.MESSAGE;
                            TK_FEEDBACK = "Y";
                            //每五分鐘更新UsedDate
                            sql = string.Format(@"update cTicketWhitelist set TK_FEEDBACK='{0}',TK_FEEDBACKMEMO='{1}',MODIFYDT='{2:yyyy-MM-dd HH:mm:ss}',MODIFYID='{3}' where TK_ORDERNO='{4}'", TK_FEEDBACK, TK_FEEDBACKMEMO, DateTime.Now, "admin", resp.ORDERS_NO);
                            SqlCommand cmd = new SqlCommand(sql, _con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                        
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void VerifyTickets_API_Stop_btn_Click(object sender, EventArgs e)
        {
            label5.Text = "批次已停止";
            VerifyTickets_timer.Stop();
            VerifyTickets_API_Start_btn.Enabled = true;
            VerifyTickets_API_Stop_btn.Enabled = false;
        }
        #endregion
    }
}
