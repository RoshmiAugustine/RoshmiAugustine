// -----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Infrastructure.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Opeeka.PICS.Domain.Entities;
using System.Web;
using Opeeka.PICS.Domain.Interfaces.Providers.Contract;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO.Input;
using System.Security.Claims;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Domain.DTO.Output;
using Newtonsoft.Json;

namespace Opeeka.PICS.Domain.Services
{
    public class UserService : IUserService
    {

        private IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly ITenantProvider _iTenantProvider;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ISystemRoleRepository _systemRoleRepository;
        private readonly ISystemRolePermissionRepository _systemRolePermissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IAgencyRepository agencyRepository;
        private readonly IConfigurationRepository configurationRepository;
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        /// Initializes a new instance of the <see cref="fileService"/> class.
        private readonly IFileService fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastService"/> class.
        /// </summary>
        /// <param name="iweatherForecastRepository">returns instance of weather forecast.</param>
        public UserService(IConfigurationRepository configurationRepository,IUserRepository userRepository, IMapper mapper, ITenantProvider tenantProvider, UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager,
        ISystemRoleRepository systemRoleRepository, IUserRoleRepository userRoleRepository, IAgencyRepository agencyRepository, ISystemRolePermissionRepository systemRolePermissionRepository,
        IRolePermissionRepository rolePermissionRepository, IHttpContextAccessor _accessor, LocalizeService localizeService, IFileService fileService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            _iTenantProvider = tenantProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _systemRoleRepository = systemRoleRepository;
            _userRoleRepository = userRoleRepository;
            this.agencyRepository = agencyRepository;
            _systemRolePermissionRepository = systemRolePermissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            this._accessor = _accessor;
            this.localize = localizeService;
            this.fileService = fileService;
            this.configurationRepository = configurationRepository;
        }


        public UserDTO VerifyUser(string userName, string password)
        {
            // userRepository.GetAsync
            /*List<AspNetUser> users = new List<AspNetUser>()
            {
               new AspNetUser { UserName = "Brielle", Email = "Brielle@abc.com", AgencyId = 4, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Maxwell", Email = "Maxwell@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Blaine", Email = "Blaine@abc.com", AgencyId = 4, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Isadora@abc.com", Email = "Isadora@abc.com", AgencyId = 4, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Keegan@abc.com", Email = "Keegan@abc.com", AgencyId = 4, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Jena@abc.com", Email = "Jena@abc.com", AgencyId = 4, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Nita@abc.com", Email = "Nita@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Benedict@abc.com", Email = "Benedict@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Maggy@abc.com", Email = "Maggy@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Stewart@abc.com", Email = "Stewart@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Beck@abc.com", Email = "Beck@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Fay@abc.com", Email = "Fay@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Lawrence@abc.com", Email = "Lawrence@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Jana@abc.com", Email = "Jana@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Bruce@abc.com", Email = "Bruce@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Lucy@abc.com", Email = "Lucy@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Denise@abc.com", Email = "Denise@abc.com", AgencyId = 1, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Aaron@abc.com", Email = "Aaron@abc.com", AgencyId = 1, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Urielle@abc.com", Email = "Urielle@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Rina@abc.com", Email = "Rina@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Stephen@abc.com", Email = "Stephen@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Daria@abc.com", Email = "Daria@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Sydney@abc.com", Email = "Sydney@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "Cherokee@abc.com", Email = "Cherokee@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "KJZPXTHR068@abc.com", Email = "KJZPXTHR068@abc.com", AgencyId = 1, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "SuperAdmin@abc.com", Email = "SuperAdmin@abc.com", AgencyId = 1, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "ABCAdmin@abc.com", Email = "ABCAdmin@abc.com", AgencyId = 1, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "FranklinAdmin@abc.com", Email = "FranklinAdmin@abc.com", AgencyId = 2, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "TXAdmin@abc.com", Email = "TXAdmin@abc.com", AgencyId = 3, IsActive = true, EmailConfirmed = true },
                new AspNetUser { UserName = "LMUAdmin@abc.com", Email = "LMUAdmin@abc.com", AgencyId = 4, IsActive = true, EmailConfirmed = true }
            //........................ and so on
            };

            // var identityUser = new AspNetUser { UserName = "System", Email = "System@abc.com", AgencyId = agencyId, IsActive = true, EmailConfirmed = true };
            foreach (var identityUser in users)
            {
                string tempPassword = "Password@123";

                var result = _userManager.CreateAsync(identityUser, tempPassword).Result;
            }
            */

            List<UserDTO> users2 = new List<UserDTO>()
             {
                new UserDTO() {Name = "ABC", Email="athulya.r@naicotech.com",Password="12345", Role="OrgAdmin", TenantID = "Hospital1", UserID=1 },
                new UserDTO() {Name = "XYZ", Email="xyz@naicoits.com",Password="12345", Role="Helper", TenantID = "1" , UserID=2},
                new UserDTO() {Name = "QWE", Email="qwe@naicoits.com",Password="12345", Role="SuperAdmin", TenantID = "1",  UserID=3}

                    //........................ and so on
                 };
            return users2.Where(x => x.Email == userName).FirstOrDefault();
        }

