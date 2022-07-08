using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Opeeka.PICS.Infrastructure.Common
{
    public class SMSSender: ISMSSender
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<SMSSender> logger;
        public SMSSender(IConfiguration config, ILogger<SMSSender> log)
        {
            this.configuration = config;
            this.logger = log;
        }

        public bool SendSMS(string body, string ToNumber, string FromNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(FromNumber))
                {
                    var _accountSid = this.configuration["TwiloAccountSid"];
                    var _authToken = this.configuration["TwiloAuthToken"];

                    TwilioClient.Init(_accountSid, _authToken);

                    string _body = body;

                    try
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        var message = MessageResource.Create(
                        to: new PhoneNumber(ToNumber),
                        from: new PhoneNumber(FromNumber),
                        body: _body.Replace("\\n", "\n")
                        );
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
