// -----------------------------------------------------------------------
// <copyright file="PersonSupportRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonSupportRepository : BaseRepository<PersonSupport>, IPersonSupportRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonSupportRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSupportRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonSupportRepository(ILogger<PersonSupportRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// To add personSupport details.
        /// </summary>
        /// <param name="personSupportDTO"></param>
        /// <returns>Guid.</returns>
        public int AddPersonSupport(PersonSupportDTO personSupportDTO)
        {
            try
            {
                PersonSupport personSupport = new PersonSupport();
                this.mapper.Map<PersonSupportDTO, PersonSupport>(personSupportDTO, personSupport);
                var result = this.AddAsync(personSupport).Result.PersonSupportID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update agent details.
        /// </summary>
        /// <param name="personSupportDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonSupportDTO UpdatePersonSupport(PersonSupportDTO personSupportDTO)
        {
            try
            {
                PersonSupport personSupport = new PersonSupport();
                this.mapper.Map<PersonSupportDTO, PersonSupport>(personSupportDTO, personSupport);
                var result = this.UpdateAsync(personSupport).Result;
                PersonSupportDTO updatedSupportType = new PersonSupportDTO();
                this.mapper.Map<PersonSupport, PersonSupportDTO>(result, updatedSupportType);
                return updatedSupportType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details agencysupportType.
        /// </summary>
        /// <param agencySupportTypeDTO="agencySupportTypeDTO">id.</param>
        /// <returns>.AgencySupportTypeDTO</returns>
        public List<PersonSupportDTO> GetPersonSupport(long id)
        {
            try
            {

                IReadOnlyList<PersonSupportDTO> personSupportDTO = new List<PersonSupportDTO>();
                IReadOnlyList<PersonSupport> personSupport = this.GetAsync(x => x.PersonID == id && !x.IsRemoved).Result;
                this.mapper.Map<IReadOnlyList<PersonSupport>, IReadOnlyList<PersonSupportDTO>>(personSupport, personSupportDTO);

                return personSupportDTO.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<PersonSupport> GetPersonSupportByDataId(long id)
        {
            try
            {
              
                var personSupport = this.GetAsync(x => x.PersonID == id && !x.IsRemoved).Result.ToList();


                return personSupport;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// To get Row details for a particular PersonSupport
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PersonSupportDTO GetPersonSupportDetails(int id)
        {
            try
            {
                PersonSupport personSupport = this.GetRowAsync(x => x.PersonSupportID == id && !x.IsRemoved).Result;
                var personSupportDTO = this.mapper.Map<PersonSupportDTO>(personSupport);
                return personSupportDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// To add personSupport details.
        /// </summary>
        /// <param name="personSupportDTO"></param>
        /// <returns>Guid.</returns>
        public void AddBulkPersonSupport(List<PersonSupportDTO> personSupportDTO)
        {
            try
            {
                List<PersonSupport> personSupport = new List<PersonSupport>();
                this.mapper.Map<List<PersonSupportDTO>, List<PersonSupport>>(personSupportDTO, personSupport);
                this.AddBulkAsync(personSupport).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonSupport> GetPersonSupportFromId(List<int> personSupportId)
        {
            try
            {
                var personSupport = this.GetAsync(x => personSupportId.Contains(x.PersonSupportID) && !x.IsRemoved).Result.ToList();
                return personSupport;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PersonSupport> UpdateBulkPersonSupport(List<PersonSupport> personSupport)
        {
            try
            {
                if (personSupport.Count > 0)
                {
                    var res = this.UpdateBulkAsync(personSupport);
                    res.Wait();
                }
                return personSupport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetPeopleSupportListForExternal.
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="personSupportSearchInputDTO"></param>
        /// <returns></returns>
        public List<SupportDetailsListDTO> GetPersonSupportListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                var query = @$";WITH PersonSupportList AS
                            (
	                          SELECT PS.PersonSupportID,PS.PersonId,PS.SupportTypeID,PS.FirstName,PS.MiddleName,PS.LastName,
								 PS.Suffix,PS.Email,PS.PhoneCode,PS.Phone,PS.Note,PS.StartDate,PS.EndDate,
                                 PS.EmailPermission, PS.TextPermission, PS.UniversalID, P.PersonIndex,
                                 PS.FirstName + COALESCE(CASE PS.MiddleName WHEN '' THEN '' ELSE ' '+PS.MiddleName END, ' '+PS.MiddleName, '')+ ' ' +PS.LastName [FullName]
                              FROM PersonSupport PS  
                                 JOIN Person P ON P.PersonId = PS.PersonId
                                 JOIN Agency A on A.AgencyId = P.AgencyId
                              WHERE PS.IsRemoved=0 AND P.IsRemoved =0  AND P.AgencyID={loggedInUserDTO.AgencyId}
                            )
                            SELECT PS.* , COUNT(PS.PersonSupportID) OVER() AS TotalCount
	                          FROM PersonSupportList PS
                            WHERE 1=1";

                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                var supportDetailsListDTO = ExecuteSqlQuery(query, x => new SupportDetailsListDTO
                {
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    PersonSupportID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"],
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"],
                    Email = x["Email"] == DBNull.Value ? string.Empty : (string)x["Email"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    PhoneCode = x["PhoneCode"] == DBNull.Value ? string.Empty : (string)x["PhoneCode"],
                    Phone = x["Phone"] == DBNull.Value ? string.Empty : (string)x["Phone"],
                    SupportTypeID = x["SupportTypeID"] == DBNull.Value ? 0 : (int)x["SupportTypeID"],
                    Suffix = x["Suffix"] == DBNull.Value ? null : (string)x["Suffix"],
                    EmailPermission = x["EmailPermission"] == DBNull.Value ? false : (bool)x["EmailPermission"],
                    TextPermission = x["TextPermission"] == DBNull.Value ? false : (bool)x["TextPermission"],
                    PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                }, queryBuilderDTO.QueryParameterDTO);
                return supportDetailsListDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
