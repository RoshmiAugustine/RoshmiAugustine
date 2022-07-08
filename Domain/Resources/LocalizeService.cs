// -----------------------------------------------------------------------
// <copyright file="LocalizeService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Opeeka.PICS.Domain.Resources
{
    public class LocalizeService
    {
        public IStringLocalizer _localizer;

        public LocalizeService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public virtual LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}