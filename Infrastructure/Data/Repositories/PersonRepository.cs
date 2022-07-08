// -----------------------------------------------------------------------
// <copyright file="PersonRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private IAssessmentReasonRepository AssessmentReasonRepository;
        private IAssessmentStatusRepository AssessmentStatusRepository;
        private IHelperRepository helperRepository;
        public PersonRepository(OpeekaDBContext dbContext, IMapper mapper, IAssessmentReasonRepository assessmentReasonRepository, IAssessmentStatusRepository assessmentStatusRepository, IHelperRepository helperRepository)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this.AssessmentReasonRepository = assessmentReasonRepository;
            this.AssessmentStatusRepository = assessmentStatusRepository;
            this.helperRepository = helperRepository;
        }

        /// <summary>
        /// GetPersonList.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PersonDTO> GetPersons(int pageNumber, int pageSize)
        {
            try
            {
                List<PersonDTO> personDTO = new List<PersonDTO>();
                var query = string.Empty;

                query = @"select distinct P.FirstName+ ' ' +isnull(P.MiddleName+' ','') +P.LastName as [Name], P.StartDate, P.PersonID, P.PersonIndex
                        from Person P where p.IsRemoved=0 and P.IsActive=1 Order by [Name]";

                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
                personDTO = ExecuteSqlQuery(query, x => new PersonDTO
                {
                    Name = x[0] == DBNull.Value ? null : (string)x[0],
                    StartDate = x[1] == DBNull.Value ? DateTime.MinValue : (DateTime)x[1],
                    PersonID = x[2] == DBNull.Value ? 0 : (long)x[2],
                    PersonIndex = x[3] == DBNull.Value ? Guid.Empty : (Guid)x[3]
                });
                return personDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int GetPersonsCount()
        {
            try
            {
                List<PersonDTO> personDTO = new List<PersonDTO>();
                var query = string.Empty;

                query = @"select count(*)
                        from Person P";

                var data = ExecuteSqlQuery(query, x => new PersonDTO
                {
                    TotalCount = (int)x[0],
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
        /// GetPeopleDetails
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <returns>PeopleDataDTO</returns>
        public PeopleDataDTO GetPeopleDetails(Guid peopleIndex)
        {
            PeopleDataDTO peopleDetails = new PeopleDataDTO();
            try
            {
                var query = string.Empty;
                query = @"SELECT P.PersonID,P.PersonIndex,P.FirstName,P.MiddleName,P.LastName,P.Suffix,P.Phone1,P.Phone2,P.Email
                        ,A.Address1,A.Address2,A.City,CS.CountryStateID,CS.Name AS State,A.Zip
                        ,P.DateOfBirth,G.IdentifiedGenderID AS IdentifiedGenderID,G.Name AS IdentifiedGender,G1.GenderID AS BioGenderID,G1.Name AS BioGender,S.SexualityID,S.Name AS Sexuality,PML.LanguageID AS PrimaryLanugageID,PML.Name AS PrimaryLanugage,PFL.LanguageID AS PreferredLanugageID,PFL.Name AS PreferredLanugage
                        ,A.AddressID,PA.PersonAddressID, P.PersonScreeningStatusID, P.StartDate, P.EndDate
                        ,P.Phone1Code,P.Phone2Code, P.IsRemoved, P.AgencyID, P.IsActive, Ag.Name As AgencyName, P.UniversalID
                        ,C.CountryID,C.Name AS Country, P.TextPermission, P.EmailPermission
                        FROM Person P
                        LEFT JOIN PersonAddress PA ON PA.PersonID = P.PersonID
                        LEFT JOIN Address A ON A.AddressID = PA.AddressID
                        LEFT JOIN [info].[CountryState] CS ON CS.CountryStateID = A.CountryStateId
                        LEFT JOIN [info].[IdentifiedGender] G ON G.IdentifiedGenderID = P.GenderID
                        LEFT JOIN [info].[Gender] G1 ON G1.GenderID = P.BiologicalSexID
                        LEFT JOIN [info].[Sexuality] S ON S.SexualityID = P.SexualityID      
                        LEFT JOIN [info].[Language] PML ON PML.LanguageID = P.PrimaryLanguageID  
                        LEFT JOIN [info].[Language] PFL ON PFL.LanguageID = P.PreferredLanguageID
                        LEFT JOIN [info].Country C ON c.CountryID=A.CountryId
                        LEFT JOIN Agency Ag ON P.AgencyID = Ag.AgencyID
                        WHERE P.PersonIndex = '" + peopleIndex + "'";

                peopleDetails = ExecuteSqlQuery(query, x => new PeopleDataDTO
                {
                    PersonID = (long)x[0],
                    PersonIndex = (Guid)x[1],
                    FirstName = x[2] == DBNull.Value ? null : (string)x[2],
                    MiddleName = x[3] == DBNull.Value ? null : (string)x[3],
                    LastName = x[4] == DBNull.Value ? null : (string)x[4],
                    Suffix = x[5] == DBNull.Value ? null : (string)x[5],
                    Phone1 = x[6] == DBNull.Value ? null : (string)x[6],
                    Phone2 = x[7] == DBNull.Value ? null : (string)x[7],
                    Email = x[8] == DBNull.Value ? null : (string)x[8],

                    Address1 = x[9] == DBNull.Value ? null : (string)x[9],
                    Address2 = x[10] == DBNull.Value ? null : (string)x[10],
                    City = x[11] == DBNull.Value ? null : (string)x[11],
                    CountryStateID = x[12] == DBNull.Value ? 0 : (int)x[12],
                    State = x[13] == DBNull.Value ? null : (string)x[13],
                    Zip = x[14] == DBNull.Value ? null : (string)x[14],

                    DateOfBirth = (DateTime)x[15],
                    IdentifiedGenderID = x[16] == DBNull.Value ? 0 : (int)x[16],
                    IdentifiedGender = x[17] == DBNull.Value ? null : (string)x[17],
                    BioGenderID = x[18] == DBNull.Value ? 0 : (int)x[18],
                    BioGender = x[19] == DBNull.Value ? null : (string)x[19],
                    SexualityID = x[20] == DBNull.Value ? 0 : (int)x[20],
                    Sexuality = x[21] == DBNull.Value ? null : (string)x[21],
                    PrimaryLanugageID = x[22] == DBNull.Value ? 0 : (int)x[22],
                    PrimaryLanugage = x[23] == DBNull.Value ? null : (string)x[23],
                    PreferredLanugageID = x[24] == DBNull.Value ? 0 : (int)x[24],
                    PreferredLanugage = x[25] == DBNull.Value ? null : (string)x[25],
                    AddressID = x[26] == DBNull.Value ? 0 : (long)x[26],
                    PersonAddressID = x[27] == DBNull.Value ? 0 : (long)x[27],
                    PersonScreeningStatusID = x[28] == DBNull.Value ? 0 : (int)x[28],
                    StartDate = (DateTime)x[29],
                    EndDate = x[30] == DBNull.Value ? null : (DateTime?)x[30],
                    Phone1Code = x[31] == DBNull.Value ? null : (string)x[31],
                    Phone2Code = x[32] == DBNull.Value ? null : (string)x[32],
                    IsRemoved = x[33] == DBNull.Value ? false : (bool)x[33],
                    AgencyID = x[34] == DBNull.Value ? 0 : (long)x[34],
                    IsActive = x[35] == DBNull.Value ? false : (bool)x[35],
                    AgencyName = x[36] == DBNull.Value ? null : (string)x[36],
                    UniversalID = x[37] == DBNull.Value ? null : (string)x[37],
                    CountryID = x[38] == DBNull.Value ? 0 : (int)x[38],
                    Country = x[39] == DBNull.Value ? null : (string)x[39],
                    TextPermission = x[40] == DBNull.Value ? false : (bool)x[40],
                    EmailPermission = x[41] == DBNull.Value ? false : (bool)x[41],
                }).FirstOrDefault();

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleIdentifierList
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns>List of PeopleIdentifierDTO</returns>
        public List<PeopleIdentifierDTO> GetPeopleIdentifierList(long personID)
        {
            try
            {
                var query = string.Empty;
                query = @"SELECT IT.IdentificationTypeID,IT.Name AS IdentifierType, PI.IdentificationNumber AS IdentifierID, PI.PersonIdentificationID
                        FROM PersonIdentification PI
                        JOIN [info].[IdentificationType] IT ON IT.IdentificationTypeID = PI.IdentificationTypeID
                        WHERE PI.IsRemoved = 0 AND PI.PersonID = " + personID;

                var peopleDetails = ExecuteSqlQuery(query, x => new PeopleIdentifierDTO
                {
                    IdentificationTypeID = (int)x[0],
                    IdentifierType = x[1] == DBNull.Value ? null : (string)x[1],
                    IdentifierID = (string)x[2],
                    PersonIdentificationID = (long)x[3]
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleRaceEthnicityList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>List of PeopleRaceEthnicityDTO</returns>
        public List<PeopleRaceEthnicityDTO> GetPeopleRaceEthnicityList(long personID)
        {
            try
            {
                var query = string.Empty;
                query = @"SELECT RE.RaceEthnicityID,RE.Name AS RaceEthnicity,PRE.PersonRaceEthnicityID 
                        FROM PersonRaceEthnicity PRE 
                        JOIN [info].[RaceEthnicity] RE ON RE.RaceEthnicityID = PRE.RaceEthnicityID
                        WHERE PRE.IsRemoved = 0 AND  PRE.PersonID = " + personID;

                var peopleDetails = ExecuteSqlQuery(query, x => new PeopleRaceEthnicityDTO
                {
                    RaceEthnicityID = (int)x[0],
                    RaceEthnicity = x[1] == DBNull.Value ? null : (string)x[1],
                    PersonRaceEthnicityID = (long)x[2]
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To add person details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List of summaries.</returns>
        public PeopleDTO AddPerson(PeopleDTO peopleDTO)
        {
            try
            {
                Person person = new Person();
                this.mapper.Map(peopleDTO, person);
                var result = this.AddAsync(person).Result;
                this.mapper.Map<Person, PeopleDTO>(result, peopleDTO);
                return peopleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleSupportList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>List of PeopleSupportDTO</returns>
        public List<PeopleSupportDTO> GetPeopleSupportList(Int64 personID)
        {
            try
            {
                var query = string.Empty;

                query = @"SELECT PS.FirstName AS SupportFirstName,PS.MiddleName AS SupportMiddleName,PS.LastName AS SupportLastName,PS.Phone AS SupportPhone,PS.Email AS SupportEmail,
                        ST.SupportTypeID AS RelationshipID,ST.Name AS Relationship,PS.PersonSupportID , PS.IsCurrent, PS.StartDate, PS.EndDate, PS.Suffix, PS.PersonID
                          ,PS.PhoneCode AS SupportPhoneCode, PS.TextPermission, PS.EmailPermission
                        FROM PersonSupport PS 
                        JOIN [info].[SupportType] ST ON ST.SupportTypeID = PS.SupportTypeID
                        WHERE PS.IsRemoved=0 AND PS.PersonID = " + personID;

                var peopleDetails = ExecuteSqlQuery(query, x => new PeopleSupportDTO
                {
                    SupportFirstName = x[0] == DBNull.Value ? null : (string)x[0],
                    SupportMiddleName = x[1] == DBNull.Value ? null : (string)x[1],
                    SupportLastName = x[2] == DBNull.Value ? null : (string)x[2],
                    SupportPhone = x[3] == DBNull.Value ? null : (string)x[3],
                    SupportEmail = x[4] == DBNull.Value ? null : (string)x[4],
                    RelationshipID = x[5] == DBNull.Value ? 0 : (int)x[5],
                    Relationship = x[6] == DBNull.Value ? null : (string)x[6],
                    PersonSupportID = x[7] == DBNull.Value ? 0 : (int)x[7],
                    IsCurrent = x[8] == DBNull.Value ? false : (bool)x[8],
                    StartDate = x[9] == DBNull.Value ? DateTime.MinValue : (DateTime)x[9],
                    EndDate = x[10] == DBNull.Value ? null : (DateTime?)x[10],
                    Suffix = x[11] == DBNull.Value ? null : (string)x[11],
                    PersonID = x[12] == DBNull.Value ? 0 : (long)x[12],
                    SupportPhoneCode = x[13] == DBNull.Value ? null : (string)x[13],
                    TextPermission = x[14] == DBNull.Value ? false : (bool)x[14],
                    EmailPermission = x[15] == DBNull.Value ? false : (bool)x[15],
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleHelperList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>List of PeopleHelperDTO</returns>
        public List<PeopleHelperDTO> GetPeopleHelperList(Int64 personID)
        {
            try
            {

                var query = string.Empty;
                query = @"SELECT H.HelperID,(H.FirstName+ ' ' + ISNULL(H.MiddleName,' ') + H.LastName) AS HelperName,PH.StartDate AS HelperStartDate,PH.EndDate AS HelperEndDate,PH.PersonHelperID, H.UserID,
                            PH.IsCurrent, PH.IsLead, PH.CollaborationID, C.Name AS CollaborationName
                            FROM PersonHelper PH 
                            JOIN Helper H ON H.HelperID = PH.HelperID
                            LEFT JOIN Collaboration C ON PH.CollaborationID = C.CollaborationID
                        WHERE PH.IsRemoved=0 AND PH.PersonID = " + personID;

                var peopleDetails = ExecuteSqlQuery(query, x => new PeopleHelperDTO
                {
                    HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                    HelperName = x["HelperName"] == DBNull.Value ? null : (string)x["HelperName"],
                    HelperStartDate = (DateTime)x["HelperStartDate"],
                    HelperEndDate = x["HelperEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)x["HelperEndDate"],
                    PersonHelperID = x["PersonHelperID"] == DBNull.Value ? 0 : (long)x["PersonHelperID"],
                    IsCurrent = x["IsCurrent"] == DBNull.Value ? false : (bool)x["IsCurrent"],
                    IsLead = x["IsLead"] == DBNull.Value ? false : (bool)x["IsLead"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    CollaborationName = x["CollaborationName"] == DBNull.Value ? string.Empty : (string)x["CollaborationName"],
                    UserID = x["UserID"] == DBNull.Value ? 0 : (int)x["UserID"],
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleCollaborationList
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>List of PeopleCollaborationDTO</returns>
        public List<PeopleCollaborationDTO> GetPeopleCollaborationList(Int64 personID, long agencyID, int questionnaireID, int userID)
        {
            try
            {
                var ConditionWithSharedQuestionIDs = string.Empty;
                var ConditionWithSharedCollaborationIDs = string.Empty;
                var ConditionWithHelperQuestionIDs = string.Empty;
                var ConditionWithHelperCollaborationIDs = string.Empty;
                if (questionnaireID != 0 && userID != 0)
                {
                    //if shared person fetch shared collaboration and shared questionnaires
                    var sharedIDs = this.GetSharedPersonQuestionnaireDetails(personID, agencyID);
                    ConditionWithSharedQuestionIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                    ConditionWithSharedCollaborationIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND C.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                    //if not a shared person check and fetch helper-assigned-collaboration and questionnaires
                    if (string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs))
                    {
                        //HelperCollabrtionDetails
                        var helperColbIDs = this.GetPersonHelperCollaborationDetails(personID, agencyID, userID);
                        ConditionWithHelperQuestionIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                        ConditionWithHelperCollaborationIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND C.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";
                    }
                }
                var query = string.Empty;
                var assessmentreason = this.AssessmentReasonRepository.GetAllAssessmentReason();
                var initialReasonID = assessmentreason.Where(x => x.Name == PCISEnum.AssessmentReason.Initial).ToList()[0].AssessmentReasonID;
                var dischargeReasonID = assessmentreason.Where(x => x.Name == PCISEnum.AssessmentReason.Discharge).ToList()[0].AssessmentReasonID;
                query = $@";WITH WindowOffsets AS
                            (
                             SELECT DISTINCT pq.PersonID,
                             q.QuestionnaireID,qw.AssessmentReasonID [ReasonID],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
                             FROM
                             PersonQuestionnaire pq
                             JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID AND pq.PersonID = {personID}
                             JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID
                             WHERE qw.AssessmentReasonID IN ({initialReasonID},{dischargeReasonID})
                                   AND Q.QuestionnaireID = {questionnaireID} {ConditionWithSharedQuestionIDs} {ConditionWithHelperQuestionIDs}
                            )
                            ,Offset AS
                            (
                              SELECT
                              (SELECT WindowOpenOffsetDays FROM WindowOffsets WHERE [ReasonID] = {initialReasonID}) AS WindowOpenOffsetDays,
                              (SELECT WindowCloseOffsetDays FROM WindowOffsets WHERE [ReasonID] = {dischargeReasonID}) AS WindowCloseOffsetDays,{personID}
                             AS PersonID
                            )
                            SELECT C.CollaborationID, C.Name AS CollaborationName, PC.EnrollDate, PC.EndDate,
                             PC.PersonCollaborationID, PC.IsPrimary, PC.IsCurrent, W.WindowOpenOffsetDays, W.WindowCloseOffsetDays
                             ,DATEADD(DAY,0-ISNULL(W.WindowOpenOffsetDays,0),CAST(PC.EnrollDate AS DATE)) AS CollaborationStartDate
                             ,DATEADD(DAY,ISNULL(W.WindowCloseOffsetDays,0), PC.EndDate) AS CollaborationEndDate
                                  FROM PersonCollaboration PC
                                  JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                  JOIN Offset W ON W.PersonID = PC.PersonID
                                  WHERE PC.IsRemoved=0 AND PC.PersonID = {personID}
                                  {ConditionWithSharedCollaborationIDs} {ConditionWithHelperCollaborationIDs}
                                  ORDER BY PC.EnrollDate DESC";

                var peopleDetails = ExecuteSqlQuery(query, x => new PeopleCollaborationDTO
                {
                    CollaborationID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    CollaborationName = x[1] == DBNull.Value ? null : (string)x[1],
                    CollaborationStartDate = (DateTime)x[9],
                    CollaborationEndDate = x[10] == DBNull.Value ? (DateTime?)null : (DateTime)x[10],
                    PersonCollaborationID = x[4] == DBNull.Value ? 0 : (long)x[4],
                    IsPrimary = x[5] == DBNull.Value ? false : (bool)x[5],
                    IsCurrent = x[6] == DBNull.Value ? false : (bool)x[6],
                    WindowOpenOffsetDays = x[7] == DBNull.Value ? 0 : (int)x[7],
                    WindowCloseOffsetDays = x[8] == DBNull.Value ? 0 : (int)x[8],
                    EnrollDate = (DateTime)x[2],
                    EndDate = x[3] == DBNull.Value ? (DateTime?)null : (DateTime)x[3],
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetPeopleCollaborationList -Reports
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>List of PeopleCollaborationDTO</returns>
        public List<PeopleCollaborationDTO> GetPeopleCollaborationListForReport(Int64 personID, long personQuestionaireID, int voiceTypeID, UserTokenDetails userTokenDetails)
        {
            try
            {

                var sharedIDs = this.GetSharedPersonQuestionnaireDetails(personID, userTokenDetails.AgencyID);
                var ConditionWithSharedCollaborationIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND C.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";
                var ConditionWithSharedQuestionIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";

                var ConditionWithHelperQuestionIDs = string.Empty;
                var ConditionWithHelperCollaborationIDs = string.Empty;
                if (string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs))
                {
                    // //HelperCollabrtionDetails
                    var helperColbIDs = this.GetPersonHelperCollaborationDetails(personID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                    ConditionWithHelperQuestionIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                    ConditionWithHelperCollaborationIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND C.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";
                }
                var query = string.Empty;
                var assessmentreason = this.AssessmentReasonRepository.GetAllAssessmentReason();
                var initialReasonID = assessmentreason.Where(x => x.Name == PCISEnum.AssessmentReason.Initial).ToList()[0].AssessmentReasonID;
                var dischargeReasonID = assessmentreason.Where(x => x.Name == PCISEnum.AssessmentReason.Discharge).ToList()[0].AssessmentReasonID;
                var assessmentStatus = this.AssessmentStatusRepository.GetAllAssessmentStatus();
                var submittedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).ToList()[0].AssessmentStatusID;
                var approvedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).ToList()[0].AssessmentStatusID;
                var returnedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).ToList()[0].AssessmentStatusID;
                query = $@"SELECT * INTO #SelectedAssessments FROM
			                (
			                	 SELECT
			                	 	a.AssessmentID,a.DateTaken,a.PersonQuestionnaireID,PQ.QuestionnaireID,pq.PersonID
			                	 FROM
			                	 Assessment a	
			                	 JOIN PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = a.PersonQuestionnaireID AND PQ.IsRemoved = 0
				            	 AND PQ.PersonID =  {personID} {ConditionWithSharedQuestionIDs}	{ConditionWithHelperQuestionIDs}			 	
				            	 WHERE a.IsRemoved = 0 AND a.AssessmentStatusID in ({returnedStatus},{submittedStatus},{approvedStatus})
				            )AS D
                            SELECT * INTO #WindowOffsets FROM
                            (
				            	 SELECT DISTINCT sa.PersonID,sa.PersonQuestionnaireID,
				            	    q.QuestionnaireID,qw.AssessmentReasonID [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
				            	 FROM #SelectedAssessments Sa
				            	 JOIN Questionnaire q ON q.QuestionnaireID= sa.QuestionnaireID 
				            	 LEFT JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
				            	 WHERE (qw.AssessmentReasonID IN ({initialReasonID},{dischargeReasonID}) OR qw.AssessmentReasonID IS NULL) 
                            )AS A
                            SELECT * INTO #QuestionWindowOffsets FROM
                            (
				            	 SELECT wo_init.PersonID,wo_init.QuestionnaireID,wo_init.WindowOpenOffsetDays,wo_disc.WindowCloseOffsetDays,wo_init.PersonQuestionnaireID 
				            	 FROM (SELECT * FROM #WindowOffsets WHERE Reason= {initialReasonID}) wo_init 
				            	 JOIN (SELECT * FROM #WindowOffsets WHERE Reason= {dischargeReasonID})wo_disc ON wo_disc.QuestionnaireID=wo_init.QuestionnaireID
                                 UNION 
								 SELECT wo_null.PersonID,wo_null.QuestionnaireID,wo_null.WindowOpenOffsetDays,wo_null.WindowCloseOffsetDays,wo_null.PersonQuestionnaireID 
								 FROM #WindowOffsets wo_null WHERE wo_null.[Reason] IS NULL
                            )AS B   
                            SELECT * INTO #CollaborationWithOffset FROM
			                (
				            	 SELECT C.CollaborationID,C.Name AS CollaborationName,PC.EnrollDate AS EnrollDate,W.QuestionnaireID,
				            	           	         PC.EndDate AS EndDate, PC.PersonCollaborationID, PC.IsPrimary, PC.IsCurrent,
				            	 	  DATEADD(DAY,0-ISNULL(W.WindowOpenOffsetDays,0),CAST(PC.EnrollDate AS DATE)) AS CollaborationStartDate,
				            	 	  DATEADD(DAY,ISNULL(W.WindowCloseOffsetDays,0),CAST(ISNULL(PC.EndDate,getdate())  AS DATE)) AS CollaborationEndDate
				            	 FROM PersonCollaboration PC
				            	 JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
				            	 JOIN #QuestionWindowOffsets W ON W.PersonID = PC.PersonID
                                 WHERE PC.IsRemoved=0 AND PC.PersonID =  {personID} {ConditionWithSharedCollaborationIDs} {ConditionWithHelperCollaborationIDs}
				            )AS C
				            
				            SELECT DISTINCT CT.CollaborationID,CT.CollaborationName,
				            	  CT.EnrollDate,CT.EndDate, CT.PersonCollaborationID, CT.IsPrimary, CT.IsCurrent
				            FROM #CollaborationWithOffset CT
                            CROSS JOIN #SelectedAssessments SA  
				            WHERE (SA.QuestionnaireID = CT.QuestionnaireID) AND (CAST(SA.DateTaken AS DATE) BETWEEN	CT.CollaborationStartDate AND CT.CollaborationEndDate)";

                var peopleDetails = ExecuteSqlQuery(query, x => new PeopleCollaborationDTO
                {
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    CollaborationName = x["CollaborationName"] == DBNull.Value ? null : (string)x["CollaborationName"],
                    CollaborationStartDate = (DateTime)x["EnrollDate"],
                    CollaborationEndDate = x["EndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)x["EndDate"],
                    PersonCollaborationID = x["PersonCollaborationID"] == DBNull.Value ? 0 : (long)x["PersonCollaborationID"],
                    IsPrimary = x["IsPrimary"] == DBNull.Value ? false : (bool)x["IsPrimary"],
                    IsCurrent = x["IsCurrent"] == DBNull.Value ? false : (bool)x["IsCurrent"]
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// To update person details.
        /// </summary>
        /// <param name="peopleDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PeopleDTO UpdatePerson(PeopleDTO peopleDTO)
        {
            try
            {
                Person person = new Person();
                this.mapper.Map<PeopleDTO, Person>(peopleDTO, person);
                var result = this.UpdateAsync(person).Result;
                PeopleDTO updatedAddress = new PeopleDTO();
                this.mapper.Map<Person, PeopleDTO>(result, updatedAddress);
                return updatedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details Person.
        /// </summary>
        /// <param id="id">id.</param>
        /// <returns>.PeopleDTO</returns>
        public PeopleDTO GetPerson(Guid id)
        {
            try
            {
               //// PeopleDTO peopleDTO = new PeopleDTO();
               // Person person = this._dbContext.Person.Where(x => x.PersonIndex == id).FirstOrDefault();
                string query = $@"select *
                                from Person P
                           WHERE P.PersonIndex='{id}'";
                var peopleDTO = ExecuteSqlQuery(query, x => new PeopleDTO
                {
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    PersonIndex = id,
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"],
                    Suffix = x["Suffix"] == DBNull.Value ? null : (string)x["Suffix"],
                    PrimaryLanguageID = x["PrimaryLanguageID"] == DBNull.Value ? null : (int?)x["PrimaryLanguageID"],
                    PreferredLanguageID = x["PreferredLanguageID"] == DBNull.Value ? null : (int?)x["PreferredLanguageID"],
                    DateOfBirth = x["DateOfBirth"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateOfBirth"],
                    GenderID = x["GenderID"] == DBNull.Value ? 0 : (int)x["GenderID"],
                    SexualityID = x["SexualityID"] == DBNull.Value ? null : (int?)x["SexualityID"],
                    BiologicalSexID = x["BiologicalSexID"] == DBNull.Value ? null : (int?)x["BiologicalSexID"],
                    Email = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    Phone1Code = x["Phone1Code"] == DBNull.Value ? null : (string)x["Phone1Code"],
                    Phone2Code = x["Phone2Code"] == DBNull.Value ? null : (string)x["Phone2Code"],
                    Phone1 = x["Phone1"] == DBNull.Value ? null : (string)x["Phone1"],
                    Phone2 = x["Phone2"] == DBNull.Value ? null : (string)x["Phone2"],
                    IsActive = x["IsActive"] == DBNull.Value ? false : (bool)x["IsActive"],
                    PersonScreeningStatusID = x["PersonScreeningStatusID"] == DBNull.Value ? 0 : (int)x["PersonScreeningStatusID"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    UniversalID = x["UniversalID"] == DBNull.Value ? null : (string)x["UniversalID"],
                    TextPermission = x["TextPermission"] == DBNull.Value ? false : (bool)x["TextPermission"],
                    EmailPermission = x["EmailPermission"] == DBNull.Value ? false : (bool)x["EmailPermission"],
                    SMSConsentStoppedON = x["SMSConsentStoppedON"] == DBNull.Value ? null : (DateTime?)x["SMSConsentStoppedON"],

                }).FirstOrDefault();
                //this.mapper.Map<Person, PeopleDTO>(person, peopleDTO);
                return peopleDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// GetPersonByLanguageCount
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns>int</returns>
        public int GetPersonByLanguageCount(int languageID)
        {
            int count = (from row in this._dbContext.Person
                         where (row.PreferredLanguageID == languageID || row.PrimaryLanguageID == languageID) && !row.IsRemoved
                         select row).Count();

            return count;
        }

        /// <summary>
        /// GetRiskNotificationList.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <returns>RiskNotificationsListDTO List.</returns>
        public List<RiskNotificationsListDTO> GetRiskNotificationList(long personID)
        {
            try
            {
                var query = string.Empty;
                query = @"select NT.[Name] NotificationType, N.NotificationDate, NS.[Name] as [Status], N.PersonID, N.IsRemoved, N.NotificationLogID, N.NotificationTypeID, NOR.NotifyRiskID, NOR.QuestionnaireNotifyRiskRuleID, NOR.AssessmentID, NOR.NotifyDate, NOR.CloseDate, NOR.IsRemoved as RiskIsRemoved, QN.[Name] as Details from NotificationLog N
                            left join info.NotificationType NT on NT.NotificationTypeID = N.NotificationTypeID
                            left join info.NotificationResolutionStatus NS on NS.NotificationResolutionStatusID = N.NotificationResolutionStatusID
                            left join NotifyRisk NOR on NOR.NotifyRiskID = N.FKeyValue
                            left join QuestionnaireNotifyRiskRule QN on QN.QuestionnaireNotifyRiskRuleID = NOR.QuestionnaireNotifyRiskRuleID
                           
                            Where N.PersonID = " + personID + "  and NT.[Name] = 'Alert' and NS.[Name] = 'Unresolved' and N.IsRemoved = 0";

                var riskNotifications = ExecuteSqlQuery(query, x => new RiskNotificationsListDTO
                {
                    NotificationType = x[0] == DBNull.Value ? null : (string)x[0],
                    NotificationDate = (DateTime)x[1],
                    Status = x[2] == DBNull.Value ? null : (string)x[2],
                    PersonID = x[3] == DBNull.Value ? 0 : (long)x[3],
                    IsRemoved = x[4] == DBNull.Value ? false : (bool)x[4],
                    NotificationLogID = x[5] == DBNull.Value ? 0 : (int)x[5],
                    NotificationTypeID = x[6] == DBNull.Value ? 0 : (int)x[6],
                    NotifyRiskID = x[7] == DBNull.Value ? 0 : (int)x[7],
                    QuestionnaireNotifyRiskRuleID = x[8] == DBNull.Value ? 0 : (int)x[8],
                    AssessmentID = x[9] == DBNull.Value ? 0 : (int)x[9],
                    NotifyDate = x[10] == DBNull.Value ? (DateTime?)null : (DateTime)x[10],
                    CloseDate = x[11] == DBNull.Value ? (DateTime?)null : (DateTime)x[11],
                    RiskIsRemoved = x[12] == DBNull.Value ? false : (bool)x[12],
                    Details = x[13] == DBNull.Value ? null : (string)x[13]
                });

                return riskNotifications;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetReminderNotificationList.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <returns>ReminderNotificationsListDTO List.</returns>
        public List<ReminderNotificationsListDTO> GetReminderNotificationList(long personID)
        {
            try
            {
                var query = string.Empty;
                query = @"select NT.[Name] NotificationType, N.NotificationDate, NS.[Name] as [Status],N.PersonID, N.IsRemoved, N.NotificationLogID, N.NotificationTypeID, NOR.NotifyReminderID, NOR.NotifyDate, NOR.PersonQuestionnaireScheduleID, NOR.QuestionnaireReminderRuleID from NotificationLog N
	                        left join info.NotificationType NT on NT.NotificationTypeID = N.NotificationTypeID
	                        left join info.NotificationResolutionStatus NS on NS.NotificationResolutionStatusID = N.NotificationResolutionStatusID
	                        left join NotifyReminder NOR on NOR.NotifyReminderID = N.FKeyValue
	                        Where N.PersonID = " + personID + "  and NT.[Name] = 'Reminder' and NS.[Name] = 'Unresolved' and N.IsRemoved = 0";

                var reminderNotifications = ExecuteSqlQuery(query, x => new ReminderNotificationsListDTO
                {
                    NotificationType = x[0] == DBNull.Value ? null : (string)x[0],
                    NotificationDate = (DateTime)x[1],
                    Status = x[2] == DBNull.Value ? null : (string)x[2],
                    PersonID = x[3] == DBNull.Value ? 0 : (long)x[3],
                    IsRemoved = x[4] == DBNull.Value ? false : (bool)x[4],
                    NotificationLogID = x[5] == DBNull.Value ? 0 : (int)x[5],
                    NotificationTypeID = x[6] == DBNull.Value ? 0 : (int)x[6],
                    NotifyReminderID = x[7] == DBNull.Value ? 0 : (int)x[7],
                    NotifyDate = (DateTime)x[8],
                    PersonQuestionnaireScheduleID = x[9] == DBNull.Value ? 0 : (long)x[9],
                    QuestionnaireReminderRuleID = x[10] == DBNull.Value ? 0 : (int)x[10]
                });

                return reminderNotifications;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <returns>PastNotificationsListDTO List.</returns>
        public List<PastNotificationsListDTO> GetPastNotificationList(long personID)
        {
            try
            {
                var query = string.Empty;
                query = @"select NT.[Name] NotificationType, N.NotificationDate, NS.[Name] as [Status],N.PersonID, N.IsRemoved, N.NotificationLogID, N.NotificationTypeID, N.FKeyValue, N.NotificationResolutionStatusID 
                            from NotificationLog N
	                        left join info.NotificationType NT on NT.NotificationTypeID = N.NotificationTypeID
	                        left join info.NotificationResolutionStatus NS on NS.NotificationResolutionStatusID = N.NotificationResolutionStatusID
	                        Where N.PersonID = " + personID + " and NS.Name = 'Resolved'";

                var pastNotificationsListDTO = ExecuteSqlQuery(query, x => new PastNotificationsListDTO
                {
                    NotificationType = x[0] == DBNull.Value ? null : (string)x[0],
                    NotificationDate = (DateTime)x[1],
                    Status = x[2] == DBNull.Value ? null : (string)x[2],
                    PersonID = x[3] == DBNull.Value ? 0 : (long)x[3],
                    IsRemoved = x[4] == DBNull.Value ? false : (bool)x[4],
                    NotificationLogID = x[5] == DBNull.Value ? 0 : (int)x[5],
                    NotificationTypeID = x[6] == DBNull.Value ? 0 : (int)x[6],
                    FKeyValue = x[7] == DBNull.Value ? 0 : (int)x[7],
                    NotificationResolutionStatusID = x[8] == DBNull.Value ? 0 : (int)x[8]
                });

                return pastNotificationsListDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllNotes.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PastNotesResponseDTO.</returns>
        public List<NotificationNotesDTO> GetAllPastNotes(int notificationLogID, int pageNumber, int pageSize)
        {
            try
            {
                var query = string.Empty;
                query = @$"select NR.NotificationLogID, NR.NotificationResolutionNoteID, NR.NotificationResolutionHistoryID, NR.Note_NoteID as NoteID, N.NoteText, N.UpdateDate, H.HelperID, N.UpdateUserID, CASE WHEN H.HelperID IS null THEN U.[Name] ELSE H.FirstName + ' ' + H.LastName END as [User], H.HelperTitleID, HT.[Name] as HelperTitle, H.FirstName + ' ' + H.LastName as Helpername, '{PCISEnum.Notes.Added}' NoteType from NotificationResolutionNote NR
                            inner join Note N on N.NoteID = NR.Note_NoteID and N.IsRemoved = 0							
							left join Helper H on H.UserID = N.UpdateUserID
							left join info.HelperTitle HT on HT.HelperTitleID = H.HelperTitleID
							left join [User] U on U.UserID = H.UserID
                            where NR.NotificationLogID ={notificationLogID}   and N.IsRemoved = 0          
                            union   
                           SELECT * FROM(
                           select Top 1 NL.NotificationLogID, null NotificationResolutionNoteID, null NotificationResolutionHistoryID,N.NoteID as NoteID, N.NoteText, N.UpdateDate,H.HelperID, N.UpdateUserID,CASE WHEN H.HelperID IS null THEN U.[Name] ELSE H.FirstName + ' ' + H.LastName END as [User],H.HelperTitleID,	HT.[Name] as HelperTitle,          	H.FirstName + ' ' + H.LastName as Helpername , case when NT.Name='{PCISEnum.AssessmentNotificationType.Approve}' then '{PCISEnum.Notes.Approved}' else '{PCISEnum.Notes.Returned}' end as NoteType   from Note N
                                left join [dbo].[AssessmentNote] AN on AN.NoteID = N.NoteID 
                                left join [dbo].[NotificationLog] NL on AN.AssessmentID = NL.FKeyValue  or AN.AssessmentNoteID=NL.AssessmentNoteID
								left join [info].[NotificationType] NT on NT.NotificationTypeID=NL.NotificationTypeID
                                left join Helper H on H.UserID = N.UpdateUserID
                                left join info.HelperTitle HT on HT.HelperTitleID = H.HelperTitleID
                                left join[User] U on U.UserID = H.UserID
                            where N.IsRemoved = 0  and AN.AssessmentID is not null  AND ISNULL(N.NoteText, '') <> ''  and NL.NotificationLogID = { notificationLogID }  and
							(NT.Name='{PCISEnum.AssessmentNotificationType.Approve}' OR NT.Name='{PCISEnum.AssessmentNotificationType.Reject}' )
                            and N.UpdateDate <= NL.UpdateDate order by N.UpdateDate desc)A
                            union                            
                             select NL.NotificationLogID, null NotificationResolutionNoteID, null NotificationResolutionHistoryID,
			null as NoteID, A.ReasoningText NoteText,
			A.UpdateDate,H.HelperID, A.UpdateUserID,CASE WHEN H.HelperID IS null THEN U.[Name] ELSE H.FirstName + ' ' + H.LastName END as [User],H.HelperTitleID,	HT.[Name] as HelperTitle,         
			H.FirstName + ' ' + H.LastName as Helpername ,
			'{PCISEnum.Notes.Reason}'   as NoteType
			from   [dbo].[Assessment] A  
                left join [dbo].[NotificationLog] NL on A.AssessmentID = NL.FKeyValue
                left join Helper H on H.UserID = CASE WHEN ISNULL(A.NoteUpdateUserID,0) = 0 THEN A.UpdateUserID ELSE A.NoteUpdateUserID END
                left join info.HelperTitle HT on HT.HelperTitleID = H.HelperTitleID
                left join[User] U on U.UserID = H.UserID
            where A.IsRemoved = 0 AND ISNULL(A.ReasoningText, '') <> '' and NL.NotificationLogID =  { notificationLogID } 
                        union                            
                             select NL.NotificationLogID, null NotificationResolutionNoteID, null NotificationResolutionHistoryID,
			null as NoteID, A.EventNotes NoteText,
			A.UpdateDate,H.HelperID, A.UpdateUserID,CASE WHEN H.HelperID IS null THEN U.[Name] ELSE H.FirstName + ' ' + H.LastName END as [User],H.HelperTitleID,	HT.[Name] as HelperTitle,         
			H.FirstName + ' ' + H.LastName as Helpername ,
			'{PCISEnum.Notes.Trigger}'   as NoteType
			from   [dbo].[Assessment] A  
                left join [dbo].[NotificationLog] NL on A.AssessmentID = NL.FKeyValue
                left join Helper H on H.UserID = ISNULL(A.EventNoteUpdatedBy, A.UpdateUserID)
                left join info.HelperTitle HT on HT.HelperTitleID = H.HelperTitleID
                left join[User] U on U.UserID = H.UserID
            where A.IsRemoved = 0 AND ISNULL(A.EventNotes, '') <> '' and NL.NotificationLogID =  { notificationLogID } 
                        order by   NoteID DESC  ";

                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
                var pastNotes = ExecuteSqlQuery(query, x => new NotificationNotesDTO
                {
                    NotificationLogID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    NotificationResolutionNoteID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    NotificationResolutionHistoryID = x[2] == DBNull.Value ? 0 : (int)x[2],
                    NoteID = x[3] == DBNull.Value ? 0 : (int)x[3],
                    NoteText = x[4] == DBNull.Value ? null : (string)x[4],
                    UpdateDate = (DateTime)x[5],
                    HelperID = x[6] == DBNull.Value ? 0 : (int)x[6],
                    UpdateUserID = x[7] == DBNull.Value ? 0 : (int)x[7],
                    User = x[8] == DBNull.Value ? null : (string)x[8],
                    HelperTitleID = x[9] == DBNull.Value ? 0 : (int)x[9],
                    HelperTitle = x[10] == DBNull.Value ? null : (string)x[10],
                    HelperName = x[11] == DBNull.Value ? null : (string)x[11],
                    NoteType = x["NoteType"] == DBNull.Value ? null : (string)x["NoteType"],
                });

                return pastNotes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonsListByHelperID.
        /// Any change in person related query should be done in GetPersonsListByHelperIDCount too.
        /// Updated function to include PCIS-2576
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns></returns>
        public List<PersonDTO> GetPersonsListByHelperID(PersonSearchDTO personSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                List<PersonDTO> personDTO = new List<PersonDTO>();
                var query = string.Empty; var sharedPersonQuery = string.Empty;
                string activeFilter = string.Empty;

                if (personSearchDTO.activeFilter == PCISEnum.ActiveFilter.Active)
                {
                    activeFilter = @$" AND p.IsActive = 1 ";
                }
                else if (personSearchDTO.activeFilter == PCISEnum.ActiveFilter.Inactive)
                {
                    activeFilter = @$" AND p.IsActive = 0 ";
                }

                sharedPersonQuery = this.GetSharedPersonIDs(personSearchDTO.agencyID, personSearchDTO.role);
                sharedPersonQuery = string.IsNullOrEmpty(sharedPersonQuery) ? "" : $@"UNION ALL 
                                 SELECT 
		                         p.FirstName+ COALESCE(CASE p.MiddleName WHEN '' THEN '' ELSE ' '+p.MiddleName END, ' '+p.MiddleName, '') 
								 + COALESCE(CASE p.LastName WHEN '' THEN '' ELSE ' '+p.LastName END, ' '+p.LastName, '') [Name], 
								 p.PersonID, p.PersonIndex ,p.StartDate, p.EndDate, p.IsRemoved,
                                 1 AS IsShared, {personSearchDTO.agencyID.ToString()} AS ReceivingAgencyId, p.AgencyID AS ServingAgencyId, p.IsActive , A.Name as Agency
                                FROM Person p JOIN Agency A ON p.AgencyID = A.AgencyID 
                                WHERE p.PersonID IN ({sharedPersonQuery}) {activeFilter} ";
                string helperColbQueryCondition = string.Empty;
                var helpermetricsQuery = string.Empty;
                if (personSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    var personIdList = this.helperRepository.GetHelperPersonInCollaborationDetails(personSearchDTO.userID, personSearchDTO.agencyID);
                    if (personIdList.Count > 0)
                    {
                        var personIDs = string.Join(",", personIdList.ToArray());

                        helperColbQueryCondition = $@"AND P.PersonID NOT IN ({personIDs})";
                        var assessmentsInColbratn = this.GetHelperAllMetricsInCollaboration(personIDs, personSearchDTO.agencyID, personSearchDTO.userID);
                        var assessmentMetricsIDS = assessmentsInColbratn.Count > 0 ? string.Join(",", assessmentsInColbratn.ToArray()) : "0";
                        helpermetricsQuery = @$"UNION 
                                                SELECT p.PersonID,		                            
				                                pc.[Days],
		                                        MAX(pc.CollaborationID)[CollaborationID],
		                                        MAX(pc.EnrollDate)[EnrollDate],
                                                MAX(pc.EndDate)[EndDate]
		                                        ,SUM(NeedsEver) NeedsEver
		                                        ,SUM(NeedsAddressing) NeedsAddressing
		                                        ,SUM(StrengthsEver) StrengthEver
		                                        ,SUM(StrengthsBuilding) StrengthBuilding,1 AS PersonMetrics
                                            FROM
	                                        #PersonList p 
	                                        LEFT JOIN PersonAssessmentMetrics pm ON pm.[PersonID]= p.PersonID
                                            JOIN #PersonswithPrimaryCollaboration pc ON pc.PersonID=p.PersonID 
                                            WHERE 1 = 1 AND pm.PersonAssessmentMetricsID IN ({assessmentMetricsIDS})
                                            GROUP BY p.PersonID,Pc.Days,pc.rownum Having  rownum = 1 ";
                    }
                }
                if (personSearchDTO.isSameAsLoggedInUser)
                {
                    if (personSearchDTO.role == PCISEnum.Roles.Supervisor || personSearchDTO.role == PCISEnum.Roles.HelperRO || personSearchDTO.role == PCISEnum.Roles.HelperRW || personSearchDTO.role == PCISEnum.Roles.Assessor)
                    {
                        activeFilter = $@" AND p.IsActive = 1";
                    }
                    query = $@"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 
		                         P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
								 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [Name], 
								 P.PersonID, P.PersonIndex ,p.StartDate, p.EndDate,p.IsRemoved,
                                 0 AS IsShared, P.AgencyID AS ReceivingAgencyId, P.AgencyID AS ServingAgencyId, p.IsActive, '' as Agency
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 {activeFilter} AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                                {sharedPersonQuery}
                            )AS A
                            SELECT * INTO #PersonswithPrimaryCollaboration FROM(
							    select ROW_NUMBER() OVER (PARTITION BY p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
							    p.PersonID,	pc.[CollaborationID],c.Name,	pc.EnrollDate	,pc.EndDate	,	
						        CASE WHEN ISNULL(CAST(pc.EndDate AS DATE),CAST(GETDATE() AS DATE))>CAST(GETDATE() AS DATE)
                                                   THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                        ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							     FROM  #PersonList P 
							    		LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0 
							    		LEFT JOIn Collaboration C ON C.collaborationID = pc.collaborationID								
							)AS B  
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics From
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber,
                                    pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								join #PersonList P on p.PersonID=pqm.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS I
                            SELECT * INTO #PersonMetrics FROM
                            (
	                            SELECT
		                            p.PersonID,		                            
				                    pc.[Days],
		                            MAX(pc.CollaborationID) [CollaborationID],
		                            MAX(pc.EnrollDate) [EnrollDate],
                                    MAX(pc.EndDate) [EndDate]
		                            ,SUM(NeedsEver) NeedsEver
		                            ,SUM(NeedsAddressing) NeedsAddressing
		                            ,SUM(StrengthsEver) StrengthEver
		                            ,SUM(StrengthsBuilding) StrengthBuilding,0 AS PersonMetrics
	                            FROM 
	                            #PersonList p 
	                            LEFT JOIN PersonQuestionnaireMetrics pm ON pm.[PersonID]=p.PersonID
                                JOIN  #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
	                            JOIN #PersonswithPrimaryCollaboration pc ON pc.PersonID=p.PersonID 
                                WHERE 1= 1 {helperColbQueryCondition}
	                            GROUP BY p.PersonID,Pc.Days,pc.rownum HAving  rownum =1 
                                {helpermetricsQuery}
                            )AS C
                            SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            H.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.HelperID={personSearchDTO.helperID.ToString()}) --FOR HELPERs only
                            )AS D
                            SELECT * INTO #PersonHelped FROM
                            (
	                            SELECT   
		                            DISTINCT p.PersonID
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
								AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN #HelperList h ON h.HelperID=ph.helperID
	                            WHERE ph.IsRemoved=0
                            )AS E
                            SELECT * INTO #PersonLead FROM
                            (
	                            SELECT   
		                            p.PersonID,
		                            h.FirstName+ COALESCE(CASE h.MiddleName WHEN '' THEN '' ELSE ' '+h.MiddleName END, ' '+h.MiddleName, '') 
									+ COALESCE(CASE h.LastName WHEN '' THEN '' ELSE ' '+h.LastName END, ' '+h.LastName, '') as [Lead]
	                            FROM 
	                            #PersonHelped p 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID AND ph.IsLead=1 AND ph.IsRemoved=0
	                            AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                                AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN Helper h ON h.HelperID=ph.HelperID AND h.IsRemoved=0
                            )AS F
                            SELECT * INTO #PersonAssessed FROM
                            (
	                            SELECT
		                            p.PersonID, COUNT(A.AssessmentID) [Assessed] FROM
	                            #PersonHelped p 
								left JOIn #PersonMetrics PM on pm.PersonID =p.PersonID
								JOIN PersonQuestionnaire PQ ON Pq.PersonID = pm.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
	                            where CAST(PM.EnrollDate AS DATE) <= CAST(A.DateTaken AS date)
								 AND (PM.EndDate is NULL OR CAST(PM.EndDate AS DATE) >= CAST(A.DateTaken AS date)) 
								 Group By p.PersonID
                            )AS G
                            SELECT
	                            COUNT(P.PersonID) OVER() AS TotalCount,
	                            P.[Name], 
	                            P.PersonID, P.PersonIndex,p.IsRemoved,
	                            pl.[Lead], 
	                            pm.EnrollDate [StartDate],
                                CASE p.IsActive
									WHEN 0 THEN (SELECT MAX(EndDate) from
                                                PersonCollaboration PC 
                                                WHERE PC.IsRemoved = 0 AND PC.PersonID = p.PersonId)
									WHEN 1 THEN pm.[EndDate] END AS EndDate,
	                            c.Name [Collaboration],
	                            pm.CollaborationID,
	                            [Days],
	                           ISNULL(pa.Assessed,0) AS Assessed,
	                            NeedsEver,NeedsAddressing,StrengthEver,StrengthBuilding,p.StartDate as PersonStartDate,p.EndDate as PersonEndDate,
                                IsShared,ReceivingAgencyId,ServingAgencyId, p.IsActive, p.Agency
                            FROM 
                            #PersonList p
                            JOIN 
                            #PersonHelped PersonToList ON p.PersonID=PersonToList.PersonID
                            LEFT JOIN #PersonMetrics pm ON pm.PersonID=p.PersonID
                            LEFT JOIN #PersonLead pl ON pl.PersonID=p.PersonID
                            LEFT JOIN #PersonAssessed pa ON pa.PersonID=p.PersonID
                            LEFT JOIN Collaboration c ON c.CollaborationID=pm.CollaborationID AND c.IsRemoved=0 
                            WHERE 1=1 ";
                }
                else
                {
                    if (personSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = @$"SELECT * INTO #PersonList FROM 
                            (
	                            SELECT 
		                         P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
								 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [Name], 
								 P.PersonID, P.PersonIndex ,p.StartDate, p.EndDate, p.IsRemoved,
                                 0 AS IsShared,P.AgencyID AS ReceivingAgencyId, P.AgencyID AS ServingAgencyId, p.IsActive, '' as Agency
	                            FROM
	                            Person p 
	                            WHERE p.AgencyID={personSearchDTO.agencyID.ToString()} {activeFilter} {sharedPersonQuery}
                            ) AS A
                            SELECT * INTO #PersonswithPrimaryCollaboration FROM(
							select ROW_NUMBER() OVER (PARTITION BY p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
							p.PersonID,	pc.[CollaborationID],c.Name,pc.EnrollDate,pc.EndDate,	
						    CASE WHEN ISNULL(CAST(pc.EndDate AS DATE),CAST(GETDATE() AS DATE))>CAST(GETDATE() AS DATE)
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  #PersonList P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0 
									LEFT JOIn Collaboration C ON C.collaborationID = pc.collaborationID		
                            )AS B
                            SELECT * INTO #PersonMetrics FROM
                            (
	                            SELECT
		                            p.PersonID,
				                    pc.[Days],
		                            MAX(pc.CollaborationID) [CollaborationID],
		                            MAX(pc.EnrollDate) [EnrollDate],
                                    MAX(pc.EndDate) [EndDate]
		                            ,SUM(NeedsEver) NeedsEver
		                            ,SUM(NeedsAddressing) NeedsAddressing
		                            ,SUM(StrengthsEver) StrengthEver
		                            ,SUM(StrengthsBuilding) StrengthBuilding
	                            FROM 
	                            #PersonList p
	                            LEFT JOIN PersonQuestionnaireMetrics pm ON pm.[PersonID]=p.PersonID
	                            JOIN #PersonswithPrimaryCollaboration pc ON pc.PersonID=p.PersonID 
	                             GROUP BY p.PersonID,Pc.Days,pc.rownum HAving  rownum =1
                            )AS C
                            SELECT * INTO #PersonLead FROM
                            (
	                            SELECT   
		                            p.PersonID,
		                            h.FirstName+ COALESCE(CASE h.MiddleName WHEN '' THEN '' ELSE ' '+h.MiddleName END, ' '+h.MiddleName, '') 
									+ COALESCE(CASE h.LastName WHEN '' THEN '' ELSE ' '+h.LastName END, ' '+h.LastName, '') as [Lead]
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID AND ph.IsLead=1 AND ph.IsRemoved=0
	                            AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                                AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN Helper h ON h.HelperID=ph.HelperID AND h.IsRemoved=0
                            ) AS D
                            SELECT * INTO #PersonAssessed FROM 
                            (
	                             SELECT 
		                          pM.PersonID ,count(A.AssessmentID) as Assessed
	                            FROM 
	                            #PersonMetrics PM
								JOIN PersonQuestionnaire PQ ON Pq.PersonID = pm.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
	                            where CAST(PM.EnrollDate AS DATE) <= CAST(A.DateTaken AS date)
								 AND (PM.EndDate is NULL OR CAST(PM.EndDate AS DATE) >= CAST(A.DateTaken AS date)) 
								 AND PM.PersonID in(select PersonID from #PersonList)
								 Group By pM.PersonID	
                            )AS E
                            SELECT
	                            COUNT(P.PersonID) OVER() AS TotalCount,
	                            P.[Name], 
	                            P.PersonID, P.PersonIndex,p.IsRemoved,
	                            pl.[Lead], 
	                            pm.EnrollDate [StartDate],
                                CASE p.IsActive
									WHEN 0 THEN (SELECT MAX(EndDate) FROM
                                                PersonCollaboration PC 
                                                WHERE PC.IsRemoved = 0 AND PC.PersonID = p.PersonId)
									WHEN 1 THEN pm.[EndDate]
	                             END AS EndDate ,
	                            c.Name [Collaboration],
	                            pm.CollaborationID,
	                            CASE p.IsActive
									WHEN 0 THEN NULL
									WHEN 1 THEN [Days]
								END AS [Days],
								CASE p.IsActive
									WHEN 0 THEN NULL
									WHEN 1 THEN ISNULL(pa.Assessed,0)
	                             END AS Assessed,
	                            NeedsEver,NeedsAddressing,StrengthEver,StrengthBuilding,p.StartDate as PersonStartDate,p.EndDate as PersonEndDate,
                                IsShared,ReceivingAgencyId,ServingAgencyId, p.IsActive, p.Agency
                            FROM 
                            #PersonList p
                            LEFT JOIN #PersonMetrics pm ON pm.PersonID=p.PersonID
                            LEFT JOIN #PersonLead pl ON pl.PersonID=p.PersonID
                            LEFT JOIN #PersonAssessed pa ON pa.PersonID=p.PersonID
                            LEFT JOIN Collaboration c ON c.CollaborationID=pm.CollaborationID AND c.IsRemoved=0 
                            WHERE 1=1 ";
                    }
                    else if (personSearchDTO.role == PCISEnum.Roles.OrgAdminRO || personSearchDTO.role == PCISEnum.Roles.OrgAdminRW)
                    {
                        query = @$"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 
		                         P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
								 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [Name], 
								 P.PersonID, P.PersonIndex ,p.StartDate, p.EndDate, p.IsRemoved,
                                 0 AS IsShared, P.AgencyID AS ReceivingAgencyId, P.AgencyID AS ServingAgencyId, p.IsActive, '' as Agency
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 {activeFilter} AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                                {sharedPersonQuery}
                            ) AS A
                            SELECT * INTO #PersonswithPrimaryCollaboration FROM(
							select ROW_NUMBER() OVER (PARTITION BY p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
							p.PersonID,	pc.[CollaborationID],c.Name,pc.EnrollDate,pc.EndDate,	
						    CASE WHEN ISNULL(CAST(pc.EndDate AS DATE),CAST(GETDATE() AS DATE))>CAST(GETDATE() AS DATE)
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  #PersonList P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0 
									LEFT JOIn Collaboration C ON C.collaborationID = pc.collaborationID		
                            )AS B
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics From
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								join #PersonList P on p.PersonID=pqm.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS I
                            SELECT * INTO #PersonMetrics FROM
                            (
	                             SELECT
		                            p.PersonID,
				                    pc.[Days],
		                            MAX(pc.CollaborationID) [CollaborationID],
		                            MAX(pc.EnrollDate) [EnrollDate],
                                    MAX(pc.EndDate) [EndDate]
		                            ,SUM(NeedsEver) NeedsEver
		                            ,SUM(NeedsAddressing) NeedsAddressing
		                            ,SUM(StrengthsEver) StrengthEver
		                            ,SUM(StrengthsBuilding) StrengthBuilding,0 AS PersonMetrics
	                            FROM 
	                            #PersonList p
	                            LEFT JOIN PersonQuestionnaireMetrics pm ON pm.[PersonID]=p.PersonID
                                JOIN  #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
	                            JOIN #PersonswithPrimaryCollaboration pc ON pc.PersonID=p.PersonID 
                                WHERE 1= 1 {helperColbQueryCondition}
	                             GROUP BY p.PersonID,Pc.Days,pc.rownum HAving rownum =1 
                                {helpermetricsQuery}
                            )AS C
                            SELECT * INTO #PersonLead FROM
                            (
	                            SELECT   
		                            p.PersonID,
		                            h.FirstName+ COALESCE(CASE h.MiddleName WHEN '' THEN '' ELSE ' '+h.MiddleName END, ' '+h.MiddleName, '') 
									+ COALESCE(CASE h.LastName WHEN '' THEN '' ELSE ' '+h.LastName END, ' '+h.LastName, '') as [Lead]
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID AND ph.IsLead=1 AND ph.IsRemoved=0
	                            AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                                AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN Helper h ON h.HelperID=ph.HelperID AND h.IsRemoved=0
                            )AS D
                            SELECT * INTO #PersonAssessed FROM
                            (
	                            SELECT 
		                          pM.PersonID ,count(A.AssessmentID) as Assessed
	                            FROM 
	                            #PersonMetrics PM
								JOIN PersonQuestionnaire PQ ON Pq.PersonID = pm.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
	                            where CAST(PM.EnrollDate AS DATE) <= CAST(A.DateTaken AS date)
								 AND (PM.EndDate is NULL OR CAST(PM.EndDate AS DATE) >= CAST(A.DateTaken AS date))  
								 AND PM.PersonID in(select PersonID from #PersonList)
								 Group By pM.PersonID
                            )AS E
                            SELECT
	                            COUNT(P.PersonID) OVER() AS TotalCount,
	                            P.[Name], 
	                            P.PersonID, P.PersonIndex,p.IsRemoved,
	                            pl.[Lead], 
	                            pm.EnrollDate [StartDate],
                                CASE p.IsActive
									WHEN 0 THEN (SELECT MAX(EndDate) from
                                                PersonCollaboration PC 
                                                WHERE PC.IsRemoved = 0 AND PC.PersonID = p.PersonId)
									WHEN 1 THEN pm.[EndDate]
	                             END AS EndDate  ,
	                            c.Name [Collaboration],
	                            pm.CollaborationID,
	                            CASE p.IsActive
									WHEN 0 THEN NULL
									WHEN 1 THEN [Days]
								END AS [Days],
								CASE p.IsActive
									WHEN 0 THEN NULL
									WHEN 1 THEN ISNULL(pa.Assessed,0)
	                             END AS Assessed,
	                            NeedsEver,NeedsAddressing,StrengthEver,StrengthBuilding,p.StartDate as PersonStartDate,p.EndDate as PersonEndDate,
                                IsShared,ReceivingAgencyId,ServingAgencyId, p.IsActive, p.Agency
                            FROM 
                            #PersonList p
                            LEFT JOIN #PersonMetrics pm ON pm.PersonID=p.PersonID
                            LEFT JOIN #PersonLead pl ON pl.PersonID=p.PersonID
                            LEFT JOIN #PersonAssessed pa ON pa.PersonID=p.PersonID
                            LEFT JOIN Collaboration c ON c.CollaborationID=pm.CollaborationID AND c.IsRemoved=0 
                            WHERE 1=1 ";
                    }
                    else if (personSearchDTO.role == PCISEnum.Roles.Supervisor)
                    {
                        query = $@"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 
		                         P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
								 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [Name], 
								 P.PersonID, P.PersonIndex ,p.StartDate, p.EndDate, p.IsRemoved,
                                 0 AS IsShared, P.AgencyID AS ReceivingAgencyId, P.AgencyID AS ServingAgencyId, p.IsActive, '' as Agency
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 AND p.IsActive = 1 AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                                {sharedPersonQuery}
                            )AS A
							SELECT * INTO #PersonswithPrimaryCollaboration FROM(
							    SELECT ROW_NUMBER() OVER (PARTITION BY p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
							    p.PersonID,	pc.[CollaborationID],c.Name,	pc.EnrollDate	,pc.EndDate	,	
						        CASE WHEN ISNULL(CAST(pc.EndDate AS DATE),CAST(GETDATE() AS DATE))>CAST(GETDATE() AS DATE)
                                                   THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                        ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							     FROM #PersonList P 
							    		LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0 
							    		LEFT JOIn Collaboration C ON C.collaborationID = pc.collaborationID								
							)AS B    
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics From
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								join #PersonList P on p.PersonID=pqm.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS I
                            SELECT * INTO #PersonMetrics FROM
                            (
	                            SELECT
		                            p.PersonID,		                            
				                    pc.[Days],
		                            MAX(pc.CollaborationID) [CollaborationID],
		                            MAX(pc.EnrollDate) [EnrollDate],
                                    MAX(pc.EndDate) [EndDate]
		                            ,SUM(NeedsEver) NeedsEver
		                            ,SUM(NeedsAddressing) NeedsAddressing
		                            ,SUM(StrengthsEver) StrengthEver
		                            ,SUM(StrengthsBuilding) StrengthBuilding,0 AS PersonMetrics
	                            FROM 
	                            #PersonList p 
	                            LEFT JOIN PersonQuestionnaireMetrics pm ON pm.[PersonID]=p.PersonID
                                JOIN  #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
	                            JOIN #PersonswithPrimaryCollaboration pc ON pc.PersonID=p.PersonID
                                WHERE 1= 1 {helperColbQueryCondition}
	                            GROUP BY p.PersonID,Pc.Days,pc.rownum HAving  rownum =1 
                                {helpermetricsQuery}
                            )AS C
                            ;WITH  SupervisorHierarchy AS
                            (
	                            SELECT
		                            H.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.HelperID={personSearchDTO.helperID.ToString()} OR h.SupervisorHelperID={personSearchDTO.helperID.ToString()})  --FOR SUPERVISOR
                                UNION ALL
							    SELECT
								    H1.HelperID
							    FROM Helper H1 
								INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID
								AND HL.HelperID <> {personSearchDTO.helperID.ToString()}
							    WHERE H1.IsRemoved=0 AND H1.AgencyID={personSearchDTO.agencyID.ToString()} --FOR SUPERVISORHierarchy
                            )
                            SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            H.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.ReviewerID={personSearchDTO.helperID.ToString()})  --FOR Reviewers
                                UNION
                                SELECT * FROM SupervisorHierarchy
                            )AS E
                            SELECT * INTO #PersonHelped FROM
                            (
	                            SELECT   
		                            DISTINCT p.PersonID
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
								AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN #HelperList h ON h.HelperID=ph.helperID
	                            WHERE ph.IsRemoved=0
                            )AS F
                            SELECT * INTO #PersonLead FROM
                            (
	                            SELECT   
		                            p.PersonID,
		                            h.FirstName+ COALESCE(CASE h.MiddleName WHEN '' THEN '' ELSE ' '+h.MiddleName END, ' '+h.MiddleName, '') 
									+ COALESCE(CASE h.LastName WHEN '' THEN '' ELSE ' '+h.LastName END, ' '+h.LastName, '') as [Lead]
	                            FROM 
	                            #PersonHelped p 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID AND ph.IsLead=1 AND ph.IsRemoved=0
	                            AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                                AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN Helper h ON h.HelperID=ph.HelperID AND h.IsRemoved=0
                            )AS G
                            SELECT * INTO #PersonAssessed FROM
                            (
	                            SELECT
		                            p.PersonID, COUNT(A.AssessmentID) [Assessed]
	                            FROM 
	                            #PersonHelped p 
								left JOIN #PersonMetrics PM on pm.PersonID =p.PersonID
								JOIN PersonQuestionnaire PQ ON Pq.PersonID = pm.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
	                            where CAST(PM.EnrollDate AS DATE) <= CAST(A.DateTaken AS date)
								 AND (PM.EndDate is NULL OR CAST(PM.EndDate AS DATE) >= CAST(A.DateTaken AS date)) 
								 Group By p.PersonID
                            )AS H
                            SELECT
	                            COUNT(P.PersonID) OVER() AS TotalCount,
	                            P.[Name], 
	                            P.PersonID, P.PersonIndex,p.IsRemoved,
	                            pl.[Lead], 
	                            pm.EnrollDate [StartDate],
                                pm.[EndDate],
	                            c.Name [Collaboration],
	                            pm.CollaborationID,
	                            [Days],
	                            ISNULL(pa.Assessed,0) AS Assessed,
	                            NeedsEver,NeedsAddressing,StrengthEver,StrengthBuilding,p.StartDate as PersonStartDate,p.EndDate as PersonEndDate,
                                IsShared,ReceivingAgencyId,ServingAgencyId, p.IsActive, p.Agency
                            FROM 
                            #PersonList p
                            JOIN 
                            #PersonHelped PersonToList ON p.PersonID=PersonToList.PersonID
                            LEFT JOIN #PersonMetrics pm ON pm.PersonID=p.PersonID
                            LEFT JOIN #PersonLead pl ON pl.PersonID=p.PersonID
                            LEFT JOIN #PersonAssessed pa ON pa.PersonID=p.PersonID
                            LEFT JOIN Collaboration c ON c.CollaborationID=pm.CollaborationID AND c.IsRemoved=0 
                            WHERE 1=1";
                    }
                    else if (personSearchDTO.role == PCISEnum.Roles.HelperRO || personSearchDTO.role == PCISEnum.Roles.HelperRW || personSearchDTO.role == PCISEnum.Roles.Assessor)
                    {
                        query = $@"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 
		                         P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
								 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [Name], 
								 P.PersonID, P.PersonIndex ,p.StartDate, p.EndDate,p.IsRemoved,
                                 0 AS IsShared, P.AgencyID AS ReceivingAgencyId, P.AgencyID AS ServingAgencyId, p.IsActive, '' as Agency
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 AND p.IsActive = 1 AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                                {sharedPersonQuery}
                            )AS A
                            SELECT * INTO #PersonswithPrimaryCollaboration FROM(
							    select ROW_NUMBER() OVER (PARTITION BY p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
							    p.PersonID,	pc.[CollaborationID],c.Name,	pc.EnrollDate	,pc.EndDate	,	
						        CASE WHEN ISNULL(CAST(pc.EndDate AS DATE),CAST(GETDATE() AS DATE))>CAST(GETDATE() AS DATE)
                                                   THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                        ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							     FROM  #PersonList P 
							    		LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0 
							    		LEFT JOIn Collaboration C ON C.collaborationID = pc.collaborationID								
							)AS B    
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics From
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								join #PersonList P on p.PersonID=pqm.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS I
                            SELECT * INTO #PersonMetrics FROM
                            (
	                            SELECT
		                            p.PersonID,		                            
				                    pc.[Days],
		                            MAX(pc.CollaborationID) [CollaborationID],
		                            MAX(pc.EnrollDate) [EnrollDate],
                                    MAX(pc.EndDate) [EndDate]
		                            ,SUM(NeedsEver) NeedsEver
		                            ,SUM(NeedsAddressing) NeedsAddressing
		                            ,SUM(StrengthsEver) StrengthEver
		                            ,SUM(StrengthsBuilding) StrengthBuilding, 0 As PersonMetrics
	                            FROM 
	                            #PersonList p 
	                            LEFT JOIN PersonQuestionnaireMetrics pm ON pm.[PersonID]=p.PersonID
                                JOIN  #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
	                            JOIN #PersonswithPrimaryCollaboration pc ON pc.PersonID=p.PersonID 
                                WHERE 1= 1 {helperColbQueryCondition}
	                            GROUP BY p.PersonID,Pc.Days,pc.rownum HAving  rownum =1 
                                {helpermetricsQuery}
                            )AS C
                            SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            H.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.HelperID={personSearchDTO.helperID.ToString()} OR h.ReviewerID={personSearchDTO.helperID.ToString()}) --FOR HELPER And ReviwerHelpers
                            )AS D
                            SELECT * INTO #PersonHelped FROM
                            (
	                            SELECT   
		                            DISTINCT p.PersonID
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
								AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN #HelperList h ON h.HelperID=ph.helperID
	                            WHERE ph.IsRemoved=0
                            )AS E
                            SELECT * INTO #PersonLead FROM
                            (
	                            SELECT   
		                            p.PersonID,
		                            h.FirstName+ COALESCE(CASE h.MiddleName WHEN '' THEN '' ELSE ' '+h.MiddleName END, ' '+h.MiddleName, '') 
									+ COALESCE(CASE h.LastName WHEN '' THEN '' ELSE ' '+h.LastName END, ' '+h.LastName, '') as [Lead]
	                            FROM 
	                            #PersonHelped p 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID AND ph.IsLead=1 AND ph.IsRemoved=0
	                            AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                                AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN Helper h ON h.HelperID=ph.HelperID AND h.IsRemoved=0
                            )AS F
                            SELECT * INTO #PersonAssessed FROM
                            (
	                            SELECT
		                            p.PersonID, COUNT(A.AssessmentID) [Assessed] FROM
	                            #PersonHelped p 
								left JOIn #PersonMetrics PM on pm.PersonID =p.PersonID
								JOIN PersonQuestionnaire PQ ON Pq.PersonID = pm.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
	                            where CAST(PM.EnrollDate AS DATE) <= CAST(A.DateTaken AS date)
								 AND (PM.EndDate is NULL OR CAST(PM.EndDate AS DATE) >= CAST(A.DateTaken AS date)) 
								 Group By p.PersonID
                            )AS G
                            SELECT
	                            COUNT(P.PersonID) OVER() AS TotalCount,
	                            P.[Name], 
	                            P.PersonID, P.PersonIndex,p.IsRemoved,
	                            pl.[Lead], 
	                            pm.EnrollDate [StartDate],
                                pm.[EndDate],
	                            c.Name [Collaboration],
	                            pm.CollaborationID,
	                            [Days],
	                           ISNULL(pa.Assessed,0) AS Assessed,
	                            NeedsEver,NeedsAddressing,StrengthEver,StrengthBuilding,p.StartDate as PersonStartDate,p.EndDate as PersonEndDate,
                                IsShared,ReceivingAgencyId,ServingAgencyId, p.IsActive, p.Agency
                            FROM 
                            #PersonList p
                            JOIN 
                            #PersonHelped PersonToList ON p.PersonID=PersonToList.PersonID
                            LEFT JOIN #PersonMetrics pm ON pm.PersonID=p.PersonID
                            LEFT JOIN #PersonLead pl ON pl.PersonID=p.PersonID
                            LEFT JOIN #PersonAssessed pa ON pa.PersonID=p.PersonID
                            LEFT JOIN Collaboration c ON c.CollaborationID=pm.CollaborationID AND c.IsRemoved=0 
                            WHERE 1=1 ";
                    }
                }

                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                personDTO = ExecuteSqlQuery(query, x => new PersonDTO
                {
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Lead = x["Lead"] == DBNull.Value ? null : (string)x["Lead"],
                    Collaboration = x["Collaboration"] == DBNull.Value ? null : (string)x["Collaboration"],
                    StartDate = x["StartDate"] == DBNull.Value ? null : (DateTime?)x["StartDate"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    Days = x["Days"] == DBNull.Value ? null : (int?)x["Days"],
                    Assessed = x["Assessed"] == DBNull.Value ? null : (int?)x["Assessed"],
                    NeedsEver = x["NeedsEver"] == DBNull.Value ? 0 : (int)x["NeedsEver"],
                    NeedsAddressing = x["NeedsAddressing"] == DBNull.Value ? 0 : (int)x["NeedsAddressing"],
                    StrengthEver = x["StrengthEver"] == DBNull.Value ? 0 : (int)x["StrengthEver"],
                    StrengthBuilding = x["StrengthBuilding"] == DBNull.Value ? 0 : (int)x["StrengthBuilding"],
                    PersonStartDate = x["PersonStartDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["PersonStartDate"],
                    PersonEndDate = x["PersonEndDate"] == DBNull.Value ? null : (DateTime?)x["PersonEndDate"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    IsShared = (int)x["IsShared"] == 0 ? false : true,
                    ReceivingAgencyId = x["ReceivingAgencyId"] == DBNull.Value ? 0 : (long)x["ReceivingAgencyId"],
                    ServingAgencyId = x["ServingAgencyId"] == DBNull.Value ? 0 : (long)x["ServingAgencyId"],
                    IsActive = x["IsActive"] == DBNull.Value ? false : (bool)x["IsActive"],
                    AgencyName = x["Agency"] == DBNull.Value ? null : (string)x["Agency"],
                }, queryBuilderDTO.QueryParameterDTO);
                return personDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonInitials.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>PersonInitialsDTO.</returns>
        public PersonInitialsDTO GetPersonInitials(Guid personIndex)
        {
            try
            {
                return (from P in this._dbContext.Person
                        where P.PersonIndex == personIndex
                        select new PersonInitialsDTO
                        {
                            PersonIndex = P.PersonIndex,
                            PersonInitials = string.IsNullOrWhiteSpace(P.MiddleName) ? string.Format("{0}{1}", P.FirstName.ToUpper().First(), P.LastName.ToUpper().First()) : string.Format("{0}{1}{2}", P.FirstName.ToUpper().First(), P.MiddleName.ToUpper().First(), P.LastName.ToUpper().First())
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonInitials.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>PersonInitialsDTO.</returns>
        public PersonInitialsDTO GetPersonInitialByPersonID(long personIID)
        {
            try
            {
                return (from P in this._dbContext.Person
                        where P.PersonID == personIID
                        select new PersonInitialsDTO
                        {
                            PersonIndex = P.PersonIndex,
                            PersonInitials = string.IsNullOrWhiteSpace(P.MiddleName) ? string.Format("{0}{1}", P.FirstName.ToUpper().First(), P.LastName.ToUpper().First()) : string.Format("{0}{1}{2}", P.FirstName.ToUpper().First(), P.MiddleName.ToUpper().First(), P.LastName.ToUpper().First())
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonDetails.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonDetailsResponseDTO.</returns>
        public PersonDetailsDTO GetPersonDetails(Guid personIndex)
        {
            try
            {
                try
                {                    
                    return (from P in this._dbContext.Person
                            where P.PersonIndex == personIndex
                            select new PersonDetailsDTO
                            {
                                PersonIndex = P.PersonIndex,
                                FirstName = P.FirstName,
                                MiddleName = string.IsNullOrWhiteSpace(P.MiddleName) ? string.Empty : P.MiddleName,
                                LastName = P.LastName,
                                DateOfBirth = P.DateOfBirth
                            }).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonsListByHelperIDCount.
        /// Any change in person related query GetPersonsListByHelperID should be done here too.
        /// Updated function to include PCIS-2576
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="helperID">helperID</param>
        /// <returns></returns>
        public Tuple<int, int> GetPersonsListByHelperIDCount(PersonSearchDTO personSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                List<PersonDTO> personDTO = new List<PersonDTO>();
                var query = string.Empty;
                var querylead = string.Empty;

                if (personSearchDTO.isSameAsLoggedInUser)
                {
                    var personActiveCondition = string.Empty;
                    if (personSearchDTO.role == PCISEnum.Roles.Supervisor || personSearchDTO.role == PCISEnum.Roles.HelperRO || personSearchDTO.role == PCISEnum.Roles.HelperRW || personSearchDTO.role == PCISEnum.Roles.Assessor)
                    {
                        personActiveCondition = $@"AND p.IsActive = 1";
                    }
                    query = $@"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 		                          
								 P.PersonID
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 {personActiveCondition} AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                            )AS A 
                            SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            h.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.HelperID={personSearchDTO.helperID.ToString()}) --FOR HELPER only
                            )AS D
                            SELECT * INTO #PersonHelped FROM
                            (
	                            SELECT   
		                            DISTINCT p.PersonID
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
								AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN #HelperList h ON h.HelperID=ph.helperID
	                            WHERE ph.IsRemoved=0
                            )AS E
                            SELECT
	                            COUNT(PersonID)  AS TotalCount
                            FROM 
                            #PersonHelped 
                            WHERE 1=1";
                }
                else
                {
                    if (personSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = @$"SELECT COUNT(P.PersonID) AS TotalCount 
	                            FROM
	                            Person p 
	                            WHERE p.AgencyID={personSearchDTO.agencyID.ToString()}";
                    }
                    else if (personSearchDTO.role == PCISEnum.Roles.OrgAdminRO || personSearchDTO.role == PCISEnum.Roles.OrgAdminRW)
                    {
                        query = @$"SELECT COUNT(P.PersonID) AS TotalCount 
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 AND p.AgencyID={personSearchDTO.agencyID.ToString()}";
                    }
                    else if (personSearchDTO.role == PCISEnum.Roles.Supervisor)
                    {
                        query = $@"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 		                          
								 P.PersonID
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                            )AS A	
                           ;WITH SupervisorHierarchy AS
                            (
	                            SELECT
		                            h.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.HelperID={personSearchDTO.helperID.ToString()} OR h.SupervisorHelperID={personSearchDTO.helperID.ToString()})  --FOR SUPERVISOR
                                UNION ALL
							    SELECT
								    H1.HelperID
							    FROM Helper H1 
								INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID
								AND HL.HelperID <> {personSearchDTO.helperID.ToString()}
							    WHERE H1.IsRemoved=0 AND H1.AgencyID={personSearchDTO.agencyID.ToString()} --FOR SUPERVISORHierarchy
                            )
                            SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            h.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.ReviewerID={personSearchDTO.helperID.ToString()})  --FOR Reviewers
                                UNION
                                SELECT * FROM SupervisorHierarchy
                            )AS E
                            SELECT * INTO #PersonHelped FROM
                            (
	                            SELECT   
		                            DISTINCT p.PersonID
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
								AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN #HelperList h ON h.HelperID=ph.helperID
	                            WHERE ph.IsRemoved=0
                            )AS F
                            SELECT
	                            COUNT(PersonID) AS TotalCount
                            FROM 
                            #PersonHelped
                            WHERE 1=1";
                    }
                    else if (personSearchDTO.role == PCISEnum.Roles.HelperRO || personSearchDTO.role == PCISEnum.Roles.HelperRW || personSearchDTO.role == PCISEnum.Roles.Assessor)
                    {
                        query = $@"SELECT * INTO #PersonList FROM
                            (
	                            SELECT 		                         
								 P.PersonID
	                            FROM
	                            Person p 
	                            WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={personSearchDTO.agencyID.ToString()}
                            )AS A 
                            SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            h.HelperID
	                            FROM
	                            Helper h WHERE h.AgencyID={personSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND (h.HelperID={personSearchDTO.helperID.ToString()} OR h.ReviewerID={personSearchDTO.helperID.ToString()}) --FOR HELPER And ReviwerHelpers
                            )AS D
                            SELECT * INTO #PersonHelped FROM
                            (
	                            SELECT   
		                            DISTINCT p.PersonID
	                            FROM 
	                            #PersonList P 
	                            JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
								AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN #HelperList h ON h.HelperID=ph.helperID
	                            WHERE ph.IsRemoved=0
                            )AS E
                            SELECT
	                            COUNT(PersonID)  AS TotalCount
                            FROM 
                            #PersonHelped
                            WHERE 1=1 ";
                    }
                }
                query += queryBuilderDTO.WhereCondition;
                var data = ExecuteSqlQuery(query, x => new PersonDTO
                {
                    TotalCount = (int)x[0],
                }, queryBuilderDTO.QueryParameterDTO);

                var datalead = new List<PersonDTO>();
                if (personSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    querylead = @$"SELECT COUNT(HelperID) as CountTotal
							    	From PersonHelper ph
								 left join Person p on p.PersonID=ph.PersonID  
								 where ph.HelperID={personSearchDTO.helperID.ToString()}  AND ph.isLead=1  AND ph.IsRemoved=0
								 AND p.IsActive=1 AND p.AgencyID={personSearchDTO.agencyID.ToString()} AND p.IsRemoved=0
								 AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
								 AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE) ";

                    querylead += queryBuilderDTO.WhereCondition;
                    datalead = ExecuteSqlQuery(querylead, x => new PersonDTO
                    {
                        TotalCount = (int)x[0],
                    }, queryBuilderDTO.QueryParameterDTO);
                }
                int PersonHelpingCount = data != null && data.Count > 0 ? data[0].TotalCount : 0;
                int LeadHelpingCount = datalead != null && datalead.Count > 0 ? datalead[0].TotalCount : 0;
                return Tuple.Create(PersonHelpingCount, LeadHelpingCount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region SharedPersonRelatedFunctions
        /// <summary>
        /// GetSharedPersonsQuery--Fetch Shared persons
        /// Any change should be done in GetPersonSharingDetails too.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <param name="role"></param>
        /// <param name="selectFields"></param>
        /// <returns></returns>
        private string GetSharedPersonsQuery(long agencyID, string role, string selectFields = "")
        {
            try
            {
                selectFields = string.IsNullOrEmpty(selectFields) ? $@"DISTINCT  P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
                             + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' ' + P.LastName END, ' ' + P.LastName, '')[Name], 
                             P.PersonID, P.PersonIndex ,p.StartDate, p.EndDate, p.IsRemoved,
							 CASE WHEN CS.AgencyID = AgS.AgencyID THEN 0 ELSE 1 END IsShared,
							 CS.AgencyID as ServingAgencyId,AgS.AgencyID as ReceivingAgencyId" : selectFields;
                string historicalConditionOnRole = string.Empty, mainQuery = string.Empty;
                historicalConditionOnRole = $@"HAVING (MAX(ISNULL(CAST(PC.EndDate AS DATE),CAST(GETDATE() AS DATE))) >= CAST(GETDATE() AS DATE))";
                if (role == PCISEnum.Roles.SuperAdmin || role == PCISEnum.Roles.OrgAdminRO || role == PCISEnum.Roles.OrgAdminRW)
                {
                    historicalConditionOnRole = $@"HAVING ((MAX(CAST(ISNULL(CS.HistoricalView,0) AS INT)) = 0 AND 
											 (MAX(ISNULL(CAST(PC.EndDate AS DATE),CAST(GETDATE() AS DATE))) >= CAST(GETDATE() AS DATE)))
											 OR ((MAX(CAST(ISNULL(CS.HistoricalView,0) AS INT)) = 1)))";
                }
                mainQuery = $@"SELECT {selectFields}
                             FROM Person p 
							 JOIN PersonCollaboration PC ON P.personId = PC.PersonID AND P.AgencyID <> {agencyID} AND PC.IsRemoved = 0
                             JOIN CollaborationSharing CS ON CS.CollaborationID = PC.CollaborationID AND CS.AgencyID <> {agencyID}
                             JOIN ReportingUnit RU ON CS.ReportingUnitID = RU.ReportingUnitID AND RU.IsRemoved = 0
                             JOIN AgencySharing AgS ON RU.ReportingUnitID = AgS.ReportingUnitID 
                                    AND AGS.AgencyID = {agencyID} --loggedInAgency
                             WHERE  RU.IsSharing = 1 AND CS.IsSharing = 1 
                                       AND CS.AgencyID <> AGS.AgencyId										 
                                       AND (SELECT A.IsSharing from AgencySharing A WHERE CS.AgencyID = A.AgencyID 
                             		   AND A.ReportingUnitID = RU.ReportingUnitID) = 1 --sharing true for serving agency
                             		   AND  ISNULL(CAST(RU.EndDate AS DATE),CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                                       AND P.IsRemoved = 0 AND P.IsActive = 1
                             		 Group by P.PersonIndex, P.PersonID,P.FirstName,P.MiddleName,P.LastName,P.StartDate,P.EndDate,p.IsRemoved,
                             		 CS.AgencyID,AgS.AgencyID  {historicalConditionOnRole} ";

                return mainQuery;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetSharedPersonCollaborationDetails - Query for Fetching shared collaborations and QuestionIDs
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        private List<SharedCollaborationDetailsDTO> GetSharedPersonCollaborationDetails(long personID, long loggedInAgencyID)
        {
            try
            {
                var query = $@"SELECT DISTINCT  PC.CollaborationID,CQ.QuestionnaireID,RU.ReportingUnitID,
							   PC.PersonCollaborationID,PC.PersonID
								FROM PersonCollaboration PC
									LEFT JOIN CollaborationSharing CS ON CS.CollaborationID = PC.CollaborationID
									LEFT JOIN CollaborationQuestionnaire cq on CQ.CollaborationID = CS.CollaborationID AND CQ.IsRemoved = 0
									LEFT JOIN ReportingUnit RU ON CS.ReportingUnitID = RU.ReportingUnitID
									LEFT JOIN AgencySharing AgS ON RU.ReportingUnitID = AgS.ReportingUnitID
									LEFT JOIN Person p ON P.personId = PC.PersonID 
									WHERE PC.IsRemoved = 0 AND RU.IsRemoved = 0 AND RU.IsSharing = 1								          
											  AND CS.IsSharing = 1  --AND AgS.IsSharing = 1 
									          AND AGS.AgencyID = {loggedInAgencyID} --loggedInAgency 
											  AND P.AgencyID <> {loggedInAgencyID} AND CS.AgencyID <> AGS.AgencyId AND P.PersonID = {personID}";

                var collaborations = ExecuteSqlQuery(query, x => new SharedCollaborationDetailsDTO
                {
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    PersonCollaborationID = x["PersonCollaborationID"] == DBNull.Value ? 0 : (long)x["PersonCollaborationID"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"]
                });
                return collaborations;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// IsSharedPerson-Check Person Shared or Not
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public bool IsSharedPerson(long personID, long loggedInAgencyID)
        {
            try
            {
                var isShared = false;
                var person = this.GetRowAsync(x => x.PersonID == personID).Result;
                if (person?.AgencyID != loggedInAgencyID)
                {
                    isShared = true;
                }
                return isShared;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetSharedPersonQuestionnaireDetails-Fetch QuestionIDs and CollaborationIDS of a shared person
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public SharedDetailsDTO GetSharedPersonQuestionnaireDetails(long personID, long loggedInAgencyID)
        {
            try
            {
                SharedDetailsDTO sharedIds = new SharedDetailsDTO();
                if (IsSharedPerson(personID, loggedInAgencyID) && personID != 0)
                {
                    List<SharedCollaborationDetailsDTO> sharedDetails = this.GetSharedPersonCollaborationDetails(personID, loggedInAgencyID);
                    List<int> sharedCollaborationIDs = sharedDetails.Select(x => x.CollaborationID).Distinct().ToList();
                    List<int> sharedQuestionIDs = sharedDetails.Select(x => x.QuestionnaireID).Distinct().ToList();
                    sharedIds.PersonID = personID;
                    sharedIds.SharedQuestionnaireIDs = sharedCollaborationIDs.Count == 0 ? "0" : string.Join(",", sharedQuestionIDs.ToArray());
                    sharedIds.SharedCollaborationIDs = sharedQuestionIDs.Count == 0 ? "0" : string.Join(",", sharedCollaborationIDs.ToArray());
                }
                return sharedIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetSharedAssessmentIDs of a shared person
        /// </summary>
        /// <param name="sharedPersonSearchDTO"></param>
        /// <returns></returns>
        public string GetSharedAssessmentIDs(SharedPersonSearchDTO sharedPersonSearchDTO)
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                if (sharedPersonSearchDTO.PersonID == 0)
                {
                    var person = this.GetPerson(sharedPersonSearchDTO.PersonIndex);
                    sharedPersonSearchDTO.PersonID = person.PersonID;
                }
                if (IsSharedPerson(sharedPersonSearchDTO.PersonID, sharedPersonSearchDTO.LoggedInAgencyID))
                {
                    var sharedDetails = this.GetSharedAssessmentsByPerson(sharedPersonSearchDTO);
                    var sharedDistinctIDs = sharedDetails.Select(x => x.AssessmentID).Distinct().ToList();
                    sharedAssessmentIDs = sharedDistinctIDs.Count == 0 ? "0" : string.Join(",", sharedDistinctIDs.ToArray());
                }
                return sharedAssessmentIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetSharedAssessmentsByPerson- For AssessmentDetails,Values and VoiceType filter.
        /// </summary>
        /// <param name="sharedPersonSearchDTO"></param>
        /// <returns></returns>
        private List<Assessment> GetSharedAssessmentsByPerson(SharedPersonSearchDTO sharedPersonSearchDTO)
        {
            try
            {
                var statusConditionForReports = string.Empty;
                List<Assessment> assessment = new List<Assessment>();
                if (!string.IsNullOrEmpty(sharedPersonSearchDTO.QueryType))
                {
                    statusConditionForReports = $@"AND ast.Name in ('Returned','Submitted','Approved')";
                }
                var selectTop = sharedPersonSearchDTO.TopRowCount == 0 ? "" : "TOP " + sharedPersonSearchDTO.TopRowCount;
                var sharedQuestionnaireDetails = this.GetSharedPersonCollaborationDetails(sharedPersonSearchDTO.PersonID, sharedPersonSearchDTO.LoggedInAgencyID);
                var sharedQAllIDs = sharedQuestionnaireDetails.Select(x => x.QuestionnaireID).Distinct().ToList();
                var sharedIDs = sharedQAllIDs.Count == 0 ? "0" : string.Join(",", sharedQAllIDs.ToArray());
                if (sharedPersonSearchDTO.QuestionnaireID != 0 && !sharedQAllIDs.Contains(sharedPersonSearchDTO.QuestionnaireID))
                {
                    return assessment;
                }
                var sharedQuestionIDs = sharedPersonSearchDTO.QuestionnaireID == 0 ? sharedIDs : sharedPersonSearchDTO.QuestionnaireID.ToString();
                var sharedCollaboratonIDs = sharedQuestionnaireDetails.Select(x => x.PersonCollaborationID).Distinct().ToList();
                foreach (var PersonCollaborationID in sharedCollaboratonIDs)
                {
                    var query = string.Empty;
                    query = @$"SELECT * INTO #WindowOffsets FROM
						    (
						    	SELECT
						    		q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays,PQ.PersonQuestionnaireID
						    	FROM 
						    	PersonQuestionnaire pq
						    	JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID AND pq.PersonID = {sharedPersonSearchDTO.PersonID}
						    	JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
						    	JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
						    	WHERE ar.Name IN ('Initial','discharge') 
                                AND (PQ.PersonQuestionnaireID = {sharedPersonSearchDTO.PersonQuestionnaireID} OR PQ.QuestionnaireID IN ({sharedQuestionIDs}))

						    ) AS A
						    SELECT * INTO #SelectedCollaboration FROM
						    ( 
						    	 SELECT PC.EnrollDate AS CollaborationStartDate,
                                                PC.EndDate AS CollaborationEndDate, PC.PersonCollaborationID,PC.IsPrimary, PC.IsCurrent
                                      	 FROM PersonCollaboration PC                                  	      
                                      	 WHERE PC.IsRemoved=0 AND PC.PersonID = {sharedPersonSearchDTO.PersonID}
                                         AND (PC.PersonCollaborationID = {PersonCollaborationID})
						    )AS B
						    SELECT * INTO #SelectedAssessments FROM
						    (
						    	SELECT
						    		a.*,					
						    		wo_init.WindowOpenOffsetDays,
						    		wo_disc.WindowCloseOffsetDays,
						    		(CASE WHEN {PersonCollaborationID}=0 THEN NULL ELSE (SELECT CollaborationStartDate FROM #SelectedCollaboration) END) [EnrollDate],
						    		(CASE WHEN {PersonCollaborationID}=0 THEN NULL ELSE (SELECT CollaborationEndDate FROM #SelectedCollaboration) END) [EndDate]
						    	FROM
						    	Assessment a	
						    	JOIn PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = a.PersonQuestionnaireID AND PQ.PersonID = {sharedPersonSearchDTO.PersonID} AND pq.IsRemoved=0
                                AND (PQ.PersonQuestionnaireID = {sharedPersonSearchDTO.PersonQuestionnaireID} OR PQ.QuestionnaireID IN ({sharedQuestionIDs})) AND a.IsRemoved = 0
						    	JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID 	{statusConditionForReports}						
						    	LEFT JOIN (SELECT * FROM #WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.PersonQuestionnaireID=a.PersonQuestionnaireID 
						    	LEFT JOIN (SELECT * FROM #WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.PersonQuestionnaireID=a.PersonQuestionnaireID
						    	WHERE (ISNULL(A.VoiceTypeFKID,0) = {sharedPersonSearchDTO.VoiceTypeFKID} OR {sharedPersonSearchDTO.VoiceTypeFKID} = 0)  AND ({sharedPersonSearchDTO.VoiceTypeID}=0 OR a.VoiceTypeID={sharedPersonSearchDTO.VoiceTypeID}) 
						    )AS C
						    SELECT DISTINCT {selectTop} SA.AssessmentID,SA.PersonQuestionnaireID,SA.VoiceTypeID,SA.VoiceTypeFKID,SA.DateTaken,SA.ReasoningText,
						          SA.AssessmentReasonID,SA.AssessmentStatusID,SA.PersonQuestionnaireScheduleID, SA.IsUpdate,
						    	  SA.Approved,SA.CloseDate,SA.IsRemoved,SA.UpdateDate,SA.UpdateUserID,SA.EventDate,SA.EventNotes
						    FROM #SelectedAssessments SA
						    JOIN #SelectedCollaboration CT ON 
						    (	
                                {PersonCollaborationID} = 0 OR		
						    	CAST(SA.DateTaken AS DATE) 
						    	BETWEEN
						    	DATEADD(DAY,0-ISNULL(SA.WindowOpenOffsetDays,0),CAST(SA.EnrollDate AS DATE)) 
						    	AND 
						    	DATEADD(DAY,ISNULL(SA.WindowCloseOffsetDays,0),ISNULL(SA.EndDate, CAST(GETDATE() AS DATE)))	)					
						    ORDER BY sa.DateTaken desc";
                    var lstassessment = ExecuteSqlQuery(query, x => new Assessment
                    {
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                        PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                        VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                        VoiceTypeFKID = x["VoiceTypeFKID"] == DBNull.Value ? 0 : (long)x["VoiceTypeFKID"],
                        DateTaken = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                        ReasoningText = x["ReasoningText"] == DBNull.Value ? null : (string)x["ReasoningText"],
                        AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                        AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                        PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? null : (long?)x["PersonQuestionnaireScheduleID"],
                        IsUpdate = x["IsUpdate"] == DBNull.Value ? false : (bool)x["IsUpdate"],
                        Approved = x["Approved"] == DBNull.Value ? null : (int?)x["Approved"],
                        CloseDate = x["CloseDate"] == DBNull.Value ? null : (DateTime?)x["CloseDate"],
                        IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                        UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                        EventDate = x["EventDate"] == DBNull.Value ? null : (DateTime?)x["EventDate"],
                        EventNotes = x["EventNotes"] == DBNull.Value ? null : (string)x["EventNotes"]
                    });
                    assessment.AddRange(lstassessment);
                }
                return assessment;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPersonSharingDetails-Fetch sharing Indexes and details for shared Persons
        /// Any change should be done in GetSharedPersonsQuery too.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public List<PersonSharingDetailsDTO> GetPersonSharingDetails(Guid personIndex, long loggedInAgencyID)
        {
            try
            {
                var query = $@"SELECT DISTINCT P.PersonID,RU.ReportingUnitID,CS.CollaborationSharingID, CS.HistoricalView AS HistoricalView,
										  PC.EnrollDate,PC.EndDate AS SharingEndDate,
										  CS.CollaborationSharingIndex,CSP.CollaborationSharingPolicyID,CSP.Weight AS CollaborationSharingWeight,
										  ags.AgencySharingIndex,ASP.AgencySharingPolicyID,ASP.Weight AS AgencySharingWeight,
										  CS.AgencyID as ServingAgencyId,AgS.AgencyID as ReceivingAgencyId,
										  CASE WHEN CS.AgencyID = AgS.AgencyID THEN 0 ELSE 1 END  IsShared,		
										  CASE WHEN ISNULL(CAST(PC.EndDate AS DATE),CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE) THEN 1 
										      ELSE 0 END AS IsActiveForSharing --Active Or Not Based on PersonCollaborationEndDate
									FROM Person p 
							        JOIN PersonCollaboration PC ON P.personId = PC.PersonID AND P.PersonIndex = '{personIndex}' 
                                        AND P.AgencyID <> {loggedInAgencyID} AND PC.IsRemoved = 0
									JOIN CollaborationSharing CS ON CS.CollaborationID = PC.CollaborationID
									JOIN CollaborationSharingPolicy CSP ON CSP.CollaborationSharingPolicyID = CS.CollaborationSharingPolicyID
									JOIN ReportingUnit RU ON CS.ReportingUnitID = RU.ReportingUnitID AND RU.IsRemoved = 0
									JOIN AgencySharing AgS ON RU.ReportingUnitID = AgS.ReportingUnitID
                                                AND AGS.AgencyID = {loggedInAgencyID} --loggedInAgency
									JOIN AgencySharingPolicy ASP ON ASP.AgencySharingPolicyID = AGS.AgencySharingPolicyID
									WHERE RU.IsSharing = 1 AND CS.IsSharing = 1  
									          AND CS.AgencyID <> AGS.AgencyId 
                                              AND (SELECT A.IsSharing from AgencySharing A WHERE CS.AgencyID = A.AgencyID 
														AND A.ReportingUnitID = RU.ReportingUnitID) = 1 --sharing true for serving agency
											  AND  ISNULL(CAST(RU.EndDate AS DATE),CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)--RU endDate checking";

                var personSharingDetailsDTO = ExecuteSqlQuery(query, x => new PersonSharingDetailsDTO
                {
                    AgencySharingWeight = x["AgencySharingWeight"] == DBNull.Value ? 0 : (int)x["AgencySharingWeight"],
                    CollaborationSharingWeight = x["CollaborationSharingWeight"] == DBNull.Value ? 0 : (int)x["CollaborationSharingWeight"],
                    HistoricalView = x["HistoricalView"] == DBNull.Value ? false : (bool)x["HistoricalView"],
                    IsShared = true,
                    AgencySharingIndex = x["AgencySharingIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["AgencySharingIndex"],
                    CollaborationSharingIndex = x["CollaborationSharingIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["CollaborationSharingIndex"],
                    IsActiveForSharing = (int)x["IsActiveForSharing"] == 0 ? false : true,
                });
                return personSharingDetailsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetSharedPersonIDs(long agencyID, string role)
        {
            try
            {
                var query = this.GetSharedPersonsQuery(agencyID, role, "DISTINCT P.PersonID");

                var personSharingDetailsDTO = ExecuteSqlQuery(query, x => new PeopleDTO
                {
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"]
                });
                return string.Join(",", personSharingDetailsDTO?.Select(x => x.PersonID).ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        /// <summary>
        /// get active persons
        /// </summary>
        /// <returns></returns>
        public List<long> GetActivePersons()
        {
            try
            {
                var query = string.Empty;

                query = @"select P.PersonID
                        from Person P where P.IsActive=1";

                var data = ExecuteSqlQuery(query, x =>
                    (long)x[0]
            );

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get active person collaboration
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<long> GetActivePersonsCollaboration(List<long> personID)
        {

            try
            {
                var query = string.Empty;
                query = $@"select PersonId from person P where P.PersonID in ({string.Join(",", personID.ToArray())}) and personID not in 
                        (select distinct PersonID from PersonCollaboration P where (enddate is null OR  CONVERT(DATE, enddate) >=CONVERT(DATE, getdate())) and PersonID in ({string.Join(",", personID.ToArray())})  )
                            and isactive=1 ";

                var data = ExecuteSqlQuery(query, x =>
                    (long)x[0]
            );
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPersonByAssessmentID
        /// </summary>
        /// <param name="assessmentID">assessmentID</param>
        /// <returns>Person</returns>
        public PersonQuestionnaireDetailsDTO GetPersonByAssessmentID(int assessmentID)
        {

            try
            {
                var query = string.Empty;
                query = $@"select PQ.QuestionnaireID,P.PersonIndex from person P
                            join PersonQuestionnaire PQ on P.PersonID=PQ.PersonID
                            join assessment A on A.PersonQuestionnaireID=PQ.PersonQuestionnaireID
                            where A.AssessmentID=" + assessmentID;

                var peopleDetails = ExecuteSqlQuery(query, x => new PersonQuestionnaireDetailsDTO
                {
                    QuestionnaireID = (int)x[0],
                    PersonIndex = (Guid)x[1]
                }).FirstOrDefault();

                return peopleDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// bulk person update
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public List<Person> UpdateBulkPersons(List<Person> persons)
        {
            try
            {
                if (persons.Count > 0)
                {
                    var res = this.UpdateBulkAsync(persons);
                    res.Wait();
                }
                return persons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetPeopleDetails
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <returns>PeopleDataDTO</returns>
        public PeopleDataDTO getPersonByUniversalId(string universalId)
        {
            PeopleDataDTO peopleDetails = new PeopleDataDTO();
            try
            {
                var query = string.Empty;
                query = @"SELECT P.PersonID,P.PersonIndex,P.FirstName,P.MiddleName,P.LastName,P.Suffix,P.Phone1,P.Phone2,P.Email
                        ,A.Address1,A.Address2,A.City,CS.CountryStateID,CS.Name AS State,A.Zip
                        ,P.DateOfBirth,G.IdentifiedGenderID AS IdentifiedGenderID,G.Name AS IdentifiedGender,G1.GenderID AS BioGenderID,G1.Name AS BioGender,S.SexualityID,S.Name AS Sexuality,PML.LanguageID AS PrimaryLanugageID,PML.Name AS PrimaryLanugage,PFL.LanguageID AS PreferredLanugageID,PFL.Name AS PreferredLanugage
                        ,A.AddressID,PA.PersonAddressID, P.PersonScreeningStatusID, P.StartDate, P.EndDate
                        ,P.Phone1Code,P.Phone2Code, P.IsRemoved, P.AgencyID, P.IsActive, Ag.Name As AgencyName, P.UniversalID
                        FROM Person P
                        LEFT JOIN PersonAddress PA ON PA.PersonID = P.PersonID
                        LEFT JOIN Address A ON A.AddressID = PA.AddressID
                        LEFT JOIN [info].[CountryState] CS ON CS.CountryStateID = A.CountryStateId
                        LEFT JOIN [info].[IdentifiedGender] G ON G.IdentifiedGenderID = P.GenderID
                        LEFT JOIN [info].[Gender] G1 ON G1.GenderID = P.BiologicalSexID
                        LEFT JOIN [info].[Sexuality] S ON S.SexualityID = P.SexualityID                                                      
                        LEFT JOIN [info].[Language] PML ON PML.LanguageID = P.PrimaryLanguageID                        
                        LEFT JOIN [info].[Language] PFL ON PFL.LanguageID = P.PreferredLanguageID   
                        LEFT JOIN Agency Ag ON P.AgencyID = Ag.AgencyID
                        WHERE P.UniversalId = '" + universalId + "'";

                peopleDetails = ExecuteSqlQuery(query, x => new PeopleDataDTO
                {
                    PersonID = (long)x[0],
                    PersonIndex = (Guid)x[1],
                    FirstName = x[2] == DBNull.Value ? null : (string)x[2],
                    MiddleName = x[3] == DBNull.Value ? null : (string)x[3],
                    LastName = x[4] == DBNull.Value ? null : (string)x[4],
                    Suffix = x[5] == DBNull.Value ? null : (string)x[5],
                    Phone1 = x[6] == DBNull.Value ? null : (string)x[6],
                    Phone2 = x[7] == DBNull.Value ? null : (string)x[7],
                    Email = x[8] == DBNull.Value ? null : (string)x[8],

                    Address1 = x[9] == DBNull.Value ? null : (string)x[9],
                    Address2 = x[10] == DBNull.Value ? null : (string)x[10],
                    City = x[11] == DBNull.Value ? null : (string)x[11],
                    CountryStateID = x[12] == DBNull.Value ? 0 : (int)x[12],
                    State = x[13] == DBNull.Value ? null : (string)x[13],
                    Zip = x[14] == DBNull.Value ? null : (string)x[14],

                    DateOfBirth = (DateTime)x[15],
                    IdentifiedGenderID = x[16] == DBNull.Value ? 0 : (int)x[16],
                    IdentifiedGender = x[17] == DBNull.Value ? null : (string)x[17],
                    BioGenderID = x[18] == DBNull.Value ? 0 : (int)x[18],
                    BioGender = x[19] == DBNull.Value ? null : (string)x[19],
                    SexualityID = x[20] == DBNull.Value ? 0 : (int)x[20],
                    Sexuality = x[21] == DBNull.Value ? null : (string)x[21],
                    PrimaryLanugageID = x[22] == DBNull.Value ? 0 : (int)x[22],
                    PrimaryLanugage = x[23] == DBNull.Value ? null : (string)x[23],
                    PreferredLanugageID = x[24] == DBNull.Value ? 0 : (int)x[24],
                    PreferredLanugage = x[25] == DBNull.Value ? null : (string)x[25],
                    AddressID = x[26] == DBNull.Value ? 0 : (long)x[26],
                    PersonAddressID = x[27] == DBNull.Value ? 0 : (long)x[27],
                    PersonScreeningStatusID = x[28] == DBNull.Value ? 0 : (int)x[28],
                    StartDate = (DateTime)x[29],
                    EndDate = x[30] == DBNull.Value ? null : (DateTime?)x[30],
                    Phone1Code = x[31] == DBNull.Value ? null : (string)x[31],
                    Phone2Code = x[32] == DBNull.Value ? null : (string)x[32],
                    IsRemoved = x[33] == DBNull.Value ? false : (bool)x[33],
                    AgencyID = x[34] == DBNull.Value ? 0 : (long)x[34],
                    IsActive = x[35] == DBNull.Value ? false : (bool)x[35],
                    AgencyName = x[36] == DBNull.Value ? null : (string)x[36],
                    UniversalID = x[37] == DBNull.Value ? null : (string)x[37],
                }).FirstOrDefault();

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleDetails
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <returns>PeopleDataDTO</returns>
        public List<PeopleDataDTO> getPersonByUniversalIdList(List<string> universalIdList, long agencyId)
        {
            List<PeopleDataDTO> peopleDetails = new List<PeopleDataDTO>();
            try
            {
                var query = string.Empty;
                query = @"SELECT P.PersonID,P.PersonIndex,P.FirstName,P.MiddleName,P.LastName,P.Suffix,P.Phone1,P.Phone2,P.Email
                        ,A.Address1,A.Address2,A.City,CS.CountryStateID,CS.Name AS State,A.Zip
                        ,P.DateOfBirth,G.IdentifiedGenderID AS IdentifiedGenderID,G.Name AS IdentifiedGender,G1.GenderID AS BioGenderID,G1.Name AS BioGender,S.SexualityID,S.Name AS Sexuality,PML.LanguageID AS PrimaryLanugageID,PML.Name AS PrimaryLanugage,PFL.LanguageID AS PreferredLanugageID,PFL.Name AS PreferredLanugage
                        ,A.AddressID,PA.PersonAddressID, P.PersonScreeningStatusID, P.StartDate, P.EndDate
                        ,P.Phone1Code,P.Phone2Code, P.IsRemoved, P.AgencyID, P.IsActive, Ag.Name As AgencyName, P.UniversalID
                        FROM Person P
                        LEFT JOIN PersonAddress PA ON PA.PersonID = P.PersonID
                        LEFT JOIN Address A ON A.AddressID = PA.AddressID
                        LEFT JOIN [info].[CountryState] CS ON CS.CountryStateID = A.CountryStateId
                        LEFT JOIN [info].[IdentifiedGender] G ON G.IdentifiedGenderID = P.GenderID
                        LEFT JOIN [info].[Gender] G1 ON G1.GenderID = P.BiologicalSexID
                        LEFT JOIN [info].[Sexuality] S ON S.SexualityID = P.SexualityID                                                      
                        LEFT JOIN [info].[Language] PML ON PML.LanguageID = P.PrimaryLanguageID                        
                        LEFT JOIN [info].[Language] PFL ON PFL.LanguageID = P.PreferredLanguageID   
                        LEFT JOIN Agency Ag ON P.AgencyID = Ag.AgencyID
                        WHERE P.UniversalId IN ('" + string.Join("','", universalIdList.ToArray()) + "') AND P.AgencyID =" + agencyId;

                peopleDetails = ExecuteSqlQuery(query, x => new PeopleDataDTO
                {
                    PersonID = (long)x[0],
                    PersonIndex = (Guid)x[1],
                    FirstName = x[2] == DBNull.Value ? null : (string)x[2],
                    MiddleName = x[3] == DBNull.Value ? null : (string)x[3],
                    LastName = x[4] == DBNull.Value ? null : (string)x[4],
                    Suffix = x[5] == DBNull.Value ? null : (string)x[5],
                    Phone1 = x[6] == DBNull.Value ? null : (string)x[6],
                    Phone2 = x[7] == DBNull.Value ? null : (string)x[7],
                    Email = x[8] == DBNull.Value ? null : (string)x[8],

                    Address1 = x[9] == DBNull.Value ? null : (string)x[9],
                    Address2 = x[10] == DBNull.Value ? null : (string)x[10],
                    City = x[11] == DBNull.Value ? null : (string)x[11],
                    CountryStateID = x[12] == DBNull.Value ? 0 : (int)x[12],
                    State = x[13] == DBNull.Value ? null : (string)x[13],
                    Zip = x[14] == DBNull.Value ? null : (string)x[14],

                    DateOfBirth = (DateTime)x[15],
                    IdentifiedGenderID = x[16] == DBNull.Value ? 0 : (int)x[16],
                    IdentifiedGender = x[17] == DBNull.Value ? null : (string)x[17],
                    BioGenderID = x[18] == DBNull.Value ? 0 : (int)x[18],
                    BioGender = x[19] == DBNull.Value ? null : (string)x[19],
                    SexualityID = x[20] == DBNull.Value ? 0 : (int)x[20],
                    Sexuality = x[21] == DBNull.Value ? null : (string)x[21],
                    PrimaryLanugageID = x[22] == DBNull.Value ? 0 : (int)x[22],
                    PrimaryLanugage = x[23] == DBNull.Value ? null : (string)x[23],
                    PreferredLanugageID = x[24] == DBNull.Value ? 0 : (int)x[24],
                    PreferredLanugage = x[25] == DBNull.Value ? null : (string)x[25],
                    AddressID = x[26] == DBNull.Value ? 0 : (long)x[26],
                    PersonAddressID = x[27] == DBNull.Value ? 0 : (long)x[27],
                    PersonScreeningStatusID = x[28] == DBNull.Value ? 0 : (int)x[28],
                    StartDate = (DateTime)x[29],
                    EndDate = x[30] == DBNull.Value ? null : (DateTime?)x[30],
                    Phone1Code = x[31] == DBNull.Value ? null : (string)x[31],
                    Phone2Code = x[32] == DBNull.Value ? null : (string)x[32],
                    IsRemoved = x[33] == DBNull.Value ? false : (bool)x[33],
                    AgencyID = x[34] == DBNull.Value ? 0 : (long)x[34],
                    IsActive = x[35] == DBNull.Value ? false : (bool)x[35],
                    AgencyName = x[36] == DBNull.Value ? null : (string)x[36],
                    UniversalID = x[37] == DBNull.Value ? null : (string)x[37],
                });

                return peopleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUniversalIDCountByAgency
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns>int</returns>
        public int GetUniversalIDCountByAgency(long agencyID)
        {
            int count = (from row in this._dbContext.Person
                         where (row.AgencyID == agencyID) && !string.IsNullOrEmpty(row.UniversalID)
                         select row).Count();

            return count;
        }

        /// <summary>
        /// getPersonsAndHelpersByPersonIDList
        /// </summary>
        /// <param name="personIdList"></param>
        /// <returns></returns>
        public List<PeopleHelperEmailDTO> getPersonsAndHelpersByPersonIDList(List<long> lstPersonID)
        {
            List<PeopleHelperEmailDTO> peopleHelperDetails = new List<PeopleHelperEmailDTO>();
            try
            {
                var query = string.Empty;
                query = $@"SELECT P.PersonID,P.PersonIndex,P.FirstName,P.MiddleName,P.LastName,P.AgencyID,
                                  PH.HelperID,H.HelperIndex,H.FirstName,h.MiddleName,h.LastName,h.Email
                        FROM Person P    
                        JOIN PersonHelper PH on P.PersonID = ph.PersonID 
                        AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                        AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
                        JOIn Helper H ON h.HelperID = ph.HelperID AND P.AgencyID = H.AgencyID
                        WHERE ph.IsRemoved=0 AND P.PersonID IN ({string.Join(",", lstPersonID.ToArray())})";

                peopleHelperDetails = ExecuteSqlQuery(query, x => new PeopleHelperEmailDTO
                {
                    PersonID = (long)x["PersonID"],
                    PersonIndex = (Guid)x["PersonIndex"],
                    PersonFirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    PersonMiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    PersonLastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    HelperID = (int)x["HelperID"],
                    HelperIndex = (Guid)x["HelperIndex"],
                    HelperFirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    HelperLastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    HelperMiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    HelperEmail = x["Email"] == DBNull.Value ? string.Empty : (string)x["Email"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],

                });

                return peopleHelperDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// getPersonsAndHelpersByPersonIDListForAlert
        /// </summary>
        /// <param name="personIdList"></param>
        /// <returns></returns>
        public List<PeopleHelperEmailDTO> getPersonsAndHelpersByPersonIDListForAlert(long lstPersonID)
        {
            List<PeopleHelperEmailDTO> peopleHelperDetails = new List<PeopleHelperEmailDTO>();
            try
            {
                var query = string.Empty;
                query = $@"SELECT P.PersonID,P.PersonIndex,P.FirstName,P.MiddleName,P.LastName,P.AgencyID,
                                  PH.HelperID,H.HelperIndex,H.FirstName,h.MiddleName,h.LastName,h.Email
                        FROM Person P    
                        JOIN PersonHelper PH on P.PersonID = ph.PersonID 
                        AND CAST(ph.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                        AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
                        JOIn Helper H ON h.HelperID = ph.HelperID AND P.AgencyID = H.AgencyID
                        WHERE ph.IsRemoved=0 AND P.PersonID =" + lstPersonID + @"
                        union
                        SELECT P.PersonID,P.PersonIndex,P.FirstName,P.MiddleName,P.LastName,P.AgencyID,
                                  H1.HelperID,H1.HelperIndex,H1.FirstName,H1.MiddleName,H1.LastName,H1.Email
                        FROM Person P
                        JOIN PersonHelper PH on P.PersonID = ph.PersonID
                        AND CAST(ph.StartDate AS DATE)<= CAST(GETDATE() AS DATE)
                        AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>= CAST(GETDATE() AS DATE)
                        JOIn Helper H ON h.HelperID = ph.HelperID AND P.AgencyID = H.AgencyID
                        JOIN Helper H1 ON H1.HelperID = h.SupervisorHelperID AND P.AgencyID = H1.AgencyID
                        WHERE ph.IsRemoved = 0 AND P.PersonID =" + lstPersonID;

                peopleHelperDetails = ExecuteSqlQuery(query, x => new PeopleHelperEmailDTO
                {
                    PersonID = (long)x["PersonID"],
                    PersonIndex = (Guid)x["PersonIndex"],
                    PersonFirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    PersonMiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    PersonLastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    HelperID = (int)x["HelperID"],
                    HelperIndex = (Guid)x["HelperIndex"],
                    HelperFirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    HelperLastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    HelperMiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    HelperEmail = x["Email"] == DBNull.Value ? string.Empty : (string)x["Email"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],

                });

                return peopleHelperDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonQuestionnaireScheduleEmailDTO> getDetailsByPersonQuestionScheduleList(List<long> lstPersonQuestionScheduleID)
        {
            try
            {
                List<PersonQuestionnaireScheduleEmailDTO> peopleQuestionDetails = new List<PersonQuestionnaireScheduleEmailDTO>();
                var query = string.Empty;
                query = $@"SELECT PQ.PersonID,PQ.QuestionnaireID,PQ.PersonQuestionnaireID,PQS.PersonQuestionnaireScheduleID,PQS.WindowDueDate,
						CASE WHEN CAST(PQS.WindowDueDate AS DATE)<CAST(GETDATE() AS DATE)  THEN '{PCISEnum.EmailDetail.ReminderOverDue}'
						     WHEN CAST(PQS.WindowDueDate AS DATE)=CAST(GETDATE() AS DATE)  THEN '{PCISEnum.EmailDetail.ReminderDueDate}'
							 WHEN CAST(PQS.WindowDueDate AS DATE)>CAST(GETDATE() AS DATE)  THEN '{PCISEnum.EmailDetail.ReminderDueApproching}'
                        END AS NotificationType
                        FROM PersonQuestionnaireSchedule PQS    
						JOIN PersonQuestionnaire PQ on PQS.PersonQuestionnaireID = PQ.PersonQuestionnaireID						
                        WHERE PQS.PersonQuestionnaireScheduleID IN ({string.Join(",", lstPersonQuestionScheduleID.ToArray())})";

                peopleQuestionDetails = ExecuteSqlQuery(query, x => new PersonQuestionnaireScheduleEmailDTO
                {
                    PersonID = (long)x["PersonID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireScheduleID"],
                    WindowDueDate = (DateTime)x["WindowDueDate"],
                    NotificationType = (string)x["NotificationType"]
                });
                return peopleQuestionDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ImportPersonBulkInsert.
        /// </summary>
        /// <param name="personList">personList.</param>
        /// <returns>List Person.</returns>
        public List<Person> ImportPersonBulkInsert(List<Person> personList)
        {
            try
            {
                var res = this.UpdateBulkAsync(personList);
                res.Wait();
                return personList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetPersonListByGUID.
        /// </summary>
        /// <param name="personIndexGuids">personIndexGuids.</param>
        /// <returns>PersonList.</returns>
        public async Task<IReadOnlyList<Person>> GetPersonListByGUID(List<Guid> personIndexGuids)
        {
            try
            {
                var assessmentResponse = await this.GetAsync(x => personIndexGuids.Contains(x.PersonIndex));
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PeopleProfileDetailsDTO> GetPersonsDetailsListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {

                var query = string.Empty;
                query = $@";WITH PersonCTE AS( SELECT P.PersonID,P.PersonIndex,
                                 P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
								 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [FullName], 
                                 P.FirstName,P.MiddleName,P.LastName,P.Suffix,P.PrimaryLanguageID,P.PreferredLanguageID,P.DateOfBirth,
								 P.genderID,P.SexualityID, P.BiologicalSexID, P.Email,P.Phone1Code, P.Phone2Code,P.Phone1,P.Phone2,P.IsActive,
								 P.PersonScreeningStatusID,P.StartDate,P.EndDate,P.IsRemoved,P.UpdateUserID,P.UpdateDate,P.AgencyID,P.UniversalID FROM Person P
										WHERE P.AgencyID = {loggedInUserDTO.AgencyId} AND P.IsRemoved = 0 
										AND P.IsActive = 1
									  )
					       SELECT COUNT(P.PersonID) OVER() AS TotalCount, P.*,
								 PA.PersonAddressID,PA.AddressID,A.AddressIndex, A.Address1,A.Address2,A.City,A.CountryStateId,A.Zip,A.Zip4,A.CountryId,
	        	                 PersonIdentifier = (SELECT PI.PersonIdentificationID,PI.IdentificationTypeID,PI.IdentificationNumber
									 FROM PersonIdentification PI
									 WHERE PI.IsRemoved = 0 AND PI.PersonID = P.PersonID FOR JSON PATH),
							     PersonRaceEthnicity = (SELECT PRE.PersonRaceEthnicityID,PRE.PersonID,PRE.RaceEthnicityID,P.PersonIndex
							         FROM PersonRaceEthnicity PRE 
							         WHERE PRE.IsRemoved = 0 AND  PRE.PersonID = P.PersonID FOR JSON PATH),
							     PersonSupport = (SELECT PS.PersonSupportID,PS.SupportTypeID,PS.IsCurrent, PS.FirstName,PS.MiddleName,PS.LastName,
								 PS.Suffix,PS.Email,PS.PhoneCode,PS.Phone,PS.Note,PS.StartDate,PS.EndDate,PS.UniversalID
							 		FROM PersonSupport PS 
							 		WHERE PS.IsRemoved=0 AND PS.PersonID = P.personID FOR JSON PATH),
							     PersonHelper = (SELECT PH.PersonHelperID,PH.HelperID,PH.IsLead,PH.IsCurrent,PH.StartDate,PH.EndDate,PH.CollaborationID
							 		FROM PersonHelper PH 
							 		WHERE PH.IsRemoved=0 AND PH.PersonID = P.personID FOR JSON PATH),
                                 PersonCollaboration = (SELECT PC.PersonCollaborationID,PC.CollaborationID,PC.EnrollDate,PC.EndDate,
                                    PC.IsCurrent,PC.IsPrimary
                                    FROM PersonCollaboration PC
                                    WHERE PC.IsRemoved=0 AND PC.PersonID = P.personID FOR JSON PATH)
                        FROM PersonCTE P
                        LEFT JOIN PersonAddress PA ON PA.PersonID = P.PersonID
                        LEFT JOIN Address A ON A.AddressID = PA.AddressID WHERE 1=1 ";

                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                var peopleProfileDetails = ExecuteSqlQuery(query, x => new PeopleProfileDetailsDTO
                {
                    PersonID = (long)x["PersonID"],
                    PersonIndex = (Guid)x["PersonIndex"],
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"],
                    Suffix = x["Suffix"] == DBNull.Value ? null : (string)x["Suffix"],
                    PrimaryLanguageID = x["PrimaryLanguageID"] == DBNull.Value ? 0 : (int)x["PrimaryLanguageID"],
                    PreferredLanguageID = x["PreferredLanguageID"] == DBNull.Value ? 0 : (int)x["PreferredLanguageID"],
                    DateOfBirth = (DateTime)x["DateOfBirth"],
                    GenderID = x["GenderID"] == DBNull.Value ? 0 : (int)x["GenderID"],
                    SexualityID = x["SexualityID"] == DBNull.Value ? 0 : (int)x["SexualityID"],
                    BiologicalSexID = x["BiologicalSexID"] == DBNull.Value ? 0 : (int)x["BiologicalSexID"],
                    Email = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    Phone1Code = x["Phone1Code"] == DBNull.Value ? null : (string)x["Phone1Code"],
                    Phone2Code = x["Phone2Code"] == DBNull.Value ? null : (string)x["Phone2Code"],
                    Phone1 = x["Phone1"] == DBNull.Value ? null : (string)x["Phone1"],
                    Phone2 = x["Phone2"] == DBNull.Value ? null : (string)x["Phone2"],
                    IsActive = x["IsActive"] == DBNull.Value ? false : (bool)x["IsActive"],
                    PersonScreeningStatusID = x["PersonScreeningStatusID"] == DBNull.Value ? 0 : (int)x["PersonScreeningStatusID"],
                    StartDate = (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    UpdateDate = (DateTime)x["UpdateDate"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    PersonAddressID = x["PersonAddressID"] == DBNull.Value ? 0 : (long)x["PersonAddressID"],
                    AddressID = x["AddressID"] == DBNull.Value ? 0 : (long)x["AddressID"],
                    AddressIndex = x["AddressIndex"] == DBNull.Value ? null : (Guid?)x["AddressIndex"],
                    Address1 = x["Address1"] == DBNull.Value ? null : (string)x["Address1"],
                    Address2 = x["Address2"] == DBNull.Value ? null : (string)x["Address2"],
                    City = x["City"] == DBNull.Value ? null : (string)x["City"],
                    CountryStateId = x["CountryStateID"] == DBNull.Value ? 0 : (int)x["CountryStateID"],
                    Zip = x["Zip"] == DBNull.Value ? null : (string)x["Zip"],
                    Zip4 = x["Zip4"] == DBNull.Value ? null : (string)x["Zip4"],
                    UniversalID = x["UniversalID"] == DBNull.Value ? null : (string)x["UniversalID"],
                    CountryId = x["CountryID"] == DBNull.Value ? 0 : (int)x["CountryID"],
                    PersonIdentifications = x["PersonIdentifier"] == DBNull.Value ? null : (string)x["PersonIdentifier"],
                    PersonRaceEthnicitys = x["PersonRaceEthnicity"] == DBNull.Value ? null : (string)x["PersonRaceEthnicity"],
                    PersonSupports = x["PersonSupport"] == DBNull.Value ? null : (string)x["PersonSupport"],
                    PersonHelpers = x["PersonHelper"] == DBNull.Value ? string.Empty : (string)x["PersonHelper"],
                    PersonCollaborations = x["PersonCollaboration"] == DBNull.Value ? null : (string)x["PersonCollaboration"],
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                }, queryBuilderDTO.QueryParameterDTO);
                return peopleProfileDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IsValidPersonInAgencyForQuestionnaire(long personID, long personAgencyID, int questionnaireID, long loggedInAgencyID, bool isEmailAssessment = false)
        {
            try
            {
                var flag = true;
                loggedInAgencyID = isEmailAssessment ? personAgencyID : loggedInAgencyID;
                if (personAgencyID != loggedInAgencyID) //if shared or not a person of same agency
                {
                    flag = false;
                    var sharedDTO = this.GetSharedPersonCollaborationDetails(personID, loggedInAgencyID);//Fetch shared Informations
                    if (sharedDTO?.Where(x => x.QuestionnaireID == questionnaireID).Count() > 0) //if shared QuestionIDs contains incomming questionnaireID
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IsValidPersonInAgencyForQuestionnaire(Guid personIndex, int questionnaireID, long loggedInAgencyID, bool isEmailAssessment = false)
        {
            try
            {
                var person = this.GetPerson(personIndex);
                if (person?.PersonID != 0)
                {
                    return this.IsValidPersonInAgencyForQuestionnaire(person.PersonID, person.AgencyID, questionnaireID, loggedInAgencyID);
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsValidPersonInAgency(long personID, long personAgencyID, long loggedInAgencyID)
        {
            try
            {
                var flag = true;
                if (personAgencyID != loggedInAgencyID) //if shared or not a person of same agency
                {
                    flag = false;
                    var sharedDTO = this.GetSharedPersonCollaborationDetails(personID, loggedInAgencyID);//Fetch shared Informations
                    if (sharedDTO?.Where(x => x.CollaborationID != 0).Count() > 0) //Check if it has a shared collaboration
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IsValidPersonInAgency(Guid personIndex, long loggedInAgencyID)
        {
            try
            {
                var person = this.GetPerson(personIndex);
                if (person?.PersonID != 0)
                {
                    return this.IsValidPersonInAgency(person.PersonID, person.AgencyID, loggedInAgencyID);
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public SharedDetailsDTO GetPersonHelperCollaborationDetails(long personID, long loggedInAgencyID, int userID)
        {
            try
            {
                SharedDetailsDTO sharedIds = new SharedDetailsDTO();
                var helpercollaborationList = this.GetHelperCollaboration(personID, loggedInAgencyID, userID);
                if (helpercollaborationList.Count > 0)
                {
                    var sharedDetails = this.GetHelperCollaborationDetails(helpercollaborationList, personID, userID);
                    var sharedCollaborationIDs = sharedDetails.Where(x => x.CollaborationID != 0).Select(x => x.CollaborationID).Distinct().ToList();
                    var sharedQuestionIDs = sharedDetails.Select(x => x.QuestionnaireID).Distinct().ToList();
                    sharedIds.PersonID = personID;
                    sharedIds.SharedQuestionnaireIDs = sharedQuestionIDs.Count == 0 ? "0" : string.Join(",", sharedQuestionIDs.ToArray());
                    sharedIds.SharedCollaborationIDs = sharedCollaborationIDs.Count == 0 ? "0" : string.Join(",", helpercollaborationList.ToArray());
                }
                return sharedIds;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private List<SharedCollaborationDetailsDTO> GetHelperCollaborationDetails(List<string> collaborationIds, long personID, int userID)
        {
            try
            {
                var query = $@";WITH PersonColb AS ( 
								 SELECT PC.CollaborationID, PC.PersonCollaborationID,PC.PersonId
                               	       FROM PersonCollaboration PC WHERE PC.PersonId = {personID} AND PC.IsRemoved = 0 
                                       AND PC.CollaborationId IN ({string.Join(",", collaborationIds)})
								)
								SELECT DISTINCT C.CollaborationID, CQ.QuestionnaireID, PC.PersonCollaborationID
                               	 FROM PersonColb PC
								 LEFT JOIN Collaboration C  ON PC.CollaborationId = C.CollaborationId
                               	 LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = C.CollaborationID 	
								 LEFT JOIN PersonQuestionnaire PQ ON PQ.PersonID = PC.PersonID 
								   AND PQ.QuestionnaireID = CQ.QuestionnaireID --AND PQ.CollaborationId = PC.CollaborationId
                               	    WHERE C.IsRemoved = 0 AND CQ.IsRemoved = 0 AND PQ.Isremoved =0
                                      AND C.CollaborationId IN ({string.Join(",", collaborationIds)})
                                UNION 
								SELECT '' AS CollaborationID, QuestionnaireID, '' AS PersonCollaborationID FROM PersonQuestionnaire 
								WHERE personid = {personID} AND ISNULL(CollaborationID, 0) = 0 AND IsRemoved = 0
								AND UpdateUserID = {userID}";

                var collaborations = ExecuteSqlQuery(query, x => new SharedCollaborationDetailsDTO
                {
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    PersonCollaborationID = x["PersonCollaborationID"] == DBNull.Value ? 0 : (long)x["PersonCollaborationID"],
                    PersonID = personID
                });
                return collaborations;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetHelperCollaboration.
        /// Get collaboration assigned for the user.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> GetHelperCollaboration(long personID, long loggedInAgencyID, long userID)
        {
            try
            {
                var query = $@"SELECT DISTINCT PH.CollaborationID, PH.PersonID 
                                     FROM Helper H
                                     JOIN PersonHelper  PH ON PH.HelperId = H.HelperID
                                	 WHERE PH.PersonID = {personID} AND H.UserID = {userID} 
                                     AND H.AgencyID = {loggedInAgencyID} AND PH.IsRemoved = 0 AND CollaborationID IS NOT NULL";

                var collaborations = ExecuteSqlQuery(query, x => new string(x["CollaborationID"] == DBNull.Value ? string.Empty : x["CollaborationID"].ToString())).Distinct();
                return collaborations.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private List<Assessment> GetHelpersAssessmentsByPerson(SharedPersonSearchDTO sharedPersonSearchDTO,ref SharedDetailsDTO sharedDetailsDTO)
        {
            try
            {
                var statusConditionForReports = string.Empty;
                List<Assessment> assessment = new List<Assessment>();
                if (!string.IsNullOrEmpty(sharedPersonSearchDTO.QueryType))
                {
                    statusConditionForReports = $@"AND ast.Name in ('Returned','Submitted','Approved')";
                }
                var selectTop = sharedPersonSearchDTO.TopRowCount == 0 ? "" : "TOP " + sharedPersonSearchDTO.TopRowCount;
                var sharedQuestionnaireDetails = new List<SharedCollaborationDetailsDTO>();
                sharedQuestionnaireDetails = this.GetHelperCollaborationDetails(sharedPersonSearchDTO.HelpercollaborationIDs, sharedPersonSearchDTO.PersonID, sharedPersonSearchDTO.UserID);

                var sharedQAllIDs = sharedQuestionnaireDetails.Select(x => x.QuestionnaireID).Distinct().ToList();
                var sharedIDs = sharedQAllIDs.Count == 0 ? "0" : string.Join(",", sharedQAllIDs.ToArray());
                if (sharedPersonSearchDTO.QuestionnaireID != 0 && !sharedQAllIDs.Contains(sharedPersonSearchDTO.QuestionnaireID))
                {
                    return assessment;
                }
                var sharedQuestionIDs = sharedPersonSearchDTO.QuestionnaireID == 0 ? sharedIDs : sharedPersonSearchDTO.QuestionnaireID.ToString();
                var sharedCollaboratonIDs = string.Join(",", sharedPersonSearchDTO.HelpercollaborationIDs.ToArray());

                var PersonCollaborationID = -1;
                //foreach (var PersonCollaborationID in sharedCollaboratonIDs)
                //{
                var query = string.Empty;
                query = @$"SELECT * INTO #WindowOffsets FROM
						    (
						    	SELECT
						    		q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays,PQ.PersonQuestionnaireID
						    	FROM 
						    	PersonQuestionnaire pq
						    	JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID AND pq.PersonID = {sharedPersonSearchDTO.PersonID}
						    	JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
						    	JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
						    	WHERE ar.Name IN ('Initial','discharge') 
                                AND (PQ.PersonQuestionnaireID = {sharedPersonSearchDTO.PersonQuestionnaireID} OR PQ.QuestionnaireID IN ({sharedQuestionIDs}))

						    ) AS A
						    SELECT * INTO #SelectedCollaboration FROM
						    ( 
						    	 SELECT PC.EnrollDate AS CollaborationStartDate,
                                                PC.EndDate AS CollaborationEndDate, PC.PersonCollaborationID,PC.IsPrimary, PC.IsCurrent
                                      	 FROM PersonCollaboration PC                                  	      
                                      	 WHERE PC.IsRemoved=0 AND PC.PersonID = {sharedPersonSearchDTO.PersonID}
                                         AND (PC.CollaborationID IN ({sharedCollaboratonIDs}))
						    )AS B
						    SELECT * INTO #SelectedAssessments FROM
						    (
						    	SELECT
						    		a.*,					
						    		wo_init.WindowOpenOffsetDays,
						    		wo_disc.WindowCloseOffsetDays,
						    		(CASE WHEN {PersonCollaborationID}=0 THEN NULL ELSE (SELECT MIN(CAST(CollaborationStartDate AS DATE)) FROM #SelectedCollaboration) END) [EnrollDate],
						    		(CASE WHEN {PersonCollaborationID}=0 THEN NULL ELSE (SELECT MAX(CAST(ISNULL(CollaborationEndDate,GETDATE()) AS DATE)) FROM #SelectedCollaboration) END) [EndDate]
						    	FROM
						    	Assessment a	
						    	JOIn PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = a.PersonQuestionnaireID AND PQ.PersonID = {sharedPersonSearchDTO.PersonID} AND pq.IsRemoved=0
                                AND (PQ.PersonQuestionnaireID = {sharedPersonSearchDTO.PersonQuestionnaireID} OR PQ.QuestionnaireID IN ({sharedQuestionIDs})) AND a.IsRemoved = 0
						    	JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID 	{statusConditionForReports}						
						    	LEFT JOIN (SELECT * FROM #WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.PersonQuestionnaireID=a.PersonQuestionnaireID 
						    	LEFT JOIN (SELECT * FROM #WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.PersonQuestionnaireID=a.PersonQuestionnaireID
						    	WHERE (ISNULL(A.VoiceTypeFKID,0) = {sharedPersonSearchDTO.VoiceTypeFKID} OR {sharedPersonSearchDTO.VoiceTypeFKID} = 0)  AND ({sharedPersonSearchDTO.VoiceTypeID}=0 OR a.VoiceTypeID={sharedPersonSearchDTO.VoiceTypeID}) 
						    )AS C
						    SELECT DISTINCT {selectTop} SA.AssessmentID,SA.PersonQuestionnaireID,SA.VoiceTypeID,SA.VoiceTypeFKID,SA.DateTaken,SA.ReasoningText,
						          SA.AssessmentReasonID,SA.AssessmentStatusID,SA.PersonQuestionnaireScheduleID, SA.IsUpdate,
						    	  SA.Approved,SA.CloseDate,SA.IsRemoved,SA.UpdateDate,SA.UpdateUserID,SA.EventDate,SA.EventNotes
						    FROM #SelectedAssessments SA
						    JOIN #SelectedCollaboration CT ON 
						    (	
                                {PersonCollaborationID} = 0 OR		
						    	CAST(SA.DateTaken AS DATE) 
						    	BETWEEN
						    	DATEADD(DAY,0-ISNULL(SA.WindowOpenOffsetDays,0),CAST(SA.EnrollDate AS DATE)) 
						    	AND 
						    	DATEADD(DAY,ISNULL(SA.WindowCloseOffsetDays,0),ISNULL(SA.EndDate, CAST(GETDATE() AS DATE)))	)					
						    ORDER BY sa.DateTaken desc";
                var lstassessment = ExecuteSqlQuery(query, x => new Assessment
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    VoiceTypeFKID = x["VoiceTypeFKID"] == DBNull.Value ? 0 : (long)x["VoiceTypeFKID"],
                    DateTaken = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    ReasoningText = x["ReasoningText"] == DBNull.Value ? null : (string)x["ReasoningText"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? null : (long?)x["PersonQuestionnaireScheduleID"],
                    IsUpdate = x["IsUpdate"] == DBNull.Value ? false : (bool)x["IsUpdate"],
                    Approved = x["Approved"] == DBNull.Value ? null : (int?)x["Approved"],
                    CloseDate = x["CloseDate"] == DBNull.Value ? null : (DateTime?)x["CloseDate"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    EventDate = x["EventDate"] == DBNull.Value ? null : (DateTime?)x["EventDate"],
                    EventNotes = x["EventNotes"] == DBNull.Value ? null : (string)x["EventNotes"]
                });
                assessment.AddRange(lstassessment);
                sharedDetailsDTO.SharedCollaborationIDs = sharedCollaboratonIDs;
                sharedDetailsDTO.SharedQuestionnaireIDs = sharedIDs;
                return assessment;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public SharedDetailsDTO GetHelpersAssessmentIDs(SharedPersonSearchDTO sharedPersonSearchDTO)
        {
            try
            {
                SharedDetailsDTO sharedDetailsDTO = new SharedDetailsDTO();
                if (sharedPersonSearchDTO.PersonID == 0)
                {
                    var person = this.GetPerson(sharedPersonSearchDTO.PersonIndex);
                    sharedPersonSearchDTO.PersonID = person.PersonID;
                }
                var helpercollaborationList = this.GetHelperCollaboration(sharedPersonSearchDTO.PersonID, sharedPersonSearchDTO.LoggedInAgencyID, sharedPersonSearchDTO.UserID);
                if (helpercollaborationList.Count > 0)
                {
                    sharedDetailsDTO.SharedCollaborationIDs = string.Join("," , helpercollaborationList.Distinct().ToArray());
                    sharedPersonSearchDTO.HelpercollaborationIDs = helpercollaborationList;
                    var sharedDetails = this.GetHelpersAssessmentsByPerson(sharedPersonSearchDTO,ref sharedDetailsDTO);
                    var sharedAssessmentIDs = sharedDetails.Select(x => x.AssessmentID).Distinct().ToList();
                    sharedDetailsDTO.SharedAssessmentIDs = sharedAssessmentIDs.Count == 0 ? "0" : string.Join(",", sharedAssessmentIDs.ToArray());
                }
                return sharedDetailsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<string> GetHelperAllMetricsInCollaboration(string personIDs, long agencyID, int userID, string statusType = "", bool lastAssessmentsOnly = true)
        {
            try
            {
                var assessmentStatus = this.AssessmentStatusRepository.GetAllAssessmentStatus();
                var submittedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).ToList()[0].AssessmentStatusID;
                var approvedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).ToList()[0].AssessmentStatusID;
                var returnedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).ToList()[0].AssessmentStatusID;
                var assessmentStatusCondition = string.Empty;
                var lastAssessmentsOnlyCondition = string.Empty;
                if (string.IsNullOrWhiteSpace(statusType))
                {
                    assessmentStatusCondition = $@"AND a.AssessmentStatusID in ({returnedStatus},{submittedStatus},{approvedStatus})";
                }
                if (lastAssessmentsOnly)
                {
                    lastAssessmentsOnlyCondition = $@"WHERE RowNumber = 1";
                }
                var query = @$";WITH PersonInHelp AS ( 
                        SELECT distinct personid
                        FROM Person
                         WHERE personid in ({ personIDs }) AND IsRemoved=0 
							   AND IsActive=1 AND agencyID = {agencyID}
                    )
                   ,PersonInHelpColb AS (
		            	SELECT DISTINCT PH.CollaborationID, PH.PersonID,PC.EnrollDate,PC.EndDate,CQ.QuestionnaireID,
                            PQ.PersonQuestionnaireID,CQ.Isremoved
                            FROM Helper H
                             JOIN PersonHelper  PH ON PH.HelperId = H.HelperID AND H.UserID = {userID}
		            		 JOIN PersonInHelp  P ON PH.PersonID = P.PersonID
		            		 JOIN PersonCollaboration PC ON PC.CollaborationId = PH.CollaborationId 
		            							       AND PC.PersonID = PH.PersonID				
		            		 JOIN Collaboration C ON PC.CollaborationId = C.CollaborationId
                             LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = PC.CollaborationID
		            		 LEFT JOIN PersonQuestionnaire PQ ON PQ.PersonID = PC.PersonID 
		            					AND PQ.QuestionnaireID = CQ.QuestionnaireID
                             WHERE  H.AgencyID = {agencyID} AND PH.IsRemoved = 0 AND PC.IsRemoved = 0  AND C.IsRemoved = 0 
		            				AND ISNULL(CQ.IsRemoved,0) = 0 AND ISNULL(PQ.Isremoved,0) = 0
		            				AND PH.CollaborationID IS NOT NULL 
		            		UNION 
		            	SELECT '' AS CollaborationID, PH.PersonID , null AS EnrollDate,null AS EndDate,
                            PQ.QuestionnaireID,PQ.PersonQuestionnaireID,0 AS         Isremoved
		            	    FROM PersonInHelp P 
                             JOIN PersonHelper  PH ON P.personid =PH.Personid
		            		 JOIN PersonQuestionnaire PQ ON PH.Personid = PQ.Personid
		            		 WHERE ISNULL(PQ.CollaborationID, 0) = 0 AND PH.IsRemoved = 0 AND PQ.IsRemoved = 0 
		            						AND PQ.UpdateUserID = {userID}
					)
                  ,PersonColb AS(
                                SELECT MIN(CAST(EnrollDate AS DATE)) AS CollaborationStartDate,
                                    MAX(ISNULL(EndDate, CAST(GETDATE() AS DATE))) AS CollaborationEndDate,PersonID
                               FROM PersonInHelpColb PHC GROUP BY PersonID
                    		  )
                  ,WindowOffsets AS(
                              SELECT
                                  qw.QuestionnaireID, qw.WindowOpenOffsetDays, qw.WindowCloseOffsetDays, ar.Name as Reason
                              FROM
                              QuestionnaireWindow qw
                              JOIN info.AssessmentReason ar ON ar.AssessmentReasonID = qw.AssessmentReasonID
                              WHERE ar.Name IN('Initial','discharge') AND qw.QuestionnaireID IN(select distinct QuestionnaireID from PersonInHelpColb)
                       )
				  ,SelectedAssessments AS
                       (
							SELECT PC.*, wo_init.WindowOpenOffsetDays,wo_disc.WindowCloseOffsetDays ,A.assessmentID,A.DateTaken,PHC.QuestionnaireID,
							DATEADD(DAY, 0 - ISNULL (wo_init.WindowOpenOffsetDays, 0), CAST(PC.CollaborationStartDate AS DATE)) AS Enrolldate,
							DATEADD(DAY, ISNULL (wo_disc.WindowCloseOffsetDays, 0), ISNULL(CAST(PC.CollaborationEndDate AS DATE), CAST(GETDATE() AS DATE)))	 AS Enddate
							   FROM PersonInHelpColb PHC
							    JOIn Assessment A ON A.PersonQuestionnaireID = PHC.PersonQuestionnaireID 
							    JOIn PersonColb PC ON PC.PersonID = PHC.PersonID
								AND a.IsRemoved = 0 {assessmentStatusCondition}
							 LEFT JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Initial')wo_init ON PHC.QuestionnaireID = wo_init.QuestionnaireID
							 LEFT JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Discharge')wo_disc ON wo_disc.QuestionnaireID = PHC.QuestionnaireID
		   	          )
				  ,AssessmentInCols AS
				     ( SELECT * FROM (
						    SELECT ROW_NUMBER() OVER (PARTITION BY SA.PersonID, sa.QuestionnaireID ORDER BY DateTaken DESC)  AS RowNumber,
							 SA.PersonID,SA.AssessmentID,SA.Datetaken,SA.Questionnaireid
						    FROM SelectedAssessments SA 
							join PersonAssessmentMetrics pqm WITH (NOLOCK) ON pqm.AssessmentID = SA.AssessmentID
							WHERE 	
						    	CAST(SA.DateTaken AS Date)
						    	BETWEEN SA.EnrollDate AND SA.EndDate
					    )A WHERE RowNumber = 1
				    )
				   ,AssessmentMetricsIDs AS (
								SELECT 
								ROW_NUMBER() OVER(PARTITION BY pqm.PersonID,pqm.ItemID,pqm.QuestionnaireID ORDER BY A.DateTaken desc ,PersonAssessmentMetricsID desc) AS RowNumber,
								pqm.PersonAssessmentMetricsID,A.DateTaken, Pqm.ItemID,pqm.assessmentID
								from AssessmentInCols A  WITH (NOLOCK)
								JOIN PersonAssessmentMetrics pqm ON pqm.AssessmentID = A.AssessmentID
								and pqm.PersonID=A.PersonID
					) SELECT distinct PersonAssessmentMetricsID FROM AssessmentMetricsIDs  WHERE RowNumber = 1";
                var resultIDs = ExecuteSqlQuery(query, x => new string(x["PersonAssessmentMetricsID"].ToString())).Distinct();
                return resultIDs.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPersonByPersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public PeopleDTO GetPersonByPersonId(long personId,long agencyId)
        {
            try
            {
                PeopleDTO peopleDTO = new PeopleDTO();
                var query = @$"Select PersonId, PersonIndex, FirstName from Person where PersonId = {personId} and agencyId = {agencyId}";
                peopleDTO = ExecuteSqlQuery(query, x => new PeopleDTO
                {
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    
                }).FirstOrDefault();
                return peopleDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// To get Peronaldetails of a Person.
        /// </summary>
        /// <param id="id">id.</param>
        /// <returns>.PeopleDTO</returns>
        public PeopleDTO GetPersonalDetails(Guid id)
        {
            try
            {
                string query = $@"select P.PersonId, P.FirstName,P.MiddleName,P.LastName
                                from Person P
                           WHERE P.PersonIndex='{id}'";
                var peopleDTO = ExecuteSqlQuery(query, x => new PeopleDTO
                {
                    PersonID = x["PersonId"] == DBNull.Value ? 0 : (long)x["PersonId"],
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"]

                }).FirstOrDefault();
                return peopleDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// GetPeopleSupportListForExternal.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<PeopleSupportExternalDTO> GetPeopleSupportForExternalByPersonId(long personID)
        {
            try
            {

                var query = string.Empty;

                query = @"SELECT PS.FirstName,PS.MiddleName,PS.LastName,PS.PersonSupportID 
                        FROM PersonSupport PS 
                        JOIN [info].[SupportType] ST ON ST.SupportTypeID = PS.SupportTypeID
                        WHERE PS.IsRemoved=0 AND PS.PersonID = " + personID;

                var peopleSupportDetails = ExecuteSqlQuery(query, x => new PeopleSupportExternalDTO
                {
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"],
                    PersonSupportID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"]
                });

                return peopleSupportDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPeopleHelperListForExternal.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<PeopleHelperExternalDTO> GetPeopleHelperForExternalByPersonId(long personID)
        {
            try
            {
                var query = string.Empty;
                query = @"SELECT H.HelperID, PH.PersonHelperID
                            FROM PersonHelper PH 
                            JOIN Helper H ON H.HelperID = PH.HelperID
                        WHERE PH.IsRemoved=0 AND PH.PersonID = " + personID;

                var peopleHelperDetails = ExecuteSqlQuery(query, x => new PeopleHelperExternalDTO
                {
                    HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                    PersonHelperID = x["PersonHelperID"] == DBNull.Value ? 0 : (long)x["PersonHelperID"]
                });

                return peopleHelperDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPeopleCollaborationListForExternal.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public List<PeopleCollaborationExternalDTO> GetPeopleCollaborationForExternalByPersonId(long personId)
        {
            try
            {
                var query = @$"SELECT PC.CollaborationID, PC.PersonCollaborationID
                                  FROM PersonCollaboration PC 
                                  JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                   WHERE PC.IsRemoved = 0 AND PC.PersonId = {personId}";
                var peopleCollaborationDTO = ExecuteSqlQuery(query, x => new PeopleCollaborationExternalDTO
                { 
                    PersonCollaborationID = x["PersonCollaborationID"] == DBNull.Value ? 0 : (long)x["PersonCollaborationID"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"]
                });
                return peopleCollaborationDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}