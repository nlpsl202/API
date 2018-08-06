using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;



class FuctionPack
{
    public string ConnStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
    public string sql { get; set; }

    /// <summary>
    /// Get Api Data
    /// </summary>
    /// <param name="LoginApi"></param>
    /// <param name="RequestApi"></param>
    public string LoginApi { get; set; }
    public string RequestApi { get; set; }
    public List<dynamic> obj { get; set; }

    public void GetApiData(string RequestApi)
    {
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            CookieContainer cookies = new CookieContainer();

            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(LoginApi);
            //webRequest.CookieContainer = cookies;
            //HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            //StreamReader responseReader = new StreamReader(response.GetResponseStream());
            //string sResponseHTML = responseReader.ReadToEnd();

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(RequestApi);
            Request.CookieContainer = cookies;
            HttpWebResponse response2 = (HttpWebResponse)Request.GetResponse();
            StreamReader responseReader2 = new StreamReader(response2.GetResponseStream());
            string result = responseReader2.ReadToEnd();

            obj = js.Deserialize<List<dynamic>>(result);

            //var result2 = result.Split(',').ToDictionary(i => i.Split(':'));
            //Dictionary<string[], string> obj = result2;
    
          //.ToDictionary(key => key[0].Trim(), value => value[1].Trim());
            //obj = js.Deserialize<Dictionary<string, dynamic>>(result);
            //Dictionary<string, dynamic> obj = js.Deserialize<Dictionary<string, dynamic>>(result);
        }
        catch (Exception e)
        {
            GetApiData(RequestApi);
        }
    }
    /// <summary>
    ///  SELECT DB DATA FOR NOW , Return Datatable seems better than Objec
    /// </summary>
    /// <param name="in_params"></param>
    /// <param name="in_value"></param>
    /// <param name="table_name"></param>
    /// <param name="result"></param>
    /// Dictionary<string,string> result <--return datatype ; result.Add(a, c)<--add data for dictionary 

    public DataTable Result { get; set; }
    public string table_name { get; set; }
    public string[] in_params { get; set; }
    public string[] in_value { get; set; }
    public string extra_script { get; set; }
    public void GetDBData(ref DataTable result)
    {

        SqlConnection _con = new SqlConnection(ConnStr);
        sql = string.Format("Select * from {0}", table_name);

        if (in_params != null)
        {
            for (int i = 0; i < in_params.Length; i++)
            {
                if (i != 0)
                {
                    sql += string.Format(" and {0}='{1}'", in_params[i], in_value[i]);
                }
                else
                {
                    sql += string.Format(" where {0}='{1}'", in_params[i], in_value[i]);
                }
            }
        }

        if (extra_script != null)
        {
            sql += " " + extra_script;
        }

        _con.Open();
        SqlCommand cmd = new SqlCommand(sql, _con);
        SqlDataAdapter objDR = new SqlDataAdapter(cmd);
        objDR.Fill(result);
        _con.Close();
    }

    public void InsertData2DB()
    {
        SqlConnection _con = new SqlConnection(ConnStr);
        sql = string.Format("insert into {0}(", table_name);
        for (int i = 0; i < in_params.Length; i++)
        {
            if (i == in_params.Length)
            {
                sql += in_params[i] + ")values(";
            }
            else
            {
                sql += in_params[i] + ",";
            }
        }

        for (int j = 0; j < in_value.Length; j++)
        {
            if (j == in_params.Length)
            {
                sql += in_value[j] + ")";
            }
            else
            {
                sql += in_value[j] + ",";
            }
        }
        _con.Open();
        SqlCommand cmd = new SqlCommand(sql, _con);
        cmd.ExecuteNonQuery();

        _con.Close();
    }
}

