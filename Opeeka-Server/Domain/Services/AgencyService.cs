// -----------------------------------------------------------------------
// <copyright file="ConsumerService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
    public class AgencyService : BaseService, IAgencyService
    {
        /// Initializes a new instance of the agencyRepository/> class.
        private readonly IAgencyRepository agencyRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private readonly IAddressrepository addressRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<AgencyService> logger;

        /// Initializes a new instance of the agencyAddressRepository/> class.
        private readonly IAgencyAddressRepository agencyAddressRepository;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly IHttpContextAccessor httpContext;

        /// <summary>
        /// Defines the queryBuilder.
        /// </summary>
        private readonly IQueryBuilder queryBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyService"/> class.
        /// </summary>
        /// <param name="agencyRepository,addressRepository,agencyAddressRepository,logger">consumerRepository.</param>
        public AgencyService(IAgencyRepository agencyRepository, IAddressrepository addressRepository, IAgencyAddressRepository agencyAddressRepository, ILogger<AgencyService> logger, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IQueryBuilder queryBuilder)
            : base(configRepo, httpContext)
        {
            this.agencyRepository = agencyRepository;
            this.addressRepository = addressRepository;
            this.agencyAddressRepository = agencyAddressRepository;
            this.logger = logger;
            this.localize = localizeService;
            this.httpContext = httpContext;
            this.queryBuilder = queryBuilder;
        }

        /// <summary>
        /// To update Agency.
        /// </summary>
        /// <param name="agencyDetailsDTO">consumerRepository.</param>
        /// <returns>ResultDTO.</returns>
        public CRUDResponseDTO RemoveAgencyDetails(Guid AgencyIndexId)
        {
            try
            {
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                if (AgencyIndexId != Guid.Empty)
                {
                    AgencyDTO agencyDTO = this.agencyRepository.GetAsync(AgencyIndexId).Result;
                    if (agencyDTO != null)
                    {
                        agencyDTO.IsRemoved = true;
                        var agencyresult = this.agencyRepository.UpdateAgency(agencyDTO);
                        if (agencyresult != null)
                        {
                            if (agencyDTO.AgencyID != 0)
                            {
                                AddressDTO addressDTO = new AddressDTO();
                                addressDTO.AddressID = this.agencyAddressRepository.GetAgencyAddress(agencyDTO.AgencyID).Result.AddressID;
                                if (addressDTO.AddressID != 0)
                                {
                                    addressDTO = this.addressRepository.GetAddress(addressDTO.AddressID).Result;
                                    if (addressDTO != null)
                                    {
                                        addressDTO.IsRemoved = true;
                                        var addressResult = this.addressRepository.UpdateAddress(addressDTO);
                                        if (addressResult != null)
                                        {

                                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);

                                        }
                                        else
                                        {

                                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                                        }
                                    }

                                }

                            }
                        }
                        else
                        {

                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                        }
                    }
                }
                return resultDTO;
            }

            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// GetAgencyList
        /// </summary>
        /// <param name="pageNumber">pageNumber</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="roles">roles</param>
        /// <returns></returns>
        public GetAgencyListDTO GetAgencyList(AgencySearchDTO agencySearchDTO, long agencyID, List<string> roles)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetAgencyListConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(agencySearchDTO.SearchFilter, fieldMappingList);
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                GetAgencyListDTO getAgencyDTO = new GetAgencyListDTO();
                List<AgencyDTO> response = new List<AgencyDTO>();
                bool getAllAgency = false;
                if (agencySearchDTO.pageNumber <= 0)
                {
                    getAgencyDTO.AgencyList = null;
                    getAgencyDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getAgencyDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getAgencyDTO;
                }
                else if (agencySearchDTO.pageLimit <= 0)
                {
                    getAgencyDTO.AgencyList = null;
                    getAgencyDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getAgencyDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getAgencyDTO;
                }
                else
                {
                    if (roles.Contains(PCISEnum.Roles.SuperAdmin))
                    {
                        getAllAgency = true;
                    }
                    if (getAllAgency)
                    {
                        var agencyResponse = this.agencyRepository.GetAgencyList(agencySearchDTO, queryBuilderDTO);
                        getAgencyDTO.AgencyList = agencyResponse.Item1;
                        var count = agencyResponse.Item2;
                        getAgencyDTO.TotalCount = count;
                    }
                    else
                    {
                        response = this.agencyRepository.GetAgencyListForOrgAdmin(agencySearchDTO.pageNumber, agencySearchDTO.pageLimit, agencyID);
                        if (response.Count > 0)
                        {
                            getAgencyDTO.AgencyList = response;
                        }
                        var count = this.agencyRepository.GetAgencyListForOrgAdminCount(agencyID);
                        getAgencyDTO.TotalCount = count;
                    }
                    getAgencyDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getAgencyDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getAgencyDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetAgencyDetails.
        /// </summary>
        /// <param name="agencyIndex">agencyIndex.</param>
        /// <returns>GetAgencyDetailsDTO.</returns>
        public GetAgencyDetailsDTO GetAgencyDetails(Guid agencyIndex)
        {
            try
            {
                GetAgencyDetailsDTO AgencyDetails = new GetAgencyDetailsDTO();
                if (agencyIndex != Guid.Empty)
                {
                    var response = this.agencyRepository.GetAgencyDetails(agencyIndex);
                    AgencyDetails.AgencyData = response;
                    AgencyDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    AgencyDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;

                    return AgencyDetails;
                }
                else
                {
                    AgencyDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.AgencyIndex));
                    AgencyDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;

                    return AgencyDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// To create AgencyAddressDTO.
        /// </summary>
        /// <param name="addressId,AgencyId">consumerRepository.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public AgencyAddressDTO CreateAgencyAddress(long AgencyID, long AddressID, int UpdateUserID)
        {
            AgencyAddressDTO agencyAddressDTO = new AgencyAddressDTO();
            if (AgencyID != 0 && AddressID != 0)
            {
                agencyAddressDTO.AgencyID = AgencyID;
                agencyAddressDTO.AddressID = AddressID;
                agencyAddressDTO.IsPrimary = true;
                agencyAddressDTO.UpdateUserID = UpdateUserID;
                return agencyAddressDTO;
            }
            else
            {
                return null;

            }
        }

        /// <summary>
        /// To create AgencyDTO.
        /// </summary>
        /// <param name="agencyDetailsDTO">consumerRepository.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public AgencyDTO CreateAgency(AgencyDetailsDTO agencyDetailsDTO, string from)
        {
            AgencyDTO agencyDTO = new AgencyDTO();
            if (from == PCISEnum.Constants.Edit)
            {
                if (agencyDetailsDTO.AgencyID != 0)
                {
                    agencyDTO.AgencyID = agencyDetailsDTO.AgencyID;
                }
            }
            if (String.IsNullOrEmpty(agencyDetailsDTO.Name) || agencyDetailsDTO.UpdateUserID == 0)
            {
                return null;

            }
            else
            {
                agencyDTO.Name = agencyDetailsDTO.Name;
                if (from == PCISEnum.Constants.Edit)
                {
                    agencyDTO.AgencyIndex = agencyDetailsDTO.AgencyIndex;
                    agencyDTO.UpdateDate = DateTime.UtcNow;
                }
                agencyDTO.UpdateUserID = agencyDetailsDTO.UpdateUserID;
                agencyDTO.Email = agencyDetailsDTO.Email;
                agencyDTO.Phone1 = agencyDetailsDTO.Phone1;
                agencyDTO.Phone2 = agencyDetailsDTO.Phone2;
                agencyDTO.ContactFirstName = agencyDetailsDTO.ContactFirstName;
                agencyDTO.ContactLastName = agencyDetailsDTO.ContactLastName;
                agencyDTO.IsCustomer = true;
                agencyDTO.IsRemoved = false;
                agencyDTO.Note = agencyDetailsDTO.Note;
                var result = this.agencyRepository.GetAgencyDetailsByAbbrev(agencyDetailsDTO.Abbrev).Result;
                if (result.AgencyID == agencyDTO.AgencyID || result.AgencyID == 0)
                {
                    agencyDTO.Abbrev = agencyDetailsDTO.Abbrev;
                }
                else
                {
                    agencyDTO.Abbrev = null;
                }
            }
            return agencyDTO;

        }

        /// <summary>
        /// TO add a new Agency.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddAgencyDetails(AgencyDetailsDTO agencyDetailsDTO)
        {
            try
            {
                var isSuccess = false;
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();

                bool validateAgencyName = this.agencyRepository.ValidateAgencyName(agencyDetailsDTO).Result;
                if (validateAgencyName)
                {
                    if (agencyDetailsDTO.Abbrev != null)
                    {
                        bool validateAgencyAbbrev = this.agencyRepository.ValidateAgencyAbbrev(agencyDetailsDTO).Result;
                        if (validateAgencyAbbrev)
                        {
                            AgencyDTO agencyDTO = AssignAgencyValues(agencyDetailsDTO, PCISEnum.Constants.Add);
                            if (agencyDTO.Abbrev != null)
                            {
                                var agencyID = this.agencyRepository.AddAgency(agencyDTO);
                                AddressDTO addressDTO = CreateAddress(agencyDetailsDTO, PCISEnum.Constants.Add);
                                if (addressDTO.UpdateUserID != 0)
                                {
                                    var addressID = this.addressRepository.AddAddress(addressDTO);
                                    AgencyAddressDTO agencyAddressDTO = CreateAgencyAddress(agencyID, addressID, addressDTO.UpdateUserID);
                                    if (agencyAddressDTO.AgencyID != 0)
                                    {
                                        var AgencyAddressID = this.agencyAddressRepository.AddAgencyAddress(agencyAddressDTO);
                                        if (AgencyAddressID != 0)
                                        {
                                            isSuccess = true;
                                        }
                                    }
                                }
                            }
                            if (isSuccess)
                            {
                                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                            }
                            else
                            {
                                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                            }
                        }
                        else
                        {
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InvalidAbbrev;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidAbbrev);
                        }
                    }
                    else
                    {
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InvalidAbbrev;
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidAbbrev);
                    }
                }
                else
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InvalidName;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidName);
                }
                return resultDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// To update exiting Agency.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateAgencyDetails(AgencyDetailsDTO agencyDetailsDTO)
        {
            try
            {
                var valid = false;
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                if (agencyDetailsDTO.AgencyIndex != Guid.Empty)
                {
                    bool validateAgencyName = this.agencyRepository.ValidateAgencyName(agencyDetailsDTO).Result;
                    if (validateAgencyName)
                    {
                        if (agencyDetailsDTO.Abbrev != null)
                        {
                            bool validateAgencyAbbrev = this.agencyRepository.ValidateAgencyAbbrev(agencyDetailsDTO).Result;
                            if (validateAgencyAbbrev)
                            {
                                AgencyDTO agencyDTO = AssignAgencyValues(agencyDetailsDTO, PCISEnum.Constants.Edit);
                                if (agencyDTO != null)
                                {
                                    var agencyresult = this.agencyRepository.UpdateAgency(agencyDTO);
                                    if (agencyresult != null && agencyDTO.AgencyID != 0)
                                    {
                                        AddressDTO addressDTO = CreateAddress(agencyDetailsDTO, PCISEnum.Constants.Edit);
                                        addressDTO.AddressID = this.agencyAddressRepository.GetAgencyAddress(agencyDTO.AgencyID).Result.AddressID;
                                        if (addressDTO.AddressID != 0)
                                        {
                                            addressDTO.AddressIndex = this.addressRepository.GetAddress(addressDTO.AddressID).Result.AddressIndex;
                                            if (addressDTO.AddressIndex != Guid.Empty)
                                            {
                                                var addressResult = this.addressRepository.UpdateAddress(addressDTO);
                                                if (addressResult != null)
                                                {
                                                    valid = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (valid)
                                {
                                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                                }
                                else
                                {
                                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);

                                }
                            }
                            else
                            {
                                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InvalidAbbrev;
                                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidAbbrev);
                            }
                        }
                        else
                        {
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InvalidAbbrev;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidAbbrev);
                        }
                    }
                    else
                    {
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InvalidName;
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidName);
                    }
                }
                else
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    resultDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.AgencyIndex);
                }

                return resultDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// AssignAgencyValues.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <param name="from">from</param>
        /// <returns>AgencyDTO.</returns>
        public AgencyDTO AssignAgencyValues(AgencyDetailsDTO agencyDetailsDTO, string from)
        {
            AgencyDTO agencyDTO = new AgencyDTO();
            if (from == PCISEnum.Constants.Edit)
            {
                var agencyDetails = this.agencyRepository.GetAsync(agencyDetailsDTO.AgencyIndex).Result;
                agencyDTO.AgencyID = agencyDetails.AgencyID;
                agencyDTO.AgencyIndex = agencyDetailsDTO.AgencyIndex;
                agencyDTO.UpdateDate = DateTime.UtcNow;
            }
            agencyDTO.Name = agencyDetailsDTO.Name;
            agencyDTO.UpdateUserID = agencyDetailsDTO.UpdateUserID;
            agencyDTO.Email = agencyDetailsDTO.Email;
            agencyDTO.Phone1 = agencyDetailsDTO.Phone1;
            agencyDTO.Phone2 = agencyDetailsDTO.Phone2;
            agencyDTO.ContactFirstName = agencyDetailsDTO.ContactFirstName;
            agencyDTO.ContactLastName = agencyDetailsDTO.ContactLastName;
            agencyDTO.IsCustomer = true;
            agencyDTO.IsRemoved = false;
            agencyDTO.Note = agencyDetailsDTO.Note;
            agencyDTO.Abbrev = agencyDetailsDTO.Abbrev;
            return agencyDTO;
        }

        /// <summary>
        /// To create AgencyAddressDTO.
        /// </summary>
        /// <param name="agencyDetailsDTO">consumerRepository.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public AddressDTO CreateAddress(AgencyDetailsDTO agencyDetailsDTO, string from)
        {
            AddressDTO addressDTO = new AddressDTO();

            if (agencyDetailsDTO.UpdateUserID == 0 || agencyDetailsDTO.CountryId == 0)
            {
                return null;
            }
            else
            {
                addressDTO.CountryID = agencyDetailsDTO.CountryId;
                addressDTO.CountryStateID = agencyDetailsDTO.CountryStateID == 0 ? null : (int?)agencyDetailsDTO.CountryStateID;
                if (from == PCISEnum.Constants.Edit)
                {
                    addressDTO.AddressIndex = agencyDetailsDTO.AddressIndex;
                    addressDTO.UpdateDate = DateTime.UtcNow;
                }
                addressDTO.UpdateUserID = agencyDetailsDTO.UpdateUserID;
                addressDTO.Address1 = agencyDetailsDTO.Address1;
                addressDTO.Address2 = agencyDetailsDTO.Address2;
                addressDTO.City = agencyDetailsDTO.City;
                addressDTO.Zip = agencyDetailsDTO.Zip;
                addressDTO.Zip4 = agencyDetailsDTO.Zip4;
                addressDTO.IsPrimary = true;
                addressDTO.IsRemoved = false;

                return addressDTO;

            }
        }

        /// <summary>
        /// GetCollaborationListConfigurationForExternal.
        /// Always the First item to the list should be the column deatils for order by (With fieldTable as OrderBy for just user identification).
        /// And the next item should be the fieldMapping for order by Column specified above.
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetAgencyListConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Agency",
                fieldAlias = "name",
                fieldTable = "T1",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(T1.ContactFirstName,'') + ISNULL(' ' + T1.ContactLastName,'')",
                fieldAlias = "contactFirstName",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Abbrev",
                fieldAlias = "abbrev",
                fieldTable = "T1",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Phone1",
                fieldAlias = "phone1",
                fieldTable = "T1",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Email",
                fieldAlias = "email",
                fieldTable = "T1",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NumberOfAddresses",
                fieldAlias = "numberOfAddresses",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NumberOfCollaboration",
                fieldAlias = "numberOfCollaboration",
                fieldTable = "",
                fieldType = "number"
            });
            return fieldMappingList;

        }

        /// <summary>
        /// Get All Agency Lookup
        /// </summary>
        /// <returns></returns>
        public AgencyLookupResponseDTO GetAllAgencyLookup()
        {
            try
            {
                AgencyLookupResponseDTO agencyLookupResponseDTO = new AgencyLookupResponseDTO();
                List<AgencyLookupDTO> AgencyLookupDTO = new List<AgencyLookupDTO>();
                AgencyLookupDTO = this.agencyRepository.GetAllAgencyLookup();
                if (AgencyLookupDTO.Count > 0)
                {
                    agencyLookupResponseDTO.AgencyLookup=AgencyLookupDTO;
                }
                agencyLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                agencyLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return agencyLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
