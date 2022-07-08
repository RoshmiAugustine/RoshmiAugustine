// -----------------------------------------------------------------------
// <copyright file="PersonCollaborationRepository.cs" company="Naicoits">
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
    public class PersonCollaborationRepository : BaseRepository<PersonCollaboration>, IPersonCollaborationRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonCollaborationRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonCollaborationRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonCollaborationRepository(ILogger<PersonCollaborationRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To add personCollaboration details.
        /// </summary>
        /// <param name="personCollaborationDTO"></param>
        /// <returns>Guid.</returns>
        public long AddPersonCollaboration(PersonCollaborationDTO personCollaborationDTO)
        {
            try
            {
                PersonCollaboration personCollaboration = new PersonCollaboration();
                this.mapper.Map<PersonCollaborationDTO, PersonCollaboration>(personCollaborationDTO, personCollaboration);
                var result = this.AddAsync(personCollaboration).Result.PersonCollaborationID;
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
        /// <param name="personCollaborationDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonCollaborationDTO UpdatePersonCollaboration(PersonCollaborationDTO personCollaborationDTO)
        {
            try
            {
                PersonCollaboration personCollaboration = new PersonCollaboration();
                this.mapper.Map<PersonCollaborationDTO, PersonCollaboration>(personCollaborationDTO, personCollaboration);
                var result = this.UpdateAsync(personCollaboration).Result;
                PersonCollaborationDTO updatedCollaboration = new PersonCollaborationDTO();
                return this.mapper.Map<PersonCollaboration, PersonCollaborationDTO>(result, updatedCollaboration);

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
        public List<PersonCollaboration> UpdateBulkPersonCollaboration(List<PersonCollaboration> personCollaboration)
        {
            try
            {
                var res = this.UpdateBulkAsync(personCollaboration);
                res.Wait();
                return personCollaboration;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get details agencypersonCollaboration.
        /// </summary>
        /// <param agencyPersonCollaborationDTO="agencyPersonCollaborationDTO">id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        public List<PeopleCollaborationDTO> GetPersonCollaboration(long personId)
        {
            try
            {
                List<PeopleCollaborationDTO> peopleCollaborationDTO = new List<PeopleCollaborationDTO>();
                var query = @$"SELECT PC.CollaborationID, C.Name AS CollaborationName, PC.EnrollDate, PC.EndDate,
                             PC.PersonCollaborationID, PC.IsPrimary, PC.IsCurrent
                                  FROM PersonCollaboration PC 
                                  JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                   WHERE PC.IsRemoved = 0 AND PC.PersonId = {personId}";
                peopleCollaborationDTO = ExecuteSqlQuery(query, x => new PeopleCollaborationDTO
                {
                    PersonCollaborationID = x["PersonCollaborationID"] == DBNull.Value ? 0 : (long)x["PersonCollaborationID"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    CollaborationName = x["CollaborationName"] == DBNull.Value ? null : (string)x["CollaborationName"],
                    IsPrimary = x["IsPrimary"] == DBNull.Value ? false : (bool)x["IsPrimary"],
                    IsCurrent = x["IsCurrent"] == DBNull.Value ? false : (bool)x["IsCurrent"],
                    EnrollDate = (DateTime)x["EnrollDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)x["EndDate"],
                    CollaborationStartDate = (DateTime)x["EnrollDate"],
                    CollaborationEndDate = x["EndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)x["EndDate"]
                });
                return peopleCollaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        /// <summary>
        /// To get details agencypersonCollaboration.
        /// </summary>
        /// <param agencyPersonCollaborationDTO="agencyPersonCollaborationDTO">id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        public async Task<IReadOnlyList<PersonCollaborationDTO>> GetPersonCollaborationByCollaborationID(long collaborationID)
        {
            try
            {
                IReadOnlyList<PersonCollaborationDTO> personCollaborationDTO = new List<PersonCollaborationDTO>();
                IReadOnlyList<PersonCollaboration> personCollaboration = await this.GetAsync(x => x.CollaborationID == collaborationID && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<PersonCollaboration>, IReadOnlyList<PersonCollaborationDTO>>(personCollaboration, personCollaborationDTO);

                return personCollaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To add personCollaboration details.
        /// </summary>
        /// <param name="personCollaborationDTO"></param>
        /// <returns>Guid.</returns>
        public void AddBulkPersonCollaboration(List<PersonCollaborationDTO> personCollaborationDTO)
        {
            try
            {
                List<PersonCollaboration> personCollaboration = new List<PersonCollaboration>();
                this.mapper.Map<List<PersonCollaborationDTO>, List<PersonCollaboration>>(personCollaborationDTO, personCollaboration);
                this.AddBulkAsync(personCollaboration).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonCollaborationByPersonIdAndCollaborationId.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="collaborationId">collaborationId.</param>
        /// <returns>PersonCollaboration.</returns>
        public PersonCollaboration GetPersonCollaborationByPersonIdAndCollaborationId(long personId, int? collaborationId)
        {
            try
            {
               var data =this.GetAsync(x => x.PersonID == personId && x.CollaborationID == collaborationId && !x.IsRemoved).Result;
                return data.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonCollaboration> GetPersonCollaborationFromId(List<long> personCollaborationId)
        {
            try
            {
                var personCollaboration = this.GetAsync(x=> personCollaborationId.Contains(x.PersonCollaborationID) && !x.IsRemoved).Result.ToList();
                return personCollaboration;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonCollaboration> GetPersonCollaborationByDataId(long personId)
        {
            try
            {
                var personCollaboration = this.GetAsync(x => x.PersonID == personId && !x.IsRemoved).Result.ToList();
                return personCollaboration;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonCollaborationByPersonCollaborationId
        /// </summary>
        /// <param name="personCollaborationId"></param>
        /// <returns></returns>
        public PersonCollaboration GetPersonCollaborationByPersonCollaborationId(long personCollaborationId)
        {
            try
            {
                var personHelper = this.GetAsync(x => x.PersonCollaborationID == personCollaborationId && !x.IsRemoved).Result.FirstOrDefault();
                return personHelper;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
