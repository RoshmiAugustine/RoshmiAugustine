// -----------------------------------------------------------------------
// <copyright file="PersonRaceEthnicityRepository.cs" company="Naicoits">
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
    public class PersonRaceEthnicityRepository : BaseRepository<PersonRaceEthnicity>, IPersonRaceEthnicityRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonRaceEthnicityRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRaceEthnicityRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonRaceEthnicityRepository(ILogger<PersonRaceEthnicityRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// To add personRaceEthnicity details.
        /// </summary>
        /// <param name="personRaceEthnicityDTO"></param>
        /// <returns>Guid.</returns>
        public long AddRaceEthnicity(PersonRaceEthnicityDTO personRaceEthnicityDTO)
        {
            try
            {
                PersonRaceEthnicity personRaceEthnicity = new PersonRaceEthnicity();
                this.mapper.Map<PersonRaceEthnicityDTO, PersonRaceEthnicity>(personRaceEthnicityDTO, personRaceEthnicity);
                var result = this.AddAsync(personRaceEthnicity).Result.PersonRaceEthnicityID;
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
        /// <param name="personRaceEthnicityDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonRaceEthnicityDTO UpdateRaceEthnicity(PersonRaceEthnicityDTO personRaceEthnicityDTO)
        {
            try
            {
                PersonRaceEthnicity personRaceEthnicity = new PersonRaceEthnicity();
                this.mapper.Map<PersonRaceEthnicityDTO, PersonRaceEthnicity>(personRaceEthnicityDTO, personRaceEthnicity);
                var result = this.UpdateAsync(personRaceEthnicity).Result;
                PersonRaceEthnicityDTO updatedAddress = new PersonRaceEthnicityDTO();
                this.mapper.Map<PersonRaceEthnicity, PersonRaceEthnicityDTO>(result, updatedAddress);
                return updatedAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details agencyaddress.
        /// </summary>
        /// <param PersonRaceEthnicityDTO="PersonRaceEthnicityDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<IReadOnlyList<PersonRaceEthnicityDTO>> GetRaceEthnicity(long id)
        {
            try
            {
                IReadOnlyList<PersonRaceEthnicityDTO> personRaceEthnicityDTO = new List<PersonRaceEthnicityDTO>();
                IReadOnlyList<PersonRaceEthnicity> personRaceEthnicity = await this.GetAsync(x => x.PersonID == id && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<PersonRaceEthnicity>, IReadOnlyList<PersonRaceEthnicityDTO>>(personRaceEthnicity, personRaceEthnicityDTO);

                return personRaceEthnicityDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<PersonRaceEthnicity> GetRaceEthnicityByid(long id)
        {
            try
            {
                //IReadOnlyList<PersonRaceEthnicityDTO> personRaceEthnicityDTO = new List<PersonRaceEthnicityDTO>();
            List<PersonRaceEthnicity> personRaceEthnicity = this.GetAsync(x => x.PersonID == id && !x.IsRemoved).Result.ToList();
                //this.mapper.Map<IReadOnlyList<PersonRaceEthnicity>, IReadOnlyList<PersonRaceEthnicityDTO>>(personRaceEthnicity, personRaceEthnicityDTO);

                return personRaceEthnicity;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// DeleteRaceEthnicity.
        /// </summary>
        /// <param name="PersonRaceEthnicityDTO">PersonRaceEthnicityDTO</param>
        public async void DeleteRaceEthnicity(PersonRaceEthnicityDTO PersonRaceEthnicityDTO)
        {
            try
            {
                PersonRaceEthnicity personRaceEthnicity = new PersonRaceEthnicity();
                this.mapper.Map<PersonRaceEthnicityDTO, PersonRaceEthnicity>(PersonRaceEthnicityDTO, personRaceEthnicity);
                await this.DeleteAsync(personRaceEthnicity);
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// To add bulk personRaceEthnicity details.
        /// </summary>
        /// <param name="personRaceEthnicityDTO"></param>
        /// <returns>Guid.</returns>
        public void AddBulkRaceEthnicity(List<PersonRaceEthnicityDTO> personRaceEthnicityDTO)
        {
            try
            {
                List<PersonRaceEthnicity> personRaceEthnicity = new List<PersonRaceEthnicity>();
                this.mapper.Map<List<PersonRaceEthnicityDTO>, List<PersonRaceEthnicity>>(personRaceEthnicityDTO, personRaceEthnicity);
                this.AddBulkAsync(personRaceEthnicity).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PersonRaceEthnicity> UpdateBulkPersonRaceEthnicity(List<PersonRaceEthnicity> personRaceEthnicity)
        {
            try
            {
                var res = this.UpdateBulkAsync(personRaceEthnicity);
                res.Wait();
                return personRaceEthnicity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonRaceEthnicity> GetPersonRaceEthnicityFromId(List<long> personRaceEthnicityId)
        {
            try
            {
                var personRaceEthnicity = this.GetAsync(x => personRaceEthnicityId.Contains(x.PersonRaceEthnicityID) && !x.IsRemoved).Result.ToList();
                return personRaceEthnicity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
