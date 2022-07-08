// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingRepository.cs" company="Naicoits">
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
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationSharingRepository : BaseRepository<CollaborationSharing>, ICollaborationSharingRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaborationSharingRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public CollaborationSharingRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddCollaborationSharing
        /// </summary>
        /// <param name="collaborationSharingDTO"></param>
        /// <returns>CollaborationSharingDTO</returns>
        public CollaborationSharingDTO AddCollaborationSharing(CollaborationSharingDTO collaborationSharingDTO)
        {
            try
            {
                CollaborationSharing collaborationSharing = new CollaborationSharing();
                this.mapper.Map<CollaborationSharingDTO, CollaborationSharing>(collaborationSharingDTO, collaborationSharing);
                var result = this.AddAsync(collaborationSharing).Result;
                this.mapper.Map<CollaborationSharing, CollaborationSharingDTO>(result, collaborationSharingDTO);
                return collaborationSharingDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To update CollaborationSharing details.
        /// </summary>
        /// <param name="collaborationSharingDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public CollaborationSharingDTO UpdateCollaborationSharing(CollaborationSharingDTO collaborationSharingDTO)
        {
            try
            {
                CollaborationSharing collaborationSharing = new CollaborationSharing();
                this.mapper.Map<CollaborationSharingDTO, CollaborationSharing>(collaborationSharingDTO, collaborationSharing);
                var result = this.UpdateAsync(collaborationSharing).Result;
                CollaborationSharingDTO updated = new CollaborationSharingDTO();
                return this.mapper.Map<CollaborationSharing, CollaborationSharingDTO>(result, updated);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details CollaborationSharing.
        /// </summary>
        /// <param CollaborationSharingDTO>id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        public async Task<CollaborationSharingDTO> GetCollaborationSharing(Guid id)
        {
            try
            {
                CollaborationSharingDTO collaborationSharingDTO = new CollaborationSharingDTO();
                CollaborationSharing collaborationSharing = await this.GetRowAsync(x => x.CollaborationSharingIndex == id);
                this.mapper.Map<CollaborationSharing, CollaborationSharingDTO>(collaborationSharing, collaborationSharingDTO);

                return collaborationSharingDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationQuestionairesList.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>QuestionnaireDTO list.</returns>
        public List<QuestionnaireDTO> GetCollaborationQuestionairesList(int collaborationID)
        {
            try
            {
                var query = string.Empty;
                query = @"select Q.InstrumentID,Q.[Name],Q.[Description],Q.Abbrev,ReminderScheduleName,Q.UpdateDate,QNRS.[Name] as NotificationScheduleName,
                            Q.QuestionnaireID, I.Abbrev as InstrumentAbbrev from CollaborationQuestionnaire CQ  
                        left join Questionnaire Q on CQ.QuestionnaireID= Q.QuestionnaireID
                        left join  QuestionnaireNotifyRiskSchedule QNRS on Q.QuestionnaireID=QNRS.QuestionnaireID
                        left join info.Instrument I ON Q.InstrumentID = I.InstrumentID
                         where CQ.IsRemoved=0 and  CQ.CollaborationID= " + collaborationID;

                var questionnaireDTO = ExecuteSqlQuery(query, x => new QuestionnaireDTO
                {
                    InstrumentID = (int)x["InstrumentID"],
                    QuestionnaireName = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Description = x["Description"] == DBNull.Value ? null : (string)x["Description"],
                    ReminderScheduleName = x["ReminderScheduleName"] == DBNull.Value ? null : (string)x["ReminderScheduleName"],
                    QuestionnaireAbbrev = x["Abbrev"] == DBNull.Value ? null : (string)x["Abbrev"],
                    UpdateDate = (DateTime)x["UpdateDate"],
                    NotificationScheduleName=x["NotificationScheduleName"] == DBNull.Value ? null : (string)x["NotificationScheduleName"],
                    QuestionnaireID = (int)x["QuestionnaireID"],
                    InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? null : (string)x["InstrumentAbbrev"]
                });

                return questionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
