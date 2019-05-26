// <copyright file="WebApiConfig.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.API
{
    using System.Web.Http;

    /// <summary>
    ///     Web API configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        ///     Register web api config
        /// </summary>
        /// <param name="config">Configuration values</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // config.MessageHandlers.Add(new ApiKeyHandler("Mistletoe"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
