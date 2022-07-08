using Newtonsoft.Json;
using System;
using System.Globalization;

namespace AlertNotficationProcess.Common
{
    public static class Extensions
    {
        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static T DeserializeJSON<T>(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        public static DateTime ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DateTime.UtcNow;
            }
            return Convert.ToDateTime(value);
        }
        public static string ToDateString(this DateTime value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                value = DateTime.UtcNow;
            }
            return value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
        public static DateTime ToDateTimeUTC(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return DateTime.UtcNow;
                }
                return DateTimeOffset.Parse(value.Replace("UTC", "")).UtcDateTime;
            }
            catch
            {
                return DateTime.UtcNow;
            }            
        }
        public static int ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return Convert.ToInt32(value);
        }
        public static long ToLong(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return Convert.ToInt64(value);
        }
    }
}
