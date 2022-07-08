using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class ExportImportRepository : BaseRepository<ExportTemplate>, IExportImportRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public ExportImportRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <param name="exportTemplateID">exportTemplateID.</param>
        /// <returns>ExportTemplateDTO.</returns>
        public async Task<ExportTemplateDTO> GetAsync(int exportTemplateID)
        {
            try
            {
                ExportTemplateDTO exportTemplateDTO = new ExportTemplateDTO();
                ExportTemplate exportTemplate = await GetRowAsync(x => x.ExportTemplateID == exportTemplateID);
                this.mapper.Map<ExportTemplate, ExportTemplateDTO>(exportTemplate, exportTemplateDTO);
                return exportTemplateDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetExportData.
        /// </summary>
        /// <param name="query">query.</param>
        /// <returns>Object.</returns>
        public object GetExportData(string query)
        {
            try
            {
                var resultList = new List<Dictionary<string, dynamic>>();

                var data = ExecuteSqlQuery(query, x =>
                {
                    var t = new Dictionary<string, dynamic>();

                    for (var i = 0; i < x.FieldCount; i++)
                    {
                        t[x.GetName(i)] = x[i];
                    }
                    resultList.Add(t);
                    return x;
                });
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetExportTemplateList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ExportTemplateDTO.</returns>
        public List<ExportTemplateDTO> GetExportTemplateList(long agencyID)
        {
            try
            {
                List<ExportTemplateDTO> listExportTemplate = new List<ExportTemplateDTO>();

                List<CollaborationDataDTO> collaborationDataDTO = new List<CollaborationDataDTO>();
                var query = string.Empty;
                query = @$"Select  E.ExportTemplateID,E.[DisplayName],E.TemplateType
                            from ExportTemplate E
                            left join ExportTemplateAgency EA on EA.ExportTemplateID = E.ExportTemplateID
                            where  (EA.AgencyID={agencyID} OR EA.AgencyID is null) and E.IsDeleted=0
                            ORDER BY E.[ListOrder],E.[DisplayName] ";

                var data = ExecuteSqlQuery(query, x => new ExportTemplateDTO
                {
                    ExportTemplateID = x["ExportTemplateID"] == DBNull.Value ? 0 : (int)x["ExportTemplateID"],
                    DisplayName = x["DisplayName"] == DBNull.Value ? null : (string)x["DisplayName"],
                    TemplateType = x["TemplateType"] == DBNull.Value ? string.Empty : (string)x["TemplateType"],
                });
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}
