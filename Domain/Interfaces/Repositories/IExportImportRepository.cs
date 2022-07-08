using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IExportImportRepository
    {
        /// <summary>
        /// GetExportTemplateList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ExportTemplateDTO.</returns>
        List<ExportTemplateDTO> GetExportTemplateList(long agencyID);

        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <param name="exportTemplateID">exportTemplateID.</param>
        /// <returns>ExportTemplateDTO.</returns>
        Task<ExportTemplateDTO> GetAsync(int exportTemplateID);

        /// <summary>
        /// GetExportData.
        /// </summary>
        /// <param name="templateSourceText">templateSourceText.</param>
        /// <returns>object.</returns>
        object GetExportData(string templateSourceText);
    }
}
