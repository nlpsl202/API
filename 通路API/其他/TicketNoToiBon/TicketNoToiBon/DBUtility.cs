using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
//using System.Web.UI.WebControls;
using System.Text;

public class rStoreProc
{
    //物件內部使用的變數, 前面以 _ 開頭表示
    //資料庫連接字串
    private string _ssConnStr = string.Empty;
    //要呼叫的 Store Procedure 名稱
    private string _ssStoreProcName = string.Empty;
    //儲存 Store Procedure 的欄位相關訊息
    private DataTable _dtStoreProcInfo;
    //執行 store procedure 是否要以 ExecuteNonQuery 的方式執行 (Insert, Update, Delete)
    private bool _bExecNonQuery = false;
    //用來執行 store procedure 的資料庫連接 object
    private SqlConnection _objConn;
    //用來執行 store procedure 的 object
    private SqlCommand _objSqlCmd;
    //做為 ruiGridViewPager 設定頁次的依據
    private int _iCurrentPageIndex = 0;

    /// <summary>
    /// 供呼叫端暫存目前顯示的次頁
    /// </summary>
    public int CurrentPageIndex
    {
        get { return _iCurrentPageIndex; }
        set { _iCurrentPageIndex = value; }
    }

    public rStoreProc(string sConnStr)
    {
	    _ssConnStr = sConnStr;
	    _objConn = new SqlConnection(_ssConnStr);
	    _dtStoreProcInfo = new DataTable();
    }

    /// <summary>
    /// 依傳入的參數名稱, 尋找參數是否存在
    /// </summary>
    /// <param name="sParamName"></param>
    /// <param name="Param"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public bool GetParameterByName(string sParamName, ref SqlParameter Param)
    {
	    if ((!sParamName.StartsWith("@")))
		    sParamName = "@" + sParamName;
	    foreach (SqlParameter P in _objSqlCmd.Parameters) {
		    if ((P.ParameterName == sParamName)) {
			    Param = P;
			    break; // TODO: might not be correct. Was : Exit For
		    }
	    }
	    return ((Param != null));
    }


    //依 ssStoreProcName 的值取得 store procedure 的參數
    //PS:後端 DB 不要設計沒有參數的 store procedure, 最少也要傳個 current user id, 最好再加個是前端的那個程式呼叫的, 方便日後系統要加 log 時, 可以由 store procedure 裡加
    //   如果 store procedure 沒有參數, 這個 function 回傳 false, 代表沒有個 store procedure.
    //執行的 SQL:
    //select sp.name as StoreProcName, typ.name as ParamType, parm.name as ParamName, parm.max_length as ParamLength,
    //       parm.precision, parm.scale, parm.is_output, parm.has_default_value, parm.default_value, typ.is_user_defined
    //  from sys.procedures sp
    //  join sys.parameters parm on sp.[object_id] = parm.[object_id]
    //  join sys.types typ ON parm.system_type_id = typ.system_type_id and parm.user_type_id = typ.user_type_id
    // where sp.name = 'SP_GetLeftMenuData'
    //order by sp.name, parm.parameter_id
    private bool GetStoreProcInfo(ref string sErrMsg)
    {
	    StringBuilder sbSQL = new StringBuilder();
	    sbSQL.AppendLine("select sp.name as StoreProcName, typ.name as ParamType, parm.name as ParamName, parm.max_length as ParamLength,");
        sbSQL.AppendLine("       parm.precision, parm.scale, parm.is_output, parm.has_default_value, parm.default_value, typ.is_user_defined");
	    sbSQL.AppendLine("  from sys.procedures sp");
	    sbSQL.AppendLine("  join sys.parameters parm on sp.[object_id] = parm.[object_id]");
	    sbSQL.AppendLine("  join sys.types typ ON parm.system_type_id = typ.system_type_id and parm.user_type_id = typ.user_type_id");
	    sbSQL.AppendLine(" where sp.name = '" + _ssStoreProcName + "'");
	    sbSQL.AppendLine("order by sp.name, parm.parameter_id");
	    try {
		    using (SqlConnection objConn = new SqlConnection(_ssConnStr)) {
			    using (SqlCommand objCmd = new SqlCommand(sbSQL.ToString(), objConn)) {
				    objConn.Open();
				    using (SqlDataReader objDR = objCmd.ExecuteReader(CommandBehavior.CloseConnection)) {
					    _dtStoreProcInfo.Load(objDR);
					    sErrMsg = "找不到 [" + _ssStoreProcName + "] store procedure 的參數(或者沒有參數)";
					    return (_dtStoreProcInfo.Rows.Count > 0);
				    }
			    }
		    }
	    } catch (Exception ex) {
		    sErrMsg = ex.Message;
		    return false;
	    }
    }

