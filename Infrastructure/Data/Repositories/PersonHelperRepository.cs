// -----------------------------------------------------------------------
// <copyright file="PersonHelperRepository.cs" company="Naicoits">
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
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonHelperRepository : BaseRepository<PersonHelper>, IPersonHelperRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonHelperRepository> logger;
        private readonly OpeekaDBContext _dbContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="PersonHelperRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonHelperRepository(ILogger<PersonHelperRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To add personHelper details.
        /// </summary>
        /// <param name="personHelperDTO"></param>
        /// <returns>Guid.</returns>
        public long AddPersonHelper(PersonHelperDTO personHelperDTO)
        {
            try
            {
                PersonHelper personHelper = new PersonHelper();
                this.mapper.Map<PersonHelperDTO, PersonHelper>(personHelperDTO, personHelper);
                var result = this.AddAsync(personHelper).Result.PersonHelperID;
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
        /// <param name="personHelperDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonHelperDTO UpdatePersonHelper(PersonHelperDTO personHelperDTO)
        {
            try
            {
                PersonHelper personHelper = new PersonHelper();
                this.mapper.Map<PersonHelperDTO, PersonHelper>(personHelperDTO, personHelper);
                var result = this.UpdateAsync(personHelper).Result;
                PersonHelperDTO updatedAddress = new PersonHelperDTO();
                this.mapper.Map<PersonHelper, PersonHelperDTO>(result, updatedAddress);
                return updatedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonHelper> UpdateBulkPersonHelper(List<PersonHelper> personHelper)
        {
            try
            {
                var res = this.UpdateBulkAsync(personHelper);
                res.Wait();
                return personHelper;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get details agencyaddress.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<IReadOnlyList<PersonHelperDTO>> GetPersonHelper(long id)
        {
            try
            {
                IReadOnlyList<PersonHelperDTO> personHelperDTO = new List<PersonHelperDTO>();
                IReadOnlyList<PersonHelper> personHelper = await this.GetAsync(x => x.PersonID == id && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<PersonHelper>, IReadOnlyList<PersonHelperDTO>>(personHelper, personHelperDTO);

                return personHelperDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<PersonHelper> GetPersonHelperByDataId(long id)
        {
            try
            {
               // IReadOnlyList<PersonHelperDTO> personHelperDTO = new List<PersonHelperDTO>();
                var personHelper =  this.GetAsync(x => x.PersonID == id && !x.IsRemoved).Result.ToList();
               // this.mapper.Map<IReadOnlyList<PersonHelper>, IReadOnlyList<PersonHelperDTO>>(personHelper, personHelperDTO);

                return personHelper;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// To add bulk personHelper details.
        /// </summary>
        /// <param name="personHelperDTO"></param>
        /// <returns>Guid.</returns>
        public void AddBulkPersonHelper(List<PersonHelperDTO> personHelperDTO)
        {
            try
            {
                List<PersonHelper> personHelper = new List<PersonHelper>();
                this.mapper.Map<List<PersonHelperDTO>, List<PersonHelper>>(personHelperDTO, personHelper);
                this.AddBulkAsync(personHelper).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonHelper> GetPersonHelperFromId(List<long> personHelperId)
        {
            try
            {
                var personHelper = this.GetAsync(x => personHelperId.Contains(x.PersonHelperID) && !x.IsRemoved).Result.ToList();
                return personHelper;


            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get details agencyaddress.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public List<PersonHelperDetailsDTO> GetPersonHelperDetails(long personID)
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT DISTINCT PH.PersonHelperID,PH.HelperID,H.HelperIndex,H.Email FROM PersonHelper PH
						JOIN Helper H ON PH.HelperID = H.HelperID
						WHERE PH.PersonID={personID}";

                var result = ExecuteSqlQuery(query, x => new PersonHelperDetailsDTO
                {
                    HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                    Email = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    HelperIndex = (Guid)x["HelperIndex"],
                    PersonHelperID = x["PersonHelperID"] == DBNull.Value ? 0 : (long)x["PersonHelperID"],
                });
                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// GetPersonHelperByPersonHelperId
        /// </summary>
        /// <param name="personHelperId"></param>
        /// <returns></returns>
        public PersonHelper GetPersonHelperByPersonHelperId(long personHelperId)
        {
            try
            {
                var personHelper = this.GetAsync(x => x.PersonHelperID== personHelperId && !x.IsRemoved).Result.FirstOrDefault();
                return personHelper;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
