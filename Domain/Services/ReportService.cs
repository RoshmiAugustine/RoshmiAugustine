// -----------------------------------------------------------------------
// <copyright file="ReportService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class ReportService : IReportService
    {
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        private readonly IReportRepository reportRepository;
        private readonly IPersonRepository personRepository;
        private readonly IAssessmentRepository assessmentRepository;
        /// initializes a new instance of the <see cref="converter"/>
        private readonly IConverter converter;
        private readonly IConfiguration config;
        private readonly IUtility utility;
        private readonly IMapper mapper;
        private readonly IAgencyPowerBIReportRepository agencyPowerBIReportRepository;
        private readonly IConfigurationRepository configurationRepository;
        public ReportService(LocalizeService localizeService, IReportRepository reportRepository, IPersonRepository personRepository, 
            IAssessmentRepository assessmentRepository, IConverter converter, IAgencyPowerBIReportRepository agencyPowerBIReportRepository,
            IMapper mapper, IConfigurationRepository configurationRepository, IConfiguration configuration, IUtility utility)
        {
            this.localize = localizeService;
            this.reportRepository = reportRepository;
            this.personRepository = personRepository;
            this.assessmentRepository = assessmentRepository;
            this.converter = converter;
            this.mapper = mapper;
            this.agencyPowerBIReportRepository = agencyPowerBIReportRepository;
            this.configurationRepository = configurationRepository;
            this.config = configuration;
            this.utility = utility;
        }

        /// <summary>
        /// GetItemReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>ItemDetailsResponseDTO.</returns>
        public ItemDetailsResponseDTO GetItemReportData(ReportInputDTO reportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                ItemDetailsResponseDTO responseDTO = new ItemDetailsResponseDTO();
                if (reportInputDTO.QuestionnaireId <= 0)
                {
                    responseDTO.ItemDetails = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.QuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (reportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.ItemDetails = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (reportInputDTO.VoiceTypeID <= 0)
                {
                    responseDTO.ItemDetails = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (reportInputDTO.PersonQuestionnaireID <= 0)
                {
                    responseDTO.ItemDetails = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonQuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var sharedIDs = GetSharedIDs(reportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                    var helperColbIDs = new SharedDetailsDTO();
                    if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                    {
                        helperColbIDs = this.GetHelperAssessmentIDs(reportInputDTO.PersonIndex, reportInputDTO.QuestionnaireId, userTokenDetails, "");
                    }
                    List<ItemDetailsDTO> response = this.reportRepository.GetItemReportData(reportInputDTO, sharedIDs, helperColbIDs);
                    responseDTO.ItemDetails = response;
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                }
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetStoryMapReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>StoryMapResponseDTO</returns>
        public StoryMapResponseDTO GetStoryMapReportData(ReportInputDTO reportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                StoryMapResponseDTO responseDTO = new StoryMapResponseDTO();
                if (reportInputDTO.QuestionnaireId <= 0)
                {
                    responseDTO.storyMapList = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.QuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (reportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.storyMapList = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (reportInputDTO.VoiceTypeID <= 0)
                {
                    responseDTO.storyMapList = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (reportInputDTO.AssessmentID <= 0)
                {
                    responseDTO.storyMapList = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.AssessmentID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var sharedIDs = GetSharedIDs(reportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                    var helperColbIDs = new SharedDetailsDTO();
                    if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                    {
                        helperColbIDs = this.GetHelperAssessmentIDs(reportInputDTO.PersonIndex, reportInputDTO.QuestionnaireId, userTokenDetails);
                    }
                    List<StoryMapDTO> response = this.reportRepository.GetStoryMapReportData(reportInputDTO, sharedIDs, helperColbIDs);
                    responseDTO.Narration = this.GenerateNarration(response, reportInputDTO.PersonIndex);
                    responseDTO.storyMapList = response;
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                }
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GenerateNarration.
        /// </summary>
        /// <param name="response">response.</param>
        /// <param name="peopleIndex">peopleIndex.</param>
        /// <returns>string.</returns>
        private string GenerateNarration(List<StoryMapDTO> response, Guid peopleIndex)
        {
            string narration = string.Empty;

            string UnderlyingNeed = string.Empty;
            string NeedFocus = string.Empty;
            string StrengthBuild = string.Empty;
            string UseStrength = string.Empty;
            string NeedBackground = string.Empty;

            List<StoryMapDTO> UnderlyingNeedList = (from t in response
                                                    where (t.Type == PCISEnum.ItemResponseType.Exposure || t.Type == PCISEnum.ItemResponseType.SupportExposure || t.Type == PCISEnum.ItemResponseType.Unmodifiable) && t.ToDo == PCISEnum.ToDo.Underlying
                                                    orderby t.Type
                                                    select t).ToList();
            List<StoryMapDTO> NeedFocusList = (from t in response
                                               where (t.Type == PCISEnum.ItemResponseType.Need|| t.Type == PCISEnum.ItemResponseType.SupportNeed) && t.ToDo == PCISEnum.ToDo.Focus
                                               orderby t.Type
                                               select t).Take(5).ToList();
            List<StoryMapDTO> StrengthBuildList = (from t in response
                                                   where (t.Type == PCISEnum.ItemResponseType.Strength|| t.Type == PCISEnum.ItemResponseType.SupportResource) && t.ToDo == PCISEnum.ToDo.Build
                                                   orderby t.Type
                                                   select t).Take(5).ToList();
            List<StoryMapDTO> UseStrengthList = (from t in response
                                                 where (t.Type == PCISEnum.ItemResponseType.Strength || t.Type == PCISEnum.ItemResponseType.SupportResource) && t.ToDo == PCISEnum.ToDo.Use
                                                 orderby t.Type
                                                 select t).Take(3).ToList();
            List<StoryMapDTO> NeedBackgroundList = (from t in response
                                                    where (t.Type == PCISEnum.ItemResponseType.Need || t.Type == PCISEnum.ItemResponseType.SupportNeed) && t.ToDo == PCISEnum.ToDo.Background
                                                    orderby t.Type
                                                    select t).ToList();

            StoryMapDTO last = new StoryMapDTO();
            if (UnderlyingNeedList.Count > 0)
            {
                last = UnderlyingNeedList.Last();
                foreach (StoryMapDTO storyMap in UnderlyingNeedList)
                {
                    if (UnderlyingNeed == string.Empty)
                    {
                        UnderlyingNeed += storyMap.Item;
                    }
                    else if (storyMap.Equals(last))
                    {
                        UnderlyingNeed += " and " + storyMap.Item;
                    }
                    else
                    {
                        UnderlyingNeed += ", " + storyMap.Item;
                    }
                }
                narration = "[Person] has " + (UnderlyingNeedList.Count == 1 ? "an" : "") + " indicated underlying need" + (UnderlyingNeedList.Count == 1 ? "" : "s") + " of [UnderlyingNeed]. ";

                narration = narration.Replace("[UnderlyingNeed]", UnderlyingNeed);
            }
            else
            {
                narration = "[Person] has no indicated underlying needs. ";
            }

            if (NeedFocusList.Count > 0)
            {
                last = NeedFocusList.Last();
                foreach (StoryMapDTO storyMap in NeedFocusList)
                {
                    if (NeedFocus == string.Empty)
                    {
                        NeedFocus += storyMap.Item;
                    }
                    else if (storyMap.Equals(last))
                    {
                        NeedFocus += " and " + storyMap.Item;
                    }
                    else
                    {
                        NeedFocus += ", " + storyMap.Item;
                    }
                }
                narration += "The " + (NeedFocusList.Count == 1 ? "" : "top") + " indicated need" + (NeedFocusList.Count == 1 ? "" : "s") + " for focus for [Person] " + (NeedFocusList.Count == 1 ? " is " : " are ") + " [NeedFocus]. ";
                narration = narration.Replace("[NeedFocus]", NeedFocus);
            }
            else
            {
                narration += "[Person] has no indicated needs for focus. ";
            }
            if (StrengthBuildList.Count > 0)
            {
                last = StrengthBuildList.Last();
                foreach (StoryMapDTO storyMap in StrengthBuildList)
                {
                    if (StrengthBuild == string.Empty)
                    {
                        StrengthBuild += storyMap.Item;
                    }
                    else if (storyMap.Equals(last))
                    {
                        StrengthBuild += " and " + storyMap.Item;
                    }
                    else
                    {
                        StrengthBuild += ", " + storyMap.Item;
                    }
                }
                narration += "The " + (StrengthBuildList.Count == 1 ? "" : "top") + " indicated strength" + (StrengthBuildList.Count == 1 ? "" : "s") + " to build " + (StrengthBuildList.Count == 1 ? "is" : "are") + " [StrengthBuild]. ";
                narration = narration.Replace("[StrengthBuild]", StrengthBuild);
            }
            else
            {
                narration += "There were no indicated strengths to build. ";
            }

            if (UseStrengthList.Count > 0)
            {
                last = UseStrengthList.Last();
                foreach (StoryMapDTO storyMap in UseStrengthList)
                {
                    if (UseStrength == string.Empty)
                    {
                        UseStrength += storyMap.Item;
                    }
                    else if (storyMap.Equals(last))
                    {
                        UseStrength += " and " + storyMap.Item;
                    }
                    else
                    {
                        UseStrength += ", " + storyMap.Item;
                    }
                }
                narration += (UseStrengthList.Count == 1 ? "The indicated strength " : "Indicated strengths ") + " to use for [Person] " + (UseStrengthList.Count == 1 ? "is" : "are") + " [UseStrength]. ";
                narration = narration.Replace("[UseStrength]", UseStrength);
            }
            else
            {
                narration += "There were no indicated strengths to use. ";
            }
            if (NeedBackgroundList.Count > 0)
            {
                last = NeedBackgroundList.Last();
                foreach (StoryMapDTO storyMap in NeedBackgroundList)
                {
                    if (NeedBackground == string.Empty)
                    {
                        NeedBackground += storyMap.Item;
                    }
                    else if (storyMap.Equals(last))
                    {
                        NeedBackground += " and " + storyMap.Item;
                    }
                    else
                    {
                        NeedBackground += ", " + storyMap.Item;
                    }
                }
                narration += "In the background, the following indicated " + (NeedBackgroundList.Count == 1 ? "need is" : "needs are") + " watchful: [NeedBackground]. ";
                narration = narration.Replace("[NeedBackground]", NeedBackground);
            }
            else
            {
                narration += "There were no indicated background needs to watch.";
            }

            var personDetails = this.personRepository.GetPersonalDetails(peopleIndex);
            string personName = string.Empty;
            personName = personDetails.FirstName + (String.IsNullOrEmpty(personDetails.MiddleName) ? "" : " " + personDetails.MiddleName) + " " + personDetails.LastName;
            narration = narration.Replace("[Person]", personName);

            return narration;
        }

        /// <summary>
        /// GetPersonStrengthFamilyReportData
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>PersonStrengthReportResponseDTO.</returns>
        public PersonStrengthReportResponseDTO GetPersonStrengthFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                PersonStrengthReportResponseDTO responseDTO = new PersonStrengthReportResponseDTO();
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (familyReportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.PersonStrengthReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeID < 0)
                {
                    responseDTO.PersonStrengthReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeFKID < 0)
                {
                    responseDTO.PersonStrengthReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeFKID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonCollaborationID < 0)
                {
                    responseDTO.PersonStrengthReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonCollaborationID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonQuestionnaireID < 0)
                {
                    responseDTO.PersonStrengthReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonQuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var person = this.personRepository.GetPersonalDetails(familyReportInputDTO.PersonIndex);
                    if (person != null)
                    {
                        var sharedIDs = GetSharedIDs(familyReportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                        var helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(sharedIDs.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                        }
                        var latestAssessments = this.assessmentRepository.GetLastAssessmentByPerson(person.PersonID, familyReportInputDTO.PersonQuestionnaireID, familyReportInputDTO.PersonCollaborationID, familyReportInputDTO.VoiceTypeID, familyReportInputDTO.VoiceTypeFKID, sharedIDs, userTokenDetails.AgencyID, helperColbIDs, 0);
                        if (latestAssessments?.Count > 0)
                        {
                            var latestAssessmentIDs = latestAssessments?.Select(x => x.AssessmentID).ToList();
                            var response = this.reportRepository.GetPersonStrengthFamilyReportData(person.PersonID, familyReportInputDTO, latestAssessmentIDs);
                            responseDTO.PersonStrengthReport = response;
                            responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }
                }
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PersonNeedReportResponseDTO GetPersonNeedsFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                PersonNeedReportResponseDTO responseDTO = new PersonNeedReportResponseDTO();
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (familyReportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.PersonNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeID < 0)
                {
                    responseDTO.PersonNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeFKID < 0)
                {
                    responseDTO.PersonNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeFKID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonCollaborationID < 0)
                {
                    responseDTO.PersonNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonCollaborationID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonQuestionnaireID < 0)
                {
                    responseDTO.PersonNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonQuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var person = this.personRepository.GetPersonalDetails(familyReportInputDTO.PersonIndex);
                    if (person != null)
                    {
                        var sharedIDs = GetSharedIDs(familyReportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                        var helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(sharedIDs.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                        }
                        var latestAssessments = this.assessmentRepository.GetLastAssessmentByPerson(person.PersonID, familyReportInputDTO.PersonQuestionnaireID, familyReportInputDTO.PersonCollaborationID, familyReportInputDTO.VoiceTypeID, familyReportInputDTO.VoiceTypeFKID, sharedIDs, userTokenDetails.AgencyID, helperColbIDs, 0);
                        if (latestAssessments?.Count > 0)
                        {
                            var latestAssessmentIDs = latestAssessments?.Select(x => x.AssessmentID).ToList();
                            var response = this.reportRepository.GetPersonNeedsFamilyReportData(person.PersonID, familyReportInputDTO, latestAssessmentIDs);
                            responseDTO.PersonNeedsReport = response;
                            responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }
                }
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public SupportResourceReportResponseDTO GetSupportResourcesFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                SupportResourceReportResponseDTO responseDTO = new SupportResourceReportResponseDTO();
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (familyReportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.SupportResourcesReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeID < 0)
                {
                    responseDTO.SupportResourcesReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeFKID < 0)
                {
                    responseDTO.SupportResourcesReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeFKID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonCollaborationID < 0)
                {
                    responseDTO.SupportResourcesReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonCollaborationID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonQuestionnaireID < 0)
                {
                    responseDTO.SupportResourcesReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonQuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var person = this.personRepository.GetPersonalDetails(familyReportInputDTO.PersonIndex);
                    if (person != null)
                    {
                        var sharedIDs = GetSharedIDs(familyReportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                        var helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(sharedIDs.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                        }
                        var latestAssessments = this.assessmentRepository.GetLastAssessmentByPerson(person.PersonID, familyReportInputDTO.PersonQuestionnaireID, familyReportInputDTO.PersonCollaborationID, familyReportInputDTO.VoiceTypeID, familyReportInputDTO.VoiceTypeFKID,sharedIDs, userTokenDetails.AgencyID, helperColbIDs, 0);
                        if (latestAssessments?.Count > 0)
                        {
                            var latestAssessmentIDs = latestAssessments?.Select(x => x.AssessmentID).ToList();
                            var response = this.reportRepository.GetSupportResourcesFamilyReportData(person.PersonID, familyReportInputDTO, latestAssessmentIDs);
                            responseDTO.SupportResourcesReport = response;
                            responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }
                }
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public SupportNeedsReportResponseDTO GetSupportNeedsFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                SupportNeedsReportResponseDTO responseDTO = new SupportNeedsReportResponseDTO();
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (familyReportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.SupportNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeID < 0)
                {
                    responseDTO.SupportNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeFKID < 0)
                {
                    responseDTO.SupportNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeFKID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonCollaborationID < 0)
                {
                    responseDTO.SupportNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonCollaborationID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonQuestionnaireID < 0)
                {
                    responseDTO.SupportNeedsReport = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonQuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var person = this.personRepository.GetPersonalDetails(familyReportInputDTO.PersonIndex);
                    if (person != null)
                    {
                        var sharedIDs = GetSharedIDs(familyReportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                        var helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(sharedIDs.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                        }
                        var latestAssessments = this.assessmentRepository.GetLastAssessmentByPerson(person.PersonID, familyReportInputDTO.PersonQuestionnaireID, familyReportInputDTO.PersonCollaborationID, familyReportInputDTO.VoiceTypeID, familyReportInputDTO.VoiceTypeFKID,sharedIDs, userTokenDetails.AgencyID, helperColbIDs, 0);
                        if (latestAssessments?.Count > 0)
                        {
                            var latestAssessmentIDs = latestAssessments?.Select(x => x.AssessmentID).ToList();
                            var response = this.reportRepository.GetSupportNeedsFamilyReportData(person.PersonID, familyReportInputDTO, latestAssessmentIDs);
                            responseDTO.SupportNeedsReport = response;
                            responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }
                }
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FamilyReportStatusResponseDTO GetFamilyReportStatus(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails)
        {
            try
            {
                FamilyReportStatusResponseDTO responseDTO = new FamilyReportStatusResponseDTO();
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (familyReportInputDTO.PersonIndex == Guid.Empty)
                {
                    responseDTO.FamilyReportStatus = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeID < 0)
                {
                    responseDTO.FamilyReportStatus = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.VoiceTypeFKID < 0)
                {
                    responseDTO.FamilyReportStatus = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.VoiceTypeFKID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonCollaborationID < 0)
                {
                    responseDTO.FamilyReportStatus = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonCollaborationID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else if (familyReportInputDTO.PersonQuestionnaireID < 0)
                {
                    responseDTO.FamilyReportStatus = null;
                    responseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PersonQuestionnaireID));
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                }
                else
                {
                    var person = this.personRepository.GetPersonalDetails(familyReportInputDTO.PersonIndex);
                    if (person != null)
                    {
                        var sharedIDs = GetSharedIDs(familyReportInputDTO.PersonIndex, userTokenDetails.AgencyID);
                        var helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(sharedIDs.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                        }
                        var latestAssessments = this.assessmentRepository.GetLastAssessmentByPerson(person.PersonID, familyReportInputDTO.PersonQuestionnaireID, familyReportInputDTO.PersonCollaborationID, familyReportInputDTO.VoiceTypeID, familyReportInputDTO.VoiceTypeFKID,sharedIDs, userTokenDetails.AgencyID, helperColbIDs, 0);
                        if (latestAssessments?.Count > 0)
                        {
                            var latestAssessmentIDs = latestAssessments?.Select(x => x.AssessmentID).ToList();
                            var response = this.reportRepository.GetFamilyReportStatus(person.PersonID, familyReportInputDTO, latestAssessmentIDs);
                            responseDTO.FamilyReportStatus = response;
                            responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }
                }
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetHtmlString(string htmlData)
        {
            var sb = new StringBuilder();
            sb.Append(htmlData);
            return sb.ToString();
        }

        public HTMLToPDFResponseDTO HTMLToPDFByteArrayConversion(PdfReportDTO htmlData)
        {
            try
            {
                var responseDTO = new HTMLToPDFResponseDTO();
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (!string.IsNullOrEmpty(htmlData.innerHTML))
                {
                    var globalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings { Top = 10 },
                        DocumentTitle = "PDF Report",
                    };

                    var objectSettings = new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = htmlData.innerHTML,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontName = "Montserrat-Regular", FontSize = 9, Right = "Page [Page] of [toPage]", Line = true },
                        FooterSettings = { FontName = "Montserrat-Regular", FontSize = 9, Center = htmlData.ReportName },
                    };

                    var pdf = new HtmlToPdfDocument
                    {
                        GlobalSettings = globalSettings,
                        Objects = { objectSettings },
                    };

                    var file = converter.Convert(pdf);
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    responseDTO.PDFByteArray = file;
                }
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }
        private SharedDetailsDTO GetSharedIDs(Guid personIndex, long agencyID)
        {
            try
            {
                var person = this.personRepository.GetPersonalDetails(personIndex);
                var sharedIDs = this.personRepository.GetSharedPersonQuestionnaireDetails(person.PersonID, agencyID);
                sharedIDs.PersonID = person.PersonID;
                return sharedIDs;
            }
            catch (Exception)
            {

                throw;
            }
            
        }        

        public AssessedQuestionnairesForPersonDTO GetAllQuestionnairesForSuperStoryMap(UserTokenDetails userTokenDetails, Guid personIndex, long personCollaborationID, int pageNumber, int pageSize)
        {
            try
            {
                AssessedQuestionnairesForPersonDTO assessedQuestionnairesForPersonDTO = new AssessedQuestionnairesForPersonDTO();
                if (personIndex != null)
                {
                    var response = this.personRepository.GetPersonalDetails(personIndex);
                    if (response.PersonID != 0 && userTokenDetails.AgencyID > 0)
                    {
                        var sharedIDs = this.personRepository.GetSharedPersonQuestionnaireDetails(response.PersonID, userTokenDetails.AgencyID);
                        var helperAssessmentsIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperAssessmentsIDs = this.GetHelperAssessmentIDs(personIndex, 0, userTokenDetails);
                        }
                        var queryResult = this.reportRepository.GetAllQuestionnairesForSuperStoryMap(response.PersonID, personCollaborationID, pageNumber, pageSize, sharedIDs, helperAssessmentsIDs);
                        if (queryResult != null && queryResult.Item1.Count > 0)
                        {
                            assessedQuestionnairesForPersonDTO.QuestionnaireData = queryResult.Item1.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                            assessedQuestionnairesForPersonDTO.TotalCount = queryResult.Item2;
                            var allItems = queryResult.Item1.Select(x => x.AssessmentID).ToArray();
                            assessedQuestionnairesForPersonDTO.SuperStoryAssessmentIDs = string.Join(",", allItems);
                            assessedQuestionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                            assessedQuestionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        }
                    }
                    else
                    {
                        assessedQuestionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        assessedQuestionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    }
                }
                return assessedQuestionnairesForPersonDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SuperStoryResponseDTO GetAllDetailsForSuperStoryMap(SuperStoryInputDTO superStoryInputDTO, long agencyID)
        {
            try
            {
                SuperStoryResponseDTO response = new SuperStoryResponseDTO();
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                var person = this.personRepository.GetPersonalDetails(superStoryInputDTO.PersonIndex);
                if (person.PersonID != 0 && agencyID > 0 && !string.IsNullOrEmpty(superStoryInputDTO.AssessmentIDs))
                {
                    var sharedIDs = this.personRepository.GetSharedPersonQuestionnaireDetails(person.PersonID, agencyID);
                    superStoryInputDTO.PersonID = person.PersonID;
                    var superStoryresult = this.reportRepository.GetAllDetailsForSuperStoryMap(superStoryInputDTO, sharedIDs);

                    //Underlying Items
                    var underlyingItems = superStoryresult.Where(x => (x.Type == PCISEnum.ItemResponseType.Unmodifiable || x.Type == PCISEnum.ItemResponseType.Exposure || x.Type == PCISEnum.ItemResponseType.SupportExposure || x.Type == PCISEnum.ItemResponseType.SupportUnmodifiable) && x.ToDo == PCISEnum.ToDo.Underlying).ToList();
                    response.UnderlyingItemGroups = GetGrouping(underlyingItems);

                    //Need for Focus
                    var needForFocusItems = superStoryresult.Where(x => (x.Type == PCISEnum.ItemResponseType.Need || x.Type == PCISEnum.ItemResponseType.SupportNeed) && x.ToDo == PCISEnum.ToDo.Focus).ToList();
                    response.NeedForFocusItemGroups = GetGrouping(needForFocusItems);

                    //Need in Background
                    var needInBackgroundItems = superStoryresult.Where(x => (x.Type == PCISEnum.ItemResponseType.Need || x.Type == PCISEnum.ItemResponseType.SupportNeed) && x.ToDo == PCISEnum.ToDo.Background).ToList();
                    response.NeedInBackgroundItemGroups = GetGrouping(needInBackgroundItems);

                    //stregth to Build/use
                    var strengthItems = superStoryresult.Where(x => (x.Type == PCISEnum.ItemResponseType.Strength || x.Type == PCISEnum.ItemResponseType.SupportResource) && (x.ToDo == PCISEnum.ToDo.Build || x.ToDo == PCISEnum.ToDo.Use)).ToList();
                    response.StrengthItemGroups = GetGrouping(strengthItems);

                    //Circumstances                 
                    var circumstancesItems = superStoryresult.Where(x => x.Type == PCISEnum.ItemResponseType.Circumstance || x.Type == PCISEnum.ItemResponseType.Preference).ToList();
                    response.CircumstancesItemGroups = GetGrouping(circumstancesItems);

                    //Goals
                    var goalsItems = superStoryresult.Where(x => x.Type == PCISEnum.ItemResponseType.Goal).ToList();
                    response.GoalsItemGroups = GetGrouping(goalsItems);

                    //Opinions
                    var opinionItems = superStoryresult.Where(x => x.Type == PCISEnum.ItemResponseType.Opinion).ToList();
                    response.OpinionItemGroups = GetGrouping(opinionItems);
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<List<SuperStoryMapDTO>> GetGrouping(List<SuperStoryMapDTO> storyMapItems)
        {
            try
            {
                var resultGroup = new List<List<SuperStoryMapDTO>>();
                if (storyMapItems != null && storyMapItems?.Count > 0)
                {
                    resultGroup = storyMapItems.ToLookup(x => new { x.InstrumentAbbrev, x.QuestionnaireID, x.VoiceTypeInDetail, x.VoiceTypeFKID }).OrderBy(x=>x.Key.QuestionnaireID).Select(group => group.ToList()).ToList();
                }
                return resultGroup;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private SharedDetailsDTO GetHelperAssessmentIDs(Guid personIndex, int questionnaireId, UserTokenDetails userTokenDetails, string queryType = "Reports")
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                SharedPersonSearchDTO sharedPersonSearchDTO = new SharedPersonSearchDTO();
                sharedPersonSearchDTO.QuestionnaireID = questionnaireId;
                sharedPersonSearchDTO.PersonIndex = personIndex;
                sharedPersonSearchDTO.LoggedInAgencyID = userTokenDetails.AgencyID;
                sharedPersonSearchDTO.UserID = userTokenDetails.UserID;
                sharedPersonSearchDTO.QueryType = queryType;
                var sharedDetails = this.personRepository.GetHelpersAssessmentIDs(sharedPersonSearchDTO);
                return sharedDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAllPowerBIReportsForAgency.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="instrumentId"></param>
        /// <returns></returns>
        public AgencyPowerBIReportResponseDTO GetAllPowerBIReportsForAgency(long agencyId, int instrumentId)
        {
            try
            {
                AgencyPowerBIReportResponseDTO responseDTO = new AgencyPowerBIReportResponseDTO();
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                var powerbiReportList = this.agencyPowerBIReportRepository.GetAllPowerBIReportsForAgency(agencyId, instrumentId);

                responseDTO.PowerBiReports = this.mapper.Map<List<AgencyPowerBIReportDTO>>(powerbiReportList);
                return responseDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPowerBIReportURLByReportID.
        /// This function calls the token generating Azure function for the power bi reports.
        /// https://docs.microsoft.com/en-us/power-bi/paginated-reports/report-builder-url-pass-parameters
        /// </summary>
        /// <param name="powerBiInputDTO"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public PowerBIReportURLResponseDTO GetPowerBIReportURLByReportID(PowerBiInputDTO powerBiInputDTO, long agencyId, int userId)
        {
            try
            {
                PowerBIReportURLResponseDTO responseDTO = new PowerBIReportURLResponseDTO();
                var worksapceId = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.PowerBIWorkspaceId, agencyId);               
                var reportDetails = this.agencyPowerBIReportRepository.GetPowerBIReportDetailsById(powerBiInputDTO.AgencyPowerBIReportId, agencyId);                
                var tokenAPIURL = this.config["PowerBITokenAPI"];
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                Guid workspaceGuid;
                if (reportDetails?.AgencyPowerBIReportId == 0)
                {
                    var msg = string.Format(PCISEnum.StatusMessages.PowerBIValidation, "Report Id");
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(msg);
                    return responseDTO;
                }
                if (!Guid.TryParse(worksapceId?.Value, out workspaceGuid))
                {
                    var msg = string.Format(PCISEnum.StatusMessages.PowerBIValidation, "Workspace Id");
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(msg);
                    return responseDTO;
                }
                if (string.IsNullOrWhiteSpace(tokenAPIURL))
                {
                    var msg = string.Format(PCISEnum.StatusMessages.PowerBIValidation, "token retreiving API Url");
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(msg);
                    return responseDTO;
                }
                string filters = reportDetails?.FiltersOrParameters;
                if (!string.IsNullOrWhiteSpace(reportDetails?.FiltersOrParameters))
                {
                    var filterDetails = this.agencyPowerBIReportRepository.GetFilterReplaceDetailsForPowerBI(powerBiInputDTO);
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.CollaborationName, filterDetails.CollaborationName);
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.CollaborationId, powerBiInputDTO.CollaborationId.ToString());
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.VoiceTypeName, filterDetails.VoiceTypeName);
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.QuestionnaireName, filterDetails.QuestionnaireName);
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.QuestionnaireId, powerBiInputDTO.QuestionnaireId.ToString());
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.PersonId, filterDetails.PersonId.ToString());
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.PersonIndex, powerBiInputDTO.PersonIndex.ToString());
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.UserId, userId.ToString());
                    filters = filters.Replace(PCISEnum.PowerBIFilterReplace.AgencyId, agencyId.ToString());
                }
                //Preparing Report Parameters as query string to be appended on to emebedURl.
                string reportParameters = string.Empty;
                if(reportDetails.IsRDLReport && !string.IsNullOrWhiteSpace(filters))
                {                    
                    var parameterObj = JsonConvert.DeserializeObject<List<PowerBiParameterDTO>>(filters);
                    foreach (var column in parameterObj)
                    {
                        foreach (var value in column.Values)
                        {
                            //space should be replaced with a plus sign (+) for query parameters in power bi.
                            var paramValue = value.Replace(" ", "+");
                            reportParameters += string.Format(PCISEnum.PowerBIFilterReplace.Parameter, column.Column, paramValue);
                        }
                    }
                }
                dynamic data = new ExpandoObject();
                data.reportId = reportDetails.ReportId;
                data.workspaceId = workspaceGuid;
                string json = JsonConvert.SerializeObject(data);
                try
                {
                    var tokenResponse = this.utility.RestApiCall(tokenAPIURL, PCISEnum.APIMethodType.PostRequest, json);
                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        var result = tokenResponse.Content.ReadAsStringAsync().Result;
                        responseDTO.PowerBiReportURLData = JsonConvert.DeserializeObject<PowerBiReportURLDTO>(result);
                        if (responseDTO.PowerBiReportURLData != null)
                        {
                            responseDTO.PowerBiReportURLData.EmbedUrl += reportParameters;
                            responseDTO.PowerBiReportURLData.Filters = filters;
                            responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                            responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.PowerBIFailure);
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                }
                return responseDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
