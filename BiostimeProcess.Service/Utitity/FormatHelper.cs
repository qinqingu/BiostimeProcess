using System;
using System.Globalization;

namespace BiostimeProcess.Service.Utitity
{
    public static class FormatHelper
    {
        public static string GetString(string value)
        {
            return value ?? string.Empty;
        }

        public static string GetDecimalString(decimal? value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return value.Value.ToString(CultureInfo.InvariantCulture);
        }

        public static int GetInt(int? value)
        {
            return value ?? 0;
        }

        public static string GetIsoDateString(DateTime? value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return value.Value.ToString("yyyy-MM-dd");
        }

        public static string GetNonNullIsoDateString(DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                return String.Empty;
            }
            return value.ToString("yyyy-MM-dd");
        }

        public static string GetNonNullIntString(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetIntString(int? value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return value.Value.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetLongString(long? value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            return value.Value.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetBooleanString(bool value)
        {
            if (value)
            {
                return "是";
            }

            return "否";
        }

        public static DateTime GetNonNullableDateValue(string value)
        {
            if (value == null)
            {
                return default(DateTime);
            }

            return DateTime.ParseExact(value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}