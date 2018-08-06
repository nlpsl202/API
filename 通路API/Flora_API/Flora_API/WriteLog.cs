using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Flora_API
{
    public class WriteLog
    {
        /// <summary>
        /// 一般 Log 的寫入路徑
        /// </summary>
        private string _sLogPath = "";

        /// <summary>
        /// 當呼叫 Write_Log 時,  會判斷這個變數, 以決定是否寫入 log(即 Log 的開關)
        /// </summary>
        private Boolean _IsWriteLog = false;

        /// <summary>
        /// 錯誤日誌的寫入路徑
        /// </summary>
        private string _sErrLogPath = "";

        /// <summary>
        /// 當呼叫 Error_Log 時, 會判斷這個變數, 以決定是否入 log (即錯誤日誌的開關)
        /// </summary>
        private Boolean _IsWriteErrLog = false;

        public WriteLog()
        {
            _sLogPath = WebConfigurationManager.AppSettings["WRITE_LOG_PATH"];
            string sWriteLog = WebConfigurationManager.AppSettings["WRITE_LOG"];
            _IsWriteLog = ((sWriteLog == "Y") && (_sLogPath.Length > 0)); //除了有開之外, 而且必需指定 Log 路徑
            //
            _sErrLogPath = WebConfigurationManager.AppSettings["ERROR_LOG_PATH"];
            string sWeritErrLog = WebConfigurationManager.AppSettings["ERROR_LOG"];
            _IsWriteErrLog = ((sWeritErrLog == "Y") && (_sErrLogPath.Length > 0)); //除了有開之外, 而且必需指定 Log 路徑
        }

        /// <summary>
        /// 一般性的 log 寫入需求
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool Write_Log(string log)
        {
            string vLogFile = _sLogPath + DateTime.Now.ToString("yyyyMMdd") + "_API.txt";
            log = DateTime.Now.ToShortDateString() + " " + DateTime.Now.TimeOfDay + " " + log;
            if (log.EndsWith("\r\n") == false)
                log = log + "\r\n";
            if (_IsWriteLog)
            {
                System.Web.HttpContext.Current.Application.Lock();
                if (File.Exists(vLogFile))
                {
                    File.AppendAllText(vLogFile, log, System.Text.Encoding.UTF8);
                }
                else
                {
                    File.WriteAllText(vLogFile, log, System.Text.Encoding.UTF8);
                }
                System.Web.HttpContext.Current.Application.UnLock();
            }
            return true;
        }

        /// <summary>
        /// 系統發生錯誤的寫入需求
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool Error_Log(string log)
        {
            string vLogFile = _sErrLogPath + DateTime.Now.ToString("yyyyMMdd") + "_API.err";
            log = DateTime.Now.ToShortDateString() + " " + DateTime.Now.TimeOfDay + " " + log;
            if (_IsWriteErrLog)
            {
                System.Web.HttpContext.Current.Application.Lock();
                if (File.Exists(vLogFile))
                {
                    File.AppendAllText(vLogFile, log, System.Text.Encoding.UTF8);
                }
                else
                {
                    File.WriteAllText(vLogFile, log, System.Text.Encoding.UTF8);
                }
                System.Web.HttpContext.Current.Application.Lock();
            }
            return true;
        }
    }
}