    //依傳入的 sType 設定 aParam.SqlDbType
    private bool SetupParamTypeWithText(string sType, ref SqlParameter aParam, ref string sErrMsg)
    {
	    if ((sType == "nvarchar")) {
		    aParam.SqlDbType = SqlDbType.NVarChar;
	    } else if ((sType == "varchar")) {
		    aParam.SqlDbType = SqlDbType.VarChar;
	    } else if ((sType == "nchar")) {
		    aParam.SqlDbType = SqlDbType.NChar;
	    } else if ((sType == "char")) {
		    aParam.SqlDbType = SqlDbType.Char;
	    } else if ((sType == "int")) {
		    aParam.SqlDbType = SqlDbType.Int;
	    } else if ((sType == "smallint")) {
		    aParam.SqlDbType = SqlDbType.SmallInt;
	    } else if ((sType == "datetime")) {
		    aParam.SqlDbType = SqlDbType.DateTime;
	    } else if ((sType == "numeric")) {
		    aParam.SqlDbType = SqlDbType.Int;
        } else if ((sType == "binary")){
            aParam.SqlDbType = SqlDbType.Binary;
        } else if ((sType == "date")){
            aParam.SqlDbType = SqlDbType.Date;
        }
        else if ((sType == "uniqueidentifier")) {
            aParam.SqlDbType = SqlDbType.UniqueIdentifier;
        } else {
		    sErrMsg = "傳入的型態[" + sType + "]超出預期!";
		    return false;
	    }
	    return true;
    }

    //依傳入的 objCtrl 取得輸入的值, 並寫入到 aParam.Value 裡
    //private bool SetParmValWithCtrl(ref System.Web.UI.Control objCtrl, ref SqlParameter aParam, ref string sErrMsg)
    //{
    //    if ((objCtrl is TextBox)) {
    //        TextBox aTxtBox = (TextBox)objCtrl;
    //        if ((aParam.SqlDbType == SqlDbType.Date) | (aParam.SqlDbType == SqlDbType.DateTime)) {
    //            //ToDoList:關於日期的轉換, 後面再來處理
    //            return false;
    //            //rdtDateUtility objDT = new rdtDateUtility(aTxtBox.Text, true);
    //            //if ((objDT.IsValidDate)) {
    //            //    aParam.Value = objDT.DateValue;
    //            //    return true;
    //            //} else {
    //            //    sErrMsg = "參數 [" + aParam.ParameterName.Substring(1) + "] 的值不為合法的日期格式!";
    //            //    return false;
    //            //}
    //            //ElseIf (aParam.SqlDbType = SqlDbType.BigInt) or (aParam.SqlDbType = SqlDbType.Int) or (aParam.SqlDbType = SqlDbType.Decimal) or (aParam.SqlDbType = s
    //        }
    //        else
    //        {
    //            aParam.Value = aTxtBox.Text;
    //            return true;
    //            //sErrMsg = "參數 [" & aParam.ParameterName.Substring(1) & "] 的值超出預期!"
    //            //Return False
    //        }
    //    } else if ((objCtrl is RadioButtonList)) {
    //        RadioButtonList objRBL = (RadioButtonList)objCtrl;
    //        aParam.Value = objRBL.SelectedValue;
    //        return true;
    //    } else if ((objCtrl is DropDownList)) {
    //        DropDownList objDDL = (DropDownList)objCtrl;
    //        aParam.Value = objDDL.SelectedValue;
    //        return true;
    //    }
    //    else if ((objCtrl is CheckBox))
    //    {
    //        sErrMsg = "不支援 CheckBox";
    //        return false;
    //    }
    //    else
    //    {
    //        sErrMsg = "傳入的 objCtrl 型態目前不支援";
    //        return false;
    //    }
    //}

