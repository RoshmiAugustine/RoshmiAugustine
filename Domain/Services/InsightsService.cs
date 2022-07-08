// -----------------------------------------------------------------------
// <copyright file="InsightsService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class InsightsService : BaseService, IInsightsService
    {
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        private readonly IMapper mapper;
        private readonly IAgencyInsightDashboardRepository insightDashboardRepository;

        public InsightsService(IConfigurationRepository configRepo, IHttpContextAccessor httpContext, LocalizeService localizeService, IMapper mapper, IAgencyInsightDashboardRepository insightDashboardRepository)
        : base(configRepo, httpContext)
        {
            this.localize = localizeService;
            this.mapper = mapper;
            this.insightDashboardRepository = insightDashboardRepository;
        }
        /// <summary>
        /// Updated the function based on PCIS-3217. 
        /// All parameters are now configurable in DB.(Filters,CustomFilters,APIUrl,UrlPath,SisenseDashboardID,SisenseAPIKey,LifeInSeconds).
        /// CustomFilters are in JSON format having a list of a name and value. 
        /// Currently AgencyId, UserRole and UserId are kept as replaceble values{{*}} and updated their values in code.
        /// Jan 6 2022 - This function is now not used as part of PCIS-3221
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GetDashBoardUrl(long agencyId, string userId, string role)
        {
            var allConfigs = this.GetConfigurationList(agencyId);
            var insightConfigs = allConfigs.Where(x => x.Name.StartsWith(PCISEnum.Insights.key)).ToList();

            var dashboardId = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseDashboardID).FirstOrDefault();
            var secretKey = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseAPISecretKey).FirstOrDefault();
            var lifeInSeconds = insightConfigs.Where(x => x.Name == PCISEnum.Insights.LifeInSeconds).FirstOrDefault();
            var apiUrl = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseAPIURL).FirstOrDefault();
            var urlDataPath = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseURLDataPath).FirstOrDefault();
            var fixedFilters = insightConfigs.Where(x => x.Name == PCISEnum.Insights.Filters).FirstOrDefault();
            var customFilters = insightConfigs.Where(x => x.Name == PCISEnum.Insights.CustomFilters).FirstOrDefault();

            //Update the customFilters-JSON by replacing the required values.
            var customFilterJSON = customFilters?.Value.Replace(PCISEnum.Insights.CustomFilterAgencyId, agencyId.ToString());
            customFilterJSON = customFilterJSON.Replace(PCISEnum.Insights.CustomFilterUserId, userId.ToString());
            customFilterJSON = customFilterJSON.Replace(PCISEnum.Insights.CustomFilterUserRole, role.ToString());
            var additionalFilters = JsonConvert.DeserializeObject<List<SisenseCustomFilter>>(customFilterJSON);

            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var timeTillNow = (int)t.TotalSeconds;
            var expirationDate = Convert.ToInt32(timeTillNow) + Convert.ToInt32(lifeInSeconds?.Value);
            var filters = fixedFilters?.Value.Split(",").ToList();

            var sisenseRequest = new SisenseRequestDTO()
            {
                Dashboard = Convert.ToInt32(dashboardId?.Value),
                PublishedVersion = "v2",
                ExpirationDate = expirationDate,
                MaintainSessionAfterExpiration = true,
                VisibleFilters = filters,
                AdditionalCustomFilters = additionalFilters
            };


            var encodedValue = JsonConvert.SerializeObject(sisenseRequest);
            var sisenseDataJson = HttpUtility.UrlEncode(JsonConvert.SerializeObject(sisenseRequest));
            var dataUrl = urlDataPath?.Value + sisenseDataJson;

            string signature = "";
            var encoding = new System.Text.ASCIIEncoding();

            byte[] keyByte = encoding.GetBytes(secretKey?.Value);

            byte[] messageBytes = encoding.GetBytes(dataUrl);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hash = new HMACSHA256(keyByte);
                byte[] signature1 = hash.ComputeHash(messageBytes);
                signature = BitConverter.ToString(signature1).Replace("-", "").ToLower();
            }

            return string.Format(apiUrl?.Value, dataUrl, signature);
        }

        /// <summary>
        /// GetInsightDashboardDetailsForAgency.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public InsightDashboardResponseDTO GetInsightDashboardDetailsForAgency(long agencyId)
        {
            try
            {
                InsightDashboardResponseDTO responseDTO = new InsightDashboardResponseDTO();
                var insightDashboardList = this.insightDashboardRepository.GetInsightDashboardDetailsByAgencyId(agencyId);
                responseDTO.InsightDashboardList = this.mapper.Map<List<AgencyInsightDashboardDTO>>(insightDashboardList);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetInsightUrlByDashboardID.
        /// Jan 6 2022 : Created as part of PCIS-3221.
        /// </summary>
        /// <param name="userTokenDetails"></param>
        /// <param name="insightDashboardPKId"></param>
        /// <returns></returns>
        public string GetInsightUrlByDashboardID(UserTokenDetails userTokenDetails, int insightDashboardPKId)
        {
            try
            {
                var allConfigs = this.GetConfigurationList(userTokenDetails.AgencyID);
                var insightConfigs = allConfigs.Where(x => x.Name.StartsWith(PCISEnum.Insights.key)).ToList();

                var secretKey = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseAPISecretKey).FirstOrDefault();
                var lifeInSeconds = insightConfigs.Where(x => x.Name == PCISEnum.Insights.LifeInSeconds).FirstOrDefault();
                var apiUrl = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseAPIURL).FirstOrDefault();
                var urlDataPath = insightConfigs.Where(x => x.Name == PCISEnum.Insights.SisenseURLDataPath).FirstOrDefault();

                var dashboardDetails = this.insightDashboardRepository.GetInsightDashboardDetailsById(insightDashboardPKId);
                if (dashboardDetails != null)
                {
                    //Update the customFilters-JSON by replacing the required values.
                    var customFilterJSON = dashboardDetails?.CustomFilters.Replace(PCISEnum.Insights.CustomFilterAgencyId, userTokenDetails.AgencyID.ToString());
                    customFilterJSON = customFilterJSON.Replace(PCISEnum.Insights.CustomFilterUserId, userTokenDetails.UserID.ToString());
                    customFilterJSON = customFilterJSON.Replace(PCISEnum.Insights.CustomFilterUserRole, userTokenDetails.Role.ToString());
                    var additionalFilters = JsonConvert.DeserializeObject<List<SisenseCustomFilter>>(customFilterJSON);

                    TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                    var timeTillNow = (int)t.TotalSeconds;
                    var expirationDate = Convert.ToInt32(timeTillNow) + Convert.ToInt32(lifeInSeconds?.Value);
                    var filters = dashboardDetails?.Filters.Split(",").ToList();

                    var sisenseRequest = new SisenseRequestDTO()
                    {
                        Dashboard = Convert.ToInt32(dashboardDetails?.DashboardId),
                        PublishedVersion = "v2",
                        ExpirationDate = expirationDate,
                        MaintainSessionAfterExpiration = true,
                        VisibleFilters = filters,
                        AdditionalCustomFilters = additionalFilters
                    };


                    var encodedValue = JsonConvert.SerializeObject(sisenseRequest);
                    var sisenseDataJson = HttpUtility.UrlEncode(JsonConvert.SerializeObject(sisenseRequest));
                    var dataUrl = urlDataPath?.Value + sisenseDataJson;

                    string signature = "";
                    var encoding = new System.Text.ASCIIEncoding();

                    byte[] keyByte = encoding.GetBytes(secretKey?.Value);

                    byte[] messageBytes = encoding.GetBytes(dataUrl);
                    using (var hmacsha256 = new HMACSHA256(keyByte))
                    {
                        var hash = new HMACSHA256(keyByte);
                        byte[] signature1 = hash.ComputeHash(messageBytes);
                        signature = BitConverter.ToString(signature1).Replace("-", "").ToLower();
                    }
                    return string.Format(apiUrl?.Value, dataUrl, signature);
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}