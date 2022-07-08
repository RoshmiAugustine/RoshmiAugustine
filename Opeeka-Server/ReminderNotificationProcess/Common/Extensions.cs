using Newtonsoft.Json;
using ReminderNotificationProcess.Enums;
using System;
using System.Globalization;

namespace ReminderNotificationProcess.Common
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

        /// <summary>
        /// Check for a day is valid in that month of Year.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDayInMonthValid(this DateTime value, int day)
        {
            return day <= DateTime.DaysInMonth(value.Year, value.Month);
        }
        /// <summary>
        /// Add Days or Hours to a DateTime value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <param name="dayOrHour"></param>
        /// <returns></returns>
        public static DateTime AddDaysOrHours(this DateTime value, int count, char? dayOrHour = PCISEnum.OffsetType.Day)
        {
            DateTime datetime = value;
            if (dayOrHour == PCISEnum.OffsetType.Day)
            {
                datetime = datetime.AddDays(count);
            }
            if (dayOrHour == PCISEnum.OffsetType.Hour)
            {
                datetime = datetime.AddHours(count);
            }
            return datetime;
        }
        /// <summary>
        /// Returns DateTime in UTC for any given timeZone.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeZoneName"></param>
        /// <returns></returns>
        public static DateTime GetUTCDateTime(this DateTime value, string timeZoneName)
        {
            DateTime utcDateTime = value;
            try
            {
                if (timeZoneName == PCISEnum.TimeZones.UTC)
                {
                    return utcDateTime;
                }
                //var times = TimeZoneInfo.GetSystemTimeZones();
                var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
                var dateTimeUnspec = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
                utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timezoneInfo);

            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("Unable to find the {0} zone in the registry.",
                                  timeZoneName);
                utcDateTime = TimeZoneInfo.ConvertTimeToUtc(value);
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the {0} zone has been corrupted.",
                                  timeZoneName);
                utcDateTime = TimeZoneInfo.ConvertTimeToUtc(value);
            }
            return utcDateTime;
        }
        /// <summary>
        /// Returns TimeSpan of a string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string value)
        {
            DateTime dateTime = DateTime.Today;
            try
            {
                dateTime = DateTime.ParseExact(value,
                                       "hh:mm tt", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                var newvalue = "0" + value;
                dateTime = DateTime.ParseExact(newvalue,
                                       "hh:mm tt", CultureInfo.InvariantCulture);
            }
            TimeSpan timeSpan = dateTime.TimeOfDay;
            return timeSpan;
        }
        /// <summary>
        /// Returns Number of Weeks in a month.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int NoOfWeeks(this DateTime value)
        {
            int daysInMonth = DateTime.DaysInMonth(value.Year, value.Month);
            int noOfWeeks = daysInMonth / 7;
            return noOfWeeks;
        }
        /// <summary>
        /// Find Nth Day Of A Month
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dayNo"></param>
        /// <returns></returns>
        public static DateTime FindNthDayOfMonth(this DateTime value, int dayNo)
        {
            return new DateTime(value.Year, value.Month, dayNo);
        }
        /// <summary>
        /// Find the ordinal week days date.ie first weekday/second weekday..
        /// ordinal = -1 checks for last ordinal weekday
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DateTime FindOrdinalWeekDayDate(this DateTime value, int ordinal)
        {
            DateTime resultDate = DateTime.MinValue;
            //last ordinal weeday
            if (ordinal == -1)
            {
                int NofDays = DateTime.DaysInMonth(value.Year, value.Month);
                resultDate = new DateTime(value.Year, value.Month, NofDays);
            }
            else
            {
                //first find the first day
                resultDate = value.FindNthDayOfMonth(1);
            }
            //if its a staurday or sunday move forward to monday
            if (resultDate.DayOfWeek == DayOfWeek.Saturday)
                resultDate = resultDate.AddDays(2);
            if (resultDate.DayOfWeek == DayOfWeek.Sunday)
                resultDate = resultDate.AddDays(1);
            //From monday add ordianl-1 days to reach ordinal day from first day date.
            resultDate = resultDate.AddDays(ordinal - 1);

            return resultDate;
        }
        /// <summary>
        /// FindOrdinalNthDayOfMonth.
        /// ordinal day of a month..ie first day/second day etc..
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DateTime FindOrdinalDayOfMonth(this DateTime value, string ordinal)
        {
            DateTime resultDate = DateTime.MinValue;
            switch (ordinal.ToLower())
            {
                case PCISEnum.RecurrenceOrdinal.First:
                    resultDate = value.FindNthDayOfMonth(1);
                    break;
                case PCISEnum.RecurrenceOrdinal.Second:
                    resultDate = value.FindNthDayOfMonth(2);
                    break;
                case PCISEnum.RecurrenceOrdinal.Third:
                    resultDate = value.FindNthDayOfMonth(3);
                    break;
                case PCISEnum.RecurrenceOrdinal.Fourth:
                    resultDate = value.FindNthDayOfMonth(4);
                    break;
                case PCISEnum.RecurrenceOrdinal.Last:
                    //For last calculate the noOf Days in month and find the day
                    int noOfDaysInMonth = DateTime.DaysInMonth(value.Year, value.Month);
                    resultDate = value.FindNthDayOfMonth(noOfDaysInMonth);
                    break;
            }
            return resultDate;
        }

        /// <summary>
        /// Find the Week day by ordinal.ie first weekday/second weekday etc..
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DateTime FindOrdinalWeekDayOfMonth(this DateTime value, string ordinal)
        {
            DateTime resultDate = DateTime.MinValue;
            switch (ordinal.ToLower())
            {
                case PCISEnum.RecurrenceOrdinal.First:
                    resultDate = value.FindOrdinalWeekDayDate(1);
                    break;
                case PCISEnum.RecurrenceOrdinal.Second:
                    resultDate = value.FindOrdinalWeekDayDate(2);
                    break;
                case PCISEnum.RecurrenceOrdinal.Third:
                    resultDate = value.FindOrdinalWeekDayDate(3);
                    break;
                case PCISEnum.RecurrenceOrdinal.Fourth:
                    resultDate = value.FindOrdinalWeekDayDate(4);
                    break;
                case PCISEnum.RecurrenceOrdinal.Last:
                    resultDate = value.FindOrdinalWeekDayDate(-1);
                    break;
            }
            return resultDate;
        }

        /// <summary>
        /// To Get OrdianlDate in a Month..
        /// For first day of month ordinal = 1
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DateTime FindTheOrdinalDayNameOfMonth(this DateTime value, DayOfWeek recurDay, string ordinal)
        {
            DateTime resultDate = DateTime.MinValue;
            switch (ordinal.ToLower())
            {
                case PCISEnum.RecurrenceOrdinal.First:
                    resultDate = value.FindTheOrdinalDayNameDate(recurDay, 1);
                    break;
                case PCISEnum.RecurrenceOrdinal.Second:
                    resultDate = value.FindTheOrdinalDayNameDate(recurDay, 2);
                    break;
                case PCISEnum.RecurrenceOrdinal.Third:
                    resultDate = value.FindTheOrdinalDayNameDate(recurDay, 3);
                    break;
                case PCISEnum.RecurrenceOrdinal.Fourth:
                    resultDate = value.FindTheOrdinalDayNameDate(recurDay, 4);
                    break;
                case PCISEnum.RecurrenceOrdinal.Last:
                    int noOfWeeks = value.NoOfWeeks();
                    resultDate = value.FindTheOrdinalDayNameDate(recurDay, noOfWeeks);
                    break;
            }
            return resultDate;
        }
        /// <summary>
        /// To Get OrdianlDate in a Month..
        /// For first day of month ordinal = 1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="recurDay"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DateTime FindTheOrdinalDayNameDate(this DateTime value, DayOfWeek recurDay, int ordinal)
        {
            //Find first day of month
            var startDate = value.FindNthDayOfMonth(1);

            //Loop to reach the required recur day from start day
            while (startDate.DayOfWeek != recurDay)
            {
                startDate = startDate.AddDays(1);
            }
            //Add (7 * the week Number(ie the ordinal)) days to get the result.
            var nDays = 7 * (ordinal - 1);
            var result = startDate.AddDays(nDays);

            result = result.Year == value.Year && result.Month == value.Month ? result : DateTime.MinValue;
            return result;
        }
        /// <summary>
        /// FindStartDateOfTheWeek
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FindStartDateOfTheWeek(this DateTime value)
        {
            DateTime resultDate = DateTime.MinValue;
            switch (value.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    //Only place which differes from FutureReminder Process.
                    //For OccurenceCounter = 1 If currentday(satrtdate).Week = sunday then start calculation a week before.
                    resultDate = value.AddDays(-7);
                    break;
                case DayOfWeek.Monday:
                    resultDate = value.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday:
                    resultDate = value.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday:
                    resultDate = value.AddDays(-3);
                    break;
                case DayOfWeek.Thursday:
                    resultDate = value.AddDays(-4);
                    break;
                case DayOfWeek.Friday:
                    resultDate = value.AddDays(-5);
                    break;
                case DayOfWeek.Saturday:
                    resultDate = value.AddDays(-6);
                    break;
            }
            return resultDate;
        }
    }
}
