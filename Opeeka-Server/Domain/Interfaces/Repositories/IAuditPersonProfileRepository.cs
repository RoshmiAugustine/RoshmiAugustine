// -----------------------------------------------------------------------
// <copyright file="IAuditPersonProfileRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAuditPersonProfileRepository
    {
        /// <summary>
        /// AddPersonProfileDetailsForAudit.
        /// </summary>
        /// <param name="auditPersonProfile"></param>
        /// <returns></returns>
        public AuditPersonProfile AddPersonProfileDetailsForAudit(AuditPersonProfile auditPersonProfile);

        /// <summary>
        /// getHelperHistoryDetails.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        List<AuditPersonProfileDTO> getHelperHistoryDetails(long personID);

        /// <summary>
        /// getCollaborationHistoryDetails'.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        List<AuditPersonProfileDTO> getCollaborationHistoryDetails(long personID);
    }
}
