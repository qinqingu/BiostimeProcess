using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BiostimeProcess.Service.Utitity
{
    public static class WebConfig
    {
        /// <summary>
        ///     是否属于调式环境
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                if (ConfigurationManager.AppSettings["IsDebug"] != null)
                {
                    return bool.Parse(ConfigurationManager.AppSettings["IsDebug"]);
                }
                return false;
            }
        }

        /// <summary>
        ///     JfzDataCapture数据库链接
        /// </summary>
        public static string BiostimeDataCaptureConnection
        {
            get
            {
                return
                    ConfigurationManager.AppSettings["BiostimeDataCaptureConnection"].ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        ///     EDoc2V4数据库链接
        /// </summary>
        public static string EDoc2V4Connection
        {
            get { return ConfigurationManager.AppSettings["EDoc2V4Connection"].ToString(CultureInfo.InvariantCulture); }
        }

        #region edoc2流程配置

        /// <summary>
        ///     Edoc2管理员帐号,默认为：2
        /// </summary>
        public static int Edoc2AdminId
        {
            get { return 2; }
        }

        /// <summary>
        ///    财务档案管理员用户组
        /// </summary>
        public static int ManagerGroup = int.Parse(ConfigurationManager.AppSettings["ManagerGroup"]);

        #endregion
    }
}
