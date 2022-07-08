// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace b2c_ms_graph
{
    class UserService
    {
        public static async Task ListUsers(GraphServiceClient graphClient)
        {
            Console.WriteLine("Getting list of users...");

            // Get all users (one page)
            var result = await graphClient.Users
                .Request()
                .Select(e => new
                {
                    e.DisplayName,
                    e.Id,
                    e.Identities
                })
                .GetAsync();

            foreach (var user in result.CurrentPage)
            {
                Console.WriteLine(JsonConvert.SerializeObject(user));
            }
        }

        public static async Task ListUsersWithCustomAttribute(GraphServiceClient graphClient, string b2cExtensionAppClientId)
        {
            if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
            {
                throw new ArgumentException("B2cExtensionAppClientId (its Application ID) is missing from appsettings.json. Find it in the App registrations pane in the Azure portal. The app registration has the name 'b2c-extensions-app. Do not modify. Used by AADB2C for storing user data.'.", nameof(b2cExtensionAppClientId));
            }

            // Declare the names of the custom attributes
            const string customAttributeName1 = "agency";
            const string customAttributeName2 = "role";
            const string customAttributeName3 = "access";
            const string customAttributeName4 = "mustResetPassword";

            // Get the complete name of the custom attribute (Azure AD extension)
            Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(b2cExtensionAppClientId);
            string _customAttributeName1 = helper.GetCompleteAttributeName(customAttributeName1);
            string _customAttributeName2 = helper.GetCompleteAttributeName(customAttributeName2);
            string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);
            string _customAttributeName4 = helper.GetCompleteAttributeName(customAttributeName4);
            //string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);

            Console.WriteLine($"Getting list of users with the custom attributes '{customAttributeName1}' (string) and '{customAttributeName2}' (boolean)");
            Console.WriteLine();

            // Get all users (one page)
            var result = await graphClient.Users
                .Request()
                .Select($"id,displayName,identities,{_customAttributeName1},{_customAttributeName2},{_customAttributeName3},{_customAttributeName4}")
                //.Select($"*")
                .GetAsync();

            foreach (var user in result.CurrentPage)
            {
                Console.WriteLine(JsonConvert.SerializeObject(user));
                Console.WriteLine();

                // Only output the custom attributes...
                //Console.WriteLine(JsonConvert.SerializeObject(user.AdditionalData));
            }
        }

        public static async Task GetUserById(GraphServiceClient graphClient)
        {
            Console.Write("Enter user object ID: ");
            string userId = Console.ReadLine();

            Console.WriteLine($"Looking for user with object ID '{userId}'...");

            try
            {
                // Get user by object ID
                var result = await graphClient.Users[userId]
                    .Request()
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities,
                        //e.
                    })
                    .GetAsync();

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task GetUserBySignInName(AppSettings config, GraphServiceClient graphClient)
        {
            Console.Write("Enter user sign-in name (username or email address): ");
            string userId = Console.ReadLine();

            Console.WriteLine($"Looking for user with sign-in name '{userId}'...");

            try
            {
                // Get user by sign-in name
                var result = await graphClient.Users
                    .Request()
                    .Filter($"identities/any(c:c/issuerAssignedId eq '{userId}' and c/issuer eq '{config.TenantId}')")
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task DeleteUserById(GraphServiceClient graphClient)
        {
            Console.Write("Enter user object ID: ");
            string userId = Console.ReadLine();

            Console.WriteLine($"Looking for user with object ID '{userId}'...");

            try
            {
                // Delete user by object ID
                await graphClient.Users[userId]
                   .Request()
                   .DeleteAsync();

                Console.WriteLine($"User with object ID '{userId}' successfully deleted.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task SetPasswordByUserId(GraphServiceClient graphClient)
        {
            Console.Write("Enter user object ID: ");
            string userId = Console.ReadLine();

            Console.Write("Enter new password: ");
            string password = Console.ReadLine();

            Console.WriteLine($"Looking for user with object ID '{userId}'...");

            var user = new User
            {
                PasswordPolicies = "DisablePasswordExpiration,DisableStrongPassword",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = false,
                    Password = password,
                }
            };

            try
            {
                // Update user by object ID
                await graphClient.Users[userId]
                   .Request()
                   .UpdateAsync(user);

                Console.WriteLine($"User with object ID '{userId}' successfully updated.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task BulkCreate(AppSettings config, GraphServiceClient graphClient)
        {
            // Get the users to import
            string appDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string dataFilePath = Path.Combine(appDirectoryPath, config.UsersFileName);

            // Verify and notify on file existence
            if (!System.IO.File.Exists(dataFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File '{dataFilePath}' not found.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Starting bulk create operation...");

            // Read the data file and convert to object
            UsersModel users = UsersModel.Parse(System.IO.File.ReadAllText(dataFilePath));

            foreach (var user in users.Users)
            {
                user.SetB2CProfile(config.TenantId);

                try
                {
                    // Create the user account in the directory
                    User user1 = await graphClient.Users
                                    .Request()
                                    .AddAsync(user);

                    Console.WriteLine($"User '{user.DisplayName}' successfully created.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }

        //SuperAdmin 
        public static async Task CreateUserWithCustomAttribute(GraphServiceClient graphClient, string b2cExtensionAppClientId, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
            {
                throw new ArgumentException("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json. Get it from the App Registrations blade in the Azure portal. The app registration has the name 'b2c-extensions-app. Do not modify. Used by AADB2C for storing user data.'.", nameof(b2cExtensionAppClientId));
            }

            // Declare the names of the custom attributes
            const string customAttributeName1 = "agency";
            const string customAttributeName2 = "role";
            const string customAttributeName3 = "access";
            const string customAttributeName4 = "mustResetPassword";

            const string customAttributeName5 = "tenantId";
            const string customAttributeName6 = "userId";
            const string customAttributeName7 = "tenantAbbreviation";
            const string customAttributeName8 = "dateTimeFormat";
            const string customAttributeName9 = "instanceURL";

            // Get the complete name of the custom attribute (Azure AD extension)
            Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(b2cExtensionAppClientId);
            string _customAttributeName1 = helper.GetCompleteAttributeName(customAttributeName1);
            string _customAttributeName2 = helper.GetCompleteAttributeName(customAttributeName2);
            string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);
            string _customAttributeName4 = helper.GetCompleteAttributeName(customAttributeName4);
            string _customAttributeName5 = helper.GetCompleteAttributeName(customAttributeName5);
            string _customAttributeName6 = helper.GetCompleteAttributeName(customAttributeName6);
            string _customAttributeName7 = helper.GetCompleteAttributeName(customAttributeName7);
            string _customAttributeName8 = helper.GetCompleteAttributeName(customAttributeName8);
            string _customAttributeName9 = helper.GetCompleteAttributeName(customAttributeName9);

            Console.WriteLine($"Create a user with the custom attributes '{customAttributeName1}' (string) and '{customAttributeName2}' (string)");

            Console.Write("Enter role: ");
            string role = Console.ReadLine();
            Console.Write("Enter userID: ");
            string user = Console.ReadLine();

            Console.Write("Enter AgencyID: ");
            string agencyId = Console.ReadLine();

            Console.Write("Enter AgencyName: ");
            string agencyName = Console.ReadLine();

            Console.Write("Enter AgencyAbbrevation: ");
            string agencyAbbrev = Console.ReadLine();

            Console.Write("Enter instanceURL: ");
            string instanceURL = Console.ReadLine();

            // Fill custom attributes
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(_customAttributeName1, agencyName); // agency
            extensionInstance.Add(_customAttributeName2, role); // role
            extensionInstance.Add(_customAttributeName3, role); // access
            extensionInstance.Add(_customAttributeName4, "true"); //mustResetPassword
            extensionInstance.Add(_customAttributeName5, agencyId);
            extensionInstance.Add(_customAttributeName6, user);
            extensionInstance.Add(_customAttributeName7, agencyAbbrev);
            extensionInstance.Add(_customAttributeName8, "");
            extensionInstance.Add(_customAttributeName9, instanceURL);

            Console.Write("Enter DisplayName: ");
            string DisplayName = Console.ReadLine();

            Console.Write("Enter GivenName: ");
            string GivenName = Console.ReadLine();

            Console.Write("Enter Surname: ");
            string Surname = Console.ReadLine();

            Console.Write("Enter emailid: ");
            string emailId = Console.ReadLine();

            try
            {
                // Create user
                var result = await graphClient.Users
                .Request()
                .AddAsync(new User
                {
                    GivenName = GivenName,
                    Surname = Surname,
                    DisplayName = DisplayName,
                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = tenantId,
                            IssuerAssignedId = emailId
                        }
                    },
                    PasswordProfile = new PasswordProfile()
                    {
                        ForceChangePasswordNextSignIn = false,
                        Password = "Password@123" //Helpers.PasswordHelper.GenerateNewPassword(4, 8, 4)
                    },
                    PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = extensionInstance
                });

                string userId = result.Id;

                Console.WriteLine($"Created the new user. Now get the created user with object ID '{userId}'...");

                // Get created user by object ID
                result = await graphClient.Users[userId]
                    .Request()
                    .Select($"id,givenName,surName,displayName,identities,{_customAttributeName1},{_customAttributeName2},{_customAttributeName3},{_customAttributeName4},{_customAttributeName9}")
                    .GetAsync();

                if (result != null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"DisplayName: {result.DisplayName}");
                    Console.WriteLine($"{customAttributeName1}: {result.AdditionalData[_customAttributeName1].ToString()}");
                    Console.WriteLine($"{customAttributeName2}: {result.AdditionalData[_customAttributeName2].ToString()}");
                    Console.WriteLine($"{customAttributeName3}: {result.AdditionalData[_customAttributeName3].ToString()}");
                    Console.WriteLine($"{customAttributeName4}: {result.AdditionalData[_customAttributeName4].ToString()}");
                    Console.WriteLine($"{customAttributeName9}: {result.AdditionalData[_customAttributeName9].ToString()}");
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Have you created the custom attributes '{customAttributeName1}' (string) and '{customAttributeName2}' (boolean) in your tenant?");
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        //Update the function as per the requirement of which attribute to be updated.
        public static async Task UpdateUserWithCustomAttribute_Old(GraphServiceClient graphClient, string b2cExtensionAppClientId, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
            {
                throw new ArgumentException("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json. Get it from the App Registrations blade in the Azure portal. The app registration has the name 'b2c-extensions-app. Do not modify. Used by AADB2C for storing user data.'.", nameof(b2cExtensionAppClientId));
            }

            Console.Write("Enter user object ID (To Whom): ");
            string userId = Console.ReadLine();


            // Declare the names of the custom attributes
            const string customAttributeName1 = "agency";
            const string customAttributeName2 = "role";
            const string customAttributeName3 = "access";
            //const string customAttributeName4 = "mustResetPassword";

            const string customAttributeName5 = "tenantId";
            const string customAttributeName6 = "userId";
            const string customAttributeName7 = "tenantAbbreviation";
            const string customAttributeName8 = "dateTimeFormat";
            const string customAttributeName9 = "instanceURL";

            // Get the complete name of the custom attribute (Azure AD extension)
            Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(b2cExtensionAppClientId);
            string _customAttributeName1 = helper.GetCompleteAttributeName(customAttributeName1);
            string _customAttributeName2 = helper.GetCompleteAttributeName(customAttributeName2);
            string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);
            // string _customAttributeName4 = helper.GetCompleteAttributeName(customAttributeName4);
            string _customAttributeName5 = helper.GetCompleteAttributeName(customAttributeName5);
            string _customAttributeName6 = helper.GetCompleteAttributeName(customAttributeName6);
            string _customAttributeName7 = helper.GetCompleteAttributeName(customAttributeName7);
            string _customAttributeName8 = helper.GetCompleteAttributeName(customAttributeName8);
            string _customAttributeName9 = helper.GetCompleteAttributeName(customAttributeName9);

            //Console.WriteLine($"Create a user with the custom attributes '{customAttributeName1}' (string) and '{customAttributeName2}' (boolean)");

            Console.Write("Enter role: ");
            string role = Console.ReadLine();
            // Console.Write("Enter userID (New User): ");
            // string user = Console.ReadLine();

            //Console.Write("Enter instancURL: ");
            //string url = Console.ReadLine(); //https://agency1.pcis-dev.com/

            // Fill custom attributes
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(_customAttributeName1, "agency one"); // agency
            extensionInstance.Add(_customAttributeName2, role); // role
            //extensionInstance.Add(_customAttributeName3, role); // access
            //extensionInstance.Add(_customAttributeName4, "true"); //mustResetPassword
            extensionInstance.Add(_customAttributeName5, 1);
            // extensionInstance.Add(_customAttributeName6, user);
            extensionInstance.Add(_customAttributeName7, "agency1");
            //extensionInstance.Add(_customAttributeName8, "");
            //extensionInstance.Add(_customAttributeName9, url);

            //Console.Write("Enter DisplayName: ");
            //string DisplayName = Console.ReadLine();

            //Console.Write("Enter GivenName: ");
            //string GivenName = Console.ReadLine();

            //Console.Write("Enter Surname: ");
            //string Surname = Console.ReadLine();

            //Console.Write("Enter emailid: ");
            //string emailId = Console.ReadLine();

            try
            {
                var userObj = new User
                {
                    // GivenName = GivenName,
                    //Surname = Surname,
                    //DisplayName = DisplayName,
                    //Identities = new List<ObjectIdentity>
                    //{
                    //    new ObjectIdentity()
                    //    {
                    //        SignInType = "emailAddress",
                    //        Issuer = tenantId,
                    //        IssuerAssignedId = emailId
                    //    }
                    //},
                    //PasswordProfile = new PasswordProfile()
                    //{
                    //    ForceChangePasswordNextSignIn = false,
                    //    Password = "Password@123" //Helpers.PasswordHelper.GenerateNewPassword(4, 8, 4)
                    //},
                    //PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = extensionInstance
                };

                // Update user by object ID
                await graphClient.Users[userId]
                   .Request()
                   .UpdateAsync(userObj);

                Console.WriteLine($"User with object ID '{userId}' successfully updated.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task CreateAPIUser_WithCustomAttribute(GraphServiceClient graphClient, string b2cExtensionAppClientId, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
            {
                throw new ArgumentException("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json. Get it from the App Registrations blade in the Azure portal. The app registration has the name 'b2c-extensions-app. Do not modify. Used by AADB2C for storing user data.'.", nameof(b2cExtensionAppClientId));
            }

            // Declare the names of the custom attributes
            const string customAttributeName1 = "agency";
            const string customAttributeName2 = "role";
            const string customAttributeName3 = "access";
            const string customAttributeName4 = "mustResetPassword";

            const string customAttributeName5 = "tenantId";
            const string customAttributeName6 = "userId";
            const string customAttributeName7 = "tenantAbbreviation";
            const string customAttributeName8 = "dateTimeFormat";
            const string customAttributeName9 = "instanceURL";

            // Get the complete name of the custom attribute (Azure AD extension)
            Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(b2cExtensionAppClientId);
            string _customAttributeName1 = helper.GetCompleteAttributeName(customAttributeName1);
            string _customAttributeName2 = helper.GetCompleteAttributeName(customAttributeName2);
            string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);
            string _customAttributeName4 = helper.GetCompleteAttributeName(customAttributeName4);
            string _customAttributeName5 = helper.GetCompleteAttributeName(customAttributeName5);
            string _customAttributeName6 = helper.GetCompleteAttributeName(customAttributeName6);
            string _customAttributeName7 = helper.GetCompleteAttributeName(customAttributeName7);
            string _customAttributeName8 = helper.GetCompleteAttributeName(customAttributeName8);
            string _customAttributeName9 = helper.GetCompleteAttributeName(customAttributeName9);

            Console.WriteLine($"Create an API user with the custom attributes against an Agency");

            string role = "API User";

            Console.Write("Enter AgencyID: ");
            string agencyId = Console.ReadLine();

            Console.Write("Enter AgencyName: ");
            string agencyName = Console.ReadLine();

            Console.Write("Enter AgencyAbbrevation: ");
            string agencyAbbrev = Console.ReadLine();

            Console.Write("Enter userID: ");
            string user = Console.ReadLine();

            Console.Write("Enter Instance URL: ");
            string loginurl = Console.ReadLine();

            // Fill custom attributes
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(_customAttributeName1, agencyName); // agency
            extensionInstance.Add(_customAttributeName2, role); // role
            extensionInstance.Add(_customAttributeName3, role); // access
            extensionInstance.Add(_customAttributeName4, "false"); //mustResetPassword
            extensionInstance.Add(_customAttributeName5, agencyId);
            extensionInstance.Add(_customAttributeName6, user);
            extensionInstance.Add(_customAttributeName7, agencyAbbrev);
            extensionInstance.Add(_customAttributeName8, "");
            extensionInstance.Add(_customAttributeName9, loginurl);


            Console.Write("Enter DisplayName: ");
            string DisplayName = Console.ReadLine();

            Console.Write("Enter GivenName: ");
            string GivenName = Console.ReadLine();

            Console.Write("Enter Surname: ");
            string Surname = Console.ReadLine();

            Console.Write("Enter emailid: ");
            string emailId = Console.ReadLine();

            Console.Write("Enter the password: ");
            string password = Console.ReadLine();


            Console.WriteLine($"Creating the API User...");

            try
            {
                // Create user
                var result = await graphClient.Users
                .Request()
                .AddAsync(new User
                {
                    GivenName = GivenName,
                    Surname = Surname,
                    DisplayName = DisplayName,
                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = tenantId,
                            IssuerAssignedId = emailId
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

                string userId = result.Id;

                Console.WriteLine("");
                Console.WriteLine($"Successfully created the new API user");

                // Get created user by object ID
                result = await graphClient.Users[userId]
                    .Request()
                    .Select($"id,givenName,surName,displayName,identities,{_customAttributeName1},{_customAttributeName2},{_customAttributeName3},{_customAttributeName4},{_customAttributeName9}")
                    .GetAsync();

                if (result != null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"ObjectID: {userId}");
                    Console.WriteLine($"DisplayName: {result.DisplayName}");
                    Console.WriteLine($"{customAttributeName1}: {result.AdditionalData[_customAttributeName1].ToString()}");
                    Console.WriteLine($"{customAttributeName2}: {result.AdditionalData[_customAttributeName2].ToString()}");
                    Console.WriteLine($"{customAttributeName3}: {result.AdditionalData[_customAttributeName3].ToString()}");
                    Console.WriteLine($"{customAttributeName4}: {result.AdditionalData[_customAttributeName4].ToString()}");
                    Console.WriteLine($"{customAttributeName9}: {result.AdditionalData[_customAttributeName9].ToString()}");
                    Console.WriteLine();
                    Console.ResetColor();
                    //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task GetUserDetails_FromObjectID(GraphServiceClient graphClient, string b2cExtensionAppClientId, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
            {
                throw new ArgumentException("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json. Get it from the App Registrations blade in the Azure portal. The app registration has the name 'b2c-extensions-app. Do not modify. Used by AADB2C for storing user data.'.", nameof(b2cExtensionAppClientId));
            }

            Console.Write("Enter User ObjectID from ADB2C: ");
            string userObjID = Console.ReadLine();

            // Declare the names of the custom attributes
            const string customAttributeName1 = "agency";
            const string customAttributeName2 = "role";
            const string customAttributeName3 = "access";
            // const string customAttributeName4 = "mustResetPassword";

            const string customAttributeName5 = "tenantId";
            const string customAttributeName6 = "userId";
            const string customAttributeName7 = "tenantAbbreviation";
            const string customAttributeName8 = "dateTimeFormat";
            const string customAttributeName9 = "instanceURL";

            // Get the complete name of the custom attribute (Azure AD extension)
            Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(b2cExtensionAppClientId);
            string _customAttributeName1 = helper.GetCompleteAttributeName(customAttributeName1);
            string _customAttributeName2 = helper.GetCompleteAttributeName(customAttributeName2);
            string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);
            //  string _customAttributeName4 = helper.GetCompleteAttributeName(customAttributeName4);
            string _customAttributeName5 = helper.GetCompleteAttributeName(customAttributeName5);
            string _customAttributeName6 = helper.GetCompleteAttributeName(customAttributeName6);
            string _customAttributeName7 = helper.GetCompleteAttributeName(customAttributeName7);
            // string _customAttributeName8 = helper.GetCompleteAttributeName(customAttributeName8);
            string _customAttributeName9 = helper.GetCompleteAttributeName(customAttributeName9);
            try
            {
                // Get created user by object ID
                var result = await graphClient.Users[userObjID]
                    .Request()
                    .Select($"mail,id,givenName,surName,displayName,identities,{_customAttributeName1},{_customAttributeName2},{_customAttributeName3},{_customAttributeName5},{_customAttributeName6},{_customAttributeName7},{_customAttributeName9}")
                    .GetAsync();

                if (result != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("          ********User Details**********");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"ObjectID: {userObjID}");
                    Console.WriteLine($"DisplayName: {result.DisplayName}");
                    Console.WriteLine($"EmailID: {result.Identities?.ToList()[0]?.IssuerAssignedId}");
                    Console.WriteLine($"{customAttributeName1}: {result.AdditionalData[_customAttributeName1].ToString()}");
                    Console.WriteLine($"{customAttributeName2}: {result.AdditionalData[_customAttributeName2].ToString()}");
                    Console.WriteLine($"{customAttributeName3}: {result.AdditionalData[_customAttributeName3].ToString()}");
                    //   Console.WriteLine($"{customAttributeName4}: {result.AdditionalData[_customAttributeName4].ToString()}");
                    Console.WriteLine($"{customAttributeName5}: {result.AdditionalData[_customAttributeName5].ToString()}");
                    Console.WriteLine($"{customAttributeName6}: {result.AdditionalData[_customAttributeName6].ToString()}");
                    Console.WriteLine($"{customAttributeName7}: {result.AdditionalData[_customAttributeName7].ToString()}");
                    // Console.WriteLine($"{customAttributeName8}: {result.AdditionalData[_customAttributeName8].ToString()}");
                    Console.WriteLine($"{customAttributeName9}: {result.AdditionalData[_customAttributeName9].ToString()}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task BulkUpdateUsersInstanceURL(AppSettings config, GraphServiceClient graphClient)
        {
            try
            {
                // Get the users to import
                string appDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string dataFilePath = Path.Combine(appDirectoryPath, config.UsersFileNameForURLUpdate);

                // Verify and notify on file existence
                if (!System.IO.File.Exists(dataFilePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"File '{dataFilePath}' not found.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter the instance URL for the users: ");
                string url = Console.ReadLine();


                Console.WriteLine($"Reading users data from file..");

                var userDataJson = System.IO.File.ReadAllText(dataFilePath);
                // Read the data file and convert to object
                var users = JsonConvert.DeserializeObject<List<UserToUpdateURL>>(userDataJson);

                Console.WriteLine($"Toatl Users from DB to be updated: {users.Count}");
                Console.WriteLine("Starting bulk update operation...");

                const string customAttributeName9 = "instanceURL";

                // Get the complete name of the custom attribute (Azure AD extension)
                Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(config.B2cExtensionAppClientId);
                string _customAttributeName9 = helper.GetCompleteAttributeName(customAttributeName9);

                IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
                extensionInstance.Add(_customAttributeName9, url); // url

                List<string> existngUserObjIDs = GetAllUsersObjectIDInADB2C(graphClient).Result;
                
                List<string> failedObjID = new List<string>();
                List<string> notExistsObjID = new List<string>();
                int i = 0;
                foreach (var user in users)
                {
                    if (existngUserObjIDs.Contains(user.AspNetUserID))
                    {
                        try
                        {
                            var userObj = new User
                            {
                                AdditionalData = extensionInstance
                            };

                            // Update user by object ID..Throws error IF objectid is invalid in ADB2C.
                            await graphClient.Users[user.AspNetUserID]
                               .Request()
                               .UpdateAsync(userObj);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($" {++i} : User object ID : {user.AspNetUserID} : Successfully Updated.");
                            Console.ResetColor();

                        }
                        catch (Exception ex)
                        {
                            failedObjID.Add(user.AspNetUserID);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($" {++i} : User object ID : {user.AspNetUserID} : Failed. Exception : {ex.Message}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        notExistsObjID.Add(user.AspNetUserID);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($" {++i} : User object ID : {user.AspNetUserID} : Does not exist in B2C");
                        Console.ResetColor();
                    }
                }
                if (failedObjID.Count > 0)
                    Console.WriteLine($"Update failed count : {failedObjID.Count} : { string.Join(",", failedObjID)}"); 
                if (notExistsObjID.Count > 0)
                    Console.WriteLine($"Not existing users count : {notExistsObjID.Count} : { string.Join(",", notExistsObjID)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static async Task<List<string>> GetAllUsersObjectIDInADB2C(GraphServiceClient graphClient)
        {
            try
            {
                List<string> lst_ObjectIDs = new List<string>();
                List<User> lst_users = new List<User>();
                var existingUsersResult = await graphClient.Users
                    .Request().Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

                lst_users.AddRange(existingUsersResult.CurrentPage);

                Console.WriteLine("Please wait...");
                while (existingUsersResult.NextPageRequest != null)
                {
                    existingUsersResult = await existingUsersResult.NextPageRequest.GetAsync();
                    lst_users.AddRange(existingUsersResult.CurrentPage);
                }
                foreach (var user in lst_users)
                {
                    lst_ObjectIDs.Add(user.Id);
                }
                return lst_ObjectIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }  

        public static async Task UpdateUserWithCustomAttribute(GraphServiceClient graphClient, string b2cExtensionAppClientId, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(b2cExtensionAppClientId))
            {
                throw new ArgumentException("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json. Get it from the App Registrations blade in the Azure portal. The app registration has the name 'b2c-extensions-app. Do not modify. Used by AADB2C for storing user data.'.", nameof(b2cExtensionAppClientId));
            }
            // Declare the names of the custom attributes
            const string customAttributeName1 = "agency";
            const string customAttributeName2 = "role";
            const string customAttributeName3 = "tenantId";
            const string customAttributeName4 = "userId";
            const string customAttributeName5 = "tenantAbbreviation";
            const string customAttributeName6 = "instanceURL";
            const string customAttributeName7 = "access";

            // Get the complete name of the custom attribute (Azure AD extension)
            Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(b2cExtensionAppClientId);
            string _customAttributeName1 = helper.GetCompleteAttributeName(customAttributeName1);
            string _customAttributeName2 = helper.GetCompleteAttributeName(customAttributeName2);
            string _customAttributeName3 = helper.GetCompleteAttributeName(customAttributeName3);
            string _customAttributeName4 = helper.GetCompleteAttributeName(customAttributeName4);
            string _customAttributeName5 = helper.GetCompleteAttributeName(customAttributeName5);
            string _customAttributeName6 = helper.GetCompleteAttributeName(customAttributeName6);
            string _customAttributeName7 = helper.GetCompleteAttributeName(customAttributeName7);

            Console.WriteLine();
            Console.WriteLine($"*Update a user with the custom attributes*");
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine();

            Console.WriteLine(" SNO   Attributes");
            Console.WriteLine("======================");
            Console.WriteLine($" [1]  {char.ToUpper(customAttributeName1[0]) + customAttributeName1.Substring(1)}");            
            Console.WriteLine($" [2]  {char.ToUpper(customAttributeName2[0]) + customAttributeName2.Substring(1)}");           
            Console.WriteLine($" [3]  {char.ToUpper(customAttributeName3[0]) + customAttributeName3.Substring(1)}");            
            Console.WriteLine($" [4]  {char.ToUpper(customAttributeName4[0]) + customAttributeName4.Substring(1)}");            
            Console.WriteLine($" [5]  {char.ToUpper(customAttributeName5[0]) + customAttributeName5.Substring(1)}");            
            Console.WriteLine($" [6]  {char.ToUpper(customAttributeName6[0]) + customAttributeName6.Substring(1)}");
            Console.WriteLine($" [7]  Email ID");
            Console.WriteLine($" [8]  Reset Password");

            Console.WriteLine();
            Console.Write("Enter the SNO of attributes to be updated (with comma seperated): ");
            string options = Console.ReadLine();
            options.Trim();
            var lst_Options = options.Split(",").ToList();

            Console.Write("Enter user object ID (To Whom): ");
            string userId = Console.ReadLine();

            // Fill custom attributes
            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            string agencyNAme = "", roleName = "", userID = "", tenantID = "", tenantAbbrev = "", instanceURL = "", emailId = "", resetPswd = "";
            foreach(var option in lst_Options)
            {
                if(!string.IsNullOrWhiteSpace(option))
                {
                    switch(option)
                    {
                        case "1" :
                            Console.Write("Enter Agency Name: ");
                            agencyNAme = Console.ReadLine();
                            if(!string.IsNullOrWhiteSpace(agencyNAme))
                            extensionInstance.Add(_customAttributeName1, agencyNAme); // agency
                            break;
                        case "2":
                            Console.Write("Enter Role Name: ");
                            roleName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(roleName))
                            {
                                extensionInstance.Add(_customAttributeName2, roleName); // role
                                extensionInstance.Add(_customAttributeName7, roleName); // access
                            }
                            break;
                        case "3":
                            Console.Write("Enter Agency/TenantID: ");
                            tenantID = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(tenantID))
                                extensionInstance.Add(_customAttributeName3, tenantID);
                            break;
                        case "4":
                            Console.Write("Enter UserID: ");
                            userID = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(userID))
                                extensionInstance.Add(_customAttributeName4, userID);
                            break;
                        case "5":
                            Console.Write("Enter Agency Abbrevation: ");
                            tenantAbbrev = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(tenantAbbrev))
                                extensionInstance.Add(_customAttributeName5, tenantAbbrev);
                            break;
                        case "6":
                            Console.Write("Enter Instance URL: ");
                            instanceURL = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(instanceURL))
                                extensionInstance.Add(_customAttributeName6, instanceURL);
                            break;
                        case "7":
                            Console.Write("Enter Email ID: ");
                            emailId = Console.ReadLine();
                            break;
                        case "8":
                            Console.Write("Are you sure to reset password(Y/N)");
                            resetPswd = Console.ReadLine();
                            break;
                        default:
                            break;
                    }
                }
            }

            try
            {
                var userObj = new User
                {                    
                    AdditionalData = extensionInstance
                };
                if (!string.IsNullOrWhiteSpace(emailId))
                {
                    userObj.Identities = new List<ObjectIdentity>
                        {
                            new ObjectIdentity()
                            {
                                SignInType = "emailAddress",
                                Issuer = tenantId,
                                IssuerAssignedId = emailId
                            }
                        };
                   
                }
                if (!string.IsNullOrWhiteSpace(resetPswd) && resetPswd.ToUpper() == "Y")
                {
                    userObj.PasswordProfile = new PasswordProfile()
                    {
                        ForceChangePasswordNextSignIn = false,
                        Password = "Password@123" //Helpers.PasswordHelper.GenerateNewPassword(4, 8, 4)
                    };
                    userObj.PasswordPolicies = "DisablePasswordExpiration";
                }

                // Update user by object ID
                await graphClient.Users[userId]
                   .Request()
                   .UpdateAsync(userObj);

                Console.WriteLine($"User with object ID '{userId}' successfully updated.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

    }
}
