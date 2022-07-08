using System;
using System.Net;
using EmailProcess.Enums;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace EmailProcess.Common
{
    public class SMSSender
    {
        private string _accountSid;
        private string _authToken { get; set; }
        public SMSSender()
        {
            _accountSid = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.TwiloAccountSid, EnvironmentVariableTarget.Process);
            _authToken = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.TwiloAuthToken, EnvironmentVariableTarget.Process); ;
        }

        public bool SendSMS(string body, string ToNumber, string FromNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(FromNumber))
                {
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
