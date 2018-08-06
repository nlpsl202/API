using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flora_API
{
    public partial class GateCount : System.Web.UI.Page
    {
        private WriteLog writeLog = new WriteLog();
        string _ConnStr;
        rStoreProc _objQry_SP;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string a = Server.MapPath(".");
            //if (WebConfigurationManager.AppSettings["IpAddress"].ToString().IndexOf(Request.ServerVariables["REMOTE_ADDR"].ToString()) > -1)
            //{
                createapi();
            //}
            //else
            //{
            //    Response.Write("<script>alert('IP權限不符！')</script>");
            //    writeLog.Write_Log(Request.ServerVariables["REMOTE_ADDR"].ToString() + "TicketUsedData IP權限不符！");
            //}
        }

        public void createapi()
        {
            string RequestURL = Request.Url.AbsoluteUri.ToString();
            string[] SplitURL = RequestURL.Split('?');

            if (SplitURL.Length > 1)
            {
                string a = System.Web.HttpUtility.UrlDecode(SplitURL[1]);
                string sErrMsg = string.Empty;
                GateCountCall deserialized = JsonConvert.DeserializeObject<GateCountCall>(a.Replace(" ",""));
                _ConnStr = WebConfigurationManager.ConnectionStrings["TWFETConnection"].ToString();
                _objQry_SP = new rStoreProc(_ConnStr);
                _objQry_SP.StoreProcedureName = "SP_GATE_GateCountAPI";
                _objQry_SP.SetupSqlCommand(ref sErrMsg);
                _objQry_SP.SqlCmd.Parameters["@SPS_ID"].Value = deserialized.SPS_ID;
                _objQry_SP.SqlCmd.Parameters["@DEVICE_ID"].Value = deserialized.DEVICE_ID;
                _objQry_SP.SqlCmd.Parameters["@START_TIME"].Value = deserialized.START_TIME;
                _objQry_SP.SqlCmd.Parameters["@END_TIME"].Value = deserialized.END_TIME;
                _objQry_SP.SqlConn.Open();
                DataTable DT = new DataTable();
                using (SqlDataReader objDR = _objQry_SP.SqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    DT.Load(objDR);
                }

                GateCountResp[] Resp = new GateCountResp[DT.Rows.Count];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    Resp[i] = new GateCountResp
                    {
                        SPS_ID = DT.Rows[i]["SPS_ID"].ToString(),
                        DEVICE_ID = DT.Rows[i]["DEVICE_ID"].ToString(),
                        COUNT_TIME = DT.Rows[i]["COUNT_TIME"].ToString(),
                        IN_COUNT = DT.Rows[i]["IN_COUNT"].ToString(),
                        OUT_COUNT = DT.Rows[i]["OUT_COUNT"].ToString()
                    };
                }
                Response.Write(new JavaScriptSerializer().Serialize(Resp));
                writeLog.Write_Log("TicketUsedData " + a + "  " + new JavaScriptSerializer().Serialize(Resp));
            }
        }
    }
}