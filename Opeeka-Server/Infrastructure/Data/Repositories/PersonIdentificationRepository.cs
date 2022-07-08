// -----------------------------------------------------------------------
// <copyright file="PersonIdentificationRepository.cs" company="Naicoits">
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
    public class PersonIdentificationRepository : BaseRepository<PersonIdentification>, IPersonIdentificationRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonIdentificationRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonIdentificationRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonIdentificationRepository(ILogger<PersonIdentificationRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// To add personIdentification details.
        /// </summary>
        /// <param name="personIdentificationDTO"></param>
        /// <returns>Guid.</returns>
        public long AddPersonIdentificationType(PersonIdentificationDTO personIdentificationDTO)
        {
            try
            {
                PersonIdentification personIdentification = new PersonIdentification();
                this.mapper.Map<PersonIdentificationDTO, PersonIdentification>(personIdentificationDTO, personIdentification);
                var result = this.AddAsync(personIdentification).Result.PersonIdentificationID;
                //var result = this.AddAsync(personIdentification).Result.IdentificationTypeID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To add personIdentification details.
        /// </summary>
        /// <param name="personIdentificationDTO"></param>
        /// <returns>Guid.</returns>
        public void AddBulkPersonIdentificationType(List<PersonIdentificationDTO> personIdentificationDTOList)
        {
            try
            {
                if (personIdentificationDTOList.Count > 0)
                {
                    List<PersonIdentification> personIdentificationList = new List<PersonIdentification>();
                    this.mapper.Map<List<PersonIdentificationDTO>, List<PersonIdentification>>(personIdentificationDTOList, personIdentificationList);
                    this.AddBulkAsync(personIdentificationList).Wait();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update agent details.
        /// </summary>
        /// <param name="personIdentificationDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonIdentificationDTO UpdatePersonIdentificationType(PersonIdentificationDTO personIdentificationDTO)
        {
            try
            {
                PersonIdentification personIdentification = new PersonIdentification();
                this.mapper.Map<PersonIdentificationDTO, PersonIdentification>(personIdentificationDTO, personIdentification);
                var result = this.UpdateAsync(personIdentification).Result;
                PersonIdentificationDTO updatedIdentificationType = new PersonIdentificationDTO();
                this.mapper.Map<PersonIdentification, PersonIdentificationDTO>(result, updatedIdentificationType);
                return updatedIdentificationType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonIdentification> UpdateBulkPersonIdentifications(List<PersonIdentification> personIdentifications)
        {
            try
            {
                var res = this.UpdateBulkAsync(personIdentifications);
                res.Wait();
                return personIdentifications;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get details agencyidentificationType.
        /// </summary>
        /// <param agencyIdentificationTypeDTO="agencyIdentificationTypeDTO">id.</param>
        /// <returns>.AgencyIdentificationTypeDTO</returns>
        public async Task<IReadOnlyList<PersonIdentificationDTO>> GetPersonIdentificationType(long id)
        {
            try
            {
                IReadOnlyList<PersonIdentificationDTO> personIdentificationDTO = new List<PersonIdentificationDTO>();
                IReadOnlyList<PersonIdentification> personIdentification = await this.GetAsync(x => x.PersonID == id && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<PersonIdentification>, IReadOnlyList<PersonIdentificationDTO>>(personIdentification, personIdentificationDTO);

                return personIdentificationDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<PersonIdentification> GetPersonIdentificationTypeByDataId(long id)
        {
            try
            {
                //IReadOnlyList<PersonIdentificationDTO> personIdentificationDTO = new List<PersonIdentificationDTO>();
                var personIdentification =  this.GetAsync(x => x.PersonID == id && !x.IsRemoved).Result.ToList();
                //this.mapper.Map<IReadOnlyList<PersonIdentification>, IReadOnlyList<PersonIdentificationDTO>>(personIdentification, personIdentificationDTO);

                return personIdentification;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<PersonIdentification> GetPersonIdentificationsFromId(List<long> personIdentificationId)
        {
            try
            {
                var personIdentification = this.GetAsync(x => personIdentificationId.Contains(x.PersonIdentificationID) && !x.IsRemoved).Result.ToList();
                return personIdentification;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
