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



namespace iBonToTicketL
{
    public partial class Form1 : Form
    {
        private int Block = 1;
        public Form1()
        {
            InitializeComponent();
            label3.Text = "";
            label2.Text = "";
            checkBox1.Checked = true;
            timeinitial();
            //FuctionPack F = new FuctionPack();
        }

        private void GetAllProducts()
        {
            FuctionPack F = new FuctionPack();
            string beginT = string.Format("{0:yyyy/MM/dd HH:00:00}", DateTime.Now);
            string endT = string.Format("{0:yyyy/MM/dd HH:00:00}", DateTime.Now.AddHours(1));
            string RequestApi = string.Empty;
            string Pro_code = (string)checkBox1.Text.ToString();
            string Pro_code2 = (string)checkBox2.Text.ToString();
            if(checkBox2.Checked == true)
            {
                RequestApi = string.Format("http://localhost:54191/api/tic?Products_Code={0}|{1}&Orders_STIME={2}&Orders_ETIME={3}", Pro_code, Pro_code2, beginT, endT);
            }
            else
            {
                RequestApi = string.Format("http://localhost:54191/api/tic?Products_Code={0}&Orders_STIME={1}&Orders_ETIME={2}", Pro_code,beginT, endT);
            }
            DateTime indbT = DateTime.Now;

            F.GetApiData(RequestApi);
            insertdb(F.obj, indbT);
        }

