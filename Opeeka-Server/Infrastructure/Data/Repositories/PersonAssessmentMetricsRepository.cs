using AutoMapper;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonAssessmentMetricsRepository : BaseRepository<PersonAssessmentMetrics>, IPersonAssessmentMetricsRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public PersonAssessmentMetricsRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }
        public List<PersonAssessmentMetrics> AddBulkPersonAssessmentMetrics(List<PersonAssessmentMetrics> personAssessmentMetrics)
        {
            try
            {
				if (personAssessmentMetrics.Count > 0)
				{
					var res = this.AddBulkAsync(personAssessmentMetrics);
					res.Wait();
				}
                return personAssessmentMetrics;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonAssessmentMetrics> UpdateBulkPersonAssessmentMetrics(List<PersonAssessmentMetrics> personAssessmentMetrics)
        {

            try
            {
				if (personAssessmentMetrics.Count > 0)
				{
					var res = this.UpdateBulkAsync(personAssessmentMetrics);
					res.Wait();
				}
                return personAssessmentMetrics;
            }
            catch (Exception)
            {
                throw;
            }
        }

		public List<PersonAssessmentMetrics> GetPersonAssessmentMetricsInDetail(DashboardMetricsInputDTO metricsInput)
		{
			try
			{
				var query = @$"select [PersonAssessmentMetricsID]
							  ,[PersonID]
							  ,[InstrumentID]
							  ,[PersonQuestionnaireID]
							  ,[ItemID]
							  ,[NeedsEver]
							  ,[NeedsIdentified]
							  ,[NeedsAddressed]
							  ,[NeedsAddressing]
							  ,[NeedsImproved]
							  ,[StrengthsEver]
							  ,[StrengthsIdentified]
							  ,[StrengthsBuilt]
							  ,[StrengthsBuilding]
							  ,[StrengthsImproved],[AssessmentID] from PersonAssessmentMetrics WITH (NOLOCK) 
							where personId= { metricsInput.personId } and AssessmentId = { metricsInput.AssessmentID }";
				var queryResult = ExecuteSqlQuery(query, x => new PersonAssessmentMetrics
				{
					PersonAssessmentMetricsID = x["PersonAssessmentMetricsID"] == DBNull.Value ? 0 : (long)x["PersonAssessmentMetricsID"],
					PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
					InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
					PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (int)x["PersonQuestionnaireID"],
					ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
					NeedsEver = x["NeedsEver"] == DBNull.Value ? 0 : (int)x["NeedsEver"],
					NeedsIdentified = x["NeedsIdentified"] == DBNull.Value ? 0 : (int)x["NeedsIdentified"],
					NeedsAddressed = x["NeedsAddressed"] == DBNull.Value ? 0 : (int)x["NeedsAddressed"],
					NeedsAddressing = x["NeedsAddressing"] == DBNull.Value ? 0 : (int)x["NeedsAddressing"],
					NeedsImproved = x["NeedsImproved"] == DBNull.Value ? 0 : (int)x["NeedsImproved"],
					StrengthsEver = x["StrengthsEver"] == DBNull.Value ? 0 : (int)x["StrengthsEver"],
					StrengthsIdentified = x["StrengthsIdentified"] == DBNull.Value ? 0 : (int)x["StrengthsIdentified"],
					StrengthsBuilt = x["StrengthsBuilt"] == DBNull.Value ? 0 : (int)x["StrengthsBuilt"],
					StrengthsBuilding = x["StrengthsBuilding"] == DBNull.Value ? 0 : (int)x["StrengthsBuilding"],
					StrengthsImproved = x["NeedsImproved"] == DBNull.Value ? 0 : (int)x["StrengthsImproved"],
					AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
				});

				return queryResult;
			}
			catch (Exception)
			{
				throw;
			}

		}

	}
}
