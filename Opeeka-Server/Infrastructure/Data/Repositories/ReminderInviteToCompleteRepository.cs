using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{

    public class ReminderInviteToCompleteRepository : BaseRepository<ReminderInviteToComplete>, IReminderInviteToCompleteRepository
    {
        private readonly OpeekaDBContext _dbContext;
        public ReminderInviteToCompleteRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetInviteToCompleteReminderDetails.
        /// </summary>
        /// <returns></returns>
        public List<ReminderInviteToCompleteDTO> GetReminderInviteDetails()
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT TOP 500 RIC.* FROM ReminderInviteToComplete RIC 
                                WHERE Status='Pending' ORDER BY CreatedDate";

                var inviteDetailsList = ExecuteSqlQuery(query, x => new ReminderInviteToCompleteDTO
                {
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? null : (int?)x["QuestionnaireID"],
                    ReminderInviteToCompleteID = x["ReminderInviteToCompleteID"] == DBNull.Value ? 0 : (long)x["ReminderInviteToCompleteID"],
                    PhoneNumber = x["PhoneNumber"] == DBNull.Value ? null : (string)x["PhoneNumber"],
                    Attributes = x["Attributes"] == DBNull.Value ? null : (string)x["Attributes"],
                    Email = x["Email"] == DBNull.Value ? null : (string?)x["Email"],
                    Status = x["Status"] == DBNull.Value ? null : (string)x["Status"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    TypeOfInviteSend = x["TypeOfInviteSend"] == DBNull.Value ? null : (string)x["TypeOfInviteSend"],
                    AssessmentURL = x["AssessmentURL"] == DBNull.Value ? null : (string)x["AssessmentURL"],
                    CreatedDate = x["CreatedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["CreatedDate"],
                    NotifyReminderID = x["NotifyReminderID"] == DBNull.Value ? 0 : (int)x["NotifyReminderID"],
                    HelperID = x["HelperID"] == DBNull.Value ? null : (int?)x["HelperID"],
                    PersonID = x["PersonID"] == DBNull.Value ? null : (long?)x["PersonID"],
                    PersonSupportID = x["PersonSupportID"] == DBNull.Value ? null : (int?)x["PersonSupportID"],
                    InviteToCompleteReceiverID = x["InviteToCompleteReceiverID"] == DBNull.Value ? 0 : (int)x["InviteToCompleteReceiverID"]
                });
                return inviteDetailsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails"></param>
        public void UpdateReminderInviteDetails(List<ReminderInviteToComplete> inviteDetails)
        {
            try
            {
                var res = this.UpdateBulkAsync(inviteDetails);
                res.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails"></param>
        public void AddReminderInviteDetails(List<ReminderInviteToComplete> inviteDetails)
        {
            try
            {
                var result = this.AddBulkAsync(inviteDetails);
                result.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