        public void insertdb(List<dynamic> obj, DateTime indbT)
        {
            DataTable objDT = new DataTable();
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            //int totalcount = 300;
            string sql = "";
            //int insidecnt = 0;
            //int dbin = 0;
            //nt dbout = 0;
            //string indbt = string.Format("{0:HH:mm}", indbT);
            //string indbtup = string.Format("{0:HH:mm}", indbT.AddMinutes(-1));
            string indbt = string.Format("{0:yyyy-MM-dd HH:mm:ss}", indbT);
            getdataFapi g = new getdataFapi();
            try
            {

                using (SqlConnection _con = new SqlConnection(completeConnStr))
                {
                    _con.Open();

                    for (int i = 0; i < obj.Count; i++)
                    {
                        g.TK_QRCODE = obj[i]["QR_Code"].ToString();//obj["data"][i]["TK_QRCODE"].ToString();
                        g.TK_ORDERNO = obj[i]["Orders_No"].ToString();
                        g.TK_PRICETYPES = obj[i]["PriceTypes"].ToString();
                        g.TK_PRICE = Convert.ToInt32(obj[i]["Price"].ToString());
                        g.TK_ORDERDT = obj[i]["Order_datetime"].ToString();
                        g.TK_PLACE = "ibon";
                        g.TK_STATUS = obj[i]["Order_Type"].ToString(); //B:購票  R:退票
                        g.TK_USED_DT = "";
                        g.TK_END_DT = "";
                        g.TK_FEEDBACK = "";
                        g.TK_FEEDBACKMEMO = "";
                        g.CREATEDT = indbt;
                        g.CREATEID = "admin";
                        g.MODIFYDT = null;
                        g.MODIFYID = null;

                        if (!GetDataDB(g.TK_QRCODE))
                        {
                            if (g.TK_STATUS == "R")
                            {
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
                                                    ,MODIFYID
                                                        ) 
                                                     values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                                                                                , g.TK_QRCODE
                                                                                , g.TK_ORDERNO
                                                                                , g.TK_PRICETYPES
                                                                                , g.TK_PRICE
                                                                                , g.TK_ORDERDT
                                                                                , g.TK_PLACE
                                                                                , "X"
                                                                                , g.TK_USED_DT
                                                                                , g.TK_END_DT
                                                                                , g.TK_FEEDBACK
                                                                                , g.TK_FEEDBACKMEMO
                                                                                , g.CREATEDT
                                                                                , g.CREATEID
                                                                                , g.MODIFYDT
                                                                                , g.MODIFYID
                                                                                );//1:售出  2:已使用  X:作廢
                                SqlCommand cmd = new SqlCommand(sql, _con);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
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
                                                    ,MODIFYID
                                                        ) 
                                                     values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                                                                                , g.TK_QRCODE
                                                                                , g.TK_ORDERNO
                                                                                , g.TK_PRICETYPES
                                                                                , g.TK_PRICE
                                                                                , g.TK_ORDERDT
                                                                                , g.TK_PLACE
                                                                                , "1"
                                                                                , g.TK_USED_DT
                                                                                , g.TK_END_DT
                                                                                , g.TK_FEEDBACK
                                                                                , g.TK_FEEDBACKMEMO
                                                                                , g.CREATEDT
                                                                                , g.CREATEID
                                                                                , g.MODIFYDT
                                                                                , g.MODIFYID
                                                                                );
                                SqlCommand cmd = new SqlCommand(sql, _con);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        //else
                        //{
//                            sql = string.Format(@"insert into cTicketWhitelist
//                                                    (TK_QRCODE
//                                                    ,TK_ORDERNO
//                                                    ,TK_PRICETYPES
//                                                    ,TK_PRICE
//                                                    ,TK_ORDERDT
//                                                    ,TK_PLACE
//                                                    ,TK_STATUS
//                                                    ,TK_USED_DT
//                                                    ,TK_END_DT
//                                                    ,TK_FEEDBACK
//                                                    ,TK_FEEDBACKMEMO
//                                                    ,CREATEDT
//                                                    ,CREATEID
//                                                    ,MODIFYDT
//                                                    ,MODIFYID
//                                                        ) 
//                                                     values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
//                                                    ,g.TK_QRCODE
//                                                    ,g.TK_ORDERNO
//                                                    ,g.TK_PRICETYPES
//                                                    ,g.TK_PRICE
//                                                    ,g.TK_ORDERDT
//                                                    ,g.TK_PLACE
//                                                    ,g.TK_STATUS
//                                                    ,g.TK_USED_DT
//                                                    ,g.TK_END_DT
//                                                    ,g.TK_FEEDBACK
//                                                    ,g.TK_FEEDBACKMEMO
//                                                    ,g.CREATEDT
//                                                    ,g.CREATEID
//                                                    ,g.MODIFYDT
//                                                    ,g.MODIFYID
//                                                    );
                        //}

                        //SqlCommand cmd = new SqlCommand(sql, _con);
                        //cmd.ExecuteNonQuery();
                    }
                    _con.Close();
                }
            }
            catch (Exception e)
            {
                // GetAllProducts();
            }
        }

        private bool GetDataDB(string TK_QRCODE)
        {
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder(myConnStr);
            string completeConnStr = sqlcsb.ConnectionString;
            SqlConnection _con = new SqlConnection(completeConnStr);

            DataTable objDT = new DataTable();

            string sql = string.Format(@"select top 1 * from cTicketWhitelist where TK_QRCODE='{0}' order by CREATEDT desc",TK_QRCODE);
            _con.Open();
            SqlCommand cmd = new SqlCommand(sql, _con);
            SqlDataAdapter objDR = new SqlDataAdapter(cmd);
            objDR.Fill(objDT);
            _con.Close();

            if(objDT.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void timeinitial()
        {
            timer1.Interval = 3000;
            timer1.Tick += new EventHandler(timer1_Tick);
            button2.Click += new EventHandler(button2_Click);
            button3.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            nowT = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            nowT2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            //GetAllProducts();
            CreateProductAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            label3.Text = "批次執行中...";
            nowT = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            nowT2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            timer1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            timer1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
        }

        class getdataFapi
        {
            //public int in_cnt { get; set; }
            //public int out_cnt { get; set; }
            //public int currentOccupancy { get; set; }
            //public double avgOccupancy { get; set; }
            //public string id { get; set; }
            //public string created { get; set; }
            //public string modified { get; set; }
            //public string deviceId { get; set; }
            //public string channelId { get; set; }
            //public int total_cnt { get; set; }
            //public string cal_date { get; set; }
            //public string cal_time { get; set; }
            //public int cal_hour { get; set; }
            //public string indb_time { get; set; }
            //public int inside_People { get; set; }
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
        class WTicket
        {
            public string Products_Code { get; set; }
            public string Orders_STIME { get; set; }
            public string Orders_ETIME { get; set; }
            public int Block { get; set; }
        }

        class WTicketResp
        {
            public string QR_Code { get; set; }
            public string Orders_No { get; set; }
            public string PriceTypes { get; set; }
            public int Price { get; set; }
            public string Order_datetime { get; set; }
            public string Order_Type { get; set; }
        }

        private async void CreateProductAsync()
        {
            string beginT = string.Format("{0:yyyy/MM/dd HH:00:00}", DateTime.Now);
            string endT = string.Format("{0:yyyy/MM/dd HH:00:00}", DateTime.Now.AddHours(1));
            string Pro_code = (string)checkBox1.Text.ToString();
            string Pro_code2 = (string)checkBox2.Text.ToString();
            WTicket ticket = new WTicket
            {
                Products_Code = "B01LITNY",
                Orders_STIME = beginT,
                Orders_ETIME = endT,
                Block = Block
            };
            Block++;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://210.61.47.134/QWARE_TICKET_ADMIN/WebServices/ibonTicket.asmx/GetTicketData", new StringContent(
                                           new JavaScriptSerializer().Serialize(ticket), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string myConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            if (response.IsSuccessStatusCode)
            {
                // Get the response
                var customerJsonString = await response.Content.ReadAsStringAsync();
                // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<WTicketResp>>(customerJsonString);
                // Do something with it
                getdataFapi g = new getdataFapi();
                string sql = "";
                try
                {
                    using (SqlConnection _con = new SqlConnection(myConnStr))
                    {
                        _con.Open();

                        foreach (WTicketResp wTicketResp in deserialized)
                        {
                            g.TK_QRCODE = wTicketResp.QR_Code;
                            g.TK_ORDERNO = wTicketResp.Orders_No;
                            g.TK_PRICETYPES = wTicketResp.PriceTypes;
                            g.TK_PRICE = Convert.ToInt32(wTicketResp.Price);
                            g.TK_ORDERDT = wTicketResp.Order_datetime;
                            g.TK_PLACE = "ibon";
                            g.TK_STATUS = wTicketResp.Order_Type; //B:購票  R:退票
                            g.TK_USED_DT = "";
                            g.TK_END_DT = "";
                            g.TK_FEEDBACK = "";
                            g.TK_FEEDBACKMEMO = "";
                            g.CREATEDT = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now); ;
                            g.CREATEID = "admin";
                            g.MODIFYDT = null;
                            g.MODIFYID = null;

                            if (!GetDataDB(g.TK_QRCODE))
                            {
                                if (g.TK_STATUS == "R")
                                {
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
                                                    ,MODIFYID
                                                        ) 
                                                     values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                                                                                    , g.TK_QRCODE
                                                                                    , g.TK_ORDERNO
                                                                                    , g.TK_PRICETYPES
                                                                                    , g.TK_PRICE
                                                                                    , g.TK_ORDERDT
                                                                                    , g.TK_PLACE
                                                                                    , "X"
                                                                                    , g.TK_USED_DT
                                                                                    , g.TK_END_DT
                                                                                    , g.TK_FEEDBACK
                                                                                    , g.TK_FEEDBACKMEMO
                                                                                    , g.CREATEDT
                                                                                    , g.CREATEID
                                                                                    , g.MODIFYDT
                                                                                    , g.MODIFYID
                                                                                    );//1:售出  2:已使用  X:作廢
                                    SqlCommand cmd = new SqlCommand(sql, _con);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
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
                                                    ,MODIFYID
                                                        ) 
                                                     values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                                                                                    , g.TK_QRCODE
                                                                                    , g.TK_ORDERNO
                                                                                    , g.TK_PRICETYPES
                                                                                    , g.TK_PRICE
                                                                                    , g.TK_ORDERDT
                                                                                    , g.TK_PLACE
                                                                                    , "1"
                                                                                    , g.TK_USED_DT
                                                                                    , g.TK_END_DT
                                                                                    , g.TK_FEEDBACK
                                                                                    , g.TK_FEEDBACKMEMO
                                                                                    , g.CREATEDT
                                                                                    , g.CREATEID
                                                                                    , g.MODIFYDT
                                                                                    , g.MODIFYID
                                                                                    );
                                    SqlCommand cmd = new SqlCommand(sql, _con);
                                    cmd.ExecuteNonQuery();
                                }
                            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            //label3.Text = "批次執行中...";
            //nowT = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            //nowT2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            //timer1.Enabled = true;
            //button1.Enabled = false;
            //button4.Enabled = true;
            CreateProductAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = "批次已停止";
            timer1.Enabled = false;
            button1.Enabled = true;
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:33880/WebService1.asmx/ConnectTest");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write("");
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                if (result == "true")
                    label2.Text = result;
                else
                    label2.Text = "false";
            }
        }
    }
}
