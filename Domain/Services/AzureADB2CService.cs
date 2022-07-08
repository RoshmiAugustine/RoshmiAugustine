using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Services
{
    public class AzureADB2CService : BaseService, IAzureADB2CService
    {
        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<AzureADB2CService> logger;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        private readonly IAgencyRepository agencyRepository;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly IAssessmentService assessmentService;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly IHttpContextAccessor httpContext;
        private readonly IEmailSender _emailSender;
        private static GraphServiceClient graphClient;

        private static string b2cExtensionAppClientId;
        private static string tenantId;
        private static string clientSecret;
        private static string appId;
        private static string instanceURL;

        public AzureADB2CService(ILogger<AzureADB2CService> logger, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IEmailSender emailSender, IAgencyRepository agencyRepository,
            IConfigurationRepository configurationRepository, IConfiguration configuration, IUserRepository userRepository, IAssessmentService assessmentService)
            : base(configRepo, httpContext)
        {
            this.logger = logger;
            this.localize = localizeService;
            this.httpContext = httpContext;
            this._emailSender = emailSender;
            this.agencyRepository = agencyRepository;
            this.configurationRepository = configurationRepository;
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.assessmentService = assessmentService;

            //set params
            b2cExtensionAppClientId = this.configuration["B2cExtensionAppClientId"];
            tenantId = this.configuration["TenantId"];
            clientSecret = this.configuration["ClientSecret"];
            appId = this.configuration["B2cAppId"];
            instanceURL = this.configuration["InstanceURL"];
        }

        /// <summary>
        /// setup B2C Connection
        /// </summary>
        private static void setupB2CConnection()
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(appId)
            .WithTenantId(tenantId)
            .WithClientSecret(clientSecret)
            .Build();
            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

            // Set up the Microsoft Graph service client with client credentials
            graphClient = new GraphServiceClient(authProvider);
        }

        /// <summary>
        /// create user in b2c
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <param name="systemRoleName"></param>
        /// <returns></returns>
        public Tuple<string,bool> BuildUserSignUpToken(HelperDetailsDTO helperDTO, string systemRoleName, int UserID)
        {
            string password = GenerateNewPassword(4, 8, 4);
            setupB2CConnection();
            string b2cUserId;bool isEmailSend = false;
            try
            {
                var agency = this.agencyRepository.GetAgencyDetailsById(helperDTO.AgencyID).Result;
                b2cUserId = CreateUserWithCustomAttribute(helperDTO, systemRoleName, password, UserID, agency).GetAwaiter().GetResult();
                if (b2cUserId != null)
                {
                    isEmailSend = true;
                    if (helperDTO.SendSignUpMail)
                    {   //send mail
                        var statusCode = DraftSignUpEmail(password, helperDTO, agency);
                        if (statusCode != HttpStatusCode.Accepted)
                        {
                            isEmailSend = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Tuple.Create(b2cUserId, isEmailSend);
        }

        /// <summary>
        /// create new user in b2c
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <param name="systemRoleName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<string> CreateUserWithCustomAttribute(HelperDetailsDTO helperDTO, string systemRoleName, string password, int UserID, AgencyDTO agency)
        {
            try
            {
                //get agency,role,access details
                var agencyConfigurationList = base.GetConfigurationList(helperDTO.AgencyID);
                ConfigurationParameterDTO agencyConfig = new ConfigurationParameterDTO();
                if (agencyConfigurationList != null && agencyConfigurationList.Count() > 0)
                {
                    agencyConfig = agencyConfigurationList.Where(x => x.Name == PCISEnum.ConfigurationParameters.DateTimeFormat).FirstOrDefault();
                }

                // Declare the names of the custom attributes
                const string customAttributeName1 = PCISEnum.B2cCustomAttributes.B2cAgency;
                const string customAttributeName2 = PCISEnum.B2cCustomAttributes.B2cRole;
                const string customAttributeName3 = PCISEnum.B2cCustomAttributes.B2cAccess;
                const string customAttributeName4 = PCISEnum.B2cCustomAttributes.B2cMustResetPassword;
                const string customAttributeName5 = PCISEnum.B2cCustomAttributes.B2cTenantId;
                const string customAttributeName6 = PCISEnum.B2cCustomAttributes.B2cTenantAbbreviation;
                const string customAttributeName7 = PCISEnum.B2cCustomAttributes.B2cUserId;
                const string customAttributeName8 = PCISEnum.B2cCustomAttributes.B2cDateTimeFormat;
                const string customAttributeName9 = PCISEnum.B2cCustomAttributes.B2cInstanceURL;

                // Get the complete name of the custom attribute (Azure AD extension)
                string _customAttributeName1 = GetCompleteAttributeName(customAttributeName1);
                string _customAttributeName2 = GetCompleteAttributeName(customAttributeName2);
                string _customAttributeName3 = GetCompleteAttributeName(customAttributeName3);
                string _customAttributeName4 = GetCompleteAttributeName(customAttributeName4);
                string _customAttributeName5 = GetCompleteAttributeName(customAttributeName5);
                string _customAttributeName6 = GetCompleteAttributeName(customAttributeName6);
                string _customAttributeName7 = GetCompleteAttributeName(customAttributeName7);
                string _customAttributeName8 = GetCompleteAttributeName(customAttributeName8);
                string _customAttributeName9 = GetCompleteAttributeName(customAttributeName9);

                // Fill custom attributes
                IDictionary<string, object> extensionInstance = new Dictionary<string, object>();

                extensionInstance.Add(_customAttributeName1, agency.Name);
                extensionInstance.Add(_customAttributeName2, systemRoleName);
                extensionInstance.Add(_customAttributeName3, systemRoleName);
                extensionInstance.Add(_customAttributeName4, "true");
                extensionInstance.Add(_customAttributeName5, agency.AgencyID);
                extensionInstance.Add(_customAttributeName6, agency.Abbrev);
                extensionInstance.Add(_customAttributeName7, UserID);
                if (agencyConfig != null)
                {
                    extensionInstance.Add(_customAttributeName8, agencyConfig.Value);
                }
                extensionInstance.Add(_customAttributeName9, instanceURL);

                string displayName = helperDTO.FirstName + (string.IsNullOrEmpty(helperDTO.MiddleName) ? "" : " " + helperDTO.MiddleName) + (string.IsNullOrEmpty(helperDTO.LastName) ? "" : " " + helperDTO.LastName);

                var result = await graphClient.Users
                .Request()
                .AddAsync(new User
                {
                    GivenName = helperDTO.FirstName,
                    Surname = helperDTO.LastName,
                    DisplayName = displayName,
                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = tenantId,
                            IssuerAssignedId = helperDTO.Email
                        }
                    },
                    PasswordProfile = new PasswordProfile()
                    {
                        ForceChangePasswordNextSignIn = false,
                        Password = password
                    },
                    PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = extensionInstance
                });
                return result.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Send mail with sign up url and password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="helperDTO"></param>
        /// <returns></returns>
        private HttpStatusCode DraftSignUpEmail(string password, HelperDetailsDTO helperDTO, AgencyDTO agency)
        {
            string signUpUrl = this.configuration["B2cSignUpUrl"];
            string toDisplayName = string.Format("{0} {1} {2}", helperDTO.FirstName, string.IsNullOrEmpty(helperDTO.MiddleName) ? "" : helperDTO.MiddleName, helperDTO.LastName);
            toDisplayName = toDisplayName.Replace("  ", " ");
            var fromemail = this.configurationRepository.GetConfigurationByName(PCISEnum.B2cEmail.FromEmail, helperDTO.AgencyID);
            var fromemailDisplayName = this.configurationRepository.GetConfigurationByName(PCISEnum.B2cEmail.FromDisplayName, helperDTO.AgencyID);

            var emailText = this.configurationRepository.GetConfigurationByName(PCISEnum.B2cEmail.EmailText, helperDTO.AgencyID);
            var emailSubject = this.configurationRepository.GetConfigurationByName(PCISEnum.B2cEmail.EmailSubject, helperDTO.AgencyID);
            string emailBody = this.localize.GetLocalizedHtmlString(emailText.Value);

            var agencyURL = this.assessmentService.CreateBaseURL(agency.Abbrev);
            var agencyUri = new Uri(agencyURL);
            var baseUri = agencyUri.GetLeftPart(System.UriPartial.Authority);

            //replace email url code in email text
            if (emailBody != null)
            {
                emailBody = emailBody.Replace(PCISEnum.B2cEmail.applicationUrlReplaceText, baseUri);
                emailBody = emailBody.Replace(PCISEnum.B2cEmail.emailPasswordReplaceText, password);
                emailBody = emailBody.Replace(PCISEnum.B2cEmail.emailUrlCodeReplaceText, signUpUrl);
                emailBody = emailBody.Replace(PCISEnum.B2cEmail.personNameReplaceText, helperDTO.FirstName);
            }

            //email body
            SendEmail sendEmail = new SendEmail()
            {
                Body = emailBody,
                IsHtmlEmail = true,
                Subject = this.localize.GetLocalizedHtmlString(emailSubject.Value),
                FromDisplayName = fromemailDisplayName.Value,
                FromEmail = fromemail.Value,
                ToDisplayName = toDisplayName,
                ToEmail = helperDTO.Email
            };
            return _emailSender.SendEmailAsync(sendEmail);
        }

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <param name="lowercase"></param>
        /// <param name="uppercase"></param>
        /// <param name="numerics"></param>
        /// <returns></returns>
        private static string GenerateNewPassword(int lowercase, int uppercase, int numerics)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }

        /// <summary>
        /// Get CompleteAttributeName
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        private string GetCompleteAttributeName(string attributeName)
        {
            string _b2cExtensionAppClientId = B2cCustomAttributeHelper();
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new System.ArgumentException("Parameter cannot be null", nameof(attributeName));
            }

            return $"extension_{_b2cExtensionAppClientId}_{attributeName}";
        }

        /// <summary>
        /// B2cCustomAttribute
        /// </summary>
        /// <returns></returns>
        private string B2cCustomAttributeHelper()
        {
            return b2cExtensionAppClientId.Replace("-", "");
        }

        /// <summary>
        /// Update User Extensions
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <param name="systemRoleName"></param>
        /// <returns></returns>
        public async Task<string> UpdateUserExtensions(HelperDTO helperDTO, string systemRoleName)
        {
            setupB2CConnection();
            try
            {
                var userDetails = this.userRepository.GetUsersByUsersIDAsync(helperDTO.UserID).GetAwaiter().GetResult();
                var agency = this.agencyRepository.GetAgencyDetailsById(helperDTO.AgencyId).Result;

                var agencyConfigurationList = base.GetConfigurationList(helperDTO.AgencyId);
                ConfigurationParameterDTO agencyConfig = new ConfigurationParameterDTO();
                if (agencyConfigurationList != null && agencyConfigurationList.Count() > 0)
                {
                    agencyConfig = agencyConfigurationList.Where(x => x.Name == PCISEnum.ConfigurationParameters.DateTimeFormat).FirstOrDefault();
                }

                // Declare the names of the custom attributes
                const string customAttributeName1 = PCISEnum.B2cCustomAttributes.B2cAgency;
                const string customAttributeName2 = PCISEnum.B2cCustomAttributes.B2cRole;
                const string customAttributeName3 = PCISEnum.B2cCustomAttributes.B2cAccess;
                const string customAttributeName5 = PCISEnum.B2cCustomAttributes.B2cTenantId;
                const string customAttributeName6 = PCISEnum.B2cCustomAttributes.B2cTenantAbbreviation;
                const string customAttributeName7 = PCISEnum.B2cCustomAttributes.B2cUserId;
                const string customAttributeName8 = PCISEnum.B2cCustomAttributes.B2cDateTimeFormat;
                const string customAttributeName9 = PCISEnum.B2cCustomAttributes.B2cInstanceURL;

                // Get the complete name of the custom attribute (Azure AD extension)
                string _customAttributeName1 = GetCompleteAttributeName(customAttributeName1);
                string _customAttributeName2 = GetCompleteAttributeName(customAttributeName2);
                string _customAttributeName3 = GetCompleteAttributeName(customAttributeName3);
                string _customAttributeName5 = GetCompleteAttributeName(customAttributeName5);
                string _customAttributeName6 = GetCompleteAttributeName(customAttributeName6);
                string _customAttributeName7 = GetCompleteAttributeName(customAttributeName7);
                string _customAttributeName8 = GetCompleteAttributeName(customAttributeName8);
                string _customAttributeName9 = GetCompleteAttributeName(customAttributeName9);

                // Fill custom attributes
                IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
                extensionInstance.Add(_customAttributeName1, agency.Name);
                extensionInstance.Add(_customAttributeName2, systemRoleName);
                extensionInstance.Add(_customAttributeName3, systemRoleName);
                extensionInstance.Add(_customAttributeName5, agency.AgencyID);
                extensionInstance.Add(_customAttributeName6, agency.Abbrev);
                extensionInstance.Add(_customAttributeName7, helperDTO.UserID);
                if (agencyConfig != null)
                {
                    extensionInstance.Add(_customAttributeName8, agencyConfig.Value);
                }
                extensionInstance.Add(_customAttributeName9, instanceURL);

                string displayName = helperDTO.FirstName + (string.IsNullOrEmpty(helperDTO.MiddleName) ? "" : " " + helperDTO.MiddleName) + (string.IsNullOrEmpty(helperDTO.LastName) ? "" : " " + helperDTO.LastName);

                var result = await graphClient.Users[userDetails.AspNetUserID].Request().UpdateAsync(new User
                {
                    DisplayName = displayName,
                    AdditionalData = extensionInstance

                });
                return "";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Update User Agency Extension
        /// </summary>
        /// <param name="usersDTO"></param>
        /// <returns></returns>
        public async Task<string> UpdateUserAgencyExtension(UsersDTO usersDTO)
        {
            setupB2CConnection();
            try
            {
                var agency = this.agencyRepository.GetAgencyDetailsById(usersDTO.AgencyID).Result;

                var agencyConfigurationList = base.GetConfigurationList(usersDTO.AgencyID);
                ConfigurationParameterDTO agencyConfig = new ConfigurationParameterDTO();
                if (agencyConfigurationList != null && agencyConfigurationList.Count() > 0)
                {
                    agencyConfig = agencyConfigurationList.Where(x => x.Name == PCISEnum.ConfigurationParameters.DateTimeFormat).FirstOrDefault();
                }

                // Declare the names of the custom attributes
                const string customAttributeName1 = PCISEnum.B2cCustomAttributes.B2cAgency;
                const string customAttributeName5 = PCISEnum.B2cCustomAttributes.B2cTenantId;
                const string customAttributeName6 = PCISEnum.B2cCustomAttributes.B2cTenantAbbreviation;
                const string customAttributeName8 = PCISEnum.B2cCustomAttributes.B2cDateTimeFormat;

                // Get the complete name of the custom attribute (Azure AD extension)
                string _customAttributeName1 = GetCompleteAttributeName(customAttributeName1);
                string _customAttributeName5 = GetCompleteAttributeName(customAttributeName5);
                string _customAttributeName6 = GetCompleteAttributeName(customAttributeName6);
                string _customAttributeName8 = GetCompleteAttributeName(customAttributeName8);

                // Fill custom attributes
                IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
                extensionInstance.Add(_customAttributeName1, agency.Name);
                extensionInstance.Add(_customAttributeName5, agency.AgencyID);
                extensionInstance.Add(_customAttributeName6, agency.Abbrev);
                if (agencyConfig != null)
                {
                    extensionInstance.Add(_customAttributeName8, agencyConfig.Value);
                }

                var result = await graphClient.Users[usersDTO.AspNetUserID].Request().UpdateAsync(new User
                {
                    DisplayName = usersDTO.Name,
                    AdditionalData = extensionInstance

                });
                return "";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete UserBy Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> DeleteUserById(string userId)
        {
            setupB2CConnection();
            try
            {
                var user = new User
                {
                    AccountEnabled = false
                };

                var result = await graphClient.Users[userId]
                   .Request()
                   .UpdateAsync(user);
                return userId;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        /// <summary>
        /// Reset Password And Send Mail
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        public string ResetPasswordAndSendMail(HelperDetailsDTO helperDTO, int UserID)
        {
            string password = GenerateNewPassword(4, 8, 4);
            setupB2CConnection();
            string b2cUserId;
            try
            {
                var agency = this.agencyRepository.GetAgencyDetailsById(helperDTO.AgencyID).Result;
                b2cUserId = ResetPassword(password, UserID).GetAwaiter().GetResult();
                if (b2cUserId != null && b2cUserId != "-1")
                {
                    //send mail
                    var statusCode = DraftSignUpEmail(password, helperDTO, agency);
                    if (statusCode != HttpStatusCode.Accepted)
                    {
                        b2cUserId = "-2";
                    }
                }
                else
                {
                    b2cUserId = "-1";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return b2cUserId;
        }

        private async Task<string> ResetPassword(string password, int UserID)
        {
            try
            {
                var existingUser = this.userRepository.GetUsersByUsersIDAsync(UserID).Result;
                if (existingUser.AspNetUserID !=null)
                {
                    const string customAttributeName = PCISEnum.B2cCustomAttributes.B2cMustResetPassword;
                    string _customAttributeName = GetCompleteAttributeName(customAttributeName);
                    IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
                    extensionInstance.Add(_customAttributeName, "true");

                    var user = new User
                    {
                        PasswordPolicies = "DisablePasswordExpiration",
                        PasswordProfile = new PasswordProfile
                        {
                            ForceChangePasswordNextSignIn = false,
                            Password = password,
                        },
                        AdditionalData = extensionInstance
                    };

                    var result = await graphClient.Users[existingUser.AspNetUserID]
                       .Request()
                       .UpdateAsync(user);
                    return existingUser.AspNetUserID;
                }
                else
                {
                    return "-1";
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
