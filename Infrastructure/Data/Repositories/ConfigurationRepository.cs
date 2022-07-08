// -----------------------------------------------------------------------
// <copyright file="ConfigurationRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class ConfigurationRepository : BaseRepository<ConfigurationParameter>, IConfigurationRepository
    {
        private readonly OpeekaDBContext _dbContext;
        public ConfigurationRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public ConfigurationParameterDTO GetConfigurationByName(string name, long agencyID = 0)
        {
            try
            {
                ConfigurationParameterDTO configurationDTO = null;
                List<ConfigurationParameterDTO> listofConfigurationDTO = new List<ConfigurationParameterDTO>();
                ConfigurationParameter configurationParameter = this.GetRowAsync(x => x.Name == name).Result;
                if (configurationParameter != null)
                {
                    configurationDTO = new ConfigurationParameterDTO();
                    var query = string.Empty;
                    query = @"select Top 1 CPC.ConfigurationParameterID,C.Value,CVT.Name as ValueType, C.ContextFKValue,
                              C.ConfigurationID
                              from info.ConfigurationParameterContext CPC
                              inner join info.Configuration C on CPC.ConfigurationParameterContextID = C.ConfigurationParameterContextID
                              inner join info.ConfigurationValueType CVT on CVT.ConfigurationValueTypeID = " + configurationParameter.ConfigurationValueTypeID +
                                  "where CPC.ConfigurationParameterID = " + configurationParameter.ConfigurationParameterID + " and " +
                                  "(ISNULL(C.ContextFKValue, 0) = 0 or ISNULL(C.ContextFKValue, 0) = " + agencyID + ") " +
                                  "order by ContextFKValue desc";

                    listofConfigurationDTO = ExecuteSqlQuery(query, x => new ConfigurationParameterDTO
                    {
                        ConfigurationParameterID = (int)x[0],
                        Value = x[1] == DBNull.Value ? null : (string)x[1],
                        ValueType = x[2] == DBNull.Value ? null : (string)x[2],
                        AgencyId = x[3] == DBNull.Value ? 0 : (int)x[3],
                        ConfigurationID = x[4] == DBNull.Value ? 0 : (int)x[4]
                    });
                    if (listofConfigurationDTO.Count > 0)
                    {
                        configurationDTO = listofConfigurationDTO[0];
                    }
                    configurationDTO.Name = configurationParameter.Name;
                    configurationDTO.Description = configurationParameter.Description;
                    configurationDTO.CanModify = configurationParameter.CanModify;
                    configurationDTO.Deprecated = configurationParameter.Deprecated;
                    configurationDTO.IsActive = configurationParameter.IsActive;
                    configurationDTO.AgencyId = agencyID;
                    configurationDTO.ConfigurationValueTypeID = configurationParameter.ConfigurationValueTypeID;
                }
                return configurationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ConfigurationParameterDTO> GetConfigurationList(long agencyID = 0)
        {
            try
            {
                List<ConfigurationParameterDTO> ConfigurationDTO = new List<ConfigurationParameterDTO>();
                var query = string.Empty;
                query = @";WITH cte AS
                           (
                              select C.ConfigurationID,C.ConfigurationParameterContextID,C.ContextFKValue  As AgencyID,C.Value,
                                     CPC.ConfigurationContextID,CPC.ConfigurationParameterID,
                                     ROW_NUMBER() OVER (PARTITION BY CPC.ConfigurationParameterID ORDER BY C.ContextFKValue DESC) AS rownmbr
                               from info.Configuration C
                               inner join info.ConfigurationParameterContext CPC 
                            on C.ConfigurationParameterContextID = CPC.ConfigurationParameterContextID
                               where ISNULL(C.ContextFKValue,0) = " + agencyID + @" or ISNULL(C.ContextFKValue,0)=0 
                           )
                           select cte.ConfigurationParameterID,CP.Name,cte.Value,CVT.Name As ValueType,cte.AgencyID,
                           A.Attachments,CP.Description,CP.CanModify,CP.Deprecated,CP.IsActive,
                           cte.ConfigurationID,CVT.ConfigurationValueTypeID, CA.ConfigurationAttachmentID,
                           cte.ConfigurationParameterContextID,cte.ConfigurationContextID
                           from cte                            
                           inner join info.ConfigurationParameter CP on cte.ConfigurationParameterID = CP.ConfigurationParameterID 
                           inner join info.ConfigurationValueType CVT on CP.ConfigurationValueTypeID = CVT.ConfigurationValueTypeID
                           left join info.ConfigurationAttachment CA on cte.ConfigurationID = CA.ConfigurationID
                           left join info.Attachment A on CA.AttachmentID = A.AttachmentID where cte.rownmbr = 1;";

                ConfigurationDTO = ExecuteSqlQuery(query, x => new ConfigurationParameterDTO
                {
                    ConfigurationParameterID = (int)x["ConfigurationParameterID"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Value = x["Value"] == DBNull.Value ? null : (string)x["Value"],
                    ValueType = x["ValueType"] == DBNull.Value ? null : (string)x["ValueType"],
                    AgencyId = x["AgencyID"] == DBNull.Value ? 0 : (int)x["AgencyID"],
                    Attachment = x["Attachments"] == DBNull.Value ? null : (string)x["Attachments"],
                    Description = x["Description"] == DBNull.Value ? null : (string)x["Description"],
                    CanModify = Convert.ToInt16(x["CanModify"]) == 0 ? false : true,
                    Deprecated = Convert.ToInt16(x["Deprecated"]) == 0 ? false : true,
                    IsActive = Convert.ToInt16(x["IsActive"]) == 0 ? false : true,
                    ConfigurationID = x["ConfigurationID"] == DBNull.Value ? 0 : (int)x["ConfigurationID"],
                    ConfigurationValueTypeID = x["ConfigurationValueTypeID"] == DBNull.Value ? 0 : (int)x["ConfigurationValueTypeID"],
                    AttachmentID = x["ConfigurationContextID"] == DBNull.Value ? 0 : (int)x["ConfigurationContextID"]
                });
                return ConfigurationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
