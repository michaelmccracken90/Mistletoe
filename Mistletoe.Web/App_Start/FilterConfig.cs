// <copyright file="FilterConfig.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web
{
    using System.Web.Mvc;

    /// <summary>
    ///     Filter configuration
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        ///     Register global filters
        /// </summary>
        /// <param name="filters">Collection of filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
