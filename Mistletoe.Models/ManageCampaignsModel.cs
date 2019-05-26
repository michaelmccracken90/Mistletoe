// <copyright file="ManageCampaignsModel.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     Manage Campaigns Model
    /// </summary>
    public class ManageCampaignsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageCampaignsModel"/> class.
        /// </summary>
        public ManageCampaignsModel()
        {
            this.CampaignList = new List<CampaignModel>();
            this.CampaignIDs = string.Empty;
        }

        /// <summary>
        ///     Gets or sets list of campaigns
        /// </summary>
        public List<CampaignModel> CampaignList { get; set; }

        /// <summary>
        ///     Gets or sets campaign IDs
        /// </summary>
        public string CampaignIDs { get; set; }
    }
}