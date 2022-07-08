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
    public class IdentificationTypeRepository : BaseRepository<IdentificationType>, IIdentificationTypeRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public IdentificationTypeRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// AddIdentificationType.
        /// </summary>
        /// <param name="IdentificationType">IdentificationType.</param>
        /// <returns>IdentificationType.</returns>
        public IdentificationType AddIdentificationType(IdentificationType IdentificationType)
        {
            try
            {
                var result = this.AddAsync(IdentificationType).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        ///  To get all identificationTypes.
        /// </summary>
        /// <returns> IdentificationType.</returns>
        public async Task<List<IdentificationTypeDTO>> GetAllIdentificationTypes()
        {
            try
            {
                var identificationTypes = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<IdentificationTypeDTO>>(identificationTypes.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetIdentificationType.
        /// </summary>
        /// <param name="identificationTypeID">identificationTypeID.</param>
        /// <returns> Task<IdentificationType>.</returns>
        public async Task<IdentificationType> GetIdentificationType(Int64 identificationTypeID)
        {
            try
            {
                IdentificationType identificationType = await this.GetRowAsync(x => x.IdentificationTypeID == identificationTypeID);
                return identificationType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetIdentificationTypeCount.
        /// </summary>
        /// <returns>int.</returns>
        public int GetIdentificationTypeCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.identificationTypes.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetIdentificationTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List<IdentificationType>.</returns>
        public List<IdentificationType> GetIdentificationTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                var genderList = this._dbContext.identificationTypes.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return genderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateIdentificationType.
        /// </summary>
        /// <param name="IdentificationType">IdentificationType</param>
        /// <returns>IdentificationType</returns>
        public IdentificationType UpdateIdentificationType(IdentificationType IdentificationType)
        {
            try
            {
                var result = this.UpdateAsync(IdentificationType).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetIdentificationTypeUsedByID.
        /// </summary>
        /// <param name="identificationTypeID">levelID.</param>
        /// <returns>int.</returns>
        public int GetIdentificationTypeUsedByID(long identificationTypeID)
        {
            int count = (from row in this._dbContext.personIdentifications
                         where (row.IdentificationTypeID == identificationTypeID) && !row.IsRemoved
                         select row).Count();

            return count;
        }

        /// <summary>
        /// GetAgencyIdentificationTypeList
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentificationTypeDTO List</returns>
        public List<IdentificationTypeDTO> GetAgencyIdentificationTypeList(long agencyID)
        {
            try
            {
                List<IdentificationTypeDTO> identificationTypeDTO = new List<IdentificationTypeDTO>();
                var identificationType = this._dbContext.identificationTypes.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                this.mapper.Map<List<IdentificationType>, List<IdentificationTypeDTO>>(identificationType, identificationTypeDTO);
                return identificationTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<IdentificationTypeDTO> GetIdentificationTypeDetailsByName(string nameCSV, long agencyID)
        {
            List<IdentificationTypeDTO> dataList = new List<IdentificationTypeDTO>();
            try
            {
                nameCSV = string.IsNullOrEmpty(nameCSV) ? null : nameCSV.ToLower();
                var query = string.Empty;
                query = @$"SELECT IdentificationTypeID,[Name]
                        FROM    [info].[IdentificationType] where Lower([Name]) in({nameCSV})
                        and  AgencyID = {agencyID}";

                dataList = ExecuteSqlQuery(query, x => new IdentificationTypeDTO
                {
                    IdentificationTypeID = (int)x["IdentificationTypeID"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                });
                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
