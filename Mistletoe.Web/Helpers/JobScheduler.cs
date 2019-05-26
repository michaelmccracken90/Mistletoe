// <copyright file="JobScheduler.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Helpers
{
    using System;
    using Hangfire;
    using NLog;

    /// <summary>
    ///     Job Scheduler
    /// </summary>
    public class JobScheduler
    {
        /// <summary>
        ///     Schedule campaign
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>True or False</returns>
        public static bool ScheduleCampaign(int campaignId)
        {
            MvcApplication.GlobalLogger.Log(LogLevel.Info, "Scheduling Campaign:" + campaignId);

            var campaign = MistletoeAPIHelper.GetCampaignModel(campaignId);

            try
            {
                RecurringDateRangeJob.AddOrUpdate(
                                                  "Campaign:" + campaignId,
                                                  () => JobPackager.SendMessages(campaignId),
                                                  campaign.Frequency,
                                                  TimeZoneInfo.Utc,
                                                  "default",
                                                  campaign.StartDate,
                                                  campaign.EndDate);
                return true;
            }
            catch (Exception exception)
            {
                MvcApplication.GlobalLogger.Log(LogLevel.Info, "Scheduling Campaign Error:" + exception.Message);
                return false;
            }
        }

        /// <summary>
        ///     Unschedule campaign
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>True or False</returns>
        public static bool UnscheduleCampaign(int campaignId)
        {
            MvcApplication.GlobalLogger.Log(LogLevel.Info, "Unscheduling Campaign:" + campaignId);

            try
            {
                RecurringDateRangeJob.RemoveIfExists("Campaign:" + campaignId);
                return true;
            }
            catch (Exception exception)
            {
                MvcApplication.GlobalLogger.Log(LogLevel.Info, "Unscheduling Campaign Error:" + exception.Message);
                return false;
            }
        }
    }
}