    /// <summary>
    /// 在傳入的 Ctrl 中, 依 _objSqlCmd.Parameters 的名字找到對應的輸入物件, 並取得其值, 寫入到 Parameter.value 裡
    /// </summary>
    /// <param name="Ctrl"></param>
    /// <param name="PreFixName"></param>
    /// <param name="sErrMsg"></param>
    /// <returns></returns>
    //public bool SetupParamValue(ref WebControl Ctrl, string PreFixName, ref string sErrMsg)
    //{
    //    string sObjName = string.Empty;
    //    System.Web.UI.Control objCtrl = null;
    //    for (int i = 0; i < _objSqlCmd.Parameters.Count; i++)
    //    {
    //        SqlParameter aParam = _objSqlCmd.Parameters[i];
    //        if (aParam.Value != DBNull.Value) //已有值的參數就不要再設定
    //            continue;
    //        sObjName = PreFixName + aParam.ParameterName.Substring(1);
    //        objCtrl = Ctrl.FindControl(sObjName);
    //        if (objCtrl == null)
    //            continue;
    //        if ((!SetParmValWithCtrl(ref objCtrl, ref aParam, ref sErrMsg)))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    /// <summary>
    /// 將所有參數的值都給 DBNull.Value
    /// </summary>
    /// <param name="sErrMsg"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public bool SetupAllParamValueToNULL(ref string sErrMsg)
    {
	    sErrMsg = string.Empty;
	    foreach (SqlParameter Param in _objSqlCmd.Parameters) {
		    Param.Value = DBNull.Value;
	    }
	    return true;
    }

    /// <summary>
    /// 依設定的相關屬性, 設定 store procedure _objSqlCmd object 的參數
    /// </summary>
    /// <param name="sErrMsg"></param>
    /// <returns></returns>
    public bool SetupSqlCommand(ref string sErrMsg)
    {
	    if ((_ssStoreProcName == string.Empty)) {
		    sErrMsg = "必需先指定 property[StoreProcedureName] 的值";
		    return false;
	    }
	    //如果 _dtStoreProcInfo 已有資料, 就判斷 store procedure 是否與 _ssStoreProcName 相同, 以決定是否需要重新取得 store procedure 的資訊
	    if ((_dtStoreProcInfo.Rows.Count > 0)) {
		    string sSPName = _dtStoreProcInfo.Rows[0]["StoreProcName"].ToString();
		    if ((sSPName != _ssStoreProcName)) {
			    if ((!GetStoreProcInfo(ref sErrMsg))) {
				    return false;
				    //如果 store procedure 沒有參數(即 _dtStoreProcInfo.Rows.Count = 0)也會回傳 false
			    }
		    }
	    } else {
		    if ((!GetStoreProcInfo(ref sErrMsg))) {
			    return false;
			    //如果 store procedure 沒有參數(即 _dtStoreProcInfo.Rows.Count = 0)也會回傳 false
		    }
	    }
	    //開始設定 store procedure
	    if (((_objSqlCmd == null))) {
		    _objSqlCmd = new SqlCommand(_ssStoreProcName, _objConn);
	    }
	    //
	    _objSqlCmd.CommandType = CommandType.StoredProcedure;
	    //執行模式為 store procedcure 
	    _objSqlCmd.CommandText = _ssStoreProcName;
	    //設定要執行的 store procedure name
	    //設定 store procedure 的參數
	    _objSqlCmd.Parameters.Clear();
	    SqlParameter aParam = null;
	    string sParType = string.Empty;
	    foreach (DataRow row in _dtStoreProcInfo.Rows) {
		    aParam = new SqlParameter();
		    aParam.ParameterName = row["ParamName"].ToString();
		    sParType = row["ParamType"].ToString();
            if (Convert.ToBoolean(row["is_user_defined"]) == false)
            {
                if ((!SetupParamTypeWithText(sParType, ref aParam, ref sErrMsg))) {
			        return false;
		        }        
            }
		    aParam.Size = Convert.ToInt32(row["ParamLength"]);
		    if ((Convert.ToBoolean(row["is_output"]) == true)) {
			    aParam.Direction = ParameterDirection.Output;
		    } else {
			    aParam.Direction = ParameterDirection.Input;
		    }
		    _objSqlCmd.Parameters.Add(aParam);
	    }
	    return true;
    }

