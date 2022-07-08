// -----------------------------------------------------------------------
// <copyright file="ConfigurationRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class EmailDetailRepository : BaseRepository<EmailDetail>, IEmailDetailRepository
    {
        private readonly OpeekaDBContext _dbContext;
        public EmailDetailRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetEmailDetails.
        /// </summary>
        /// <returns>EmailDetailsResponseDTO.</returns>
        public List<EmailDetailsDTO> GetEmailDetails()
        {
            try
            {
                var query = string.Empty;
                query = $@"Select top 500 Ed.*,H.HelperID,H.IsEmailReminderAlerts from emaildetail ED 
                                left join Helper H on H.HelperID=ED.HelperID
                                where Status='Pending' order by CreatedDate";

                var emailDetailsList = ExecuteSqlQuery(query, x => new EmailDetailsDTO
                {
                    EmailDetailID = x["EmailDetailID"] == DBNull.Value ? 0 : (long)x["EmailDetailID"],
                    Email = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    EmailAttributes = x["EmailAttributes"] == DBNull.Value ? null : (string)x["EmailAttributes"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long?)x["AgencyID"],
                    HelperID = x["HelperID"] == DBNull.Value ? 0 : (int?)x["HelperID"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long?)x["PersonID"],
                    Status = x["Status"] == DBNull.Value ? null : (string)x["Status"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? null : (int?)x["UpdateUserID"],
                    Type = x["Type"] == DBNull.Value ? null : (string)x["Type"],
                    URL = x["URL"] == DBNull.Value ? null : (string)x["URL"],
                    CreatedDate = x["CreatedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["CreatedDate"],
                    IsEmailReminderAlerts=x["IsEmailReminderAlerts"] == DBNull.Value ? false : (bool)x["IsEmailReminderAlerts"],                    
                });
                return emailDetailsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateEmailDetails.
        /// </summary>
        /// <param name="EmailDetails">EmailDetails.</param>
        /// <returns>EmailDetails list.</returns>
        public List<EmailDetail> UpdateEmailDetails(List<EmailDetail> EmailDetails)
        {
            try
            {
                var res = this.UpdateBulkAsync(EmailDetails);
                res.Wait();
                return EmailDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddEmailDetails
        /// </summary>
        /// <param name="EmailDetails"></param>
        /// <returns>EmailDetails</returns>
        public void AddEmailDetails(List<EmailDetail> EmailDetails)
        {
            try
            {
                var result = this.AddBulkAsync(EmailDetails);
                result.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

