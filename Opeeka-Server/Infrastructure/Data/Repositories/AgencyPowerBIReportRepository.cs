using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AgencyPowerBIReportRepository : BaseRepository<AgencyPowerBIReport>, IAgencyPowerBIReportRepository
    {
        private readonly OpeekaDBContext _dbContext;
        public AgencyPowerBIReportRepository(OpeekaDBContext dbContext)
         : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<AgencyPowerBIReport> GetAllPowerBIReportsForAgency(long agencyId, int instrumentId)
        {
            try
            {
                var query = string.Empty;
                query = @$"SELECT AgencyPowerBIReportId, ReportId, ReportName,FiltersOrParameters AS Filters,
                                ListOrder FROM AgencyPowerBIReport where IsRemoved = 0 
                                and AgencyID = { agencyId } and instrumentId = {instrumentId} order by ListOrder";

                var result = ExecuteSqlQuery(query, x => new AgencyPowerBIReport
                {
                    AgencyPowerBIReportId = (int)x["AgencyPowerBIReportId"],
                    ReportId = (Guid)x["ReportId"],
                    ReportName = x["ReportName"] == DBNull.Value ? string.Empty : (string)x["ReportName"],
                    FiltersOrParameters = x["Filters"] == DBNull.Value ? string.Empty : (string)x["Filters"],
                    AgencyId = agencyId,
                    ListOrder = (int)x["ListOrder"],
                    InstrumentId = instrumentId
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PowerBiFilterDTO GetFilterReplaceDetailsForPowerBI(PowerBiInputDTO powerBiInputDTO)
        {
            try
            {
                var query = string.Empty;
                query = @$";WITH Colb AS( 
		                          SELECT name AS CollaborationName , 1 AS UqID
			            	            	FROM Collaboration C 
			            					 where C.CollaborationID = {powerBiInputDTO.CollaborationId}
			            	   ) 
			                 ,Voice AS( 
		                            SELECT name AS VoiceName, 1 AS UqID FROM info.VoiceType  
			            					 where VoiceTypeID = {powerBiInputDTO.VoiceTypeId}
			            		  ) 
			                ,qname AS( 
		                            SELECT name AS QuestionnaireName, 1 AS UqID FROM Questionnaire
			            					 where QuestionnaireId  = {powerBiInputDTO.QuestionnaireId}
			            	     )
                            ,personCte AS( 
		                            SELECT PersonId, 1 AS UqID FROM Person
			            					 where PersonIndex  = '{powerBiInputDTO.PersonIndex}'
			            	     ) SELECT C.CollaborationName, v.VoiceName, q.QuestionnaireName, p.PersonId
                                    FROM personCte P
                                     LEFT JOIN Qname Q ON q.UqID = p.UqID
                                     LEFT JOIN Voice V ON v.UqID = q.UqID
			            			 LEFT JOIN Colb C ON c.UqID = v.UqID";

                var result = ExecuteSqlQuery(query, x => new PowerBiFilterDTO
                {
                    AgencyPowerBIReportId = powerBiInputDTO.AgencyPowerBIReportId,
                    CollaborationName = x["CollaborationName"] == DBNull.Value ? "Lifetime" : (string)x["CollaborationName"],
                    QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? string.Empty : (string)x["QuestionnaireName"],
                    VoiceTypeName = x["VoiceName"] == DBNull.Value ? string.Empty : (string)x["VoiceName"],
                    CollaborationId = powerBiInputDTO.CollaborationId,
                    VoiceTypeId = powerBiInputDTO.VoiceTypeId,
                    VoiceTypeFKId = powerBiInputDTO.VoiceTypeFKId,
                    PersonQuestionnaireID = powerBiInputDTO.PersonQuestionnaireID,
                    PersonId = x["PersonId"] == DBNull.Value ? 0 : (long)x["PersonId"]
                }).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AgencyPowerBIReport GetPowerBIReportDetailsById(int agencyPowerBIReportId, long agencyId)
        {
            try
            {
                var result = this.GetRowAsync(x => x.AgencyPowerBIReportId == agencyPowerBIReportId 
                                    && x.AgencyId == agencyId && !x.IsRemoved).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
