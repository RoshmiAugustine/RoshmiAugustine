// -----------------------------------------------------------------------
// <copyright file="OpeekaCoreModule.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Opeeka.PICS.Infrastructure.Data;
using Opeeka.PICS.Infrastructure.Logging;
using Module = Autofac.Module;

namespace Opeeka.PICS.Infrastructure
{
    public class OpeekaCoreModule : Module
    {
        private readonly IConfiguration configuration;

        public OpeekaCoreModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            /**********************************************
             **
             **     PLEASE ENSURE THESE REMAIN ALPHABETICAL
             **
             **/
            var serviceCollection = new ServiceCollection();
            SerilogConfig.Configure(serviceCollection, this.configuration);
            // Registering DI
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();

            // Register any services
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                  .Where(t => t.Name.EndsWith("Provider"))
                  .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var accessor = c.Resolve<IHttpContextAccessor>();
                var opt = new DbContextOptionsBuilder<OpeekaDBContext>();
                opt.UseSqlServer(this.configuration.GetValue<string>("OpeekaDatabase"));

                return new OpeekaDBContext(opt.Options, config, accessor);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
