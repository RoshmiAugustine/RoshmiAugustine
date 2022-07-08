using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ImportProcess.Common
{
    public static class Extensions
    {
        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static bool TryParseValidDate(this string value,ref DateTime formatedDateTime)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            var date = value.Split(' ');
           // string dateFormats = "MM/dd/yyyy";
            var dateFormats = new[] { "MM/dd/yyyy", "M/d/yyyy" };
            bool validDate = DateTime.TryParseExact(value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out formatedDateTime);
            if (validDate)
                return true;
            else
                return false;


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
        public static DateTime? ToDateTimeNull(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
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
        public static int? ToIntNull(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Convert.ToInt32(value);
        }
        public static long? ToLongNull(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
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
        public static bool IsNullOrEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return false;
        }
        public static Guid ToGuid(this string value)
        {
            Guid outGuid;
            bool isValid = Guid.TryParse(value, out outGuid);
            if (!isValid)
            {
                return Guid.Empty;
            }
            return outGuid;
        }
        public static string TrimToLower(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            var result = Regex.Replace(value, @"\s+", "");
            return result.ToLower();
        }
        public static string TrimToString(this JToken value)
        {         
            return value.ToString().Trim();
        }
    }
}
