// -----------------------------------------------------------------------
// <copyright file="Utility.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Common
{
    public class Utility : IUtility
    {
        private static readonly HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 30, 0) };
        public DateTime ConvertUtcToLocalDateTime(DateTime utcDateTime, string standardTimezoneID = "Central Standard Time")
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(standardTimezoneID);
            DateTime localDatetime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
            string format = ci.DateTimeFormat.UniversalSortableDateTimePattern;
            string utcstr = localDatetime.ToString(format, ci);
            DateTime parsedBackUTC = DateTime.ParseExact(utcstr, format, ci.DateTimeFormat, DateTimeStyles.AdjustToUniversal);
            return parsedBackUTC;
        }

        public string ConvertUtcToLocalDateTimeString(string culture, DateTime utcDateTime, string standardTimezoneID = "Central Standard Time")
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(standardTimezoneID);
            DateTime localDatetime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
            string format = ci.DateTimeFormat.ShortDatePattern + ' ' + ci.DateTimeFormat.ShortTimePattern;
            string parsedBackUTC = localDatetime.ToString(format, new CultureInfo(culture));
            return parsedBackUTC;
        }

        public string ConvertCurrencyFormat(double amount)
        {
            string currentVal = amount.ToString("C", Thread.CurrentThread.CurrentCulture);
            return currentVal;
        }

        public DateTime ConvertToUtcDateTime(DateTime currentTime, int offset)
        {
            // From front-end, timezoneoffset can be received from new Date().getTimezoneOffset() which
            //will return as time offset in minutes from utc (for e.g; IST will be -330).
            // adding this offset to the date time received from client will convert that to UTC
            //Note:: this offset can be received from the header also.
            var utcTime = currentTime.AddMinutes(offset);
            return utcTime;
        }

        /// <summary>
        /// For internal API calls.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestType"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public HttpResponseMessage RestApiCall(string url, string requestType = PCISEnum.APIMethodType.GetRequest, string postParam = "")
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                HttpResponseMessage response = null;
                if (!String.IsNullOrEmpty(requestType))
                {
                    if (requestType.Equals(PCISEnum.APIMethodType.GetRequest))
                    {
                        httpRequestMessage.Method = HttpMethod.Get;
                        httpRequestMessage.RequestUri = new Uri(url);

                        response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;
                    }
                    else if (requestType.Equals(PCISEnum.APIMethodType.PostRequest))
                    {
                        if (postParam != null)
                        {
                            StringContent content = new StringContent(postParam, Encoding.UTF8, "application/json");
                            httpRequestMessage.Content = content;
                        }
                        httpRequestMessage.Method = HttpMethod.Post;
                        httpRequestMessage.RequestUri = new Uri(url);
                        response = Task.Run(() => client.SendAsync(httpRequestMessage)).Result;
                    }
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
