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

namespace iBon_API
{
    public partial class Form1 : Form
    {
        static string StartupPath = System.Windows.Forms.Application.StartupPath;
        public ServiceReference1.ibonTicketSoapClient Svc = new ServiceReference1.ibonTicketSoapClient();
        int block;
        IniHelper ini;
        public Form1()
        {
            InitializeComponent();
            ConnectionStatus_lbl.Text = "";
            TotalCount_lbl.Text = "";
            BlockCount_lbl.Text = "";
            label12.Text = "";
            label13.Text = "";
            block_lbl.Text = "";
            textBox1.Text = "2018/05/22 00:00:00";
            textBox2.Text = "2018/06/28 23:59:59";
            ini = new IniHelper(StartupPath + "\\Setup.ini");
            if (File.Exists(StartupPath + "\\Setup.ini"))
            {
                if (ini.ReadValue("Setup", "block") != null && ini.ReadValue("Setup", "block") != "")
                    block = Convert.ToInt32(ini.ReadValue("Setup", "block"));
                else
                    block = 1;
            }
            timeinitial();
        }

        private void timeinitial()
        {
            GetTicketData_timer.Interval = 300000;
            CheckTicketData_timer.Interval = 300000;
            GetTicketData_API_Stop_btn.Enabled = false;
            CheckTicketData_API_Stop_btn.Enabled = false;
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
            sql = string.Format(@"select TK_QRCODE,SUBSTRING(TK_USED_DT,1,4)+'/'+SUBSTRING(TK_USED_DT,5,2)+'/'+SUBSTRING(TK_USED_DT,7,2)+' '+SUBSTRING(TK_USED_DT,9,2)+':'+SUBSTRING(TK_USED_DT,11,2)+':'+SUBSTRING(TK_USED_DT,13,2) as TK_USED_DT from cTicketWhitelist where TK_STATUS = '2' and TK_FEEDBACK<>'Y' and TK_PLACE='ibon'");
            SqlCommand cmd = new SqlCommand(sql, _con);
            SqlDataAdapter objDR = new SqlDataAdapter(cmd);
            objDR.Fill(objDT);
            _con.Close();
            result = objDT;
        }

        #region CheckConnection
        private void CheckConnection_btn_Click(object sender, EventArgs e)
        {
            if (Svc.CheckConnection() == "true")
                ConnectionStatus_lbl.Text = "ok";
            else
                ConnectionStatus_lbl.Text = "failed";
        }
        #endregion

