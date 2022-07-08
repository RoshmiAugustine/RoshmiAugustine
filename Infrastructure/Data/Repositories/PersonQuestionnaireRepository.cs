// -----------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonQuestionnaireRepository : BaseRepository<PersonQuestionnaire>, IPersonQuestionnaireRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonQuestionnaireRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonQuestionnaire"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonQuestionnaireRepository(ILogger<PersonQuestionnaireRepository> logger, OpeekaDBContext _dbContext, IMapper mapper)
            : base(_dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = _dbContext;
        }

        /// <summary>
        /// To get person questionnaire list.
        /// </summary>
        /// <param personID="personID">id.</param>
        /// <returns>PersonQuestionnaireDTO List</returns>
        public async Task<IReadOnlyList<PersonQuestionnaireDTO>> GetPersonQuestionnaireList(long id)
        {
            try
            {
                IReadOnlyList<PersonQuestionnaireDTO> personQuestionnaireDTO = new List<PersonQuestionnaireDTO>();
                IReadOnlyList<PersonQuestionnaire> personQuestionnaire = await this.GetAsync(x => x.PersonID == id && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<PersonQuestionnaire>, IReadOnlyList<PersonQuestionnaireDTO>>(personQuestionnaire, personQuestionnaireDTO);
                return personQuestionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// To add personQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO"></param>
        /// <returns>Guid.</returns>
        public long AddPersonQuestionnaire(PersonQuestionnaireDTO personQuestionnaireDTO)
        {
            try
            {
                PersonQuestionnaire personQuestionnaire = new PersonQuestionnaire();
                this.mapper.Map<PersonQuestionnaireDTO, PersonQuestionnaire>(personQuestionnaireDTO, personQuestionnaire);
                var result = this.AddAsync(personQuestionnaire).Result.PersonQuestionnaireID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update PersonQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonQuestionnaireDTO UpdatePersonQuestionnaire(PersonQuestionnaireDTO personQuestionnaireDTO)
        {
            try
            {
                PersonQuestionnaire personQuestionnaire = new PersonQuestionnaire();
                this.mapper.Map<PersonQuestionnaireDTO, PersonQuestionnaire>(personQuestionnaireDTO, personQuestionnaire);
                var result = this.UpdateAsync(personQuestionnaire).Result;
                PersonQuestionnaireDTO updatedAddress = new PersonQuestionnaireDTO();
                this.mapper.Map<PersonQuestionnaire, PersonQuestionnaireDTO>(result, updatedAddress);
                return updatedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<PersonQuestionnaireDTO> GetPersonQuestionnaire(long id, long personID)
        {
            try
            {
                PersonQuestionnaireDTO personQuestionnaireDTO = new PersonQuestionnaireDTO();
                PersonQuestionnaire personQuestionnaire = await this.GetRowAsync(x => x.QuestionnaireID == id && x.PersonID == personID && !x.IsRemoved);
                this.mapper.Map<PersonQuestionnaire, PersonQuestionnaireDTO>(personQuestionnaire, personQuestionnaireDTO);

                return personQuestionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public PersonQuestionnaireDTO GetPersonQuestionnaireWithNoAssessment(long id, long personID)
        {
            try
            {
                PersonQuestionnaireDTO personQuestionnaireDTO = new PersonQuestionnaireDTO();

                PersonQuestionnaire personQuestionnaire = this._dbContext.PersonQuestionnaires
                    .Where(p => !this._dbContext.Assessments.Any(a => p.PersonQuestionnaireID == a.PersonQuestionnaireID && !a.IsRemoved)
                    && !p.IsRemoved && p.PersonID == personID && p.QuestionnaireID == id).FirstOrDefault();
                this.mapper.Map<PersonQuestionnaire, PersonQuestionnaireDTO>(personQuestionnaire, personQuestionnaireDTO);

                return personQuestionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonQuestionnaire> GetPersonQuestionnaireWithNoAssessmentById(List<int> id, long personID)
        {
            try
            {
                var personQuestionnaire = this._dbContext.PersonQuestionnaires
                    .Where(p => !this._dbContext.Assessments.Any(a => p.PersonQuestionnaireID == a.PersonQuestionnaireID && !a.IsRemoved)
                    && !p.IsRemoved && p.PersonID == personID && id.Contains(p.QuestionnaireID)).ToList();
                return personQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// To GetcollaborationQuestionaire.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>Task<PersonQuestionnaire>.</returns>
        public async Task<PersonQuestionnaire> GetcollaborationQuestionaire(int collaborationID, long questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"select  PersonQuestionnaireID  FROM PersonQuestionnaire where 
                IsRemoved=0 And QuestionnaireID=" + questionnaireID + "  And CollaborationID=" + collaborationID;

                var PersonQuestionnaire = ExecuteSqlQuery(query, x => new PersonQuestionnaire
                {
                    PersonQuestionnaireID = x[0] == DBNull.Value ? 0 : (long)x[0]

                }).FirstOrDefault();

                return PersonQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<PersonQuestionnaireDTO> GetPersonQuestionnaireByID(long personQuestionnaireID)
        {
            try
            {
                PersonQuestionnaireDTO personQuestionnaireDTO = new PersonQuestionnaireDTO();
                var query = string.Empty;
                query = @"select  *  FROM PersonQuestionnaire where 
                IsRemoved=0 And PersonQuestionnaireID=" + personQuestionnaireID;
                 personQuestionnaireDTO = ExecuteSqlQuery(query, x => new PersonQuestionnaireDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? null : (int?)x["CollaborationID"],
                     StartDate=x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDueDate= x["EndDueDate"] == DBNull.Value ? null : (DateTime?)x["EndDueDate"],
                     IsActive = x["IsActive"] == DBNull.Value ? false : (bool)x["IsActive"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"]

                }).FirstOrDefault();
                return personQuestionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<IReadOnlyList<PersonQuestionnaireDTO>> GetPersonQuestionnaireByCollaborationAndQuestionnaire(long collaborationId, long questionnaireId)
        {
            try
            {
                IReadOnlyList<PersonQuestionnaireDTO> personQuestionnaireDTO = new List<PersonQuestionnaireDTO>();
                IReadOnlyList<PersonQuestionnaire> personQuestionnaire = await this.GetAsync(x => x.QuestionnaireID == questionnaireId && x.CollaborationID == collaborationId && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<PersonQuestionnaire>, IReadOnlyList<PersonQuestionnaireDTO>>(personQuestionnaire, personQuestionnaireDTO);
                return personQuestionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateBulkPersonQuestionnaires.
        /// </summary>
        /// <param name="notes">List of Note</param>
        /// <returns>List of Note</returns>
        public List<PersonQuestionnaire> UpdateBulkPersonQuestionnaires(List<PersonQuestionnaire> personQuestionnaire)
        {
            try
            {
                var res = this.UpdateBulkAsync(personQuestionnaire);
                res.Wait();
                return personQuestionnaire;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// UpdateBulkPersonQuestionnaires.
        ///// </summary>
        ///// <param name="notes">List of Note</param>
        ///// <returns>List of Note</returns>
        //public List<PersonQuestionnaireDTO> BulkUpdatePersonQuestionnaires(List<PersonQuestionnaireDTO> personQuestionnaireDTOList)
        //{
        //    try
        //    {
        //        var res = this.
        //        res.Wait();
        //        return personQuestionnaireDTOList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        /// <summary>
        /// GetAllPersonQuestionnaireIdsByCollaborationID.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>List of PersonQuestionnaireID</returns>
        public List<long> GetAllPersonQuestionnaireIdsByCollaborationID(long collaborationID)
        {
            try
            {
                var res = from PQ in this._dbContext.PersonQuestionnaires
                          join P in this._dbContext.Person on PQ.PersonID equals P.PersonID
                          join C in this._dbContext.Collaborations on PQ.CollaborationID equals C.CollaborationID
                          join CQ in this._dbContext.CollaborationQuestionnaire on new { C.CollaborationID, PQ.QuestionnaireID } equals new { CQ.CollaborationID, CQ.QuestionnaireID }
                          where PQ.CollaborationID == collaborationID && PQ.IsRemoved == false && P.IsRemoved == false && CQ.IsReminderOn == true
                          select PQ.PersonQuestionnaireID;
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAllPersonQuestionnaireIdsByQuestionnaireID.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>List of PersonQuestionnaireID</returns>
        public List<long> GetAllPersonQuestionnaireIdsByQuestionnaireID(int questionnaireID)
        {
            try
            {
                var res = from PQ in this._dbContext.PersonQuestionnaires
                          join P in this._dbContext.Person on PQ.PersonID equals P.PersonID
                          where PQ.QuestionnaireID == questionnaireID && PQ.IsRemoved == false && P.IsRemoved == false
                          select PQ.PersonQuestionnaireID;
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetPersonQuestionnaireID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>long</returns>
        public List<PersonQuestionnaire> GetPersonQuestionaireByPersonQuestionaireID(long personQuestionnaireID)
        {
            try
            {
                var data = (from P in this._dbContext.PersonQuestionnaires
                            where P.PersonQuestionnaireID == personQuestionnaireID
                            select P).FirstOrDefault();
                if (data != null)
                {
                    return (from P in this._dbContext.PersonQuestionnaires
                            join A in this._dbContext.Assessments on P.PersonQuestionnaireID equals A.PersonQuestionnaireID into t
                            from nt in t.DefaultIfEmpty()
                            where P.PersonID == data.PersonID && P.QuestionnaireID == data.QuestionnaireID
                            orderby nt.DateTaken descending
                            select P).ToList();
                }
                else
                {
                    return new List<PersonQuestionnaire>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>long</returns>
        public List<Assessment> GetAssessmentsByPersonQuestionaireID(long personQuestionnaireID, List<int> assessmentStatusIDList)
        {
            try
            {
                var data = (from P in this._dbContext.PersonQuestionnaires
                            where P.PersonQuestionnaireID == personQuestionnaireID
                            select P).FirstOrDefault();
                if (data != null)
                {
                    return (from P in this._dbContext.PersonQuestionnaires
                            join A in this._dbContext.Assessments on P.PersonQuestionnaireID equals A.PersonQuestionnaireID
                            where P.PersonID == data.PersonID && P.QuestionnaireID == data.QuestionnaireID && assessmentStatusIDList.Contains(A.AssessmentStatusID)
                            orderby A.DateTaken descending
                            select A).ToList();
                }
                else
                {
                    return new List<Assessment>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// UpdatePersonQuestionnaire.
        /// </summary>
        /// <param name="personQuestionnaire">personQuestionnaire.</param>
        /// <returns>PersonQuestionnaire.</returns>
        public PersonQuestionnaire UpdatePersonQuestionnaire(PersonQuestionnaire personQuestionnaire)
        {
            try
            {
                return this.UpdateAsync(personQuestionnaire).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// GetAssessmentsByPersonIndex
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>list of Assessment</returns>
        public List<Assessment> GetAssessmentsByPersonIndex(Guid personIndex, int questionnaireID)
        {
            try
            {

                return (from P in this._dbContext.PersonQuestionnaires
                        join per in this._dbContext.Person on P.PersonID equals per.PersonID
                        join A in this._dbContext.Assessments on P.PersonQuestionnaireID equals A.PersonQuestionnaireID
                        where per.PersonIndex == personIndex && P.QuestionnaireID == questionnaireID && A.IsRemoved == false
                        orderby A.DateTaken descending
                        select A).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireByPersonIndex
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>list of Assessment</returns>
        public List<PersonQuestionnaire> GetPersonQuestionnaireByPersonIndex(Guid personIndex, int questionnaireID)
        {
            try
            {

                return (from P in this._dbContext.PersonQuestionnaires
                        join per in this._dbContext.Person on P.PersonID equals per.PersonID
                        where per.PersonIndex == personIndex && P.QuestionnaireID == questionnaireID && !P.IsRemoved
                        select P).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// To add personQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO"></param>
        /// <returns>Guid.</returns>
        public void AddBulkPersonQuestionnaire(List<PersonQuestionnaireDTO> personQuestionnaireDTO)
        {
            try
            {
                List<PersonQuestionnaire> personQuestionnaire = new List<PersonQuestionnaire>();
                this.mapper.Map<List<PersonQuestionnaireDTO>, List<PersonQuestionnaire>>(personQuestionnaireDTO, personQuestionnaire);
                this.AddBulkAsync(personQuestionnaire).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<PersonQuestionnaireDTO> GetPersonQuestionnaireByCollaborationQuestionnaireAndPersonID(long collaborationId, long questionnaireId, long personID)
        {
            try
            {
                PersonQuestionnaireDTO personQuestionnaireDTO = new PersonQuestionnaireDTO();
                PersonQuestionnaire personQuestionnaire = await this.GetRowAsync(x => x.QuestionnaireID == questionnaireId && x.CollaborationID == collaborationId && x.PersonID == personID && !x.IsRemoved);
                this.mapper.Map<PersonQuestionnaire, PersonQuestionnaireDTO>(personQuestionnaire, personQuestionnaireDTO);
                return personQuestionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonQuestionnaire> GetAllpersonQuestionnaire(List<long> personID, int questionnaireID)
        {
            try
            {
                List<PersonQuestionnaire> personQuestionnaire = this._dbContext.PersonQuestionnaires.Where(x => x.QuestionnaireID == questionnaireID && personID.Contains(x.PersonID) && !x.IsRemoved).ToList();
                return personQuestionnaire;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Person GetPersonByPersonQuestionnaireID(long personQuestionnaireID)
        {
            try
            {
                var person = new Person();
                var personQuestionaire = this.GetRowAsync(x => x.PersonQuestionnaireID == personQuestionnaireID).Result;
                if (personQuestionaire?.PersonID != 0)
                {
                    person = (from P in this._dbContext.Person where P.PersonID == personQuestionaire.PersonID select P).FirstOrDefault();
                }
                return person;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetPersonQuestionnaireIdsWithReminderOn.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public List<PersonQuestionnaireDTO> GetPersonQuestionnaireIdsWithReminderOn(long personId, List<int> collaborationIds = null)
        {
            try
            {
                var collaborationCondition = collaborationIds?.Count > 0 ? $@"AND PC.CollaborationId in ({string.Join(",", collaborationIds)})" : "";
                var query = @$"SELECT DISTINCT PQ.PersonQuestionnaireID
                               FROM PersonCollaboration PC 
							   JOIN PersonQuestionnaire PQ  ON PQ.PersonId = PC.PersonID AND PQ.CollaborationId = PC.CollaborationId 
                               JOIN CollaborationQuestionnaire CQ ON CQ.CollaborationId = PQ.CollaborationId 
							    AND PQ.QuestionnaireID = CQ.QuestionnaireID
                                WHERE PQ.PersonID = {personId} {collaborationCondition} AND PQ.IsRemoved = 0 
                                AND CQ.IsRemoved = 0 AND PC.IsRemoved = 0 AND CQ.IsReminderOn = 1";

                var result = ExecuteSqlQuery(query, x => new PersonQuestionnaireDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"]
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