    public SqlConnection SqlConn {
	    get { return _objConn; }
    }

    //供呼叫端讀取的 SqlCommand object
    public SqlCommand SqlCmd {
	    get { return _objSqlCmd; }
    }

    //設定要執行的 store procdedure name
    public string StoreProcedureName {
	    get { return _ssStoreProcName; }
	    set { _ssStoreProcName = value; }
    }

    /// <summary>
    /// 執行 store procedure , 將回傳的資料透過 DataTable 回傳
    /// </summary>
    /// <param name="objDT"></param>
    /// <param name="sErrMsg"></param>
    /// <returns></returns>
    public bool GetDataTableWithStoreProcedure(ref DataTable objDT, ref string sErrMsg)
    {
        try
        {
            this._objConn.Open();
            using (SqlDataReader objDR = this._objSqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                objDT.Load(objDR);
            }
            this._objConn.Close();
            return true;
        }
        catch (Exception ex)
        {
            sErrMsg = ex.Message;
            return false;
        }
    }
}

public class rGetDataTable
{
    public delegate void OnSetSqlParameterValue(SqlCommand objCmd);

    public static bool GetDataTable(string sConnStr, string sSQL, OnSetSqlParameterValue SetParValue, ref string sErrMsg, ref DataTable dtResult)
    {
        try
        {
            using (SqlConnection objConn = new SqlConnection(sConnStr))
            {
                using (SqlCommand objCmd = new SqlCommand(sSQL, objConn))
                {
                    SetParValue(objCmd);
                    objConn.Open();
                    using (SqlDataReader objDR = objCmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dtResult == null)
                            dtResult = new DataTable();
                        dtResult.Load(objDR);
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sErrMsg = ex.Message;
            return false;
        }
    }

    public static bool GetDataTable(string sConnStr, string sSQL, ref string sErrMsg, ref DataTable dtResult)
    {
        try
        {
            using (SqlConnection objConn = new SqlConnection(sConnStr))
            {
                using (SqlCommand objCmd = new SqlCommand(sSQL, objConn))
                {
                    objConn.Open();
                    using (SqlDataReader objDR = objCmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dtResult == null)
                            dtResult = new DataTable(); 
                        dtResult.Load(objDR);
                        return true;
                    }                    
                }
            }
        }
        catch (Exception ex)
        {
            sErrMsg = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 傳入 objConn 是為了共用 Connection 物件, 注意:這裡不會自動關閉連線
    /// </summary>
    /// <param name="objConn"></param>
    /// <param name="sSQL"></param>
    /// <param name="sErrMsg"></param>
    /// <param name="dtResult"></param>
    /// <returns></returns>
    public static bool GetDataTable(SqlConnection objConn, string sSQL, out string sErrMsg, ref DataTable dtResult)
    {        
        try
        {
            using (SqlCommand objCmd = new SqlCommand(sSQL, objConn))
            {
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                using (SqlDataReader objDR = objCmd.ExecuteReader())
                {
                    if (dtResult == null)
                        dtResult = new DataTable();
                    dtResult.Load(objDR);
                    sErrMsg = "";
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            sErrMsg = ex.Message;
            return false;
        }
    }

    public static string writeDB(string sConnStr, string sSQL)
    {
        string strRet = "N";
        SqlConnection conn = new SqlConnection(sConnStr);
        SqlCommand cmd = new SqlCommand();

        cmd = new SqlCommand(sSQL, conn);

        conn.Open();
        if (cmd.ExecuteNonQuery() > 0)
        {
            // 啟用成功
            strRet = "Y";
        }
        cmd.Clone();
        conn.Close();

        return strRet;
    }
}