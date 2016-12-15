using System;
using EDoc2.Website;

namespace BiostimeProcess.Service.Utitity
{
    public class Edoc2LogHelper
    {
        public static void WriteLog(string value)
        {
            //日志记录路径:Macrowing\EDoc2v4\Website\Logs\年月目录\日期.txt
            WebsiteUtility.WriteLog(value);
        }

        public static void WriteProcessSubmitExceptionLog(Exception ex)
        {
            //日志记录路径:Macrowing\EDoc2v4\Website\Logs\年月目录\日期.txt
            string erroLog = string.Format(
                "Submit Exception,ex.Message={0},ex.StackTrace={1}", ex.Message, ex.StackTrace);
            WriteLog(erroLog);
        }
    }
}