        // public UsersDTO GetUser(string email, string tenantID)
        // {
        //     List<UserDTO> users = new List<UserDTO>()
        //     {
        //        new UserDTO() {Name = "ABC", Email="athulya.r@naicotech.com",Password="12345", Role="OrgAdmin", TenantID = "Hospital1", UserID=1 },
        //        new UserDTO() {Name = "XYZ", Email="xyz@naicoits.com",Password="12345", Role="Helper", TenantID = "1" , UserID=2},
        //        new UserDTO() {Name = "QWE", Email="qwe@naicoits.com",Password="12345", Role="SuperAdmin", TenantID = "1",  UserID=3}

        //        //........................ and so on
        //     };
        //     return users.Where(x => x.Email == email).Where(x => x.TenantID == tenantID || x.TenantID == "0").FirstOrDefault();
        // }



        // public async Task<User> CreateUser(UserDTO userDto)
        // {
        //     try
        //     {
        //         IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
        //             .Create("552bf333-3f56-454c-87b6-0a7f963e11d1")
        //             .WithTenantId("9c797e99-1a40-4c9e-960a-fddfcc56d44d")
        //             .WithClientSecret("9hi632PVqyWdMO2.P.1eo_-y4pH4ikQ5K1") // or .WithCertificate(certificate)
        //             .Build();

        //         ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);


        //         GraphServiceClient graphClient = new GraphServiceClient(authProvider);

        //         var user = new User
        //         {
        //             AccountEnabled = true,
        //             DisplayName = userDto.Name,
        //             MailNickname = userDto.Name.ToLower(),
        //             UserPrincipalName = userDto.Name.ToLower() +"@b2cusercreation.onmicrosoft.com",
        //             PasswordProfile = new PasswordProfile
        //             {
        //                 ForceChangePasswordNextSignIn = true,
        //                 ForceChangePasswordNextSignInWithMfa = false,
        //                 ODataType = null,
        //                 Password = userDto.Password
        //             },
        //             Identities = new List<ObjectIdentity>
        //                 {
        //                     new ObjectIdentity()
        //                         {
        //                             SignInType = "emailAddress",
        //                             Issuer = "b2cusercreation.onmicrosoft.com",
        //                             IssuerAssignedId = userDto.Email
        //                         }
        //                 },
        //             Mail = userDto.Email,
        //             PasswordPolicies = "DisablePasswordExpiration",
        //             ODataType = null,
        //             CreationType = "LocalAccount"
        //         };

        //         // var Identity = new ObjectIdentity()
        //         // {
        //         //     SignInType = "emailAddress",
        //         //     Issuer = "haseenaamnaicotech.onmicrosoft.com",
        //         //     IssuerAssignedId = "haseena.am@naicotech.com"
        //         // };
        //         // var Identities = new List<ObjectIdentity>();
        //         // Identities.Add(Identity);
        //         // user.Identities = mapper.Map<IEnumerable<ObjectIdentity>>(Identities);
        //         var result = await graphClient.Users
        //             .Request()
        //             .AddAsync(user);
        //         // var result = userRepository.GetUserPermissionsByUserID(userID);
        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         return null;
        //     }
        // }

