using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    class ImportRepository : BaseRepository<FileImport>, IImportRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public ImportRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// InsertImportFileDetails.
        /// </summary>
        /// <param name="importDTO"></param>
        /// <returns></returns>
        public int InsertImportFileDetails(FileImportDTO importDTO)
        {
            try
            {
                FileImport fileImport = new FileImport();
                this.mapper.Map<FileImportDTO, FileImport>(importDTO, fileImport);
                var result = this.AddAsync(fileImport).Result.FileImportID;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetFileImportData.
        /// </summary>
        /// <param name="importType">importType.</param>
        /// <returns>FileImportDTO List.</returns>
        public List<FileImportDTO> GetFileImportData(string importType)
        {
            try
            {
                var condition = string.Empty;
                if (!string.IsNullOrEmpty(importType))
                {
                    condition = $@"and IT.Name='{importType}'";
                }
                var query = string.Empty;
                query = @$"SELECT FI.FileImportID,FI.FileJsonData,IT.Name AS ImportType,FI.IsProcessed,FI.UpdateUserID,FI.AgencyID,FI.ImportTypeID,FI.QuestionnaireID,
                                    FI.ImportFileName
                                  FROM FileImport FI
                                  JOIN info.ImportType IT ON FI.ImportTypeID = IT.ImportTypeID
                                  WHERE FI.IsProcessed=0 {condition} AND IT.IsRemoved=0 
                                  ORDER BY FI.CreatedDate ";
                var notes = ExecuteSqlQuery(query, x => new FileImportDTO
                {
                    FileImportID = x["FileImportID"] == DBNull.Value ? 0 : (int)x["FileImportID"],
                    FileJsonData = x["FileJsonData"] == DBNull.Value ? null : (string)x["FileJsonData"],
                    ImportType = x["ImportType"] == DBNull.Value ? null : (string)x["ImportType"],
                    IsProcessed = x["IsProcessed"] == DBNull.Value ? false : (bool)x["IsProcessed"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    ImportTypeID = x["ImportTypeID"] == DBNull.Value ? 0 : (int)x["ImportTypeID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    ImportFileName = x["ImportFileName"] == DBNull.Value ? string.Empty  : (string)x["ImportFileName"],

                }).ToList();

                return notes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateFileImport.
        /// </summary>
        /// <param name="fileImport">fileImport.</param>
        /// <returns>FileImport.</returns>
        public FileImport UpdateFileImport(FileImport fileImport)
        {
            try
            {
                FileImport result = this.UpdateAsync(fileImport).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetFileImportDataByID.
        /// </summary>
        /// <param name="fileImportID">fileImportID.</param>
        /// <returns>FileImport.</returns>
        public Task<FileImport> GetFileImportDataByID(int fileImportID)
        {
            try
            {
                return  this.GetRowAsync(x => x.FileImportID == fileImportID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ImportType> GetAllImportTypes(long agencyID)
        {
            try
            {
                return _dbContext.ImportType.Where(x => x.IsRemoved == false).OrderBy(x => x.ListOrder).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ImportType> GetImportTypeIDByName(string importType)
        {
            try
            {
                return  _dbContext.ImportType.Where(x => x.IsRemoved == false && x.Name == importType).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AssessmemtTemplateDTO> GetAllQuestionnaireItems(int questionnaireId)
        {
            try
            {
                var query = string.Empty;
                query = @$"SELECT  CF.Name AS CategoryFocus, Ins.Abbrev + '_' + C.Abbrev + '_' + I.Label AS QuestionnaireItemName,'' as DefaultItemvalue,QI.QuestionnaireItemID
                                FROM QuestionnaireItem QI
                                JOIN Item I on QI.ItemID = I.itemID
                                JOIN info.Category C on QI.CategoryID = C.CategoryID
                                JOIn info.CategoryFocus CF ON CF.CategoryFocusID = C.CategoryFocusID
                                LEFT JOIN Questionnaire Q ON Q.QuestionnaireID = {questionnaireId}
                                JOIn Info.Instrument Ins ON Ins.InstrumentID = Q.InstrumentID
                                WHERE QI.QuestionnaireID = {questionnaireId} and QI.IsRemoved =0 ORDER BY CF.CategoryFocusID";

                var result = ExecuteSqlQuery(query, x => new AssessmemtTemplateDTO
                {
                    QuestionnaireItemName = x["QuestionnaireItemName"] == DBNull.Value ? string.Empty : (string)x["QuestionnaireItemName"],
                    CategoryFocus = x["CategoryFocus"] == DBNull.Value ? string.Empty : (string)x["CategoryFocus"],
                    DefaultItemvalue = x["DefaultItemvalue"] == DBNull.Value ? string.Empty : (string)x["DefaultItemvalue"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"]

                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
