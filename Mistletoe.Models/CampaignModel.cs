// <copyright file="CampaignModel.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Models
{
    using System;
    using System.Collections.Generic;
    using NCrontab.Advanced;

    /// <summary>
    ///     Campaign model
    /// </summary>
    public class CampaignModel
    {
        /// <summary>
        ///     Gets or sets campaign ID
        /// </summary>
        public string CampaignID { get; set; }

        /// <summary>
        ///     Gets or sets user ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        ///     Gets or sets Campaign Name
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        ///     Gets or sets start Data
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Gets or sets end Date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Gets or sets frequency
        /// </summary>
        public string Frequency { get; set; }

        /// <summary>
        ///     Gets or sets data File Path
        /// </summary>
        public string DataFilePath { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether IsArchived
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        ///     Gets next runs of the campaign
        /// </summary>
        public IEnumerable<DateTime> GetRuns()
        {
            var cronInstance = CrontabSchedule.Parse(this.Frequency);
            return cronInstance.GetNextOccurrences(this.StartDate, this.EndDate);            
        }

        /// <summary>
        ///     Gets start date for display
        /// </summary>
        public string StartDateForDisplay
        {
            get
            {
                return this.StartDate.ToString("dd-MMM-yyyy");
            }
        }

        /// <summary>
        ///     Gets end date for display
        /// </summary>
        public string EndDateForDisplay
        {
            get
            {
                return this.EndDate.ToString("dd-MMM-yyyy");
            }
        }

        /// <summary>
        ///     Gets status color
        /// </summary>
        public string StatusColor
        {
            get
            {
                if (this.IsActive)
                {
                    return "success";
                }
                else
                {
                    return "danger";
                }
            }
        }
    }
}