        // public async Task<User> CreateUser(UserDTO userDto)
        // {
        //     try
        //     {
        //         // TokenResult tokenResult = new TokenResult();
        //         var identityUser = new Users { UserName = user.UserName, Email = user.Email};
        //         var result = await _userManager.CreateAsync(identityUser, user.Password);
        //         if (result.Succeeded)
        //         {
        //             var createdUser = await _userManager.FindByIdAsync(identityUser.Id);
        //             var token = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
        //             string codeHtmlVersion = HttpUtility.UrlEncode(token);
        //             tokenResult.EmailToken = codeHtmlVersion;
        //         }
        //         return tokenResult;
        //     }
        //     catch (Exception ex)
        //     {
        //         return null;
        //     }
        // }

        public async Task<AspNetUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<AspNetUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<AspNetUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<bool> ConfirmEmailAndPassword(AspNetUser user, AccountConfirmationModelDTO passwordResetModel)
        {
            string decodedEmailToken = HttpUtility.UrlDecode(passwordResetModel.EmailConfirmationToken);
            decodedEmailToken = HttpUtility.UrlDecode(decodedEmailToken);

            var result = await _userManager.ConfirmEmailAsync(user, decodedEmailToken);
            if (result.Succeeded)
            {
                // return Ok("Email Confirmed");
                string decodedPasswordToken = HttpUtility.UrlDecode(passwordResetModel.PasswordConfirmationToken);
                decodedPasswordToken = HttpUtility.UrlDecode(decodedPasswordToken);

                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, decodedPasswordToken, passwordResetModel.Password);
                if (resetPasswordResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<UsersDTO> Login(LoginDTO user)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(user.Email);
                if (identityUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(identityUser, user.Password, user.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var agencyId = await _iTenantProvider.GetCurrentTenant();
                        if (agencyId == 0)
                        {
                            return null;
                        }
                        // var agency = await agencyRepository.GetAgencyDetailsByAbbrev(abbrev);
                        var userData = await userRepository.GetUserByUserNameAndAgencyIdAsync(user.Email, agencyId);
                        if (!userData.IsActive)
                        {
                            return null;
                        }
                        var roles = await userRepository.GetUserRoleList(userData.UserID);
                        userData.Roles = roles.Select(x => x.Name).ToList();
                        var roleDetails = roles.FirstOrDefault();
                        if (roleDetails != null)
                        {
                            userData.RoleID = roleDetails.SystemRoleID;
                        }

                        string abbrev = _accessor.HttpContext.Request.Headers[PCISEnum.Parameters.tenantId].ToString();
                        userData.AgencyAbbrev = abbrev;
                        userData.AgencyID = agencyId;
                        return userData;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public async Task<TokenResultDTO> Register(UsersDTO user)
        {
            TokenResultDTO tokenResult = new TokenResultDTO();
            try
            {
                var agencyId = await _iTenantProvider.GetCurrentTenant();
                var existingUser = await userRepository.GetUserByUserNameAndAgencyIdAsync(user.Email, agencyId);
                if (existingUser != null && !String.IsNullOrEmpty(existingUser.UserName)
                    && existingUser.UserName.ToLower() == user.Email.ToLower())
                {
                    tokenResult.AlreadyExists = true;
                    return tokenResult;
                }
                // var agency = await agencyRepository.GetAgencyDetailsByAbbrev(abbrev);
                // string codeHtmlVersion = string.Empty;

                var identityUser = new AspNetUser { UserName = user.UserName, Email = user.Email, AgencyId = agencyId, EmailConfirmed = true, IsActive = true };
                string tempPassword = "Password@123";

                var result = await _userManager.CreateAsync(identityUser, tempPassword);

                identityUser = await _userManager.FindByNameAsync(user.UserName);
                if (result.Succeeded)
                {
                    UsersDTO usersDTO = new UsersDTO
                    {
                        AgencyID = agencyId,
                        IsActive = true,
                        Name = user.Name,
                        Password = tempPassword,
                        UserName = user.Email,
                        AspNetUserID = user.AspNetUserID
                    };
                    int userId = CreateUser(usersDTO);
                    //inserting individual user role permission

                    if (user.Roles != null)
                    {
                        var systemRole = _systemRoleRepository.GetSystemRoleByRoleNameAsync(user.Roles);
                        List<UserRoleDTO> userRoles = new List<UserRoleDTO>();
                        // List<RolePermissionDTO> rolePermissionDTOList = new List<RolePermissionDTO>();
                        foreach (var item in systemRole)
                        {
                            var systemRolePermissions = await _systemRolePermissionRepository.GetSystemRolePermissionsAsync(item.SystemRoleID);

                            UserRoleDTO userRole = new UserRoleDTO
                            {
                                SystemRoleID = item.SystemRoleID,
                                UserID = userId
                            };
                            int userRoleId = _userRoleRepository.CreateUserRole(userRole, userRole.UserID);
                            // userRoles.Add(userRole);

                            /*foreach (var permission in systemRolePermissions)
                            {
                                RolePermissionDTO rolePermissionDTO = new RolePermissionDTO();
                                rolePermissionDTO.UserRoleID = userRoleId;
                                rolePermissionDTO.PermissionID = permission.PermissionID;
                                rolePermissionDTOList.Add(rolePermissionDTO);
                            }*/
                        }
                        // if (rolePermissionDTOList.Count > 0)
                        //await _rolePermissionRepository.CreateBulkRolePermissions(rolePermissionDTOList);

                        // _userRoleRepository.CreateBulkUserRole(userRoles);
                    }

                    // var createdUser = await _userManager.FindByIdAsync(identityUser.Id);
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                    var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                    tokenResult.UserID = userId;
                    tokenResult.Id = identityUser.Id;
                    string codeHtmlVersionEmail = HttpUtility.UrlEncode(emailToken);
                    string codeHtmlVersionPassword = HttpUtility.UrlEncode(passwordToken);

                    tokenResult.EmailToken = codeHtmlVersionEmail;
                    tokenResult.PasswordToken = codeHtmlVersionPassword;

                    string abbrev = ((ClaimsIdentity)_accessor.HttpContext.User.Identity).FindFirst(PCISEnum.TokenClaims.AgencyAbbrev).Value;
                    // string abbrev = "";
                    tokenResult.TenantId = abbrev;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tokenResult;
        }

        /// <summary>
        /// RegisterBulk.
        /// </summary>
        /// <param name="userList">userList.</param>
        /// <returns>TokenResultDTO List.</returns>
        public async Task<List<TokenResultDTO>> RegisterBulk(List<UsersDTO> userList)
        {
            List<TokenResultDTO> tokenResultList = new List<TokenResultDTO>();
            List<UsersDTO> usersDTOList = new List<UsersDTO>();
            List<UserRoleDTO> userRoleList = new List<UserRoleDTO>();
            List<Guid> UserIndexGuids = new List<Guid>();
            try
            {
                foreach (UsersDTO user in userList)
                {
                    TokenResultDTO tokenResult = new TokenResultDTO();
                    var agencyId =user.AgencyID;
                    var identityUser = new AspNetUser { UserName = user.UserName, Email = user.Email, AgencyId = agencyId, EmailConfirmed = true, IsActive = true };
                    string tempPassword = "Password@123";
                    var result = await _userManager.CreateAsync(identityUser, tempPassword);
                    if (result.Succeeded)
                    {
                        UsersDTO usersDTO = new UsersDTO
                        {
                            AgencyID = agencyId,
                            IsActive = true,
                            Name = user.Name,
                            Password = tempPassword,
                            UserName = user.Email,
                            AspNetUserID = user.AspNetUserID,
                            UserIndex = Guid.NewGuid()
                        };
                        UserIndexGuids.Add(usersDTO.UserIndex);
                        usersDTOList.Add(usersDTO);
                        if (user.Roles != null)
                        {
                            var systemRole = _systemRoleRepository.GetSystemRoleByRoleNameAsync(user.Roles);
                            List<UserRoleDTO> userRoles = new List<UserRoleDTO>();
                            foreach (var item in systemRole)
                            {
                                var systemRolePermissions = await _systemRolePermissionRepository.GetSystemRolePermissionsAsync(item.SystemRoleID);
                                UserRoleDTO userRole = new UserRoleDTO
                                {
                                    SystemRoleID = item.SystemRoleID,
                                    UserIndex = usersDTO.UserIndex
                                };
                                userRoleList.Add(userRole);
                            }
                        }
                        tokenResult.UserIndex = usersDTO.UserIndex;
                        tokenResult.helperIndex = user.helperIndex;
                        tokenResultList.Add(tokenResult);
                    }
                }
                // bulk insert user
                var res = CreateUserBulk(usersDTOList);
                if (res.Count > 0)
                {
                    var personListByGuid = userRepository.GetUserListByGUID(UserIndexGuids).Result;
                    foreach (var item in userRoleList)
                    {
                        item.UserID = personListByGuid.Where(x => x.UserIndex == item.UserIndex).FirstOrDefault().UserID;
                    }
                    _userRoleRepository.CreateBulkUserRole(userRoleList);
                    foreach (var items in tokenResultList)
                    {
                        items.UserID = personListByGuid.Where(x => x.UserIndex == items.UserIndex).FirstOrDefault().UserID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tokenResultList;
        }

        public List<UsersDTO> CreateUserBulk(List<UsersDTO> userList)
        {
            List<UsersDTO> result = userRepository.CreateUserBulk(userList);
            return result;
        }


        public async Task<TokenResultDTO> UpdateUser(UsersDTO user)
        {
            TokenResultDTO tokenResult = new TokenResultDTO();
            try
            {
                var agencyId = await _iTenantProvider.GetCurrentTenant();
                var userData = await userRepository.GetUserByUserNameAndAgencyIdAsync(user.Email, agencyId);
                if (userData.UserID != 0 && userData.UserID != user.UserID)
                {
                    tokenResult.AlreadyExists = true;
                    return tokenResult;
                }
                var identityUser = await _userManager.FindByNameAsync(user.ExistingEmail);

                bool emailChanged = user.Email != identityUser.Email;

                identityUser.Email = user.Email;
                identityUser.UserName = user.Email;

                var result = await _userManager.UpdateAsync(identityUser);
                if (emailChanged)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                    var emailConfirmationResult = await _userManager.ConfirmEmailAsync(identityUser, token);
                }
                // userData.UserName = user.Email;

                var updatingdata = this.userRepository.GetUsersByUsersIDAsync(user.UserID);
                updatingdata.Result.UserName = user.Email;

                userRepository.UpdateUser(updatingdata.Result);
                var userRolesToRemove = await _userRoleRepository.GetUserRoleList(updatingdata.Result.UserID);
                //var rolePermissionToRemove = await _rolePermissionRepository.GetRolePermissionsAsync(userRolesToRemove.Select(x => x.UserRoleID).ToList());
                //await _rolePermissionRepository.DeleteBulkRolePermissions(rolePermissionToRemove);
                await _userRoleRepository.DeleteBulkUserRole(userRolesToRemove);
                var systemRoleNew = _systemRoleRepository.GetSystemRoleByRoleNameAsync(user.Roles);
                // List<RolePermissionDTO> rolePermissionDTOList = new List<RolePermissionDTO>();
                foreach (var item in systemRoleNew)
                {
                    //var systemRolePermissions = await _systemRolePermissionRepository.GetSystemRolePermissionsAsync(item.SystemRoleID);

                    UserRoleDTO userRole = new UserRoleDTO
                    {
                        SystemRoleID = item.SystemRoleID,
                        UserID = updatingdata.Result.UserID
                    };
                    int userRoleId = _userRoleRepository.CreateUserRole(userRole, userRole.UserID);

                    /*foreach (var permission in systemRolePermissions)
                    {
                        RolePermissionDTO rolePermissionDTO = new RolePermissionDTO();
                        rolePermissionDTO.UserRoleID = userRoleId;
                        rolePermissionDTO.PermissionID = permission.PermissionID;
                        rolePermissionDTOList.Add(rolePermissionDTO);
                    }*/
                }
                //if (rolePermissionDTOList.Count > 0)
                //await _rolePermissionRepository.CreateBulkRolePermissions(rolePermissionDTOList);

                return tokenResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            string decodedToken = HttpUtility.UrlDecode(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public int CreateUser(UsersDTO user)
        {
            var result = userRepository.CreateUser(user);

            return result;
        }

        public async Task<string> GetResetPasswordToken(string email)
        {
            string codeHtmlVersion = string.Empty;
            TokenResultDTO result = new TokenResultDTO();
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                result.PasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                codeHtmlVersion = HttpUtility.UrlEncode(result.PasswordToken);
                //         tokenResult.EmailToken = codeHtmlVersion;
            }
            return codeHtmlVersion;
        }

        public async Task<IdentityResult> ResetPassword(AspNetUser user, PasswordResetDTO model)
        {
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            return result;
        }
        // public async Task<TokenResult> Register(UserLogin user)
        // {
        //     TokenResult tokenResult = new TokenResult();
        //     var identityUser = new Users { UserName = user.UserName, Email = user.Email};
        //     var result = await _userManager.CreateAsync(identityUser, user.Password);
        //     if (result.Succeeded)
        //     {
        //         var createdUser = await _userManager.FindByIdAsync(identityUser.Id);
        //         var token = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
        //         string codeHtmlVersion = HttpUtility.UrlEncode(token);
        //         tokenResult.EmailToken = codeHtmlVersion;
        //     }
        //     return tokenResult;
        // }

        // public async Task<TokenResult> GetPasswordTokenForForgotPassword(ForgotPassword forgotPasswordModel)
        // {
        //     TokenResult result = new TokenResult();
        //     var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
        //     if(user != null && await _userManager.IsEmailConfirmedAsync(user))
        //     {
        //         result.PasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        //     }
        //     return result;
        // }
        public async Task<bool> ForgotPasswordReset(AspNetUser user, ForgotPasswordDTO model)
        {
            string decodedToken = HttpUtility.UrlDecode(model.PasswordToken);
            // decodedToken = HttpUtility.UrlDecode(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// GetUserProfile.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="userRole">userRole.</param>
        /// <returns>UserProfileResponseDTO.</returns>
        public UserProfileResponseDTO GetUserProfile(int userID, List<string> userRole, long agencyID)
        {
            try
            {                
                UserProfileResponseDTO userProfileResponseDTO = new UserProfileResponseDTO();
                if (userID != 0)
                {
                    if (userRole.Contains(PCISEnum.Roles.SuperAdmin))
                    {
                        userProfileResponseDTO.UserProfile = this.userRepository.GetUserProfile(userID, true);
                        agencyID = userProfileResponseDTO.UserProfile.AgencyID;
                    }
                    else
                    {
                        userProfileResponseDTO.UserProfile = this.userRepository.GetUserProfile(userID, false);
                    }
                    if (userProfileResponseDTO.UserProfile != null)
                    {
                        if (userProfileResponseDTO.UserProfile.AzureFileName != null)
                        {
                            string blobReferenceKey = userProfileResponseDTO.UserProfile.AzureFileName;
                            userProfileResponseDTO.UserProfile.ImageByteArray = this.fileService.DownloadFileFromBlob(blobReferenceKey, agencyID, userID, userRole);
                            userProfileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            userProfileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }

                    return userProfileResponseDTO;
                }
                else
                {
                    userProfileResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.UserId);
                    userProfileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;

                    return userProfileResponseDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserProfileResponseDTO GetUserProfilePic(int userID, long agencyID, List<string> userRole)
        {
            try
            {
                UserProfileResponseDTO userProfileResponseDTO = new UserProfileResponseDTO();
                if (userID != 0)
                {

                    userProfileResponseDTO.UserProfile = this.userRepository.GetUserProfilePicDetails(userID);
                    if (userProfileResponseDTO.UserProfile != null)
                    {
                        if (userProfileResponseDTO.UserProfile.AzureFileName != null)
                        {
                            string blobReferenceKey = userProfileResponseDTO.UserProfile.AzureFileName;
                            userProfileResponseDTO.UserProfile.ImageByteArray = this.fileService.DownloadFileFromBlob(blobReferenceKey, agencyID, userID, userRole);
                            userProfileResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            userProfileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        }
                    }
                    return userProfileResponseDTO;
                }
                else
                {
                    userProfileResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.UserId);
                    userProfileResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;

                    return userProfileResponseDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateUserLastLogin.
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateUserLastLogin(int userID, long agencyID)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                var userLastLogin = this.userRepository.UserLastLoginTime(userID);
                if (userLastLogin.LastLogin.HasValue)
                {
                    response.LastLogin = userLastLogin.NotificationViewedOn.Value;
                }
                var userData = this.userRepository.GetByIdAsync(userID).Result;
                var ConfigValue= this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.LanguageLocalization, agencyID);
                var ConfigAutoSave= this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssessmentAutoSave, agencyID);
                var configAppTimeout = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.ApplicationTimeout, agencyID);
                var configAssesmentPageCount = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssesmentPageSize, agencyID);
                if (userData != null)
                {
                    userData.LastLogin = DateTime.UtcNow;
                    var result = this.userRepository.UpdateAsync(userData).Result;
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    response.Language = ConfigValue?.Value;
                    response.AssesmentAutoSaveTime = ConfigAutoSave?.Value;
                    response.AssesmentPageCount = configAssesmentPageCount?.Value;
                    response.ApplicationTimeout = (Convert.ToInt32(configAppTimeout?.Value ?? "0") * 60).ToString();
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                }
                else
                {
                    response.Language = ConfigValue?.Value;
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
             
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUserEmail.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserDataResponseDTO.</returns>
        public UserDataResponseDTO GetUserEmail(int userID)
        {
            try
            {
                UserDataResponseDTO response = new UserDataResponseDTO();
                response.UserDetails =  this.userRepository.GetUserEmail(userID);
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// UpdateUserNotificationViewDate.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public CRUDResponseDTO UpdateUserNotificationViewDate(int userID, long agencyID)
        {

            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();                
                var userData = this.userRepository.GetByIdAsync(userID).Result;
                if (userData != null)
                {
                    userData.NotificationViewedOn = DateTime.UtcNow;
                    var result = this.userRepository.UpdateAsync(userData).Result;
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAssessmentConfigurations.
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <returns>CRUDResponseDTO.</returns>
        public AssessmentConfigurationDTO GetAssessmentConfigurations(int userID, long agencyID)
        {
            try
            {
                AssessmentConfigurationDTO response = new AssessmentConfigurationDTO();                
                var userData = this.userRepository.GetByIdAsync(userID).Result;
                var ConfigValue = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.LanguageLocalization, agencyID);
                var ConfigAutoSave = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssessmentAutoSave, agencyID);
                var configAppTimeout = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.ApplicationTimeout, agencyID);
                var configAssesmentPageCount = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssesmentPageSize, agencyID);
                var configAssessmentFileTypes = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssessmentFileTypes, agencyID);

                response.AssesmentAutoSaveTime = ConfigAutoSave?.Value;
                response.AssesmentPageCount = configAssesmentPageCount?.Value;
                response.ApplicationTimeout = (Convert.ToInt32(configAppTimeout?.Value ?? "0") * 60).ToString();
                response.AssesmentFileTypes = configAssessmentFileTypes.Value;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetSSOConfigurations.
        /// SSO configuration should be a JSON array with keys name and value.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>SSOResponseDTO.</returns>
        public SSOResponseDTO GetSSOConfigurations(string client)
        {
            try
            {
                SSOResponseDTO responseDTO = new SSOResponseDTO();
                responseDTO.RedirectURL = string.Empty;
                var configURLJson = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.SSOConfig);
                if (!string.IsNullOrWhiteSpace(configURLJson?.Value))
                {
                    SSOSettingsDTO sSOSettingsDTO = new SSOSettingsDTO();
                    var urlConfig = JsonConvert.DeserializeAnonymousType(configURLJson.Value, sSOSettingsDTO);
                    var baseURL = urlConfig.SSOBaserUrl;
                    var clientURL = urlConfig.RedirectUrls.Where(x => x.Name == client).FirstOrDefault();
                    if (clientURL != null && !string.IsNullOrWhiteSpace(baseURL))
                    {
                        responseDTO.RedirectURL = baseURL.Replace(PCISEnum.ConfigurationParameters.SSOConfigReplace, clientURL.Value);
                    }
                }
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return responseDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
