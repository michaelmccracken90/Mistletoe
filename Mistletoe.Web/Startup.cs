// <copyright file="Startup.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
using Hangfire;
using Hangfire.RecurringDateRange.Server;
using Hangfire.SqlServer;
using Microsoft.Owin;
using NCrontab.Advanced.Enumerations;
using Owin;

[assembly: OwinStartup("OwinHangfireConfig", typeof(Mistletoe.Web.Startup))]
namespace Mistletoe.Web
{
    /// <summary>
    ///     Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Configuration setup
        /// </summary>
        /// <param name="app">App Builder instance</param>
        public void Configuration(IAppBuilder app)
        {
            var options = new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true
            };

            GlobalConfiguration.Configuration.UseSqlServerStorage("MistletoeData", options);

            app.UseHangfireDashboard();

            var backgroundServerJobOptions = new BackgroundJobServerOptions();
            app.UseHangfireServer(backgroundServerJobOptions, new RecurringDateRangeJobScheduler(CronStringFormat.WithSecondsAndYears));
        }
    }
}
