using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IImportRepository
    {
        /// <summary>
        /// InsertImportFileDetails.
        /// </summary>
        /// <param name="importDTO">importDTO.</param>
        /// <returns>int.</returns>
        int InsertImportFileDetails(FileImportDTO importDTO);

        /// <summary>
        /// GetFileImportData.
        /// </summary>
        /// <param name="importType">importType.</param>
        /// <returns>FileImportDTO List.</returns>
        List<FileImportDTO> GetFileImportData(string importType);

        /// <summary>
        /// UpdateFileImport.
        /// </summary>
        /// <param name="fileImport">fileImport.</param>
        /// <returns>FileImport.</returns>
        FileImport UpdateFileImport(FileImport fileImport);

        /// <summary>
        /// GetFileImportDataByID.
        /// </summary>
        /// <param name="fileImportID">fileImportID.</param>
        /// <returns>FileImport.</returns>
        Task<FileImport> GetFileImportDataByID(int fileImportID);

        /// <summary>
        /// GetAllImportTypes.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns>ImportType</returns>
        List<ImportType> GetAllImportTypes(long agencyID);

        /// <summary>
        /// GetImportTypeIDByName.
        /// </summary>
        /// <param name="importType"></param>
        /// <returns></returns>
        Task<ImportType> GetImportTypeIDByName(string importType);

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        List<AssessmemtTemplateDTO> GetAllQuestionnaireItems(int questionnaireId);
    }
}
