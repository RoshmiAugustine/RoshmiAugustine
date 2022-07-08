// -----------------------------------------------------------------------
// <copyright file="PersonLanguageRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonLanguageRepository : BaseRepository<PersonLanguage>, IPersonLanguageRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonLanguageRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonLanguageRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonLanguageRepository(ILogger<PersonLanguageRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// To add personLanguage details.
        /// </summary>
        /// <param name="personLanguageDTO"></param>
        /// <returns>Guid.</returns>
        public long AddPersonLanguage(PersonLanguageDTO personLanguageDTO)
        {
            try
            {
                PersonLanguage personLanguage = new PersonLanguage();
                this.mapper.Map<PersonLanguageDTO, PersonLanguage>(personLanguageDTO, personLanguage);
                var result = this.AddAsync(personLanguage).Result.PersonID;
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
        /// <param name="personLanguageDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonLanguageDTO UpdatePersonLanguage(PersonLanguageDTO personLanguageDTO)
        {
            try
            {
                PersonLanguage personLanguage = new PersonLanguage();
                this.mapper.Map<PersonLanguageDTO, PersonLanguage>(personLanguageDTO, personLanguage);
                var result = this.UpdateAsync(personLanguage).Result;
                PersonLanguageDTO updatedAddress = new PersonLanguageDTO();
                this.mapper.Map<PersonLanguage, PersonLanguageDTO>(result, updatedAddress);
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
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<IReadOnlyList<PersonLanguageDTO>> GetPersonLanguage(long id)
        {
            try
            {
                IReadOnlyList<PersonLanguageDTO> personLanguageDTO = new List<PersonLanguageDTO>();
                IReadOnlyList<PersonLanguage> personLanguage = await this.GetAsync(x => x.PersonID == id);
                this.mapper.Map<IReadOnlyList<PersonLanguage>, IReadOnlyList<PersonLanguageDTO>>(personLanguage, personLanguageDTO);

                return personLanguageDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
