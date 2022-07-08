// -----------------------------------------------------------------------
// <copyright file="CollaborationQuestionnaireRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationQuestionnaireRepository : BaseRepository<CollaborationQuestionnaire>, ICollaborationQuestionnaireRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        public CollaborationQuestionnaireRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To add collaboration details.
        /// </summary>
        /// <param name="collaborationQuestionnaireDTO">collaborationDetailsDTO.</param>
        /// <returns>id.</returns>
        public Int64 AddCollaborationQuestionnaire(CollaborationQuestionnaireDTO collaborationQuestionnaireDTO)
        {
            try
            {
                CollaborationQuestionnaire collaborationQuestionnaire = new CollaborationQuestionnaire();
                this.mapper.Map<CollaborationQuestionnaireDTO, CollaborationQuestionnaire>(collaborationQuestionnaireDTO, collaborationQuestionnaire);
                var result = (this.AddAsync(collaborationQuestionnaire).Result.CollaborationQuestionnaireID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update collaboration questionnaire details.
        /// </summary>
        /// <param name="CollaborationQuestionnaireDTO">CollaborationQuestionnaireDTO.</param>
        /// <returns>CollaborationQuestionnaireDTO.</returns>
        public CollaborationQuestionnaireDTO UpdateCollaborationQuestionnaire(CollaborationQuestionnaireDTO collaborationQuestionnaireDTO)
        {
            try
            {
                CollaborationQuestionnaire collaborationQuestionnaire = new CollaborationQuestionnaire();
                this.mapper.Map<CollaborationQuestionnaireDTO, CollaborationQuestionnaire>(collaborationQuestionnaireDTO, collaborationQuestionnaire);
                var result = this.UpdateAsync(collaborationQuestionnaire).Result;
                CollaborationQuestionnaireDTO updatedCollaborationQuestionnaire = new CollaborationQuestionnaireDTO();
                this.mapper.Map<CollaborationQuestionnaire, CollaborationQuestionnaireDTO>(result, updatedCollaborationQuestionnaire);
                return updatedCollaborationQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationQuestionnaireData.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationQuestionnaireDTO List.</returns>
        public List<CollaborationQuestionnaireDTO> GetCollaborationQuestionnaireData(int collaborationID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select CollaborationQuestionnaireID, QuestionnaireID, CollaborationID, IsMandatory, StartDate, EndDate, IsRemoved,
IsReminderOn  from CollaborationQuestionnaire
                            WHERE IsRemoved=0 AND CollaborationID = " + collaborationID;

                var CollaborationQuestionnaire = ExecuteSqlQuery(query, x => new CollaborationQuestionnaireDTO
                {
                    CollaborationQuestionnaireID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    QuestionnaireID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    CollaborationID = x[2] == DBNull.Value ? 0 : (int)x[2],
                    IsMandatory = x[3] == DBNull.Value ? false : (bool)x[3],
                    StartDate = x[4] == DBNull.Value ? null : (DateTime?)x[4],
                    EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                    IsRemoved = x[6] == DBNull.Value ? false : (bool)x[6],
                    IsReminderOn = x[7] == DBNull.Value ? false : (bool)x[7]

                });
                return CollaborationQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationQuestionnaireData.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationQuestionnaireDTO List.</returns>
        public CollaborationQuestionnaireDTO GetCollaborationQuestionnaireByCollaborationAndQuestionnaire(long collaborationID, long questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select CollaborationQuestionnaireID, QuestionnaireID, CollaborationID, IsMandatory, StartDate, EndDate, IsRemoved,
IsReminderOn  from CollaborationQuestionnaire
                            WHERE IsRemoved=0 AND CollaborationID = " + collaborationID + " AND QuestionnaireID = " + questionnaireID + " AND IsRemoved = 0";

                var CollaborationQuestionnaire = ExecuteSqlQuery(query, x => new CollaborationQuestionnaireDTO
                {
                    CollaborationQuestionnaireID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    QuestionnaireID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    CollaborationID = x[2] == DBNull.Value ? 0 : (int)x[2],
                    IsMandatory = x[3] == DBNull.Value ? false : (bool)x[3],
                    StartDate = x[4] == DBNull.Value ? null : (DateTime?)x[4],
                    EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                    IsRemoved = x[6] == DBNull.Value ? false : (bool)x[6],
                    IsReminderOn = x[7] == DBNull.Value ? false : (bool)x[7]

                }).FirstOrDefault();
                return CollaborationQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all collaboratin for a personcollaboration
        /// </summary>
        /// <param name="list_personCollaborationIds"></param>
        /// <returns></returns>
        public List<PeopleCollaborationDTO> GetAllPersonCollaborationForReminders(List<long> list_personCollaborationIds)
        {

            try
            {
                var query = string.Empty;
                query = @$"SELECT PC.PersonCollaborationID, PC.EnrollDate, PC.EndDate, PQ.PersonQuestionnaireID 
                            FROM PersonCollaboration PC
                            JOIN PersonQuestionnaire PQ ON PC.CollaborationID = PQ.CollaborationID AND PQ.PersonID =PC.PersonID
                            WHERE PQ.IsRemoved = 0 AND PC.IsRemoved = 0 AND PQ.PersonQuestionnaireID 
                            in ({string.Join(",", list_personCollaborationIds)})";
                var peopleCollaborationDTO = ExecuteSqlQuery(query, x => new PeopleCollaborationDTO
                {
                    PersonCollaborationID = x["PersonCollaborationID"] == DBNull.Value ? 0 : (long)x["PersonCollaborationID"],
                    EnrollDate = (DateTime)x["EnrollDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"]
                });
                return peopleCollaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetCollaborationQuestionnaireData.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationQuestionnaireDTO List.</returns>
        public List<CollaborationQuestionnaireDTO> GetCollaborationQuestionnaireData(List<int> collaborationIDs)
        {
            try
            {
                var query = string.Empty;
                var condition = collaborationIDs.Count > 0 ? @$"AND CollaborationID in ({string.Join(",", collaborationIDs)})" : "AND CollaborationID in (0)";
                query = @$"Select CollaborationQuestionnaireID, QuestionnaireID, CollaborationID, IsMandatory, StartDate, EndDate, IsRemoved,
                            IsReminderOn  from CollaborationQuestionnaire
                            WHERE IsRemoved=0 {condition}";

                var CollaborationQuestionnaire = ExecuteSqlQuery(query, x => new CollaborationQuestionnaireDTO
                {
                    CollaborationQuestionnaireID = x["CollaborationQuestionnaireID"] == DBNull.Value ? 0 : (int)x["CollaborationQuestionnaireID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"]
                });
                return CollaborationQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
