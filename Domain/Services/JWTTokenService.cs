// -----------------------------------------------------------------------
// <copyright file="JWTTokenService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Configuration;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Newtonsoft.Json;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Domain.Interfaces.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Protocols;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Opeeka.PICS.Domain.Interfaces;
    using System.Diagnostics;
    using System.IO;

    public class JWTTokenService : BaseService, IJWTTokenService
    {
        private IConfiguration _config;
        private const string Secret = "VGhpcyMjaXMjI215IyNTZWNyZXQjI0tleQ==";
        private readonly IApplicationObjectTypeService applicationObjectTypeService;
        private readonly IUserRepository _userRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPersonCollaborationRepository _personCollaborationRepository;
        private readonly ILogger<JWTTokenService> _logger;
        private readonly ICollaborationSharingRepository _collaborationSharingRepository;
        private readonly IAgencySharingRepository _agencySharingRepository;
        private readonly ISystemRoleRepository _systemRoleRepository;
        private readonly ICache _cache;
        private List<OpenIdConnectConfiguration> openIdconfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        public JWTTokenService(IConfiguration config, IApplicationObjectTypeService applicationObjectTypeService, IUserRepository userRepository,
            IAgencyRepository agencyRepository, ILogger<JWTTokenService> logger, IConfigurationRepository configRepo, IHttpContextAccessor httpContext,
            IPermissionRepository permissionRepository, IPersonRepository personRepository, IPersonCollaborationRepository personCollaborationRepository,
            ICollaborationSharingRepository collaborationSharingRepository, IAgencySharingRepository agencySharingRepository, ISystemRoleRepository systemRoleRepository, ICache cache)
        : base(configRepo, httpContext)
        {
            _config = config;
            this.applicationObjectTypeService = applicationObjectTypeService;
            _userRepository = userRepository;
            _agencyRepository = agencyRepository;
            this._logger = logger;
            _permissionRepository = permissionRepository;
            _personRepository = personRepository;
            _collaborationSharingRepository = collaborationSharingRepository;
            _agencySharingRepository = agencySharingRepository;
            _systemRoleRepository = systemRoleRepository;
            this._cache = cache;
            openIdconfig = this._cache.GetOpenIDConfig();
            httpContextAccessor = httpContext;
        }
        public string GenerateJSONWebToken(UsersDTO userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt-Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.UtcNow.AddMinutes(360),
              signingCredentials: credentials);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

        public ClaimsPrincipal GetPrincipal(string token, string tenantId, int agencyId, string personIndex, string collaborationSharingIndex, string agencySharingIndex, bool isActivePerson = false)
        {
            try
            {
                _logger.LogInformation($"GetPrincipal started");
                var issuer = _config["B2CIssuer"];
                var B2CAudience = _config["B2CAudience"];
               // ConfigurationManager<OpenIdConnectConfiguration> configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
               // var configTask = configManager.GetConfigurationAsync();
                var tokenHandler = new JwtSecurityTokenHandler();
                string keyVaultAthToken = _config["APISecret"];
                if (token == keyVaultAthToken)
                {
                    tenantId = string.IsNullOrEmpty(tenantId) ? 0.ToString() : tenantId;
                    var claims = new List<Claim> {
                                    new Claim(PCISEnum.TokenClaims.EHRRequest, "true"),
                                    new Claim(PCISEnum.TokenClaims.TenantId, tenantId.ToString())
                    };
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt-Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var generatedToken = new JwtSecurityToken(_config["Jwt:Issuer"],
                      _config["Jwt:Issuer"],
                      claims,
                      expires: DateTime.UtcNow.AddMinutes(360),
                      signingCredentials: credentials);
                    string jwtTokenResult = new JwtSecurityTokenHandler().WriteToken(generatedToken);

                    var validationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidAudience = _config["Jwt:Issuer"]
                    };
                    var EHRprincipal = tokenHandler.ValidateToken(jwtTokenResult, validationParameters, out var ehrSecurityToken);
                    var claimsIdentity = new ClaimsIdentity(EHRprincipal.Claims, "AuthenticationTypes.Federation");
                    var claimsPrincipal = new ClaimsPrincipal();
                    claimsPrincipal.AddIdentity(claimsIdentity);
                    return claimsPrincipal;
                }
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken == null)
                {
                    return null;
                }
                if (jwtToken.Issuer != issuer)
                {
                    _logger.LogError($"Issuer is not matched with issuer in Token");
                    return null;
                }
                List<string> RolesList = new List<string>
                    {
                        jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cRole).FirstOrDefault().Value.ToString()
                    };

                var systemRolesTask = _systemRoleRepository.GetSystemRoleByRoleNameAsync(RolesList);

                var openIdConnector = openIdconfig[0];

                var policyNameOfRPOC = jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cRPOCTFP).FirstOrDefault();
                if(policyNameOfRPOC != null)
                {
                    openIdConnector = openIdconfig.Where(x => x.TokenEndpoint.ToLower().Contains(policyNameOfRPOC.Value.ToLower())).FirstOrDefault();
                }

                var policyNameOfSSO = jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cAuthenticationSource).FirstOrDefault();
                if (policyNameOfSSO != null && policyNameOfSSO.Value.ToLower()==PCISEnum.AzureConstants.SSOAuthentication.ToLower())
                {
                    openIdConnector = openIdconfig[2];
                }

                var parameter = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = openIdConnector.SigningKeys,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ValidIssuer = issuer,
                    ValidAudience = B2CAudience
                };

                var principal = tokenHandler.ValidateToken(token, parameter, out var securityToken);

                UsersDTO usersDTO = new UsersDTO();
                usersDTO.UserName = usersDTO.Email = jwtToken.Claims.Where(x => x.Type.Contains(PCISEnum.B2cClaims.B2cEmail)).FirstOrDefault().Value.ToString();
                usersDTO.Name = jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cName).FirstOrDefault().Value.ToString();
                usersDTO.UserID = Convert.ToInt32(jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cUserId).FirstOrDefault().Value);
                usersDTO.AgencyID = Convert.ToInt32(jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cTenantId).FirstOrDefault().Value);
                usersDTO.AgencyAbbrev = jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cTenantAbbreviation).FirstOrDefault().Value.ToString();
                usersDTO.InstanceURL = jwtToken.Claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cInstanceURL).FirstOrDefault().Value.ToString();
                var systemRoleName = systemRolesTask;

                if (systemRoleName.Count > 0)
                {
                    var role = systemRoleName.First();
                    usersDTO.RoleID = role.SystemRoleID;
                    if (role.Name == PCISEnum.Roles.SuperAdmin)
                    {
                        if (agencyId != 0)
                        {
                            usersDTO.AgencyID = agencyId;
                        }
                        if (tenantId != string.Empty)
                        {
                            usersDTO.AgencyAbbrev = tenantId;
                        }
                    }
                }
                usersDTO.Roles = RolesList;

                if (usersDTO != null)
                {
                    if (tenantId != "" && usersDTO.AgencyAbbrev != null && usersDTO.AgencyAbbrev.ToLower() != tenantId.ToLower())
                    {
                        _logger.LogError($"This tenantid is not included in this user's associated tenants");
                        return null;
                    }
                    var env_instanceURL = _config["InstanceURL"];
                    if (env_instanceURL!= null && (usersDTO.InstanceURL.ToLower() != env_instanceURL.ToLower() || string.IsNullOrWhiteSpace(usersDTO.InstanceURL)))
                    {
                        _logger.LogError($"This user's({usersDTO.UserID}) instanceURL in token : {usersDTO.InstanceURL} is not matching with environment instanceURL : {env_instanceURL}");
                        return null;
                    }
                }
                _logger.LogInformation($"GetPrincipal ended");
                return PopulateUserIdentity(principal, usersDTO, personIndex, collaborationSharingIndex, agencySharingIndex, isActivePerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                return null;
            }
        }

        /// <inheritdoc/>


        /// <summary>
        /// Method for populating claim
        /// </summary>
        /// <param name="claimsFromJWT"></param>
        /// <param name="rawToken"></param>
        /// <returns>ClaimsPrincipal</returns>
        private ClaimsPrincipal PopulateUserIdentity(ClaimsPrincipal claimsFromJWT, UsersDTO userDTO, string personIndex, string collaborationSharingIndex, string agencySharingIndex, bool isActivePerson = false)
        {
            try
            {
                var claimsIdentity = new ClaimsIdentity(claimsFromJWT.Claims, "AuthenticationTypes.Federation");

            // ....... if using B2C auth, adding clamis..............
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.RoleID, userDTO.RoleID.ToString()));
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.UserId, userDTO.UserID.ToString()));
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.AgencyAbbrev, userDTO.AgencyAbbrev));
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.TenantId, userDTO.AgencyID.ToString()));
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.Email, userDTO.UserName));
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.Roles, JsonConvert.SerializeObject(userDTO.Roles)));
            claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.InstanceURL, JsonConvert.SerializeObject(userDTO.InstanceURL)));
            var agencyConfiguration = base.GetConfigurationByName(PCISEnum.ConfigurationParameters.Culture, userDTO.AgencyID);
            if (agencyConfiguration != null && !String.IsNullOrEmpty(agencyConfiguration.Value))
            {
                claimsIdentity.AddClaim(new Claim(PCISEnum.TokenClaims.Culture, agencyConfiguration.Value));
            }
           
            var roleID = userDTO.RoleID;
            
            var userPermissions = GetUserPermissionsByRoleID(roleID, PCISEnum.ApplicationObjectTypes.APIEndPoint);
            var claimsPrincipal = new ClaimsPrincipal();
            //claimsIdentity.AddClaim(new Claim("token", rawToken));
            // if(!string.IsNullOrEmpty(b2cToken))
            // {
            //     claimsIdentity.AddClaim(new Claim("b2cToken", b2cToken));
            // }

            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            var userIDclaim = claimsIdentity.FindFirst(PCISEnum.TokenClaims.UserId);
            var userID = Convert.ToInt32(userIDclaim.Value);

            var path = httpContextAccessor.HttpContext.Request.Path.Value;
            var routeValues = httpContextAccessor.HttpContext.Request.RouteValues.ToList();
            var parameterRouteValues = routeValues.Where(rv => rv.Key != "controller" && rv.Key != "action").ToList();
            path = ReplaceRouteValues(path, parameterRouteValues);
            var currentPermission = userPermissions.Where(p => p.Name.ToLower().Trim('/') == path.ToLower().Trim('/')).ToList();
            var distinctApplicationObjectNames = currentPermission.Select(x => x.Name).Distinct().ToList();
            if (currentPermission.Count > 0)
            {
                foreach (var obj in distinctApplicationObjectNames)
                {
                    ApplicationObjectDTO applicationObject = new ApplicationObjectDTO();
                    applicationObject.OperationTypes = new List<string>();
                    applicationObject.Name = obj;
                    var permissions = currentPermission.Where(x => x.Name == obj).ToList();
                    applicationObject.ApplicationObjectTypeName = permissions.FirstOrDefault().ApplicationObjectTypeName;
                    applicationObject.OperationTypes = permissions.Select(x=>x.OperationTypeName.ToLower()).ToList();

                    /*foreach (var permission in userPermissions)
                    {
                        if (obj == permission.Name)
                        {
                            applicationObject.Name = permission.Name;
                            applicationObject.ApplicationObjectTypeName = permission.ApplicationObjectTypeName;
                            applicationObject.OperationTypes.Add(permission.OperationTypeName.ToLower());
                        }
                    }*/
                    if (!string.IsNullOrEmpty(applicationObject.Name))
                    {
                        var json = JsonConvert.SerializeObject(applicationObject);
                        if (string.IsNullOrEmpty(json))
                        {
                            continue;
                        }
                        claimsIdentity.AddClaim(new Claim(PCISEnum.ApplicationObjectTypes.APIEndPoint + "Permission", json));
                    }
                }
            }
            var tenantIDclaim = claimsIdentity.FindFirst(PCISEnum.TokenClaims.TenantId);
            var tenantID = Convert.ToInt32(tenantIDclaim.Value);
            if (!string.IsNullOrEmpty(personIndex))
            {
                var person = _personRepository.GetPerson(Guid.Parse(personIndex));
                if (person.AgencyID != tenantID)
                {
                    claimsIdentity.AddClaim(new Claim("IsSharedPermission", "true"));

                        var agencySharingPolicyID = _agencySharingRepository.GetAgencySharing(Guid.Parse(agencySharingIndex)).Result.AgencySharingPolicyID;
                        var collaborationSharingPolicyID = _collaborationSharingRepository.GetCollaborationSharing(Guid.Parse(collaborationSharingIndex)).Result.CollaborationSharingPolicyID;
                        List<string> applicationObjectTypes = new List<string>();
                        applicationObjectTypes.Add(PCISEnum.ApplicationObjectTypes.APIEndPoint);
                        var sharedPermissions = _permissionRepository.GetUserSharedPermissionsByRoleID(roleID, applicationObjectTypes, agencySharingPolicyID ?? 0, collaborationSharingPolicyID ?? 0, isActivePerson);
                        if (sharedPermissions.Count > 0)
                        {
                            var distinctSharedApplicationObjectNames = sharedPermissions.Select(x => x.Name).Distinct();
                            foreach (var obj in distinctSharedApplicationObjectNames)
                            {
                                ApplicationObjectDTO applicationObject = new ApplicationObjectDTO();
                                applicationObject.OperationTypes = new List<string>();

                                foreach (var permission in sharedPermissions)
                                {
                                    if (obj == permission.Name)
                                    {
                                        applicationObject.Name = permission.Name;
                                        applicationObject.ApplicationObjectTypeName = permission.ApplicationObjectTypeName;
                                        applicationObject.OperationTypes.Add(permission.OperationTypeName.ToLower());
                                    }
                                }
                                if (!string.IsNullOrEmpty(applicationObject.Name))
                                {
                                    var json = JsonConvert.SerializeObject(applicationObject);
                                    if (string.IsNullOrEmpty(json)) continue;

                                    claimsIdentity.AddClaim(new Claim(PCISEnum.ApplicationObjectTypes.APIEndPoint + "SharedPermission", json));
                                }
                            }
                        }
                    }
                    else
                    {
                        claimsIdentity.AddClaim(new Claim(PCISEnum.Parameters.IsSharedPermission, "false"));
                    }
                }
                claimsPrincipal.AddIdentity(claimsIdentity);
                return claimsPrincipal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ApplicationObjectDTO> GetUserPermissionsByUserID(long userID, string applicationObjectType)
        {
            var result = _userRepository.GetUserPermissionsByUserID(userID, applicationObjectType);
            return result;
        }

        public List<ApplicationObjectDTO> GetUserPermissionsByRoleID(int roleID, string applicationObjectType)
        {
            try
            {
                List<string> applicationObjectTypes = new List<string>();
                applicationObjectTypes.Add(applicationObjectType);
                var result = _permissionRepository.GetUserPermissionsByRoleID(roleID, applicationObjectTypes);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ReplaceRouteValues(string path, List<KeyValuePair<string, object>> parameterRouteValues)
        {
            var segments = path.Split('/').ToList();
            var routeSegments = segments.SkipLast(parameterRouteValues.Count).ToList();
            var parameterSegments = segments.Skip(routeSegments.Count).ToList();
            foreach (var item in parameterRouteValues)
            {
                string oldValue = item.Value.ToString();
                string newValue = "{" + item.Key + "}";
                int index = parameterSegments.IndexOf(oldValue);
                if (index != -1)
                {
                    parameterSegments[index] = newValue;
                }
            }

            routeSegments.AddRange(parameterSegments);
            var result = string.Join('/', routeSegments.ToArray());
            return result;
        }

    }
}