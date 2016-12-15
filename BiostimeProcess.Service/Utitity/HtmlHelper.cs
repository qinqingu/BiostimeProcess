using System;
using System.Web;

namespace BiostimeProcess.Service.Utitity
{
    public static class HtmlHelper
    {
        public static string Encode(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }

            return HttpUtility.HtmlEncode(value);
        }
    }
}