        #region GetTicketData_API
        private void GetTicketData_API_Start_btn_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked)
            {
                MessageBox.Show("請勾選商品代碼");
                return;
            }
            label12.Text = "批次執行中......";
            GetTicketData_timer.Start();
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            GetTicketData_API_Start_btn.Enabled = false;
            GetTicketData_API_Stop_btn.Enabled = true;
            GetTicketData_timer_Tick(null,null);
        }

        private void GetTicketData_timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (File.Exists(StartupPath + "\\Setup.ini"))
            {
                dt = DateTime.Parse(ini.ReadValue("Setup", "LastTime"));
            }
            string Products_Code = string.Empty;
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                Products_Code += checkBox1.Text;
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                Products_Code += checkBox1.Text + "|" + checkBox2.Text;
            }
            else if (!checkBox1.Checked && checkBox2.Checked)
            {
                Products_Code += checkBox2.Text;
            }
            GetTicketData ticket = new GetTicketData
            {
                Products_Code = Products_Code,
                Orders_STIME = "2018/05/22 00:00:00",
                Orders_ETIME = "",
                Block = block
            };
            string ResponseData = Svc.GetTicketData("[" + JsonConvert.SerializeObject(ticket) + "]");
            string sql = "";
            try
            {
                block_lbl.Text = block.ToString();
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<GetTicketDataResp>>(ResponseData);
                if (deserialized.Count<GetTicketDataResp>() == 300)
                {
                    block++;
                    ini.WriteValue("Setting", "block", block.ToString());
                }
                using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    _con.Open();
                    foreach (GetTicketDataResp resp in deserialized)
                    {
                        string TK_STATUS = string.Empty;
                        if (!GetDataDB(resp.QR_Code, resp.Orders_No))
                        {
                            if (resp.Order_Type == "R")
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
                                                    , resp.QR_Code
                                                    , resp.Orders_No
                                                    , resp.PriceTypes
                                                    , Convert.ToInt32(resp.Price)
                                                    , resp.Order_datetime
                                                    , "ibon"
                                                    , TK_STATUS
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                                    , "admin"
                                                    , null
                                                    , null);//1:售出  2:已使用  X:作廢
                            SqlCommand cmd = new SqlCommand(sql, _con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                ini.WriteValue("Setup", "LastTime", String.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now));//儲存最後一次排程的時間
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void GetTicketData_API_Stop_btn_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            label12.Text = "批次已停止";
            GetTicketData_timer.Stop();
            GetTicketData_API_Start_btn.Enabled = true;
            GetTicketData_API_Stop_btn.Enabled = false;
        }
        #endregion

        #region CheckTicketData_API
        private void CheckTicketData_API_Start_btn_Click(object sender, EventArgs e)
        {
            label13.Text = "批次執行中......";
            CheckTicketData_timer.Start();
            CheckTicketData_API_Start_btn.Enabled = false;
            CheckTicketData_API_Stop_btn.Enabled = true;
            CheckTicketData_timer_Tick(null, null);
        }

        private void CheckTicketData_timer_Tick(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            GetDataDB(ref DT);
            CheckTicketData[] ticket = new CheckTicketData[DT.Rows.Count];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ticket[i] = new CheckTicketData
                {
                    QR_code = DT.Rows[i]["TK_QRCODE"].ToString(),
                    QR_time = DT.Rows[i]["TK_USED_DT"].ToString(),
                };
            }
            string ResponseData = Svc.CheckTicketData(JsonConvert.SerializeObject(ticket));
            string sql = "";
            try
            {
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<CheckTicketDataResp>>(ResponseData);
                using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    _con.Open();
                    string TK_FEEDBACKMEMO = string.Empty;
                    string TK_FEEDBACK = string.Empty;
                    int i = 0;
                    foreach (CheckTicketDataResp ticketResp in deserialized)
                    {
                        if (ticketResp.status == "S000")
                        {
                            TK_FEEDBACKMEMO = "成功";
                            TK_FEEDBACK = "Y";
                        }
                        else if (ticketResp.status == "E003")
                        {
                            TK_FEEDBACKMEMO = "票券不存在,或非該場次票券";
                            TK_FEEDBACK = "Y";
                        }
                        else if (ticketResp.status == "E004")
                        {
                            TK_FEEDBACKMEMO = "該票券已使用";
                            TK_FEEDBACK = "Y";
                        }
                        else if (ticketResp.status == "E005")
                        {
                            TK_FEEDBACKMEMO = "該票券已無效";
                            TK_FEEDBACK = "Y";
                        }
                        else
                        {
                            TK_FEEDBACKMEMO = "ERROR";
                            TK_FEEDBACK = "N";
                        }
                        //每五分鐘更新UsedDate
                        sql = string.Format(@"update cTicketWhitelist set TK_FEEDBACK='{0}',TK_FEEDBACKMEMO='{1}',MODIFYDT='{2:yyyy-MM-dd HH:mm:ss}',MODIFYID='{3}' where TK_QRCODE='{4}'", TK_FEEDBACK, TK_FEEDBACKMEMO, DateTime.Now, "admin", ticket[i].QR_code);
                        SqlCommand cmd = new SqlCommand(sql, _con);
                        cmd.ExecuteNonQuery();
                        i++;
                    }
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void CheckTicketData_API_Stop_btn_Click(object sender, EventArgs e)
        {
            label13.Text = "批次已停止";
            CheckTicketData_timer.Stop();
            CheckTicketData_API_Start_btn.Enabled = true;
            CheckTicketData_API_Stop_btn.Enabled = false;
        }
        #endregion

        #region GetTicketCount_API
        private void GetTicketCount_API_Start_btn_Click(object sender, EventArgs e)
        {
            string Products_Code = string.Empty;
            if (checkBox3.Checked && !checkBox4.Checked)
            {
                Products_Code += checkBox3.Text;
            }
            if (checkBox3.Checked && checkBox4.Checked)
            {
                Products_Code += checkBox3.Text + "|" + checkBox4.Text;
            }
            if (!checkBox3.Checked && checkBox4.Checked)
            {
                Products_Code += checkBox4.Text;
            }
            if (!checkBox3.Checked && !checkBox4.Checked)
            {
                MessageBox.Show("請勾選商品代碼");
                return;
            }
            GetTicketCount ticket = new GetTicketCount
            {
                Products_Code = Products_Code,
                Orders_STIME = textBox1.Text,
                Orders_ETIME = textBox2.Text,
            };
            string ResponseData = Svc.GetTicketCount("[" + JsonConvert.SerializeObject(ticket) + "]");
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<GetTicketCountResp>>(ResponseData);
            foreach (GetTicketCountResp resp in deserialized)
            {
                TotalCount_lbl.Text = resp.TotalCount.ToString();
                BlockCount_lbl.Text = resp.BlockCount.ToString();
            }
        }
        #endregion
    }
}
