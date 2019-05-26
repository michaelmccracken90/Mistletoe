// <copyright file="Global.asax.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.API
{
    using System.Web.Http;
    using Mistletoe.BLL;
    using Mistletoe.Common;

    /// <summary>
    ///     Web API Application
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        ///     Application Start
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMapping();
        }
    }
}
