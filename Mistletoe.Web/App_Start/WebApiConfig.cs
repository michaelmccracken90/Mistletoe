// <copyright file="WebApiConfig.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web
{
    using System.Web.Http;

    /// <summary>
    ///     Web API Configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        ///     Register Http Configuration
        /// </summary>
        /// <param name="config">Configuration values</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
