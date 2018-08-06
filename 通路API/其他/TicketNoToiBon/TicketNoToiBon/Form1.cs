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
//using NPOI;
//using NPOI.HSSF.UserModel;
//using NPOI.XSSF.UserModel;
//using NPOI.SS.UserModel;

namespace TicketNoToiBon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            timeinitial();
        }

        private void GetAllProducts()
        {
            DateTime indbT = DateTime.Now;
            FuctionPack F = new FuctionPack();
            DataTable DT = new DataTable();

            GetDataDB(ref DT);

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string RequestApi = string.Format("http://localhost:50199/api/qrcode?QR_code='{0}'&QR_time='{1:yyyyMMdd}'", DT.Rows[i]["TK_QRCODE"].ToString(), DT.Rows[i]["TK_USED_DT"].ToString());
                F.GetApiData(RequestApi);
                updatedb(F.obj, indbT, DT.Rows[i]["TK_QRCODE"].ToString(), DT.Rows[i]["TK_USED_DT"].ToString());
            }           
        }

        public void updatedb(List<dynamic> obj, DateTime indbT,string qrcode,string useddate)
        {
            DataTable objDT = new DataTable();
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            string sql = "";
            string indbt = string.Format("{0:yyyy-MM-dd HH:mm:ss}", indbT);
            getdataFapi g = new getdataFapi();
            try
            {
                using (SqlConnection _con = new SqlConnection(completeConnStr))
                {
                    _con.Open();

                    for (int i = 0; i < obj.Count; i++)
                    {
                        g.TK_STATUS = obj[i]["status"].ToString();
                        
                        if (g.TK_STATUS == "S000")
                        {
                            g.TK_FEEDBACKMEMO = "成功";
                            g.TK_FEEDBACK = "Y";
                        }
                        else if (g.TK_STATUS == "E003")
                        {
                            g.TK_FEEDBACKMEMO = "票券不存在,或非該場次票券";
                            g.TK_FEEDBACK = "Y";
                        }
                        else if (g.TK_STATUS == "E004")
                        {
                            g.TK_FEEDBACKMEMO = "該票券已使用";
                            g.TK_FEEDBACK = "Y";
                        }
                        else if (g.TK_STATUS == "E005")
                        {
                            g.TK_FEEDBACKMEMO = "該票券已無效";
                            g.TK_FEEDBACK = "Y";
                        }
                        else
                        {
                            g.TK_FEEDBACKMEMO = "ERROR";
                            g.TK_FEEDBACK = "N";
                        }
                        g.TK_PRICETYPES = obj[i]["priceType"].ToString();
                        //每五分鐘更新UsedDate
                        sql = string.Format(@"update cTicketWhitelist set TK_FEEDBACK='{0}',TK_FEEDBACKMEMO='{1}',MODIFYDT='{2:yyyy-MM-dd HH:mm:ss}',MODIFYID='{3}' where TK_QRCODE='{4}' and MODIFYDT = ''", g.TK_FEEDBACK, g.TK_FEEDBACKMEMO, DateTime.Now, "admin", qrcode);
                        SqlCommand cmd = new SqlCommand(sql, _con);
                        cmd.ExecuteNonQuery();
                    }
                    _con.Close();
                }
            }
            catch (Exception e)
            {
                // GetAllProducts();
            }
        }
        private void GetDataDB(ref DataTable result)
        {
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            SqlConnection _con = new SqlConnection(completeConnStr);

            DataTable objDT = new DataTable();
            string sql2 = "", sql1 = "";
            //string TK_ORDERNO = "";
            //
            _con.Open();
            sql1 = string.Format(@"update cTicketWhitelist set TK_USED_DT='{0:yyyyMMdd}' where TK_STATUS = '2'",DateTime.Now);
            SqlCommand cmd = new SqlCommand(sql1, _con);
            cmd.ExecuteNonQuery();
            sql2 = string.Format(@"select * from cTicketWhitelist where TK_STATUS = '2'");
            SqlCommand cmd2 = new SqlCommand(sql2, _con);
            cmd2.ExecuteNonQuery();
            _con.Close();
            SqlDataAdapter objDR = new SqlDataAdapter(cmd2);
            objDR.Fill(objDT);
            result = objDT;
        }
        private void timeinitial()
        {
            timer1.Interval = 10000;//一秒=1000毫秒 一分鐘=60000毫秒 這裡為五分鐘同步一次
            timer1.Tick += new EventHandler(timer1_Tick);
            button2.Click += new EventHandler(button2_Click);
            button3.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //nowT = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            //nowT2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            //GetAllProducts();
            CreateProductAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FuctionPack F = new FuctionPack();
            //string[] in_params = { "deviceId", "channelId" };
            //string[] in_values = { "1", "1" };
            F.table_name = "cTicketWhiteList";
            //F.in_params = in_params;
            //F.in_value = in_values;
            //F.extra_script = "order by in_cnt desc";
            DataTable result = new DataTable();
            F.GetDBData(ref result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "批次執行中...";
            nowT = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            nowT2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            timer1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            timer1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
        }
        
        class getdataFapi
        {
            public string TK_QRCODE { get; set; }
            public string TK_ORDERNO { get; set; }
            public string TK_PRICETYPES { get; set; }
            public int TK_PRICE { get; set; }
            public string TK_ORDERDT { get; set; }
            public string TK_PLACE { get; set; }
            public string TK_STATUS { get; set; }
            public string TK_USED_DT { get; set; }
            public string TK_END_DT { get; set; }
            public string TK_FEEDBACK { get; set; }
            public string TK_FEEDBACKMEMO { get; set; }
            public string CREATEDT { get; set; }
            public string CREATEID { get; set; }
            public string MODIFYDT { get; set; }
            public string MODIFYID { get; set; }
        }
        public string nowT { get; set; }
        public string nowT2 { get; set; }






        //POST
        class QTicket
        {
            public string TK_QRCODE { get; set; }
            public string TK_USED_DT { get; set; }
        }

        class QTicketResp
        {
            public string TK_QRCODE { get; set; }
            public string TK_USED_DT { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public string priceType { get; set; }
            public string priceAreas { get; set; }
            public string seat { get; set; }
        }

        private async void CreateProductAsync()
        {
            DataTable DT = new DataTable();
            GetDataDB(ref DT);

            QTicket[] qtic = new QTicket[DT.Rows.Count];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                qtic[i] = new QTicket
                {
                    TK_QRCODE = DT.Rows[i]["TK_QRCODE"].ToString(),
                    TK_USED_DT = DT.Rows[i]["TK_USED_DT"].ToString(),
                };
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://localhost:50199/api/qrcode", new StringContent(
            new JavaScriptSerializer().Serialize(qtic), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            if (response.IsSuccessStatusCode)
            {
                // Get the response
                var customerJsonString = await response.Content.ReadAsStringAsync();
                // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<QTicketResp>>(customerJsonString);
                // Do something with it
                getdataFapi g = new getdataFapi();
                string sql = "";
                try
                {
                    using (SqlConnection _con = new SqlConnection(myConnStr))
                    {
                        _con.Open();

                        foreach (QTicketResp qTicketResp in deserialized)
                        {
                            if (qTicketResp.status == "S000")
                            {
                                g.TK_FEEDBACKMEMO = "成功";
                                g.TK_FEEDBACK = "Y";
                            }
                            else if (qTicketResp.status == "E003")
                            {
                                g.TK_FEEDBACKMEMO = "票券不存在,或非該場次票券";
                                g.TK_FEEDBACK = "Y";
                            }
                            else if (qTicketResp.status == "E004")
                            {
                                g.TK_FEEDBACKMEMO = "該票券已使用";
                                g.TK_FEEDBACK = "Y";
                            }
                            else if (qTicketResp.status == "E005")
                            {
                                g.TK_FEEDBACKMEMO = "該票券已無效";
                                g.TK_FEEDBACK = "Y";
                            }
                            else
                            {
                                g.TK_FEEDBACKMEMO = "ERROR";
                                g.TK_FEEDBACK = "N";
                            }
                            //每五分鐘更新UsedDate
                            sql = string.Format(@"update cTicketWhitelist set TK_FEEDBACK='{0}',TK_FEEDBACKMEMO='{1}',MODIFYDT='{2:yyyy-MM-dd HH:mm:ss}',MODIFYID='{3}' where TK_QRCODE='{4}'", g.TK_FEEDBACK, g.TK_FEEDBACKMEMO, DateTime.Now, "admin", qTicketResp.TK_QRCODE);
                            SqlCommand cmd = new SqlCommand(sql, _con);
                            cmd.ExecuteNonQuery();
                        }

                        _con.Close();
                    }
                }
                catch (Exception e)
                {
                }
            }
            // return URI of the created resource.
            //return response.Headers.Location;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "批次執行中...";
            nowT = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            nowT2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            timer1.Enabled = true;
            button1.Enabled = false;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "批次已停止";
            timer1.Enabled = false;
            button1.Enabled = true;
            button4.Enabled = false;
        }
    }
}
