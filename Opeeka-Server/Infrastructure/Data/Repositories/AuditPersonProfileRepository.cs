// -----------------------------------------------------------------------
// <copyright file="AuditPersonProfileRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AuditPersonProfileRepository : BaseRepository<AuditPersonProfile>, IAuditPersonProfileRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        /// <summary>
        /// AuditPersonProfileRepository.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AuditPersonProfileRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddPersonProfileDetailsForAudit.
        /// </summary>
        /// <param name="auditPersonProfile"></param>
        /// <returns></returns>
        public AuditPersonProfile AddPersonProfileDetailsForAudit(AuditPersonProfile auditPersonProfile)
        {
            try
            {
                var result = this.AddAsync(auditPersonProfile).Result;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// getHelperHistoryDetails.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<AuditPersonProfileDTO> getHelperHistoryDetails(long personID)
        {
            try
            {
                var query = string.Empty;

                query = $@"SELECT AP.AuditPersonProfileID,AP.ParentID,Ap.ChildRecordID, AP.TypeName,
                                (H.FirstName + ' ' + ISNULL(H.MiddleName, ' ') + H.LastName) AS ChildRecordName, AP.StartDate,AP.EndDate,
                                AP.UpdateDate,AP.UpdateUserID
                                   FROM AuditPersonProfile AP
                                JOIN Helper H ON H.HelperID = AP.ChildRecordID
                                WHERE AP.ParentID = {personID} AND TypeName= '{PCISEnum.AuditPersonProfileType.Helper}'";
                var result = ExecuteSqlQuery(query, x => new AuditPersonProfileDTO
                {
                    AuditPersonProfileID = x["AuditPersonProfileID"] == DBNull.Value ? 0 : (int)x["AuditPersonProfileID"],
                    ParentID = x["ParentID"] == DBNull.Value ? 0 : (long)x["ParentID"],
                    ChildRecordID = x["ChildRecordID"] == DBNull.Value ? 0 : (int)x["ChildRecordID"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],                    
                    TypeName = x["TypeName"] == DBNull.Value ? string.Empty : (string)x["TypeName"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ChildRecordName = x["ChildRecordName"] == DBNull.Value ? string.Empty : (string)x["ChildRecordName"]
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// getCollaborationHistoryDetails.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<AuditPersonProfileDTO> getCollaborationHistoryDetails(long personID)
        {
            try
            {
                var query = string.Empty;

                query = $@"SELECT AP.AuditPersonProfileID,AP.ParentID,Ap.ChildRecordID, AP.TypeName,
                                C.Name AS ChildRecordName, AP.StartDate,AP.EndDate,
                                AP.UpdateDate,AP.UpdateUserID
                                   FROM AuditPersonProfile AP
                                JOIN Collaboration C ON C.CollaborationID = AP.ChildRecordID
                                WHERE AP.ParentID = {personID} AND TypeName='{ PCISEnum.AuditPersonProfileType.Collaboration }'";
                    var result = ExecuteSqlQuery(query, x => new AuditPersonProfileDTO
                {
                    AuditPersonProfileID = x["AuditPersonProfileID"] == DBNull.Value ? 0 : (int)x["AuditPersonProfileID"],
                    ParentID = x["ParentID"] == DBNull.Value ? 0 : (long)x["ParentID"],
                    ChildRecordID = x["ChildRecordID"] == DBNull.Value ? 0 : (int)x["ChildRecordID"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    TypeName = x["TypeName"] == DBNull.Value ? string.Empty : (string)x["TypeName"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ChildRecordName = x["ChildRecordName"] == DBNull.Value ? string.Empty : (string)x["ChildRecordName"]
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
