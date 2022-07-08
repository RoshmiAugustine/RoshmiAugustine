// -----------------------------------------------------------------------
// <copyright file="GeneralContextSeed.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Infrastructure.Data.Seeding
{
    public class GeneralContextSeed
    {
        //        public static async Task SeedAsync(OpeekaDBContext dBContext, ILoggerFactory loggerFactory)
        //        {
        //            if (!await dBContext.WeatherForecasts.AnyAsync())
        //            {
        //                await dBContext.WeatherForecasts.AddAsync(new Domain.Entities.WeatherForecast()
        //                {
        //                    TemperatureC = 30,
        //                    Summary = "Too hot to handle"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.WeatherForecasts.AddAsync(new Domain.Entities.WeatherForecast()
        //                {
        //                    TemperatureC = 10,
        //                    Summary = "Too cold"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.WeatherForecasts.AddAsync(new Domain.Entities.WeatherForecast()
        //                {
        //                    TemperatureC = 22,
        //                    Summary = "Just about perfect"
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.User.AnyAsync())
        //            {
        //                await dBContext.User.AddAsync(new Domain.Entities.User()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "hazeena@gmail.com",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    AgencyID = 1,
        //                    Name = "Hazeena"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.User.AddAsync(new Domain.Entities.User()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "abc@naicoits.com",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    AgencyID = 1,
        //                    Name = "ABC"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.User.AddAsync(new Domain.Entities.User()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "xyz@naicoits.com",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    AgencyID = 1,
        //                    Name = "XYZ"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.User.AddAsync(new Domain.Entities.User()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "qwe@naicoits.com",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    AgencyID = 1,
        //                    Name = "QWE"
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.SystemRoles.AnyAsync())
        //            {
        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Super Admin",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    IsExternal = false,
        //                    ListOrder = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Organization Admin RW",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    IsExternal = false,
        //                    ListOrder = 2
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Organization Admin RO",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    IsExternal = false,
        //                    ListOrder = 3
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Supervisor",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    IsExternal = false,
        //                    ListOrder = 4
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Helper RW",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    IsExternal = false,
        //                    ListOrder = 5
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Helper RO",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    IsExternal = false,
        //                    ListOrder = 6
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Support",
        //                    IsRemoved = true,
        //                    UpdateUserID = 1,
        //                    IsExternal = true,
        //                    ListOrder = 7
        //                });
        //                await dBContext.SaveChangesAsync();


        //                // await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                // {
        //                //     Name = "OrgAdmin",
        //                //     IsRemoved = false,
        //                //     UpdateUserID = 1,
        //                //     IsExternal = false,
        //                //     ListOrder = 1
        //                // });
        //                // await dBContext.SaveChangesAsync();


        //                await dBContext.SystemRoles.AddAsync(new Domain.Entities.SystemRole()
        //                {
        //                    Name = "Person",
        //                    IsRemoved = true,
        //                    UpdateUserID = 1,
        //                    IsExternal = true,
        //                    ListOrder = 8
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.UserRole.AnyAsync())
        //            {
        //                await dBContext.UserRole.AddAsync(new Domain.Entities.UserRole()
        //                {
        //                    UserID = 1,
        //                    SystemRoleID = 6
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.UserRole.AddAsync(new Domain.Entities.UserRole()
        //                {
        //                    UserID = 2,
        //                    SystemRoleID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.UserRole.AddAsync(new Domain.Entities.UserRole()
        //                {
        //                    UserID = 2,
        //                    SystemRoleID = 3
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.UserRole.AddAsync(new Domain.Entities.UserRole()
        //                {
        //                    UserID = 4,
        //                    SystemRoleID = 3
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ApplicationObjectTypes.AnyAsync())
        //            {
        //                await dBContext.ApplicationObjectTypes.AddAsync(new Domain.Entities.ApplicationObjectType()
        //                {
        //                    Name = "ENTITY",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();


        //                await dBContext.ApplicationObjectTypes.AddAsync(new Domain.Entities.ApplicationObjectType()
        //                {
        //                    Name = "UIComponent",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ApplicationObjectTypes.AddAsync(new Domain.Entities.ApplicationObjectType()
        //                {
        //                    Name = "APIEndPoint",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ApplicationObjectTypes.AddAsync(new Domain.Entities.ApplicationObjectType()
        //                {
        //                    Name = "Namespace",
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            // if (!await dBContext.ApplicationObjects.AnyAsync())
        //            // {
        //            //     await dBContext.ApplicationObjects.AddAsync(new Domain.Entities.ApplicationObject()
        //            //     {
        //            //         ApplicationObjectTypeID = 3,
        //            //         Name = "/api/Account/",
        //            //         ListOrder = 1,
        //            //         IsRemoved = false,
        //            //         UpdateDate = DateTime.Now,
        //            //         UpdateUserID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            // await dBContext.ApplicationObjects.AddAsync(new Domain.Entities.ApplicationObject()
        //            // {
        //            //     ApplicationObjectTypeID = 3,
        //            //     Name = "/Account/GetPermissions/",
        //            //     ListOrder = 2,
        //            //     IsRemoved = false,
        //            //     UpdateDate = DateTime.Now,
        //            //     UpdateUserID = 1
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.ApplicationObjects.AddAsync(new Domain.Entities.ApplicationObject()
        //            // {
        //            //     ApplicationObjectTypeID = 4,
        //            //     Name = "Opeeka.PICS.Api.Controllers",
        //            //     ListOrder = 3,
        //            //     IsRemoved = false,
        //            //     UpdateDate = DateTime.Now,
        //            //     UpdateUserID = 1
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.ApplicationObjects.AddAsync(new Domain.Entities.ApplicationObject()
        //            // {
        //            //     ApplicationObjectTypeID = 3,
        //            //     Name = "/Account/LoginWithB2CToken/",
        //            //     ListOrder = 4,
        //            //     IsRemoved = false,
        //            //     UpdateDate = DateTime.Now,
        //            //     UpdateUserID = 1
        //            // });
        //            // await dBContext.SaveChangesAsync();
        //            // }

        //            // if (!await dBContext.OperationTypes.AnyAsync())
        //            // {
        //            //     await dBContext.OperationTypes.AddAsync(new Domain.Entities.OperationType()
        //            //     {
        //            //         Name = "Get",
        //            //         Abbrev = "Get request",
        //            //         Description = "Get request",
        //            //         IsRemoved = false,
        //            //         ListOrder = 1,
        //            //         UpdateUserID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            //     await dBContext.OperationTypes.AddAsync(new Domain.Entities.OperationType()
        //            //     {
        //            //         Name = "Post",
        //            //         Abbrev = "Post request",
        //            //         Description = "Post request",
        //            //         IsRemoved = false,
        //            //         ListOrder = 2,
        //            //         UpdateUserID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();
        //            // }

        //            // if (!await dBContext.Permissions.AnyAsync())
        //            // {
        //            //     await dBContext.Permissions.AddAsync(new Domain.Entities.Permission()
        //            //     {
        //            //         ApplicationObjectID = 1,
        //            //         OperationTypeID = 1,
        //            //         ListOrder = 1,
        //            //         IsRemoved = false,
        //            //         UpdateDate = DateTime.Now,
        //            //         UpdateUserID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            //     await dBContext.Permissions.AddAsync(new Domain.Entities.Permission()
        //            //     {
        //            //         ApplicationObjectID = 1,
        //            //         OperationTypeID = 2,
        //            //         ListOrder = 2,
        //            //         IsRemoved = false,
        //            //         UpdateDate = DateTime.Now,
        //            //         UpdateUserID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            // await dBContext.Permissions.AddAsync(new Domain.Entities.Permission()
        //            // {
        //            //     ApplicationObjectID = 3,
        //            //     OperationTypeID = 1,
        //            //     ListOrder = 2,
        //            //     IsRemoved = false,
        //            //     UpdateDate = DateTime.Now,
        //            //     UpdateUserID = 1
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.Permissions.AddAsync(new Domain.Entities.Permission()
        //            // {
        //            //     ApplicationObjectID = 4,
        //            //     OperationTypeID = 1,
        //            //     ListOrder = 2,
        //            //     IsRemoved = false,
        //            //     UpdateDate = DateTime.Now,
        //            //     UpdateUserID = 1
        //            // });
        //            // await dBContext.SaveChangesAsync();
        //            // }

        //            // if (!await dBContext.RolePermissions.AnyAsync())
        //            // {
        //            //     await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            //     {
        //            //         UserRoleID = 1,
        //            //         PermissionID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            //     await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            //     {
        //            //         UserRoleID = 1,
        //            //         PermissionID = 2
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            // await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            // {
        //            //     UserRoleID = 1,
        //            //     PermissionID = 3
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            // {
        //            //     UserRoleID = 1,
        //            //     PermissionID = 4
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            // {
        //            //     UserRoleID = 4,
        //            //     PermissionID = 4
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            // {
        //            //     UserRoleID = 4,
        //            //     PermissionID = 1
        //            // });
        //            // await dBContext.SaveChangesAsync();
        //            // await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            // {
        //            //     UserRoleID = 4,
        //            //     PermissionID = 2
        //            // });
        //            // await dBContext.SaveChangesAsync();
        //            // await dBContext.RolePermissions.AddAsync(new Domain.Entities.RolePermission()
        //            // {
        //            //     UserRoleID = 4,
        //            //     PermissionID = 3
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // }

        //            // if (!await dBContext.SystemRolePermission.AnyAsync())
        //            // {
        //            //     await dBContext.SystemRolePermission.AddAsync(new Domain.Entities.SystemRolePermission()
        //            //     {
        //            //         SystemRoleID = 6,
        //            //         PermissionID = 1
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            //     await dBContext.SystemRolePermission.AddAsync(new Domain.Entities.SystemRolePermission()
        //            //     {
        //            //         SystemRoleID = 6,
        //            //         PermissionID = 2
        //            //     });
        //            //     await dBContext.SaveChangesAsync();

        //            // await dBContext.SystemRolePermission.AddAsync(new Domain.Entities.SystemRolePermission()
        //            // {
        //            //     SystemRoleID = 6,
        //            //     PermissionID = 3
        //            // });
        //            // await dBContext.SaveChangesAsync();

        //            // await dBContext.SystemRolePermission.AddAsync(new Domain.Entities.SystemRolePermission()
        //            // {
        //            //     SystemRoleID = 6,
        //            //     PermissionID = 4
        //            // });
        //            // await dBContext.SaveChangesAsync();
        //            // }

        //            if (!await dBContext.Agencies.AnyAsync())
        //            {
        //                await dBContext.Agencies.AddAsync(new Domain.Entities.Agency()
        //                {
        //                    Name = "agency1",
        //                    AgencyIndex = Guid.NewGuid(),
        //                    UpdateUserID = 1,
        //                    IsCustomer = false,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now,
        //                    Abbrev = "agency1"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Agencies.AddAsync(new Domain.Entities.Agency()
        //                {
        //                    Name = "agency2",
        //                    AgencyIndex = Guid.NewGuid(),
        //                    UpdateUserID = 1,
        //                    IsCustomer = false,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now,
        //                    Abbrev = "agency2"
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Categories.AnyAsync())
        //            {

        //                await dBContext.Categories.AddAsync(new Domain.Entities.Category()
        //                {
        //                    CategoryFocusID = 1,
        //                    Name = "BEHAVIORAL/EMOTIONAL NEEDS DOMAIN",
        //                    Abbrev = "BEN",
        //                    Description = "The ratings in this section identify the behavioral health needs of the youth.  While the CANS is not a diagnostic tool, it is designed to be consistent with diagnostic communication.   In the DSM, a diagnosis is defined by a SET of symptoms that is associated with either dysfunction or distress.  This is consistent with the ratings of ?2? or ?3? as described by the action levels below.",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Categories.AddAsync(new Domain.Entities.Category()
        //                {
        //                    CategoryFocusID = 1,
        //                    Name = "LIFE FUNCTIONING DOMAIN",
        //                    Abbrev = "LFD",
        //                    Description = "Life domains are the different arenas of social interaction found in the lives of children, youths, and their families. This domain rates how they are functioning in the individual, family, peer, school, and community realms. This section is rated using the needs scale and therefore will highlight any struggles the individual and family are experiencing.",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Categories.AddAsync(new Domain.Entities.Category()
        //                {
        //                    CategoryFocusID = 1,
        //                    Name = "BEHAVIORAL/EMOTIONAL NEEDS DOMAIN",
        //                    Abbrev = "BEN",
        //                    Description = "The ratings in this section identify the behavioral health needs of the youth.  While the CANS is not a diagnostic tool, it is designed to be consistent with diagnostic communication.   In the DSM, a diagnosis is defined by a SET of symptoms that is associated with either dysfunction or distress.  This is consistent with the ratings of ?2? or ?3? as described by the action levels below.",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Categories.AddAsync(new Domain.Entities.Category()
        //                {
        //                    CategoryFocusID = 1,
        //                    Name = "CULTURAL FACTORS DOMAIN",
        //                    Abbrev = "CUL",
        //                    Description = "These items identify linguistic or cultural issues for which service providers need to make accommodations (e.g., provide interpreter, finding therapist who speaks family?s primary language, and/or ensure that a child/youth in placement has the opportunity to participate in cultural rituals associated with their cultural identity). Items in the Cultural Factors Domain describe difficulties that children and youth may experience or encounter as a result of their membership in any cultural group, and/or because of the relationship between members of that group and members of the dominant society.   It is it important to remember when using the CANS that the family should be defined from the individual youth?s perspective (i.e., who the individual describes as part of her/his family). The cultural issues in this domain should be considered in relation to the impact they are having on the life of the individual when rating these items and creating a treatment or service plan.  ",
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Countries.AnyAsync())
        //            {
        //                await dBContext.Countries.AddAsync(new Domain.Entities.Country()
        //                {
        //                    Name = "US",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.CountryStates.AnyAsync())
        //            {
        //                await dBContext.CountryStates.AddAsync(new Domain.Entities.CountryState()
        //                {
        //                    CountryID = 1,
        //                    Name = "Alabama",
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CountryStates.AddAsync(new Domain.Entities.CountryState()
        //                {
        //                    CountryID = 1,
        //                    Name = "Alaska",
        //                    IsRemoved = true,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Address.AnyAsync())
        //            {
        //                await dBContext.Address.AddAsync(new Domain.Entities.Address()
        //                {
        //                    AddressIndex = new Guid(),
        //                    CountryStateId = 1,
        //                    UpdateUserID = 1,
        //                    Address1 = "xxx House"

        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.Address.AddAsync(new Domain.Entities.Address()
        //                {
        //                    AddressIndex = new Guid(),
        //                    CountryStateId = 1,
        //                    UpdateUserID = 1,
        //                    Address1 = "YYY House"

        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.Address.AddAsync(new Domain.Entities.Address()
        //                {
        //                    AddressIndex = new Guid(),
        //                    CountryStateId = 1,
        //                    UpdateUserID = 1,
        //                    Address1 = "zzz House"

        //                });
        //                await dBContext.SaveChangesAsync();
        //            }


        //            if (!await dBContext.HelperTitles.AnyAsync())
        //            {
        //                await dBContext.HelperTitles.AddAsync(new Domain.Entities.HelperTitle()
        //                {
        //                    Name = "Doctor",
        //                    Abbrev = "Dr.",
        //                    Description = null,
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.HelperTitles.AddAsync(new Domain.Entities.HelperTitle()
        //                {
        //                    Name = "Mister",
        //                    Abbrev = "Mr.",
        //                    Description = null,
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.HelperTitles.AddAsync(new Domain.Entities.HelperTitle()
        //                {
        //                    Name = "Miss",
        //                    Abbrev = "Ms.",
        //                    Description = null,
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Helper.AnyAsync())
        //            {
        //                await dBContext.Helper.AddAsync(new Domain.Entities.Helper()
        //                {
        //                    UserID = 1,
        //                    AgencyID = 1,
        //                    Email = "navas.s@naicotech.com",
        //                    FirstName = "Navas",
        //                    HelperTitleID = 1,
        //                    LastName = "Shareef",
        //                    Phone = "8083160680",
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.HelperAddress.AnyAsync())
        //            {
        //                await dBContext.HelperAddress.AddAsync(new Domain.Entities.HelperAddress()
        //                {
        //                    HelperAddressIndex = new Guid(),
        //                    HelperID = 1,
        //                    AddressID = 1,
        //                    IsPrimary = true
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonScreeningStatus.AnyAsync())
        //            {
        //                await dBContext.PersonScreeningStatus.AddAsync(new Domain.Entities.PersonScreeningStatus()
        //                {
        //                    Name = "ScreeningStatus",
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Today,
        //                    ListOrder = 1,
        //                    UpdateUserId = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Languages.AnyAsync())
        //            {
        //                await dBContext.Languages.AddAsync(new Domain.Entities.Language()
        //                {
        //                    Name = "English",
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Sexualities.AnyAsync())
        //            {
        //                await dBContext.Sexualities.AddAsync(new Domain.Entities.Sexuality()
        //                {
        //                    Name = "Heterosexual",
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Genders.AnyAsync())
        //            {
        //                await dBContext.Genders.AddAsync(new Domain.Entities.Gender()
        //                {
        //                    Name = "Male",
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.Genders.AddAsync(new Domain.Entities.Gender()
        //                {
        //                    Name = "Female",
        //                    IsRemoved = false,
        //                    ListOrder = 2,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.RaceEthnicities.AnyAsync())
        //            {
        //                await dBContext.RaceEthnicities.AddAsync(new Domain.Entities.RaceEthnicity()
        //                {
        //                    Name = "American Indian or Alaska Native",
        //                    Description = "A person having origins in any of the original peoples of North and South America (including Central America), and who maintains tribal affiliation or community attachment.",
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.identificationTypes.AnyAsync())
        //            {
        //                await dBContext.identificationTypes.AddAsync(new Domain.Entities.IdentificationType()
        //                {
        //                    Name = "Behavorial Health ID",
        //                    Description = null,
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.SupportTypes.AnyAsync())
        //            {
        //                await dBContext.SupportTypes.AddAsync(new Domain.Entities.SupportType()
        //                {
        //                    Name = "Aunt",
        //                    Description = null,
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }


        //            if (!await dBContext.Instruments.AnyAsync())
        //            {
        //                await dBContext.Instruments.AddAsync(new Domain.Entities.Instrument()
        //                {
        //                    Name = "Adult Needs and Strengths Assessment",
        //                    Abbrev = "ANSA",
        //                    Description = "Praed Foundation managed assessment for adults 18+ as ANSA; has different approved versions by jurisdiction",
        //                    Instructions = "The PSC consists of 35 items that are rated as ?Never,? ?Sometimes,? or  ?Often? present and scored 0, 1, and 2, respectively. The total score is calculated  by adding together the score for each of the 35 items. For children and  adolescents ages 6 through 16, a cutoff score of 28 or higher indicates psychological  impairment. For children ages 4 and 5, the PSC cutoff score is 24 or  higher (Little et al., 1994; Pagano et al., 1996). The cutoff score for the Y-PSC  is 30 or higher. Items that are left blank are simply ignored (i.e., score equals  0). If four or more items are left blank, the questionnaire is considered invalid.",
        //                    Authors = "John Lyons",
        //                    Source = "www.praedfoundation.org",
        //                    ListOrder = 1,
        //                    UpdateUserID = 1

        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Instruments.AddAsync(new Domain.Entities.Instrument()
        //                {
        //                    Name = "Alcohol Abuse and Alcoholism Test � AUDIT Screen",
        //                    Abbrev = "AUDIT",
        //                    Description = "The definition of one drink = one can of beer (12 oz approx 330 ml 5% alcohol) = one glass of wine (5 oz approx 140 ml 12% alcohol) = one shot of liquor (1.5 oz approx 40 ml 40% alcohol).",
        //                    Instructions = "If you scored 8 or more, you are probably alcohol dependent. The test correctly classifies 95% of people. It may seem that the AUDIT questionnaire is an easy test to fail. If you applied this test to other aspects of your life you will almost certainly score as being addicted to something. For example, most people watch too much television, or eat too much of their favorite food. But those are so-called 'soft addictions', and the AUDIT questionnaire was not designed to assess those. No single test is completely accurate. You should always consult your physician when making decisions about your health.",
        //                    Authors = "Thomas F. Babor John C. Higgins-Biddle John B. Saunders Maristela G. Monteiro",
        //                    Source = "https://www.addictionsandrecovery.org/tools/alcohol-abuse-alcoholism-test-audit-who.htm Babor, T.F., Higgins-Biddle, J.C., Saunders, J.B., & Monteiro, M.G., AUDIT: The Alcohol Use Disorders Identification Test. Guidelines for Use in Primary Care. World Health Organization, Department of Mental Health and Substance Dependence. http://whqlibdoc.who.int/hq/2001/WHO_MSD_MSB_01.6a.pdf.",
        //                    ListOrder = 100,
        //                    UpdateUserID = 1

        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Questionnaires.AnyAsync())
        //            {
        //                await dBContext.Questionnaires.AddAsync(new Domain.Entities.Questionnaire()
        //                {
        //                    InstrumentID = 1,
        //                    AgencyID = 1,
        //                    Name = "Questionnaire 1",
        //                    Abbrev = "Q1",
        //                    Description = "The California CANS Core 50 for Child Welfare and Behavioral Health plus 12 Trauma items.",
        //                    ReminderScheduleName = "Reminder Name",
        //                    RequiredConfidentialityLanguage = "Signed Release Required",
        //                    PersonRequestedConfidentialityLanguage = "Person Requested Confidential",
        //                    OtherConfidentialityLanguage = "Person Reaction Sensitive",
        //                    IsPubllished = true,
        //                    IsBaseQuestionnaire = false,
        //                    StartDate = DateTime.UtcNow,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Questionnaires.AddAsync(new Domain.Entities.Questionnaire()
        //                {
        //                    InstrumentID = 1,
        //                    AgencyID = 1,
        //                    Name = "Questionnaire 2",
        //                    Abbrev = "Q2",
        //                    Description = "The California CANS Core 50 for Child Welfare and Behavioral Health plus 12 Trauma items.",
        //                    ReminderScheduleName = "Reminder Name",
        //                    RequiredConfidentialityLanguage = "Signed Release Required",
        //                    PersonRequestedConfidentialityLanguage = "Person Requested Confidential",
        //                    OtherConfidentialityLanguage = "Person Reaction Sensitive",
        //                    IsPubllished = true,
        //                    ParentQuestionnaireID = 1,
        //                    IsBaseQuestionnaire = false,
        //                    StartDate = DateTime.UtcNow,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Questionnaires.AddAsync(new Domain.Entities.Questionnaire()
        //                {
        //                    InstrumentID = 2,
        //                    Name = "Questionnaire 3",
        //                    Abbrev = "Q3",
        //                    Description = "The California CANS Core 50 for Child Welfare and Behavioral Health plus 12 Trauma items.",
        //                    ReminderScheduleName = "Reminder Name",
        //                    RequiredConfidentialityLanguage = "Signed Release Required",
        //                    PersonRequestedConfidentialityLanguage = "Person Requested Confidential",
        //                    OtherConfidentialityLanguage = "Person Reaction Sensitive",
        //                    IsPubllished = true,
        //                    ParentQuestionnaireID = 1,
        //                    IsBaseQuestionnaire = true,
        //                    StartDate = DateTime.UtcNow,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ItemResponseTypes.AnyAsync())
        //            {
        //                await dBContext.ItemResponseTypes.AddAsync(new Domain.Entities.ItemResponseType()
        //                {
        //                    Name = "Need",
        //                    IsRemoved = false,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseTypes.AddAsync(new Domain.Entities.ItemResponseType()
        //                {
        //                    Name = "Strength",
        //                    IsRemoved = false,
        //                    ListOrder = 2,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseTypes.AddAsync(new Domain.Entities.ItemResponseType()
        //                {
        //                    Name = "Exposure",
        //                    IsRemoved = false,
        //                    ListOrder = 3,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseTypes.AddAsync(new Domain.Entities.ItemResponseType()
        //                {
        //                    Name = "Past Behavior",
        //                    IsRemoved = false,
        //                    ListOrder = 4,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseTypes.AddAsync(new Domain.Entities.ItemResponseType()
        //                {
        //                    Name = "Support",
        //                    IsRemoved = false,
        //                    ListOrder = 5,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ItemResponseBehaviors.AnyAsync())
        //            {

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Name = "Focus",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Name = "Background",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Name = "None",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 2,
        //                    Name = "Build",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 2,
        //                    Name = "Use",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 2,
        //                    Name = "None",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 3,
        //                    Name = "Underlying",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 3,
        //                    Name = "None",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 5,
        //                    Name = "Focus",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 5,
        //                    Name = "Background",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ItemResponseBehaviors.AddAsync(new Domain.Entities.ItemResponseBehavior()
        //                {
        //                    ItemResponseTypeID = 5,
        //                    Name = "None",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ResponseValueTypes.AnyAsync())
        //            {

        //                await dBContext.ResponseValueTypes.AddAsync(new Domain.Entities.ResponseValueType()
        //                {
        //                    Name = "ResponseValueType1",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Items.AnyAsync())
        //            {
        //                await dBContext.Items.AddAsync(new Domain.Entities.Item()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Label = "PSYCHOSIS",
        //                    Name = "Psychosis (Thought Disorder)",
        //                    Description = "This item rates the symptoms of psychiatric disorders with a known neurological base, including schizophrenia spectrum and other psychotic disorders.  The common symptoms of these disorders include hallucinations (i.e. experiencing things others do not experience), delusions (i.e. a false belief or an incorrect inference about reality that is firmly sustained despite the fact that nearly everybody thinks the belief is false or proof exists of its inaccuracy), disorganized thinking, and bizarre/idiosyncratic behavior.",
        //                    SupplementalDescription = "Does the child/youth exhibit behaviors that are unusual or difficult to understand?  Does the child/youth experience hallucinations or delusions, bizarre behavior?  Are the unusual behaviors, hallucinations or delusions interfering with the youth's functioning?  ",
        //                    ResponseValueTypeID = 1,
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Items.AddAsync(new Domain.Entities.Item()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Label = "IMPULSIVITY_HYPERACTIVITY",
        //                    Name = "Impulsivity/Hyperactivity",
        //                    Description = "Problems with impulse control and impulsive behaviors, including motoric disruptions, are rated here. This includes behavioral symptoms associated with Attention-Deficit Hyperactivity Disorder (ADHD), Impulse-Control Disorders and mania as indicated in the DSM-5. Children/youth with impulse problems tend to engage in behavior without thinking, regardless of the consequences. This can include compulsions to engage in gambling, violent behavior (e.g., road rage), and sexual behavior, fire-starting or stealing.",
        //                    SupplementalDescription = "Is the child/youth unable to sit still for any length of time?  Does the child/youth have trouble paying attention for more than a few minutes?  Is the child/youth able to control their behavior, talking, etc.?  ",
        //                    ResponseValueTypeID = 1,
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Items.AddAsync(new Domain.Entities.Item()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Label = "DEPRESSION",
        //                    Name = "Depression",
        //                    Description = "Symptoms included in this item are irritable or depressed mood, social withdrawal, sleep disturbances, weight/eating disturbances, and loss of motivation, interest or pleasure in daily activities.  This item can be used to rate symptoms of the depressive disorders as specified in DSM-5.",
        //                    SupplementalDescription = "Is the child/youth concerned about possible depression or chronic low mood and irritability?  Has the child/youth withdrawn from normal activities?  Does the child/youth seem lonely or not interested in others?  ",
        //                    ResponseValueTypeID = 1,
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Items.AddAsync(new Domain.Entities.Item()
        //                {
        //                    ItemResponseTypeID = 1,
        //                    Label = "ANXIETY",
        //                    Name = "Anxiety",
        //                    Description = "This item rates symptoms associated with DSM-5 Anxiety Disorders characterized by excessive fear and anxiety and related behavioral disturbances (including avoidance behaviors).  Panic attacks can be a prominent type of fear response.",
        //                    SupplementalDescription = "Does the child/youth have any problems with anxiety or fearfulness?   Is the child/youth avoiding normal activities out of fear?  Does the child/youth act frightened or afraid?  ",
        //                    ResponseValueTypeID = 1,
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Responses.AnyAsync())
        //            {
        //                await dBContext.Responses.AddAsync(new Domain.Entities.Response()
        //                {
        //                    ItemID = 1,
        //                    Label = "0",
        //                    KeyCodes = "0",
        //                    Description = "No current need; no need for action or intervention. No evidence of psychotic symptoms. Both thought processes and content are within normal range. ",
        //                    Value = 0,
        //                    DefaultItemResponseBehaviorID = 3,
        //                    IsItemResponseBehaviorDisabled = true,
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Responses.AddAsync(new Domain.Entities.Response()
        //                {
        //                    ItemID = 1,
        //                    BackgroundColorPaletteID = 4,
        //                    Label = "1",
        //                    KeyCodes = "1",
        //                    Description = "Identified need requires monitoring, watchful waiting, or preventive activities.   Evidence of disruption in thought processes or content. Child/youth may be somewhat tangential in speech or evidence somewhat illogical thinking (age-inappropriate). This also includes child/youth with a history of hallucinations but none currently. Use this category for child/youth who are below the threshold for one of the DSM diagnoses listed above.   ",
        //                    Value = 1,
        //                    DefaultItemResponseBehaviorID = 3,
        //                    IsItemResponseBehaviorDisabled = false,
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Responses.AddAsync(new Domain.Entities.Response()
        //                {
        //                    ItemID = 1,
        //                    BackgroundColorPaletteID = 2,
        //                    Label = "2",
        //                    KeyCodes = "2",
        //                    Description = "Action or intervention is required to ensure that the identified need is addressed; need is interfering with functioning.   Evidence of disturbance in thought process or content that may be impairing the child/youth's functioning in at least one life domain. Child/youth may be somewhat delusional or have brief intermittent hallucinations. Speech may be at times quite tangential or illogical.   ",
        //                    Value = 2,
        //                    DefaultItemResponseBehaviorID = 2,
        //                    IsItemResponseBehaviorDisabled = false,
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Responses.AddAsync(new Domain.Entities.Response()
        //                {
        //                    ItemID = 1,
        //                    BackgroundColorPaletteID = 1,
        //                    Label = "3",
        //                    KeyCodes = "3",
        //                    Description = "Problems are dangerous or disabling; requires immediate and/or intensive action.   Clear evidence of dangerous hallucinations, delusions, or bizarre behavior that might be associated with some form of psychotic disorder that places the child/youth or others at risk of physical harm.   ",
        //                    Value = 3,
        //                    DefaultItemResponseBehaviorID = 1,
        //                    IsItemResponseBehaviorDisabled = false,
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.QuestionnaireItems.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireItems.AddAsync(new Domain.Entities.QuestionnaireItem()
        //                {
        //                    QuestionnaireID = 1,
        //                    CategoryID = 1,
        //                    ItemID = 1,
        //                    IsOptional = false,
        //                    CanOverrideLowerResponseBehavior = true,
        //                    CanOverrideMedianResponseBehavior = true,
        //                    CanOverrideUpperResponseBehavior = false,
        //                    LowerItemResponseBehaviorID = 3,
        //                    MedianItemResponseBehaviorID = 2,
        //                    UpperItemResponseBehaviorID = 1,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    LowerResponseValue = 1,
        //                    UpperResponseValue = 2
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireItems.AddAsync(new Domain.Entities.QuestionnaireItem()
        //                {
        //                    QuestionnaireID = 1,
        //                    CategoryID = 1,
        //                    ItemID = 2,
        //                    IsOptional = false,
        //                    CanOverrideLowerResponseBehavior = false,
        //                    CanOverrideMedianResponseBehavior = true,
        //                    CanOverrideUpperResponseBehavior = false,
        //                    LowerItemResponseBehaviorID = 3,
        //                    MedianItemResponseBehaviorID = 2,
        //                    UpperItemResponseBehaviorID = 1,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    LowerResponseValue = 3,
        //                    UpperResponseValue = 4
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireItems.AddAsync(new Domain.Entities.QuestionnaireItem()
        //                {
        //                    QuestionnaireID = 1,
        //                    CategoryID = 1,
        //                    ItemID = 3,
        //                    IsOptional = false,
        //                    CanOverrideLowerResponseBehavior = false,
        //                    CanOverrideMedianResponseBehavior = true,
        //                    CanOverrideUpperResponseBehavior = false,
        //                    LowerItemResponseBehaviorID = 3,
        //                    MedianItemResponseBehaviorID = 2,
        //                    UpperItemResponseBehaviorID = 1,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    LowerResponseValue = 5,
        //                    UpperResponseValue = 6
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireItems.AddAsync(new Domain.Entities.QuestionnaireItem()
        //                {
        //                    QuestionnaireID = 1,
        //                    CategoryID = 1,
        //                    ItemID = 4,
        //                    IsOptional = false,
        //                    CanOverrideLowerResponseBehavior = false,
        //                    CanOverrideMedianResponseBehavior = true,
        //                    CanOverrideUpperResponseBehavior = false,
        //                    LowerItemResponseBehaviorID = 3,
        //                    MedianItemResponseBehaviorID = 2,
        //                    UpperItemResponseBehaviorID = 1,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    LowerResponseValue = 7,
        //                    UpperResponseValue = 8
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.QuestionnaireNotifyRiskSchedules.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireNotifyRiskSchedules.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskSchedule()
        //                {
        //                    QuestionnaireID = 1,
        //                    Name = "Self Harm Only",
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.NotificationTypes.AnyAsync())
        //            {
        //                await dBContext.NotificationTypes.AddAsync(new Domain.Entities.NotificationType()
        //                {
        //                    Name = "Danger",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationTypes.AddAsync(new Domain.Entities.NotificationType()
        //                {
        //                    Name = "Reminder",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (await dBContext.NotificationTypes.AnyAsync())
        //            {
        //                var notificationType1 = dBContext.NotificationTypes.Where(x => x.Name == "AssessmentSubmit").FirstOrDefault();
        //                if (notificationType1 == null)
        //                {
        //                    await dBContext.NotificationTypes.AddAsync(new Domain.Entities.NotificationType()
        //                    {
        //                        Name = "AssessmentSubmit",
        //                        ListOrder = 3,
        //                        IsRemoved = false,
        //                        UpdateUserID = 1,
        //                    });
        //                    await dBContext.SaveChangesAsync();
        //                }
        //                var notificationType2 = dBContext.NotificationTypes.Where(x => x.Name == "EmailAssessmentSubmit").FirstOrDefault();
        //                if (notificationType2 == null)
        //                {
        //                    await dBContext.NotificationTypes.AddAsync(new Domain.Entities.NotificationType()
        //                    {
        //                        Name = "EmailAssessmentSubmit",
        //                        ListOrder = 4,
        //                        IsRemoved = false,
        //                        UpdateUserID = 1,
        //                    });
        //                    await dBContext.SaveChangesAsync();
        //                }
        //                var notificationType3 = dBContext.NotificationTypes.Where(x => x.Name == "AssessmentApproved").FirstOrDefault();
        //                if (notificationType3 == null)
        //                {
        //                    await dBContext.NotificationTypes.AddAsync(new Domain.Entities.NotificationType()
        //                    {
        //                        Name = "AssessmentApproved",
        //                        ListOrder = 5,
        //                        IsRemoved = false,
        //                        UpdateUserID = 1,
        //                    });
        //                    await dBContext.SaveChangesAsync();
        //                }
        //                var notificationType4 = dBContext.NotificationTypes.Where(x => x.Name == "AssessmentRejected").FirstOrDefault();
        //                if (notificationType4 == null)
        //                {
        //                    await dBContext.NotificationTypes.AddAsync(new Domain.Entities.NotificationType()
        //                    {
        //                        Name = "AssessmentRejected",
        //                        ListOrder = 6,
        //                        IsRemoved = false,
        //                        UpdateUserID = 1,
        //                    });
        //                    await dBContext.SaveChangesAsync();
        //                }
        //            }

        //            if (!await dBContext.NotificationLevels.AnyAsync())
        //            {
        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 1,
        //                    Name = "Safety Supervision",
        //                    ListOrder = 1,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 1,
        //                    Name = "Danger to Others",
        //                    ListOrder = 2,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 1,
        //                    Name = "Danger to Self",
        //                    ListOrder = 3,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "Window Opening",
        //                    ListOrder = 1,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "Questionnaire Due",
        //                    ListOrder = 2,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "Questionnaire Overdue (O)",
        //                    ListOrder = 3,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "Window Closing",
        //                    ListOrder = 4,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "Questionnaire Late (L)",
        //                    ListOrder = 5,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "Review Required",
        //                    ListOrder = 6,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLevels.AddAsync(new Domain.Entities.NotificationLevel()
        //                {
        //                    NotificationTypeID = 2,
        //                    Name = "SQuestionnaire Rejected",
        //                    ListOrder = 7,
        //                    RequireResolution = true,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.QuestionnaireNotifyRiskRules.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireNotifyRiskRules.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRule()
        //                {
        //                    Name = "Self Harm Only 1",
        //                    QuestionnaireNotifyRiskScheduleID = 1,
        //                    NotificationLevelID = 1,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireNotifyRiskRules.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRule()
        //                {
        //                    Name = "Self Harm Only 2",
        //                    QuestionnaireNotifyRiskScheduleID = 1,
        //                    NotificationLevelID = 2,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireNotifyRiskRules.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRule()
        //                {
        //                    Name = "Self Harm Only 3",
        //                    QuestionnaireNotifyRiskScheduleID = 1,
        //                    NotificationLevelID = 3,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireNotifyRiskRules.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRule()
        //                {
        //                    Name = "Self Harm Only 4",
        //                    QuestionnaireNotifyRiskScheduleID = 1,
        //                    NotificationLevelID = 1,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.QuestionnaireNotifyRiskRuleConditions.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireNotifyRiskRuleConditions.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRuleCondition()
        //                {
        //                    QuestionnaireItemID = 1,
        //                    ComparisonOperator = ">=",
        //                    ComparisonValue = 1,
        //                    QuestionnaireNotifyRiskRuleID = 1,
        //                    ListOrder = 1,
        //                    JoiningOperator = "AND",
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireNotifyRiskRuleConditions.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRuleCondition()
        //                {
        //                    QuestionnaireItemID = 2,
        //                    ComparisonOperator = "<=",
        //                    ComparisonValue = 2,
        //                    QuestionnaireNotifyRiskRuleID = 2,
        //                    ListOrder = 2,
        //                    JoiningOperator = "AND",
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireNotifyRiskRuleConditions.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRuleCondition()
        //                {
        //                    QuestionnaireItemID = 3,
        //                    ComparisonOperator = "==",
        //                    ComparisonValue = 3,
        //                    QuestionnaireNotifyRiskRuleID = 3,
        //                    ListOrder = 3,
        //                    JoiningOperator = "OR",
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireNotifyRiskRuleConditions.AddAsync(new Domain.Entities.QuestionnaireNotifyRiskRuleCondition()
        //                {
        //                    QuestionnaireItemID = 4,
        //                    ComparisonOperator = "<",
        //                    ComparisonValue = 4,
        //                    QuestionnaireNotifyRiskRuleID = 4,
        //                    ListOrder = 4,
        //                    JoiningOperator = "OR",
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.TherapyType.AnyAsync())
        //            {
        //                await dBContext.TherapyType.AddAsync(new Domain.Entities.TherapyType()
        //                {
        //                    Name = "Cognitive Behaviorial Therapy",
        //                    Abbrev = "CBT",
        //                    Description = "Cognitive behavioral therapy (CBT) is a short-term form of psychotherapy directed at present-time issues and based on the idea that the way an individual thinks and feels affects the way he or she behaves. The focus is on problem solving, and the goal is to change clients' thought patterns in order to change their responses to difficult situations. A CBT approach can be applied to a wide range of mental health issues and conditions.",
        //                    ListOrder = 1,
        //                    IsResidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.TherapyType.AddAsync(new Domain.Entities.TherapyType()
        //                {
        //                    Name = "Biofeedback",
        //                    ListOrder = 2,
        //                    IsResidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.TherapyType.AddAsync(new Domain.Entities.TherapyType()
        //                {
        //                    Name = "Eye Movement Desensitization and Reprocessing Therapy",
        //                    Abbrev = "EMDR",
        //                    ListOrder = 3,
        //                    IsResidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.TherapyType.AddAsync(new Domain.Entities.TherapyType()
        //                {
        //                    Name = "Dialectical Behavior Therapy",
        //                    Abbrev = "DBT",
        //                    ListOrder = 3,
        //                    IsResidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.CollaborationLevel.AnyAsync())
        //            {
        //                await dBContext.CollaborationLevel.AddAsync(new Domain.Entities.CollaborationLevel()
        //                {
        //                    Name = "Low Intensity",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CollaborationLevel.AddAsync(new Domain.Entities.CollaborationLevel()
        //                {
        //                    Name = "Residential",
        //                    ListOrder = 10,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CollaborationLevel.AddAsync(new Domain.Entities.CollaborationLevel()
        //                {
        //                    Name = "Community Based Programs",
        //                    ListOrder = 100,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.CollaborationLevel.AddAsync(new Domain.Entities.CollaborationLevel()
        //                {
        //                    Name = "Foster Care",
        //                    ListOrder = 101,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CollaborationLevel.AddAsync(new Domain.Entities.CollaborationLevel()
        //                {
        //                    Name = "Mental Health",
        //                    ListOrder = 102,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.CollaborationLevel.AddAsync(new Domain.Entities.CollaborationLevel()
        //                {
        //                    Name = "sABA",
        //                    ListOrder = 103,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.CollaborationTagType.AnyAsync())
        //            {
        //                await dBContext.CollaborationTagType.AddAsync(new Domain.Entities.CollaborationTagType()
        //                {
        //                    Name = "Medicaid",
        //                    Abbrev = "MED",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CollaborationTagType.AddAsync(new Domain.Entities.CollaborationTagType()
        //                {
        //                    Name = "Child Welfare",
        //                    Abbrev = "CHW",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CollaborationTagType.AddAsync(new Domain.Entities.CollaborationTagType()
        //                {
        //                    Name = "Probation",
        //                    Abbrev = "PRB",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AssessmentReasons.AnyAsync())
        //            {

        //                await dBContext.AssessmentReasons.AddAsync(new Domain.Entities.AssessmentReason()
        //                {
        //                    Name = "Initial",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentReasons.AddAsync(new Domain.Entities.AssessmentReason()
        //                {
        //                    Name = "Discharge",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();


        //                await dBContext.AssessmentReasons.AddAsync(new Domain.Entities.AssessmentReason()
        //                {
        //                    Name = "Scheduled",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentReasons.AddAsync(new Domain.Entities.AssessmentReason()
        //                {
        //                    Name = "Triggering Event",
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AssessmentStatuses.AnyAsync())
        //            {
        //                await dBContext.AssessmentStatuses.AddAsync(new Domain.Entities.AssessmentStatus()
        //                {
        //                    Name = "In Progress",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentStatuses.AddAsync(new Domain.Entities.AssessmentStatus()
        //                {
        //                    Name = "Submitted",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentStatuses.AddAsync(new Domain.Entities.AssessmentStatus()
        //                {
        //                    Name = "Complete",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentStatuses.AddAsync(new Domain.Entities.AssessmentStatus()
        //                {
        //                    Name = "Email Sent",
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ReviewerHistories.AnyAsync())
        //            {
        //                await dBContext.ReviewerHistories.AddAsync(new Domain.Entities.ReviewerHistory()
        //                {
        //                    StatusFrom = 1,
        //                    StatusTo = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.ReviewerHistories.AddAsync(new Domain.Entities.ReviewerHistory()
        //                {
        //                    StatusFrom = 2,
        //                    StatusTo = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.QuestionnaireReminderTypes.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireReminderTypes.AddAsync(new Domain.Entities.QuestionnaireReminderType()
        //                {
        //                    Name = "Window Open",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    NotificationLevelID = 4
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderTypes.AddAsync(new Domain.Entities.QuestionnaireReminderType()
        //                {
        //                    Name = "Assesment Due",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    NotificationLevelID = 5
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderTypes.AddAsync(new Domain.Entities.QuestionnaireReminderType()
        //                {
        //                    Name = "Window Close",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    NotificationLevelID = 7
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderTypes.AddAsync(new Domain.Entities.QuestionnaireReminderType()
        //                {
        //                    Name = "Questionnaire Late",
        //                    ListOrder = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    NotificationLevelID = 8
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderTypes.AddAsync(new Domain.Entities.QuestionnaireReminderType()
        //                {
        //                    Name = "Questionnaire Overdue",
        //                    ListOrder = 5,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    NotificationLevelID = 6
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.QuestionnaireReminderRules.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireReminderRules.AddAsync(new Domain.Entities.QuestionnaireReminderRule()
        //                {
        //                    QuestionnaireID = 1,
        //                    QuestionnaireReminderTypeID = 1,
        //                    ReminderOffsetDays = 1,
        //                    CanRepeat = false,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderRules.AddAsync(new Domain.Entities.QuestionnaireReminderRule()
        //                {
        //                    QuestionnaireID = 1,
        //                    QuestionnaireReminderTypeID = 2,
        //                    ReminderOffsetDays = 0,
        //                    CanRepeat = false,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderRules.AddAsync(new Domain.Entities.QuestionnaireReminderRule()
        //                {
        //                    QuestionnaireID = 1,
        //                    QuestionnaireReminderTypeID = 3,
        //                    ReminderOffsetDays = 0,
        //                    CanRepeat = false,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireReminderRules.AddAsync(new Domain.Entities.QuestionnaireReminderRule()
        //                {
        //                    QuestionnaireID = 1,
        //                    QuestionnaireReminderTypeID = 4,
        //                    CanRepeat = true,
        //                    RepeatInterval = 7,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.QuestionnaireWindows.AnyAsync())
        //            {
        //                await dBContext.QuestionnaireWindows.AddAsync(new Domain.Entities.QuestionnaireWindow()
        //                {
        //                    QuestionnaireID = 1,
        //                    AssessmentReasonID = 1,
        //                    DueDateOffsetDays = 0,
        //                    WindowOpenOffsetDays = 30,
        //                    WindowCloseOffsetDays = 30,
        //                    CanRepeat = false,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireWindows.AddAsync(new Domain.Entities.QuestionnaireWindow()
        //                {
        //                    QuestionnaireID = 1,
        //                    AssessmentReasonID = 2,
        //                    DueDateOffsetDays = 0,
        //                    WindowOpenOffsetDays = 30,
        //                    WindowCloseOffsetDays = 30,
        //                    CanRepeat = false,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireWindows.AddAsync(new Domain.Entities.QuestionnaireWindow()
        //                {
        //                    QuestionnaireID = 1,
        //                    AssessmentReasonID = 3,
        //                    DueDateOffsetDays = 0,
        //                    WindowOpenOffsetDays = 30,
        //                    WindowCloseOffsetDays = 30,
        //                    CanRepeat = true,
        //                    RepeatIntervalDays = 120,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.QuestionnaireWindows.AddAsync(new Domain.Entities.QuestionnaireWindow()
        //                {
        //                    QuestionnaireID = 1,
        //                    AssessmentReasonID = 4,
        //                    DueDateOffsetDays = 0,
        //                    WindowOpenOffsetDays = 0,
        //                    WindowCloseOffsetDays = 30,
        //                    CanRepeat = false,
        //                    IsSelected = true
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }



        //            if (!await dBContext.AspNetUsers.AnyAsync())
        //            {
        //                await dBContext.AspNetUsers.AddAsync(new Domain.Entities.AspNetUser()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "hazeena",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    Email = "hazeena@gmail.com",
        //                    AgencyId = 1,
        //                    PasswordHash = "AQAAAAEAACcQAAAAEOGDhEhIOe6PKfAeYkr1AjW1AqG1HkbXeHd4G09ocK7ewkzjjqXL00ExQnotLPLpjA==",
        //                    // Name = "Hazeena"
        //                    NormalizedUserName = "HAZEENA@GMAIL.COM",
        //                    EmailConfirmed = true,
        //                    SecurityStamp = "AJJPAT7DOHB2SGR2K4ZMDWHY2JCVGQWK",
        //                    PhoneNumberConfirmed = false,
        //                    TwoFactorEnabled = false,
        //                    LockoutEnabled = false,
        //                    AccessFailedCount = 0,
        //                    UserID = 0,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AspNetUsers.AddAsync(new Domain.Entities.AspNetUser()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "abc",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    Email = "abc@naicoits.com",
        //                    AgencyId = 1,
        //                    PasswordHash = "AQAAAAEAACcQAAAAEOGDhEhIOe6PKfAeYkr1AjW1AqG1HkbXeHd4G09ocK7ewkzjjqXL00ExQnotLPLpjA==",
        //                    // Name = "Hazeena"
        //                    NormalizedUserName = "ABC@NAICOITS.COM",
        //                    EmailConfirmed = true,
        //                    SecurityStamp = "AJJPAT7DOHB2SGR2K4ZMDWHY2JCVGQWK",
        //                    ConcurrencyStamp = "9776222b-b1f5-4bc1-96b1-b05afe7665cf",
        //                    PhoneNumberConfirmed = false,
        //                    TwoFactorEnabled = false,
        //                    LockoutEnabled = false,
        //                    AccessFailedCount = 0,
        //                    UserID = 0,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AspNetUsers.AddAsync(new Domain.Entities.AspNetUser()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "xyz",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    Email = "xyz@naicoits.com",
        //                    AgencyId = 1,
        //                    PasswordHash = "AQAAAAEAACcQAAAAEOGDhEhIOe6PKfAeYkr1AjW1AqG1HkbXeHd4G09ocK7ewkzjjqXL00ExQnotLPLpjA==",
        //                    // Name = "Hazeena"
        //                    NormalizedUserName = "XYZ@NAICOITS.COM",
        //                    EmailConfirmed = true,
        //                    SecurityStamp = "AJJPAT7DOHB2SGR2K4ZMDWHY2JCVGQWK",
        //                    PhoneNumberConfirmed = false,
        //                    TwoFactorEnabled = false,
        //                    LockoutEnabled = false,
        //                    AccessFailedCount = 0,
        //                    UserID = 0,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AspNetUsers.AddAsync(new Domain.Entities.AspNetUser()
        //                {
        //                    UserIndex = Guid.NewGuid(),
        //                    UserName = "qwe",
        //                    LastLogin = DateTime.Now,
        //                    IsActive = true,
        //                    Email = "qwe@naicoits.com",
        //                    AgencyId = 1,
        //                    PasswordHash = "AQAAAAEAACcQAAAAEOGDhEhIOe6PKfAeYkr1AjW1AqG1HkbXeHd4G09ocK7ewkzjjqXL00ExQnotLPLpjA==",
        //                    // Name = "Hazeena"
        //                    NormalizedUserName = "QWE@NAICOITS.COM",
        //                    EmailConfirmed = true,
        //                    SecurityStamp = "AJJPAT7DOHB2SGR2K4ZMDWHY2JCVGQWK",
        //                    PhoneNumberConfirmed = false,
        //                    TwoFactorEnabled = false,
        //                    LockoutEnabled = false,
        //                    AccessFailedCount = 0,
        //                    UserID = 0,
        //                });
        //                await dBContext.SaveChangesAsync();


        //            }



        //            if (!await dBContext.AgencySharingPolicys.AnyAsync())
        //            {
        //                await dBContext.AgencySharingPolicys.AddAsync(new Domain.Entities.AgencySharingPolicy()
        //                {
        //                    AgencySharingID = 1,
        //                    SharingPolicyID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.AgencySharings.AnyAsync())
        //            {
        //                await dBContext.AgencySharings.AddAsync(new Domain.Entities.AgencySharing()
        //                {
        //                    AgencyID = 1,
        //                    ReportingUnitID = 1,
        //                    IsActive = true,
        //                    StartDate = DateTime.UtcNow,
        //                    EndDate = DateTime.UtcNow,
        //                    HistoricalView = true,
        //                    AgencySharingPolicyID = 1

        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.SharingPolicys.AnyAsync())
        //            {
        //                await dBContext.SharingPolicys.AddAsync(new Domain.Entities.SharingPolicy()
        //                {
        //                    AccessName = "Read/Write"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.SharingPolicys.AddAsync(new Domain.Entities.SharingPolicy()
        //                {
        //                    AccessName = "Read Only"
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.AuditTableName.AnyAsync())
        //            {
        //                await dBContext.AuditTableName.AddAsync(new Domain.Entities.AuditTableName()
        //                {
        //                    TableName = "Address",
        //                    Label = "Address"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AuditTableName.AddAsync(new Domain.Entities.AuditTableName()
        //                {
        //                    TableName = "Agency",
        //                    Label = "Agency"
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AuditFieldName.AnyAsync())
        //            {
        //                await dBContext.AuditFieldName.AddAsync(new Domain.Entities.AuditFieldName()
        //                {
        //                    TableName = "Address",
        //                    FieldName = "Address1",
        //                    Label = "Address1"
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.AuditFieldName.AddAsync(new Domain.Entities.AuditFieldName()
        //                {
        //                    TableName = "Address",
        //                    FieldName = "CountryStateId",
        //                    Label = "CountryStateId"
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.AuditFieldName.AddAsync(new Domain.Entities.AuditFieldName()
        //                {
        //                    TableName = "Address",
        //                    FieldName = "IsPrimary",
        //                    Label = "IsPrimary"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AuditFieldName.AddAsync(new Domain.Entities.AuditFieldName()
        //                {
        //                    TableName = "Agency",
        //                    FieldName = "Name",
        //                    Label = "Name"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AuditFieldName.AddAsync(new Domain.Entities.AuditFieldName()
        //                {
        //                    TableName = "Agency",
        //                    FieldName = "AgencyID",
        //                    Label = "AgencyID"
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ReportingUnits.AnyAsync())
        //            {
        //                await dBContext.ReportingUnits.AddAsync(new Domain.Entities.ReportingUnit()
        //                {

        //                    Name = "Los Angeles",
        //                    Abbrev = "LAC",
        //                    StartDate = DateTime.UtcNow,
        //                    UpdateDate = DateTime.UtcNow,
        //                    ParentAgencyID = 1,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.ColorPalettes.AnyAsync())
        //            {
        //                await dBContext.ColorPalettes.AddAsync(new Domain.Entities.ColorPalette()
        //                {
        //                    Name = "Dark Orange",
        //                    RGB = "#F7AD38",
        //                    UpdateDate = DateTime.UtcNow,
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.ColorPalettes.AddAsync(new Domain.Entities.ColorPalette()
        //                {
        //                    Name = "Light Orange",
        //                    RGB = "#E8C95D",
        //                    UpdateDate = DateTime.UtcNow,
        //                    ListOrder = 2,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.ColorPalettes.AddAsync(new Domain.Entities.ColorPalette()
        //                {
        //                    Name = "Light Yellow",
        //                    RGB = "#F8DF8A",
        //                    UpdateDate = DateTime.UtcNow,
        //                    ListOrder = 3,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Person.AnyAsync())
        //            {
        //                await dBContext.Person.AddAsync(new Domain.Entities.Person()
        //                {
        //                    FirstName = "Navas",
        //                    MiddleName = "N",
        //                    LastName = "Shareef",
        //                    PersonScreeningStatusID = 1,
        //                    StartDate = DateTime.Now,
        //                    DateOfBirth = DateTime.Now,
        //                    UpdateUserID = 1,
        //                    Suffix = "MR",
        //                    AgencyID = 1,
        //                    Phone1 = "9876543987",
        //                    PreferredLanguageID = 1,
        //                    PrimaryLanguageID = 1,
        //                    SexualityID = 1,
        //                    BiologicalSexID = 1,
        //                    PersonIndex = new Guid(),
        //                    GenderID = 1,
        //                    IsActive = true,
        //                    IsRemoved = false

        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.Person.AddAsync(new Domain.Entities.Person()
        //                {
        //                    FirstName = "Geethu",
        //                    MiddleName = "",
        //                    LastName = "Joseph",
        //                    PersonScreeningStatusID = 1,
        //                    StartDate = DateTime.Now,
        //                    DateOfBirth = DateTime.Now,
        //                    UpdateUserID = 1,
        //                    Suffix = "MRS",
        //                    AgencyID = 1,
        //                    Phone1 = "9037860356",
        //                    PreferredLanguageID = 1,
        //                    PrimaryLanguageID = 1,
        //                    SexualityID = 1,
        //                    BiologicalSexID = 2,
        //                    PersonIndex = new Guid(),
        //                    GenderID = 2,
        //                    IsActive = true,
        //                    IsRemoved = false

        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.Person.AddAsync(new Domain.Entities.Person()
        //                {
        //                    FirstName = "Ajeesh",
        //                    MiddleName = "k",
        //                    LastName = "Koshy",
        //                    PersonScreeningStatusID = 1,
        //                    StartDate = DateTime.Now,
        //                    DateOfBirth = DateTime.Now,
        //                    UpdateUserID = 1,
        //                    Suffix = "MR",
        //                    AgencyID = 1,
        //                    Phone1 = "9035860356",
        //                    PreferredLanguageID = 1,
        //                    PrimaryLanguageID = 1,
        //                    SexualityID = 1,
        //                    BiologicalSexID = 1,
        //                    PersonIndex = new Guid(),
        //                    GenderID = 1,
        //                    IsActive = true,
        //                    IsRemoved = false

        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonAddress.AnyAsync())
        //            {
        //                await dBContext.PersonAddress.AddAsync(new Domain.Entities.PersonAddress()
        //                {
        //                    PersonID = 1,
        //                    IsPrimary = true,
        //                    AddressID = 1

        //                }); ;
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.PersonAddress.AddAsync(new Domain.Entities.PersonAddress()
        //                {
        //                    PersonID = 2,
        //                    IsPrimary = true,
        //                    AddressID = 2

        //                }); ;
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonRaceEthnicity.AnyAsync())
        //            {
        //                await dBContext.PersonRaceEthnicity.AddAsync(new Domain.Entities.PersonRaceEthnicity()
        //                {
        //                    PersonID = 1,
        //                    RaceEthnicityID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Collaborations.AnyAsync())
        //            {
        //                await dBContext.Collaborations.AddAsync(new Domain.Entities.Collaboration()
        //                {
        //                    ReportingUnitID = 1,
        //                    TherapyTypeID = 1,
        //                    Name = "Collaboration1",
        //                    IntervalDays = 100,
        //                    StartDate = DateTime.Now,
        //                    CollaborationLeadUserID = 1,
        //                    UpdateUserID = 1,
        //                    AgencyID = 1,
        //                    CollaborationLevelID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonCollaborations.AnyAsync())
        //            {
        //                await dBContext.PersonCollaborations.AddAsync(new Domain.Entities.PersonCollaboration()
        //                {
        //                    PersonID = 1,
        //                    CollaborationID = 1,
        //                    EnrollDate = DateTime.Now,
        //                    UpdateUserID = 1,
        //                    IsCurrent = true,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonHelpers.AnyAsync())
        //            {
        //                await dBContext.PersonHelpers.AddAsync(new Domain.Entities.PersonHelper()
        //                {
        //                    PersonID = 1,
        //                    HelperID = 1,
        //                    StartDate = DateTime.Now,
        //                    IsCurrent = true,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonSupport.AnyAsync())
        //            {
        //                await dBContext.PersonSupport.AddAsync(new Domain.Entities.PersonSupport()
        //                {
        //                    FirstName = "Punya",
        //                    LastName = "Midhun",
        //                    StartDate = DateTime.Today,
        //                    IsCurrent = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                    PersonID = 1,
        //                    SupportTypeID = 1,
        //                    Email = "geethu.joseph@naicoits.com"
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonIdentification.AnyAsync())
        //            {
        //                await dBContext.PersonIdentification.AddAsync(new Domain.Entities.PersonIdentification()
        //                {
        //                    IdentificationNumber = "ID1",
        //                    IsRemoved = false,
        //                    PersonID = 1,
        //                    UpdateUserID = 1,
        //                    IdentificationTypeID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.PersonQuestionnaires.AnyAsync())
        //            {
        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 1,
        //                    QuestionnaireID = 1,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1

        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 2,
        //                    QuestionnaireID = 1,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 1,
        //                    QuestionnaireID = 2,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 2,
        //                    QuestionnaireID = 2,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 3,
        //                    QuestionnaireID = 1,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 3,
        //                    QuestionnaireID = 2,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 3,
        //                    QuestionnaireID = 3,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 2,
        //                    QuestionnaireID = 3,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.PersonQuestionnaires.AddAsync(new Domain.Entities.PersonQuestionnaire()
        //                {
        //                    PersonID = 1,
        //                    QuestionnaireID = 3,
        //                    StartDate = DateTime.UtcNow.Date,
        //                    IsActive = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.VoiceTypes.AnyAsync())
        //            {
        //                await dBContext.VoiceTypes.AddAsync(new Domain.Entities.VoiceType()
        //                {
        //                    Name = "Communimetric",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.VoiceTypes.AddAsync(new Domain.Entities.VoiceType()
        //                {
        //                    Name = "Person",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.VoiceTypes.AddAsync(new Domain.Entities.VoiceType()
        //                {
        //                    Name = "Support",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Assessments.AnyAsync())
        //            {
        //                await dBContext.Assessments.AddAsync(new Domain.Entities.Assessment()
        //                {
        //                    PersonQuestionnaireID = 1,
        //                    VoiceTypeID = 1,
        //                    DateTaken = DateTime.UtcNow.Date,
        //                    AssessmentReasonID = 2,
        //                    AssessmentStatusID = 2,
        //                    IsRemoved = false,
        //                    IsUpdate = false,
        //                    Approved = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Assessments.AddAsync(new Domain.Entities.Assessment()
        //                {
        //                    PersonQuestionnaireID = 2,
        //                    VoiceTypeID = 1,
        //                    DateTaken = DateTime.UtcNow.Date,
        //                    AssessmentReasonID = 2,
        //                    AssessmentStatusID = 2,
        //                    IsRemoved = false,
        //                    IsUpdate = false,
        //                    Approved = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Assessments.AddAsync(new Domain.Entities.Assessment()
        //                {
        //                    PersonQuestionnaireID = 3,
        //                    VoiceTypeID = 1,
        //                    DateTaken = DateTime.UtcNow.Date,
        //                    AssessmentReasonID = 2,
        //                    AssessmentStatusID = 2,
        //                    IsRemoved = false,
        //                    IsUpdate = false,
        //                    Approved = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Assessments.AddAsync(new Domain.Entities.Assessment()
        //                {
        //                    PersonQuestionnaireID = 4,
        //                    VoiceTypeID = 1,
        //                    DateTaken = DateTime.UtcNow.Date,
        //                    AssessmentReasonID = 2,
        //                    AssessmentStatusID = 2,
        //                    IsRemoved = false,
        //                    IsUpdate = false,
        //                    Approved = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Assessments.AddAsync(new Domain.Entities.Assessment()
        //                {
        //                    PersonQuestionnaireID = 5,
        //                    VoiceTypeID = 1,
        //                    DateTaken = DateTime.UtcNow.Date,
        //                    AssessmentReasonID = 2,
        //                    AssessmentStatusID = 2,
        //                    IsRemoved = false,
        //                    IsUpdate = false,
        //                    Approved = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Assessments.AddAsync(new Domain.Entities.Assessment()
        //                {
        //                    PersonQuestionnaireID = 6,
        //                    VoiceTypeID = 1,
        //                    DateTaken = DateTime.UtcNow.Date,
        //                    AssessmentReasonID = 2,
        //                    AssessmentStatusID = 2,
        //                    IsRemoved = false,
        //                    IsUpdate = false,
        //                    Approved = 1,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Collaborations.AnyAsync())
        //            {
        //                await dBContext.Collaborations.AddAsync(new Domain.Entities.Collaboration()
        //                {

        //                    Name = "ABC Reporting Unit CBT",
        //                    StartDate = DateTime.UtcNow,
        //                    UpdateDate = DateTime.UtcNow,
        //                    CollaborationLeadUserID = 1,
        //                    CollaborationLevelID = 1,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false,
        //                    ReportingUnitID = 1,
        //                    AgencyID = 1,
        //                    IntervalDays = 1,
        //                    TherapyTypeID = 1
        //                });

        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AssessmentResponses.AnyAsync())
        //            {
        //                await dBContext.AssessmentResponses.AddAsync(new Domain.Entities.AssessmentResponse()
        //                {
        //                    AssessmentID = 1,
        //                    ResponseID = 1,
        //                    ItemResponseBehaviorID = 3,
        //                    IsRequiredConfidential = false,
        //                    IsCloned = false,
        //                    QuestionnaireItemID = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponses.AddAsync(new Domain.Entities.AssessmentResponse()
        //                {
        //                    AssessmentID = 1,
        //                    ResponseID = 2,
        //                    ItemResponseBehaviorID = 3,
        //                    IsRequiredConfidential = true,
        //                    IsCloned = false,
        //                    QuestionnaireItemID = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponses.AddAsync(new Domain.Entities.AssessmentResponse()
        //                {
        //                    AssessmentID = 1,
        //                    ResponseID = 3,
        //                    ItemResponseBehaviorID = 1,
        //                    IsRequiredConfidential = false,
        //                    IsCloned = false,
        //                    QuestionnaireItemID = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponses.AddAsync(new Domain.Entities.AssessmentResponse()
        //                {
        //                    AssessmentID = 1,
        //                    ResponseID = 4,
        //                    ItemResponseBehaviorID = 3,
        //                    IsRequiredConfidential = true,
        //                    IsCloned = false,
        //                    QuestionnaireItemID = 4,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Notes.AnyAsync())
        //            {
        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "nec tempus mauris erat eget ipsum. Suspendisse sagittis.",
        //                    IsConfidential = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "egestas. Aliquam nec enim. Nunc",
        //                    IsConfidential = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "nec tempus mauris erat eget ipsum. Suspendisse sagittis.arcu eu odio tristique pharetra. Quisque ac libero nec ligula consectetuer rhoncus. Nullam velit dui, semper et, lacinia vitae, sodales at, velit. Pellentesque ultricies dignissim",
        //                    IsConfidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "sodales nisi magna sed dui. Fusce aliquam, enim nec tempus scelerisque, lorem ipsum sodales purus, in",
        //                    IsConfidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "Curabitur ut odio vel est tempor bibendum. Donec felis orci, adipiscing non, luctus sit amet, faucibus",
        //                    IsConfidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "Proin sed turpis nec mauris blandit mattis. Cras eget nisi dictum augue malesuada malesuada. Integer id",
        //                    IsConfidential = false,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.Notes.AddAsync(new Domain.Entities.Note()
        //                {
        //                    NoteText = "ante dictum cursus.",
        //                    IsConfidential = true,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AssessmentResponseNotes.AnyAsync())
        //            {
        //                await dBContext.AssessmentResponseNotes.AddAsync(new Domain.Entities.AssessmentResponseNote()
        //                {
        //                    AssessmentResponseID = 1,
        //                    NoteID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponseNotes.AddAsync(new Domain.Entities.AssessmentResponseNote()
        //                {
        //                    AssessmentResponseID = 2,
        //                    NoteID = 2
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponseNotes.AddAsync(new Domain.Entities.AssessmentResponseNote()
        //                {
        //                    AssessmentResponseID = 3,
        //                    NoteID = 3
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponseNotes.AddAsync(new Domain.Entities.AssessmentResponseNote()
        //                {
        //                    AssessmentResponseID = 4,
        //                    NoteID = 4
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.NotificationResolutionStatus.AnyAsync())
        //            {
        //                await dBContext.NotificationResolutionStatus.AddAsync(new Domain.Entities.NotificationResolutionStatus()
        //                {
        //                    Name = "Unresolved",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow.Date,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.NotificationResolutionStatus.AddAsync(new Domain.Entities.NotificationResolutionStatus()
        //                {
        //                    Name = "Resolved",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow.Date,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.NotificationLog.AnyAsync())
        //            {
        //                await dBContext.NotificationLog.AddAsync(new Domain.Entities.NotificationLog()
        //                {
        //                    NotificationDate = DateTime.UtcNow.Date,
        //                    PersonID = 1,
        //                    NotificationTypeID = 1,
        //                    FKeyValue = 1,
        //                    NotificationResolutionStatusID = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.NotificationLog.AddAsync(new Domain.Entities.NotificationLog()
        //                {
        //                    NotificationDate = DateTime.UtcNow.Date,
        //                    PersonID = 1,
        //                    NotificationTypeID = 2,
        //                    FKeyValue = 1,
        //                    NotificationResolutionStatusID = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.NotificationLog.AddAsync(new Domain.Entities.NotificationLog()
        //                {
        //                    NotificationDate = DateTime.UtcNow.Date,
        //                    PersonID = 1,
        //                    NotificationTypeID = 1,
        //                    FKeyValue = 1,
        //                    NotificationResolutionStatusID = 2,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.NotificationResolutionNote.AnyAsync())
        //            {
        //                await dBContext.NotificationResolutionNote.AddAsync(new Domain.Entities.NotificationResolutionNote()
        //                {
        //                    NotificationLogID = 1,
        //                    Note_NoteID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.NotificationResolutionNote.AddAsync(new Domain.Entities.NotificationResolutionNote()
        //                {
        //                    NotificationLogID = 2,
        //                    Note_NoteID = 2,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.NotifyRisk.AnyAsync())
        //            {
        //                await dBContext.NotifyRisk.AddAsync(new Domain.Entities.NotifyRisk()
        //                {
        //                    QuestionnaireNotifyRiskRuleID = 1,
        //                    PersonID = 1,
        //                    AssessmentID = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.NotifyRisk.AddAsync(new Domain.Entities.NotifyRisk()
        //                {
        //                    QuestionnaireNotifyRiskRuleID = 2,
        //                    PersonID = 2,
        //                    AssessmentID = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.UtcNow,
        //                    UpdateUserID = 1
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.NotifyReminder.AnyAsync())
        //            {
        //                await dBContext.NotifyReminder.AddAsync(new Domain.Entities.NotifyReminder()
        //                {
        //                    //PersonQuestionnaireScheduleID = 1,
        //                    QuestionnaireReminderRuleID = 1,
        //                    NotifyDate = DateTime.UtcNow.Date
        //                });
        //                await dBContext.SaveChangesAsync();
        //                await dBContext.NotifyReminder.AddAsync(new Domain.Entities.NotifyReminder()
        //                {
        //                    //PersonQuestionnaireScheduleID = 2,
        //                    QuestionnaireReminderRuleID = 2,
        //                    NotifyDate = DateTime.UtcNow.Date
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AssessmentNotes.AnyAsync())
        //            {
        //                await dBContext.AssessmentNotes.AddAsync(new Domain.Entities.AssessmentNote()
        //                {
        //                    AssessmentID = 1,
        //                    NoteID = 1
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentNotes.AddAsync(new Domain.Entities.AssessmentNote()
        //                {
        //                    AssessmentID = 1,
        //                    NoteID = 2
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentNotes.AddAsync(new Domain.Entities.AssessmentNote()
        //                {
        //                    AssessmentID = 2,
        //                    NoteID = 3
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AssessmentResponseTexts.AnyAsync())
        //            {
        //                await dBContext.AssessmentResponseTexts.AddAsync(new Domain.Entities.AssessmentResponseText()
        //                {
        //                    AssessmentResponseID = 1,
        //                    ResponseText = "Response1"
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.AssessmentResponseTexts.AddAsync(new Domain.Entities.AssessmentResponseText()
        //                {
        //                    AssessmentResponseID = 2,
        //                    ResponseText = "Response1"
        //                });
        //                await dBContext.SaveChangesAsync();

        //            }

        //            if (!await dBContext.CategoryFocus.AnyAsync())
        //            {
        //                await dBContext.CategoryFocus.AddAsync(new Domain.Entities.CategoryFocus()
        //                {
        //                    Name = "Self",
        //                    ListOrder = 1,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.CategoryFocus.AddAsync(new Domain.Entities.CategoryFocus()
        //                {
        //                    Name = "Natural Support",
        //                    ListOrder = 2,
        //                    UpdateUserID = 1,
        //                    IsRemoved = false
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.Address.AnyAsync())
        //            {
        //                await dBContext.Address.AddAsync(new Domain.Entities.Address()
        //                {
        //                    AddressIndex = Guid.NewGuid(),
        //                    Address1 = "Address1",
        //                    UpdateUserID = 1,
        //                    IsPrimary = true,
        //                    CountryStateId = 1,
        //                    IsRemoved = false,
        //                    UpdateDate = DateTime.Now
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.AgencyAddresses.AnyAsync())
        //            {
        //                await dBContext.AgencyAddresses.AddAsync(new Domain.Entities.AgencyAddress()
        //                {
        //                    AgencyID = 1,
        //                    AddressID = 1,
        //                    UpdateUserID = 1,
        //                    IsPrimary = true
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }

        //            if (!await dBContext.VoiceTypes.AnyAsync())
        //            {
        //                await dBContext.VoiceTypes.AddAsync(new Domain.Entities.VoiceType()
        //                {
        //                    Name = "Communimetric",
        //                    ListOrder = 1,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.VoiceTypes.AddAsync(new Domain.Entities.VoiceType()
        //                {
        //                    Name = "Person",
        //                    ListOrder = 2,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();

        //                await dBContext.VoiceTypes.AddAsync(new Domain.Entities.VoiceType()
        //                {
        //                    Name = "Support",
        //                    ListOrder = 3,
        //                    IsRemoved = false,
        //                    UpdateUserID = 1,
        //                });
        //                await dBContext.SaveChangesAsync();
        //            }
        //        }
    }
}