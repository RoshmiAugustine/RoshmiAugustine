// -----------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Infrastructure.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;

namespace Opeeka.PICS.Infrastructure.Common
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<EmailSender> logger;
        public EmailSender(IConfiguration config, ILogger<EmailSender> log)
        {
            this.configuration = config;
            this.logger = log;
        }

        public HttpStatusCode SendEmailAsync(SendEmail sendEmail)
        {
            HttpStatusCode responseCode = HttpStatusCode.BadRequest;
            try
            {
                var apiKey = this.configuration["Email-Key"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(sendEmail.FromEmail, sendEmail.FromDisplayName);
                // var from = new EmailAddress(this.configuration["Email:FromEmail"], this.configuration["Email:FromDisplayName"]);
                var subject = sendEmail.Subject;
                var to = new EmailAddress(sendEmail.ToEmail, sendEmail.ToDisplayName);
                SendGridMessage msg = new SendGridMessage();

                if (sendEmail.IsHtmlEmail)
                {
                    msg = MailHelper.CreateSingleEmail(from, to, subject, null, sendEmail.Body);
                }
                else
                {
                    msg = MailHelper.CreateSingleEmail(from, to, subject, sendEmail.Body, null);
                }
                var response = client.SendEmailAsync(msg).Result;
                responseCode = response.StatusCode;
                this.logger.LogInformation(MyLogEvents.InsertItem, $"EmailSender/SendEMail : Exception while sending email. {response.StatusCode}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"EmailSender/SendEMail : Exception while sending email. {ex.Message}");
            }
            return responseCode;
        }
    }
}
