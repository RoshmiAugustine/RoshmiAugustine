using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ImportProcess.DTO;
using ImportProcess.Enums;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ImportProcess.Common
{
    public class EmailSender
    {
        public HttpStatusCode SendEmailAsync(SendEmailDTO sendEmail)
        {
            HttpStatusCode responseCode = HttpStatusCode.BadRequest;
            try
            {
                var apiKey = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.EmailKeyFromKeyVault, EnvironmentVariableTarget.Process);
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
            }
            catch (Exception)
            {
                throw;
            }
            return responseCode;
        }
    }
}
