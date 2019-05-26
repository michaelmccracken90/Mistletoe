// <copyright file="RouteConfig.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    ///     Route configuration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     Register routes
        /// </summary>
        /// <param name="routes">Collection of routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
