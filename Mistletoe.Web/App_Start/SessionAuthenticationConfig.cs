// <copyright file="SessionAuthenticationConfig.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mistletoe.Web.App_Start.SessionAuthenticationConfig), "PreAppStart")]

namespace Mistletoe.Web.App_Start
{
    /// <summary>
    ///     Session Authentication Configuration
    /// </summary>
    public static class SessionAuthenticationConfig
    {
        /// <summary>
        ///     Pre App Start registration
        /// </summary>
        public static void PreAppStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(System.IdentityModel.Services.SessionAuthenticationModule));
        }
    }
}