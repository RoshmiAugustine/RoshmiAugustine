using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EHRProcess.Common
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
            var date = value.Split(' ');
            return Convert.ToDateTime(date[0]);
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
        public static bool IsValidEmail(this string emailToTest)
        {
            bool isEmail = false;
            if (!string.IsNullOrEmpty(emailToTest))
            {
                isEmail = Regex.IsMatch(emailToTest.ToString(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            return isEmail;
        }
        public static bool IsValidPhoneNumber(this string phoneNoToTest)
        {
            bool isEmail = false;
            if (!string.IsNullOrEmpty(phoneNoToTest))
            {
                isEmail = Regex.IsMatch(phoneNoToTest.ToString(), @"[-0-9+/ ()]$", RegexOptions.IgnoreCase);
            }
            return isEmail;
        }
        public static T DeserializeJSON<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// ToDateStringWithTimeZone.If timezone null then just fecth the date part only.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        public static string ToDateStringWithTimeZone(this DateTime value, string timezone)
        {            
            if (string.IsNullOrEmpty(value.ToString()))
            {
                value = DateTime.UtcNow;
            }
            DateTime datetime = value;
            if (!string.IsNullOrEmpty(timezone))
            {
                datetime = TimeZoneInfo.ConvertTimeFromUtc(value, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            }
            return datetime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}

