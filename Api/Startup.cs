// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Autofac;
    using AutoWrapper;
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Logging;
    using Microsoft.OpenApi.Models;
    using Opeeka.PICS.Api.Filters;
    using Opeeka.PICS.API.Filters.Policy;
    using Opeeka.PICS.Api.Middleware;
    using Opeeka.PICS.Domain.Entities;
    using Opeeka.PICS.Domain.Interfaces;
    using Opeeka.PICS.Domain.Interfaces.Providers.Contract;
    using Opeeka.PICS.Domain.Providers.Implementations;
    using Opeeka.PICS.Domain.Resources;
    using Opeeka.PICS.Infrastructure;
    using Opeeka.PICS.Infrastructure.Caching;
    using Opeeka.PICS.Infrastructure.Common;
    using Opeeka.PICS.Infrastructure.Data;
    using Opeeka.PICS.Infrastructure.DI;
    using Opeeka.PICS.Infrastructure.Logging;
    using StackExchange.Profiling.Storage;
    using AuthorizationMiddleware = Opeeka.PICS.Api.Filters.AuthorizationMiddleware;
    using AntiXssMiddleware.Middleware;
    using AspNetCoreRateLimit;
    using Opeeka.PICS.Api.Middleware.Security;

    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        readonly string PCISOrigins = "PCISOrigins";
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            /*var contentRoot = Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var context = new PDFGenerator.Utilities.CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(contentRoot, "libwkhtmltox.dll"));*/
            var directorySeparator = Path.DirectorySeparatorChar;
            var wkHtmlToPdfFileName = "libwkhtmltox";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                wkHtmlToPdfFileName += ".so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                wkHtmlToPdfFileName += ".dylib";
            }
            var architectureFolder = (IntPtr.Size == 8) ? "64bit" : "32bit";
            var wkHtmlToPdfPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"wkhtmltox{directorySeparator}v0.12.4{directorySeparator}{architectureFolder}{directorySeparator}{wkHtmlToPdfFileName}");
            var context = new PDFGenerator.Utilities.CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(wkHtmlToPdfPath);

            /*var directorySeparator = Path.DirectorySeparatorChar;
            var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
            var wkHtmlToPdfPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"wkhtmltox"+ directorySeparator + "v0.12.4"+ directorySeparator + architectureFolder + directorySeparator + "libwkhtmltox.dll");
            var context = new PDFGenerator.Utilities.CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(wkHtmlToPdfPath);*/
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddMiniProfiler().AddEntityFramework();
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";

                // (Optional) Control storage
                // (default is 30 minutes in MemoryCacheStorage)
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);

                // (Optional) Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

                // (Optional) To control authorization, you can use the Func<HttpRequest, bool> options:
                // (default is everyone can access profilers)
                /* options.ResultsAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                 options.ResultsListAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                 // Or, there are async versions available:
                 options.ResultsAuthorizeAsync = async request => (await MyGetUserFunctionAsync(request)).CanSeeMiniProfiler;
                 options.ResultsAuthorizeListAsync = async request => (await MyGetUserFunctionAsync(request)).CanSeeMiniProfilerLists;

                 // (Optional)  To control which requests are profiled, use the Func<HttpRequest, bool> option:
                 // (default is everything should be profiled)
                 options.ShouldProfile = request => MyShouldThisBeProfiledFunction(request);

                 // (Optional) Profiles are stored under a user ID, function to get it:
                 // (default is null, since above methods don't use it by default)
                 options.UserIdProvider = request => MyGetUserIdFunction(request);

                 // (Optional) Swap out the entire profiler provider, if you want
                 // (default handles async and works fine for almost all applications)
                 options.ProfilerProvider = new MyProfilerProvider();*/

                // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
                // (defaults to true, and connection opening/closing is tracked)
                options.TrackConnectionOpenClose = true;
                options.PopupShowTimeWithChildren = true;

                // (Optional) Use something other than the "light" color scheme.
                // (defaults to "light")
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;

                // The below are newer options, available in .NET Core 3.0 and above:

                // (Optional) You can disable MVC filter profiling
                // (defaults to true, and filters are profiled)
                options.EnableMvcFilterProfiling = true;

                // ...or only save filters that take over a certain millisecond duration (including their children)
                // (defaults to null, and all filters are profiled)
                // options.MvcFilterMinimumSaveMs = 1.0m;

                // (Optional) You can disable MVC view profiling
                // (defaults to true, and views are profiled)
                options.EnableMvcViewProfiling = true;

                // ...or only save views that take over a certain millisecond duration (including their children)
                // (defaults to null, and all views are profiled)
                // options.MvcViewMinimumSaveMs = 1.0m;

                // (Optional - not recommended) You can enable a heavy debug mode with stacks and tooltips when using memory storage
                // It has a lot of overhead vs. normal profiling and should only be used with that in mind
                // (defaults to false, debug/heavy mode is off)
                //options.EnableDebugMode = true;
            });

            services.AddIdentity<AspNetUser, RolesLookup>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
                opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                opt.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultProvider;
                opt.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider;
                opt.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                opt.User.AllowedUserNameCharacters = String.Empty;
                opt.User.RequireUniqueEmail = true;
            })
            .AddDefaultTokenProviders();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddSingleton<ICache, RedisCache>();
            services.AddControllers();
            services.AddScoped<TransactionFilter>();
            services.AddSwaggerGen(c =>
            {
                var assemblyName = $"{Assembly.GetExecutingAssembly().GetName().Name}";
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                c.SwaggerDoc("api-v1", new OpenApiInfo
                {
                    Title = assemblyName.ToUpper(),
                    Version = "v1",
                    Description = assemblyName,
                });

                c.SwaggerDoc("api-external", new OpenApiInfo
                {
                    Title = "External APIs",
                    Version = "v1",
                    Description = "APIs for External API Users.",
                });

                c.SwaggerDoc("api-azurefunction", new OpenApiInfo
                {
                    Title = "AzureFunction APIs",
                    Version = "v1",
                    Description = "APIs used in Azure Functions.",
                });

                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<OpeekaDBContext>(options => options.UseSqlServer(Configuration.GetValue<string>("OpeekaDatabase")));
            services.AddCors(options =>
            {
                options.AddPolicy(name: PCISOrigins,
                builder =>
                {
                    builder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(Configuration.GetSection("AllowedHosts").Get<List<string>>().ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Authentication
            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => this.Configuration.Bind("AzureAdB2C", options));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddScheme<AuthenticationSchemeOptions, AuthorizationMiddleware>(JwtBearerDefaults.AuthenticationScheme, null);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("WebAPIPermission", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new CustomRequirement());
                });
                options.AddPolicy("EHRAPIPermission", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    //policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new EHRAPIPermissionRequirement());
                });
                options.AddPolicy("APIUserPermission", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    //policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new APIUserPermissionRequirement());
                });
            });
            services.AddScoped<IAuthorizationHandler, WebAPIPermissionHandler>();
            services.AddScoped<IAuthorizationHandler, EHRAPIPermissionHandler>();
            services.AddScoped<IAuthorizationHandler, APIUserPermissionHandler>();

            services.AddScoped<LocalizeService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                            new CultureInfo("de-CH"),
                            new CultureInfo("fr-CH"),
                            new CultureInfo("it-CH"),
                            new CultureInfo("es-ES"),
                        };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    // You can change which providers are configured to determine the culture for requests, or even add a custom
                    // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                    // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                    // By default, the following built-in providers are configured:
                    // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                    // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                    // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                    // options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                    options.RequestCultureProviders.Clear();
                    options.RequestCultureProviders.Add(new CustomCultureProvider());
                });

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            //////
            //DataProtection For QueryParameters
            services.AddDataProtection();

             #region Rate_Limit

            // needed to store rate limit counters and ip rules.
            services.AddMemoryCache();

            // inject counter and rules stores.
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // the IHttpContextAccessor service is not registered by default.
            // the clientId/clientIp resolvers use it.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion
        }

        /// <summary>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you by the factory.
        /// </summary>
        /// <param name="builder">The autofac container.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register Autofac Modules
            AutofacConfig.Register(builder);
            builder.RegisterModule(new OpeekaCoreModule(this.Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            var responseWrapper = new AutoWrapperOptions { ShowApiVersion = true, ApiVersion = "1.0", ShowStatusCode = true };
            app.UseApiResponseAndExceptionWrapper<ResponseWrapper>(responseWrapper);

            if (env.EnvironmentName == "Production" || env.EnvironmentName == "Production1")
            {
                #region Security.
                app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder()
                        .AddDefaultSecurePolicy());
                #endregion
            }

            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseRequestResponseLogging();
            app.UseCors(PCISOrigins);
            app.UseCustomIPRateLimitMiddleware();
            app.UseAntiXssMiddleware();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(locOptions.Value);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.EnvironmentName != "Production" || env.EnvironmentName != "Demo" || env.EnvironmentName != "Production1" || env.EnvironmentName != "Training")
            {

                app.UseMiniProfiler();
            }

            if (env.EnvironmentName != "Production" || env.EnvironmentName != "Production1")
            {
                app.UseSwagger();
                var swaggerName = $"{Assembly.GetExecutingAssembly().GetName().Name} v1";
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/api-v1/swagger.json", swaggerName);
                    c.SwaggerEndpoint("/swagger/api-external/swagger.json", "ExternalAPI");
                    c.SwaggerEndpoint("/swagger/api-azurefunction/swagger.json", "AzureFunctionAPI");
                });
            }

        }

    }
}
