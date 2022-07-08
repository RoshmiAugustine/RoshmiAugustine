// -----------------------------------------------------------------------
// <copyright file="HelperTitleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class HelperTitleRepository : BaseRepository<HelperTitle>, IHelperTitleRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public HelperTitleRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To get all HelperTitle.
        /// </summary>
        /// <returns> HelperTitle.</returns>
        public async Task<List<HelperTitleDTO>> GetAllHelperTitle()
        {
            try
            {
                var HelperTitle = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<HelperTitleDTO>>(HelperTitle.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Helper Title list 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperTitleDTO</returns>
        public List<HelperTitleDTO> GetHelperTitleList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<HelperTitleDTO> helperTitleDTO = new List<HelperTitleDTO>();
                var helperTitle = this._dbContext.HelperTitles.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<HelperTitle>, List<HelperTitleDTO>>(helperTitle, helperTitleDTO);
                return helperTitleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the total count of Helper titles
        /// </summary>
        /// <returns></returns>
        public int GetHelperTitleCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.HelperTitles.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add new Helper Title
        /// </summary>
        /// <param name="helperTitleDetailsDTO"></param>
        /// <returns>HelperTitleDetailsDTO</returns>
        public HelperTitleDTO AddHelperTitle(HelperTitleDTO helperTitleDetailsDTO)
        {
            try
            {
                HelperTitle helperTitle = new HelperTitle();
                this.mapper.Map<HelperTitleDTO, HelperTitle>(helperTitleDetailsDTO, helperTitle);
                var result = this.AddAsync(helperTitle).Result;
                this.mapper.Map<HelperTitle, HelperTitleDTO>(result, helperTitleDetailsDTO);
                return helperTitleDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update existing Helper Title
        /// </summary>
        /// <param name="helperTitleDetailsDTO"></param>
        /// <returns>HelperTitleDetailsDTO</returns>
        public HelperTitleDTO UpdateHelperTitle(HelperTitleDTO helperTitleDetailsDTO)
        {
            try
            {
                HelperTitle helperTitle = new HelperTitle();
                this.mapper.Map<HelperTitleDTO, HelperTitle>(helperTitleDetailsDTO, helperTitle);
                var result = this.UpdateAsync(helperTitle).Result;
                this.mapper.Map<HelperTitle, HelperTitleDTO>(result, helperTitleDetailsDTO);
                return helperTitleDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get one Helper Title by Id
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>HelperTitleDTO</returns>
        public async Task<HelperTitleDTO> GetHelperTitle(int helperTitleID)
        {
            try
            {
                HelperTitleDTO helperTitleDTO = new HelperTitleDTO();
                HelperTitle helperTitle = await this.GetRowAsync(x => x.HelperTitleID == helperTitleID);
                this.mapper.Map<HelperTitle, HelperTitleDTO>(helperTitle, helperTitleDTO);
                return helperTitleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Helper Title for an agency list
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>HelperTitleDTO</returns>
        public List<HelperTitleDTO> GetHelperTitleList(long agencyId, int pageNumber, int pageSize)
        {
            try
            {
                List<HelperTitleDTO> helperTitleDTO = new List<HelperTitleDTO>();
                var helperTitle = this._dbContext.HelperTitles.Where(x => x.AgencyID == agencyId && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<HelperTitle>, List<HelperTitleDTO>>(helperTitle, helperTitleDTO);
                return helperTitleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Helper Title for an agency.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns>HelperTitleDTO</returns>
        public List<HelperTitleDTO> GetAgencyHelperTitleList(long agencyId)
        {
            try
            {
                List<HelperTitleDTO> helperTitleDTO = new List<HelperTitleDTO>();
                var helperTitle = this._dbContext.HelperTitles.Where(x => x.AgencyID == agencyId && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                this.mapper.Map<List<HelperTitle>, List<HelperTitleDTO>>(helperTitle, helperTitleDTO);
                return helperTitleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
