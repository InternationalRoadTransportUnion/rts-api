using System;
using System.IO;
using System.Text;

namespace CIFConversion
{
    /// <summary>
    /// Summary description for LogFiles.
    /// </summary>
    public class LogFiles
    {
        private string sLogFormat;
        private string sErrorTime;

        public LogFiles()
        {
            //
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            sErrorTime = DateTime.Now.ToString("yyyyMMdd");
        }

        public void ErrorLog(string sPathName, string sErrMsg)
        {
            StreamWriter sw = new StreamWriter(sPathName + sErrorTime + ".log", true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}
