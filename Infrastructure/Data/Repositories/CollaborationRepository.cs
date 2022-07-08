// -----------------------------------------------------------------------
// <copyright file="CollaborationRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.ExternalAPI;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationRepository : BaseRepository<Collaboration>, ICollaborationRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        public CollaborationRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._cache = cache;
        }


        /// <summary>
        /// To add collaboration details
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <returns>id.</returns>
        public int AddCollaboration(CollaborationDTO collaborationDTO)
        {
            try
            {
                Collaboration collaboration = new Collaboration();
                this.mapper.Map<CollaborationDTO, Collaboration>(collaborationDTO, collaboration);
                var result = this.AddAsync(collaboration).Result.CollaborationID;
                this._cache.Delete(PCISEnum.Caching.GetAllCollaboration);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get Collaboration details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>CollaborationDTO.</returns>
        public async Task<CollaborationDTO> GetAsync(Guid id)
        {
            try
            {
                CollaborationDTO collaborationDTO = new CollaborationDTO();
                Collaboration collaboration = await this.GetRowAsync(x => x.CollaborationIndex == id);
                this.mapper.Map<Collaboration, CollaborationDTO>(collaboration, collaborationDTO);
                return collaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update collaboration details.
        /// </summary>
        /// <param name="CollaborationDTO">CollaborationDTO.</param>
        /// <returns>CollaborationDTO.</returns>
        public CollaborationDTO UpdateCollaboration(CollaborationDTO collaborationDTO)
        {
            try
            {
                Collaboration collaboration = new Collaboration();
                this.mapper.Map<CollaborationDTO, Collaboration>(collaborationDTO, collaboration);
                var result = this.UpdateAsync(collaboration).Result;
                CollaborationDTO updatedCollaboration = new CollaborationDTO();
                this.mapper.Map<Collaboration, CollaborationDTO>(result, updatedCollaboration);
                this._cache.Delete(PCISEnum.Caching.GetAllCollaboration);
                return updatedCollaboration;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        public List<CollaborationDataDTO> GetCollaborationList(int pageNumber, int pageSize)
        {
            try
            {
                List<CollaborationDataDTO> collaborationDataDTO = new List<CollaborationDataDTO>();
                var query = string.Empty;
                query = @"Select C.CollaborationID, C.CollaborationIndex,C.[Name], C.Code, C.StartDate, C.EndDate, A.[Name] as Agency, CL.Name as [Level], 
                            T.[Name] as [Type] , C.TherapyTypeID, C.AgencyID, C.CollaborationLevelID
                            from Collaboration C
                            left join Agency A on A.AgencyID = C.AgencyID
                            left join info.CollaborationLevel CL on CL.CollaborationLevelID = C.CollaborationLevelID
                            left join info.TherapyType T on T.TherapyTypeID = C.TherapyTypeID
                            ORDER BY C.[Name]
                            OFFSET " + ((pageNumber - 1) * pageSize) + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY " +
                            "Where C.IsRemoved = 0 ";


                var data = ExecuteSqlQuery(query, x => new CollaborationDataDTO
                {
                    CollaborationID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    CollaborationIndex = x[1] == DBNull.Value ? Guid.Empty : (Guid)x[1],
                    Name = x[2] == DBNull.Value ? null : (string)x[2],
                    Code = x[3] == DBNull.Value ? null : (string)x[3],
                    StartDate = x[4] == DBNull.Value ? DateTime.MinValue : (DateTime)x[4],
                    EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                    Agency = x[6] == DBNull.Value ? null : (string)x[6],
                    Level = x[7] == DBNull.Value ? null : (string)x[7],
                    Type = x[8] == DBNull.Value ? null : (string)x[8],
                    TherapyTypeID = x[9] == DBNull.Value ? 0 : (int)x[9],
                    AgencyID = x[10] == DBNull.Value ? 0 : (long)x[10],
                    CollaborationLevelID = x[11] == DBNull.Value ? 0 : (int)x[11],

                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationCount.
        /// </summary>
        /// <returns>Collaboration Count.</returns>
        public int GetCollaborationCount()
        {
            try
            {
                var count = this._dbContext.Collaborations.Where(x => !x.IsRemoved).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationDetails.
        /// </summary>
        /// <param name="peopleIndex">peopleIndex.</param>
        /// <returns>CollaborationInfoDTO.</returns>
        public CollaborationInfoDTO GetCollaborationDetails(Guid collaborationIndex, long agencyID)
        {
            CollaborationInfoDTO collaborationInfoDTO = new CollaborationInfoDTO();
            try
            {
                var query = string.Empty;
                query = @"Select C.CollaborationID, C.CollaborationIndex, C.[Name], C.Code, C.[Description], C.StartDate, C.EndDate, A.[Name] as Agency, CL.Abbrev as [Level],
                            T.[Name] as [Type],C.Abbreviation, C.TherapyTypeID, C.CollaborationLevelID, C.AgencyID  from Collaboration C
                            left join Agency A on A.AgencyID = C.AgencyID
                            left join info.CollaborationLevel CL on CL.CollaborationLevelID = C.CollaborationLevelID
                            left join info.TherapyType T on T.TherapyTypeID = C.TherapyTypeID
                            WHERE C.CollaborationIndex = '" + collaborationIndex + "' AND C.IsRemoved = 0 AND C.AgencyID= " + agencyID;

                collaborationInfoDTO = ExecuteSqlQuery(query, x => new CollaborationInfoDTO
                {
                    CollaborationID = (int)x[0],
                    CollaborationIndex = (Guid)x[1],
                    Name = x[2] == DBNull.Value ? null : (string)x[2],
                    Code = x[3] == DBNull.Value ? null : (string)x[3],
                    Description = x[4] == DBNull.Value ? null : (string)x[4],
                    StartDate = x[5] == DBNull.Value ? DateTime.MinValue : (DateTime)x[5],
                    EndDate = x[6] == DBNull.Value ? null : (DateTime?)x[6],
                    Agency = x[7] == DBNull.Value ? null : (string)x[7],
                    Level = x[8] == DBNull.Value ? null : (string)x[8],
                    Type = x[9] == DBNull.Value ? null : (string)x[9],
                    Abbreviation = x[10] == DBNull.Value ? null : (string)x[10],
                    TherapyTypeID = x[11] == DBNull.Value ? 0 : (int)x[11],
                    CollaborationLevelID = x[12] == DBNull.Value ? 0 : (int)x[12],
                    AgencyID = x[13] == DBNull.Value ? 0 : (long)x[13],

                }).FirstOrDefault();

                return collaborationInfoDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationQuestionnaireList.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>Questionnaire List.</returns>
        public List<QuestionnaireDataDTO> GetCollaborationQuestionnaireList(int collaborationID)
        {

            try
            {
                var query = string.Empty;
                query = @"Select Q.QuestionnaireID, Q.[Name] as Questionnaire, CQ.StartDate, CQ.EndDate , CQ.IsMandatory, CQ.CollaborationQuestionnaireID,CQ.IsReminderOn
                            from Collaboration C
	                        left join CollaborationQuestionnaire CQ on CQ.CollaborationID = C.CollaborationID
	                        left join Questionnaire Q on Q.QuestionnaireID = CQ.QuestionnaireID
                            WHERE CQ.IsRemoved = 0 AND C.CollaborationID = " + collaborationID;

                var questionnaireDetails = ExecuteSqlQuery(query, x => new QuestionnaireDataDTO
                {
                    QuestionnaireID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    Questionnaire = x[1] == DBNull.Value ? null : (string)x[1],
                    StartDate = x[2] == DBNull.Value ? null : (DateTime?)x[2],
                    EndDate = x[3] == DBNull.Value ? null : (DateTime?)x[3],
                    IsMandatory = x[4] == DBNull.Value ? false : (bool)x[4],
                    CollaborationQuestionnaireID = x[5] == DBNull.Value ? 0 : (int)x[5],
                    IsReminderOn = x[6] == DBNull.Value ? false : (bool)x[6],
                });

                return questionnaireDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationLeads.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>CollaborationLead List.</returns>
        public List<CollaborationLeadDTO> GetCollaborationLeads(int collaborationID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select H.HelperID as CollaborationLeadUserID, H.FirstName + '' + H.MiddleName + '' + H.LastName as CollaborationLead, L.StartDate, L.EndDate, L.CollaborationLeadHistoryID from Collaboration C
	                    left join CollaborationLeadHistory L on L.CollaborationID = C.CollaborationID
	                    left join Helper H on H.HelperID = L.LeadUserID
	                    WHERE L.IsRemoved = 0 AND C.CollaborationID = " + collaborationID;

                var leadsDetails = ExecuteSqlQuery(query, x => new CollaborationLeadDTO
                {
                    CollaborationLeadUserID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    CollaborationLead = x[1] == DBNull.Value ? null : (string)x[1],
                    StartDate = x[2] == DBNull.Value ? null : (DateTime?)x[2],
                    EndDate = x[3] == DBNull.Value ? null : (DateTime?)x[3],
                    CollaborationLeadHistoryID = x[4] == DBNull.Value ? 0 : (int)x[4]
                });

                return leadsDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationCategories.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>Category List.</returns>
        public List<CategoryDataDTO> GetCollaborationCategories(int collaborationID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select T.CollaborationTagTypeID as CategoryID, T.[Name] as Category, CT.CollaborationTagID from Collaboration C
	                        left join CollaborationTag CT on CT.CollaborationID = C.CollaborationID
	                        left join info.CollaborationTagType T on T.CollaborationTagTypeID = CT.CollaborationTagTypeID
	                        WHERE CT.IsRemoved = 0 AND C.CollaborationID = " + collaborationID;

                var categoryData = ExecuteSqlQuery(query, x => new CategoryDataDTO
                {
                    CategoryID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    Category = x[1] == DBNull.Value ? null : (string)x[1],
                    CollaborationTagID = x[2] == DBNull.Value ? 0 : (int)x[2],
                });

                return categoryData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all Agencycollaborations.
        /// </summary>
        /// <returns> CollaborationDTO.</returns>
        public List<CollaborationLookupDTO> GetAllAgencycollaborations(long id)
        {
            try
            {
                var query = string.Empty;
                query = @"select  CollaborationID, Name   FROM Collaboration where 
 IsRemoved=0 And (EndDate is NULL OR EndDate > getdate()) And AgencyID=" + id + " Order by Name";

                var collaborationDTO = ExecuteSqlQuery(query, x => new CollaborationLookupDTO
                {
                    CollaborationID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    Name = x[1] == DBNull.Value ? null : (string)x[1],

                });

                return collaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetCollaborationCountByLevel.
        /// </summary>
        /// <param name="levelID">levelID.</param>
        /// <returns>int.</returns>
        public int GetCollaborationCountByLevel(long levelID)
        {
            int count = (from row in this._dbContext.Collaborations
                         where (row.CollaborationLevelID == levelID) && !row.IsRemoved
                         select row).Count();

            return count;
        }

        /// <summary>
        /// Get the count of Collaborations having a specific therapy type
        /// </summary>
        /// <param name="therapyTypeID"></param>
        /// <returns>int</returns>
        public int GetCollaborationCountByTherapy(int therapyTypeID)
        {
            int count = (
                            from row
                            in this._dbContext.Collaborations
                            where
                                row.TherapyTypeID == therapyTypeID && !row.IsRemoved
                            select
                                row
                        ).Count();

            return count;
        }

        /// <summary>
        /// GetCollaborationListForOrgAdmin.
        /// </summary>
        /// <param name="collaborationSearchDTO">pageNumber.</param>
        /// <param name="queryBuilderDTO">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        public Tuple<List<CollaborationDataDTO>, int> GetCollaborationListForOrgAdmin(CollaborationSearchDTO collaborationSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO, long agencyID)
        {
            try
            {
                List<CollaborationDataDTO> collaborationDataDTO = new List<CollaborationDataDTO>();
                var query = string.Empty;
                query = @"Select C.CollaborationID, C.CollaborationIndex,C.[Name], C.Code, C.StartDate, C.EndDate, A.[Name] as Agency, CL.Name as [Level], 
                            T.[Name] as [Type] , C.TherapyTypeID, C.AgencyID, C.CollaborationLevelID, COUNT(*) OVER() AS TotalCount
                            from Collaboration C
                            left join Agency A on A.AgencyID = C.AgencyID
                            left join info.CollaborationLevel CL on CL.CollaborationLevelID = C.CollaborationLevelID
                            left join info.TherapyType T on T.TherapyTypeID = C.TherapyTypeID where C.IsRemoved = 0 and C.AgencyID = " + agencyID;
                //query += @" OFFSET " + ((collaborationSearchDTO.pageNumber - 1) * collaborationSearchDTO.pageSize) + "ROWS FETCH NEXT " + collaborationSearchDTO.pageSize + " ROWS ONLY";
                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;

                int totalCount = 0;
                var data = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x[12] == DBNull.Value ? 0 : (int)x[12];
                    return new CollaborationDataDTO
                    {
                        CollaborationID = x[0] == DBNull.Value ? 0 : (int)x[0],
                        CollaborationIndex = x[1] == DBNull.Value ? Guid.Empty : (Guid)x[1],
                        Name = x[2] == DBNull.Value ? null : (string)x[2],
                        Code = x[3] == DBNull.Value ? null : (string)x[3],
                        StartDate = x[4] == DBNull.Value ? DateTime.MinValue : (DateTime)x[4],
                        EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                        Agency = x[6] == DBNull.Value ? null : (string)x[6],
                        Level = x[7] == DBNull.Value ? null : (string)x[7],
                        Type = x[8] == DBNull.Value ? null : (string)x[8],
                        TherapyTypeID = x[9] == DBNull.Value ? 0 : (int)x[9],
                        AgencyID = x[10] == DBNull.Value ? 0 : (long)x[10],
                        CollaborationLevelID = x[11] == DBNull.Value ? 0 : (int)x[11],

                    };
                }, queryBuilderDTO.QueryParameterDTO);
                return Tuple.Create(data, totalCount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationListForOrgAdminCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        public int GetCollaborationListForOrgAdminCount(long agencyID)
        {
            try
            {
                List<CollaborationDataDTO> collaborationDataDTO = new List<CollaborationDataDTO>();
                var query = string.Empty;
                query = @"Select count(*) from Collaboration C
                            left join Agency A on A.AgencyID = C.AgencyID
                            left join info.CollaborationLevel CL on CL.CollaborationLevelID = C.CollaborationLevelID
                            left join info.TherapyType T on T.TherapyTypeID = C.TherapyTypeID where C.IsRemoved = 0 and C.AgencyID =" + agencyID;

                var data = ExecuteSqlQuery(query, x => new CollaborationDataDTO
                {
                    TotalCount = (int)x[0]
                });

                if (data != null && data.Count > 0)
                {
                    return data[0].TotalCount;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get Collaboration details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>CollaborationDTO.</returns>
        public async Task<CollaborationDTO> GetCollaborationAsync(int id)
        {
            try
            {
                CollaborationDTO collaborationDTO = new CollaborationDTO();
                Collaboration collaboration = await this.GetRowAsync(x => x.CollaborationID == id);
                this.mapper.Map<Collaboration, CollaborationDTO>(collaboration, collaborationDTO);
                return collaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetCollaborationDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationInfoDTO List.</returns>
        public List<CollaborationInfoDTO> GetCollaborationDetailsByName(string nameCSV, long agencyID)
        {
            List<CollaborationInfoDTO> DataList = new List<CollaborationInfoDTO>();

            try
            {
                nameCSV = string.IsNullOrEmpty(nameCSV) ? null : nameCSV.ToLower();
                var query = string.Empty;
                query = @$"SELECT CollaborationID,CollaborationIndex,[Name],AgencyID, StartDate, EndDate
                        FROM    [dbo].[Collaboration] where LOWER([Name]) in ({nameCSV})
                        and  AgencyID = {agencyID} and IsRemoved = 0 ";

                DataList = ExecuteSqlQuery(query, x => new CollaborationInfoDTO
                {
                    CollaborationID = (int)x["CollaborationID"],
                    CollaborationIndex = (Guid)x["CollaborationIndex"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                });
                return DataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CollaborationDetailsListDTO> GetPCollaborationDetailsListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO, CollaborationSearchInputDTO collaborationSearchInputDTO)
        {

            try
            {
                var query = string.Empty;
                string joinQuery = string.Empty;
                string columnQuery = string.Empty;

                if (collaborationSearchInputDTO.SearchFields?.PersonIndex != null)
                {
                    joinQuery = $@"left join PersonCollaboration Pc on Pc.CollaborationID = C.CollaborationID
                                    left join Person P on P.PersonID = pc.PersonID";
                    columnQuery = $@",P.PersonIndex";
                }

                query = $@";WITH CollaborationCTE AS (Select C.CollaborationID, C.CollaborationIndex,C.TherapyTypeID,C.[Name], C.StartDate,C.EndDate,C.IsRemoved,C.UpdateUserID, C.AgencyID,C.CollaborationLevelID,C.Code,ISNULL(C.Abbreviation,'') AS Abbreviation,ISNULL(C.Description,'') AS Description,
                                 Questionnaire = (Select CollaborationQuestionnaireID, QuestionnaireID, CollaborationID,
                                 IsMandatory, StartDate,EndDate,IsRemoved,IsReminderOn  from CollaborationQuestionnaire WHERE IsRemoved = 0 AND 
                                 CollaborationID = C.CollaborationID FOR JSON PATH),
                                 CollaboratonTags = (Select CollaborationTagID, CollaborationID, CollaborationTagTypeID, IsRemoved from CollaborationTag
                                 WHERE IsRemoved = 0 AND CollaborationID = C.CollaborationID FOR JSON PATH),
			                     CollaborationLeads = (Select CollaborationLeadHistoryID, CollaborationID, LeadUserID, RemovedUserID, StartDate, EndDate, IsRemoved
                                 from CollaborationLeadHistory WHERE IsRemoved = 0 AND CollaborationID = C.CollaborationID FOR JSON PATH){columnQuery}
                        from Collaboration C
                        {joinQuery}
                        where C.AgencyID = {loggedInUserDTO.AgencyId} AND C.IsRemoved = 0 )
                        SELECT  C.*,COUNT(C.CollaborationID) OVER() AS TotalCount FROM CollaborationCTE C WHERE 1=1 ";

                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                var collaborationDetailsListDTO = ExecuteSqlQuery(query, x => new CollaborationDetailsListDTO
                {
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    CollaborationIndex = x["CollaborationIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["CollaborationIndex"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Code = x["Code"] == DBNull.Value ? null : (string)x["Code"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    TherapyTypeID = x["TherapyTypeID"] == DBNull.Value ? 0 : (int)x["TherapyTypeID"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    CollaborationLevelID = x["CollaborationLevelID"] == DBNull.Value ? 0 : (int)x["CollaborationLevelID"],
                    Questionnaires = x["Questionnaire"] == DBNull.Value ? null : (string)x["Questionnaire"],
                    Categories = x["CollaboratonTags"] == DBNull.Value ? string.Empty : (string)x["CollaboratonTags"],
                    Leads = x["CollaborationLeads"] == DBNull.Value ? null : (string)x["CollaborationLeads"],
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    Abbreviation = x["Abbreviation"] == DBNull.Value ? null : (string)x["Abbreviation"],
                    Description = x["Description"] == DBNull.Value ? null : (string)x["Description"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                }, queryBuilderDTO.QueryParameterDTO);
                return collaborationDetailsListDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}