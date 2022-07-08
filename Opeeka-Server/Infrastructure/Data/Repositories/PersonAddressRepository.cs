using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonAddressRepository : BaseRepository<PersonAddress>, IPersonAddressRepository
    {
        private readonly IMapper mapper;

        public PersonAddressRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// To add person address details.
        /// </summary>
        /// <param personAddressDTO="personAddressDTO">id.</param>
        /// <returns>Guid.</returns>
        public long AddPersonAddress(PersonAddressDTO personAddressDTO)
        {
            try
            {
                PersonAddress personAddress = new PersonAddress();
                this.mapper.Map<PersonAddressDTO, PersonAddress>(personAddressDTO, personAddress);
                var result = this.AddAsync(personAddress).Result.PersonAddressID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details personAddressDTO.
        /// </summary>
        /// <param personAddressDTO="personAddressDTO">id.</param>
        /// <returns>.PersonAddressDTO</returns>
        public async Task<PersonAddressDTO> GetPersonAddress(long id)
        {
            try
            {
                PersonAddressDTO personAddressDTO = new PersonAddressDTO();
                PersonAddress personAddress = await this.GetRowAsync(x => x.PersonID == id);
                this.mapper.Map<PersonAddress, PersonAddressDTO>(personAddress, personAddressDTO);

                return personAddressDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To add person address details.
        /// </summary>
        /// <param personAddressDTO="personAddressDTO">id.</param>
        /// <returns>Guid.</returns>
        public void AddBulkPersonAddress(List<PersonAddressDTO> personAddressDTOList)
        {
            try
            {
                List<PersonAddress> personAddressList = new List<PersonAddress>();
                this.mapper.Map<List<PersonAddressDTO>, List<PersonAddress>>(personAddressDTOList, personAddressList);
                this.AddBulkAsync(personAddressList).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

