using Newtonsoft.Json;
using System;

namespace AlertAutoResolveProcess.Common
{
    public static class Extensions
    {
        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static DateTime ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DateTime.UtcNow;
            }
            return Convert.ToDateTime(value);
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
