// -----------------------------------------------------------------------
// <copyright file="IInsightsService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IInsightsService
    {
        string GetDashBoardUrl(long AgencyId, string userId, string role);
        InsightDashboardResponseDTO GetInsightDashboardDetailsForAgency(long agencyId);
        string GetInsightUrlByDashboardID(UserTokenDetails userTokenDetails, int insightDashboardPKId);
    }
}