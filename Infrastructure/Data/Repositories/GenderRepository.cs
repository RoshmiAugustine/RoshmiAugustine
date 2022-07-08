// -----------------------------------------------------------------------
// <copyright file="GenderRepository.cs" company="Naicoits">
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
    public class GenderRepository : BaseRepository<Gender>, IGenderRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public GenderRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// To get all type of genders.
        /// </summary>
        /// <returns> GenderDTO.</returns>
        public async Task<List<GenderDTO>> GetAllGenders()
        {
            try
            {
                var gender = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<GenderDTO>>(gender.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddGender.
        /// </summary>
        /// <param name="GenderDTO">GenderDTO.</param>
        /// <returns>GenderDTO.</returns>
        public GenderDTO AddGender(GenderDTO GenderDTO)
        {
            try
            {
                Gender notificationLevel = new Gender();
                this.mapper.Map<GenderDTO, Gender>(GenderDTO, notificationLevel);
                var result = this.AddAsync(notificationLevel).Result;
                this.mapper.Map<Gender, GenderDTO>(result, GenderDTO);
                return GenderDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetGenderCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int.</returns>
        public int GetGenderCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.Genders.Where(x => x.AgencyID == agencyID && !x.IsRemoved).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetGender.
        /// </summary>
        /// <param name="genderID">notificationLevelID.</param>
        /// <returns>GenderDTO.</returns>
        public async Task<GenderDTO> GetGender(Int64 genderID)
        {
            try
            {
                GenderDTO genderDTO = new GenderDTO();
                Gender gender = await this.GetRowAsync(x => x.GenderID == genderID);
                this.mapper.Map<Gender, GenderDTO>(gender, genderDTO);
                return genderDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of GenderDTO.</returns>
        public List<GenderDTO> GetGenderList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<GenderDTO> genderListDTO = new List<GenderDTO>();
                var genderList = this._dbContext.Genders.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<Gender>, List<GenderDTO>>(genderList, genderListDTO);
                return genderListDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLevelCountByNotificationLevelID
        /// </summary>
        /// <param name="notificationLevelID">notificationLevelID</param>
        /// <returns>int</returns>
        public int GetGenderCountByGenderID(int genderID)
        {
            int count = (
                            from row
                            in this._dbContext.Person
                            where
                                row.GenderID == genderID && !row.IsRemoved
                            select
                                row
                        ).Count();

            //int questionnaireReminderTypescount = (
            //                from row
            //                in this._dbContext.QuestionnaireReminderTypes
            //                where
            //                    row.NotificationLevelID == notificationLevelID && !row.IsRemoved
            //                select
            //                    row
            //            ).Count();

            return count;
        }

        /// <summary>
        /// UpdateGender.
        /// </summary>
        /// <param name="GenderDTO">GenderDTO.</param>
        /// <returns>GenderDTO.</returns>
        public GenderDTO UpdateGender(GenderDTO GenderDTO)
        {
            try
            {
                Gender gender = new Gender();
                this.mapper.Map<GenderDTO, Gender>(GenderDTO, gender);
                var result = this.UpdateAsync(gender).Result;
                this.mapper.Map<Gender, GenderDTO>(result, GenderDTO);
                return GenderDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyGenderList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of GenderDTO.</returns>
        public List<GenderDTO> GetAgencyGenderList(long agencyID)
        {
            try
            {
                List<GenderDTO> genderListDTO = new List<GenderDTO>();
                var genderList = this._dbContext.Genders.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                this.mapper.Map<List<Gender>, List<GenderDTO>>(genderList, genderListDTO);
                return genderListDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
