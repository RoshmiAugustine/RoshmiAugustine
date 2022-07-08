// -----------------------------------------------------------------------
// <copyright file="IUtility.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net.Http;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Interfaces.Common
{
    public interface IUtility
    {
        DateTime ConvertUtcToLocalDateTime(DateTime utcDateTime, string standardTimezoneID = "Central Standard Time");
        string ConvertUtcToLocalDateTimeString(string culture, DateTime utcDateTime, string standardTimezoneID = "Central Standard Time");
        string ConvertCurrencyFormat(double amount);
        DateTime ConvertToUtcDateTime(DateTime currentTime, int offset);
        HttpResponseMessage RestApiCall(string url, string requestType = PCISEnum.APIMethodType.GetRequest, string postParam = "");
    }
}
