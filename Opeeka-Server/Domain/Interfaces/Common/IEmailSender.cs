// -----------------------------------------------------------------------
// <copyright file="IUtility.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Net;

namespace Opeeka.PICS.Domain.Interfaces.Common
{
    public interface IEmailSender
    {
        HttpStatusCode SendEmailAsync(SendEmail sendEmail);
    }
}
