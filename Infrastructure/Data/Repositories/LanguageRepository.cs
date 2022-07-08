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
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public LanguageRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To get all languages.
        /// </summary>
        /// <returns> LanguageDTO.</returns>
        public async Task<List<LanguageDTO>> GetAllLanguages()
        {
            try
            {
                var language = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<LanguageDTO>>(language.OrderBy(y => y.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddLanguage
        /// </summary>
        /// <param name="languageDetailsDTO"></param>
        /// <returns>LanguageDetailsDTO</returns>
        public LanguageDTO AddLanguage(LanguageDTO languageDetailsDTO)
        {
            try
            {
                Language language = new Language();
                this.mapper.Map<LanguageDTO, Language>(languageDetailsDTO, language);
                var result = this.AddAsync(language).Result;
                this.mapper.Map<Language, LanguageDTO>(result, languageDetailsDTO);
                return languageDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateLanguage
        /// </summary>
        /// <param name="languageDetailsDTO"></param>
        /// <returns>LanguageDetailsDTO</returns>
        public LanguageDTO UpdateLanguage(LanguageDTO languageDetailsDTO)
        {
            try
            {
                Language language = new Language();
                this.mapper.Map<LanguageDTO, Language>(languageDetailsDTO, language);
                var result = this.UpdateAsync(language).Result;
                this.mapper.Map<Language, LanguageDTO>(result, languageDetailsDTO);
                return languageDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetLanguage
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns>LanguageDTO</returns>
        public async Task<LanguageDTO> GetLanguage(int languageID)
        {
            try
            {
                LanguageDTO languageDTO = new LanguageDTO();
                Language language = await this.GetRowAsync(x => x.LanguageID == languageID);
                this.mapper.Map<Language, LanguageDTO>(language, languageDTO);
                return languageDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetLanguageList
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>LanguageDTO</returns>
        public List<LanguageDTO> GetLanguageList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<LanguageDTO> languageDTO = new List<LanguageDTO>();
                var language = this._dbContext.Languages.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<Language>, List<LanguageDTO>>(language, languageDTO);
                return languageDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetLanguageCount
        /// </summary>
        /// <returns></returns>
        public int GetLanguageCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.Languages.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyLanguageList
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>LanguageDTO</returns>
        public List<LanguageDTO> GetAgencyLanguageList(long agencyID)
        {
            try
            {
                List<LanguageDTO> languageDTO = new List<LanguageDTO>();
                var language = this._dbContext.Languages.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                this.mapper.Map<List<Language>, List<LanguageDTO>>(language, languageDTO);
                return languageDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
