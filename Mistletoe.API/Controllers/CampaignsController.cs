// <copyright file="CampaignsController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Mistletoe.BLL.Interfaces;
    using Mistletoe.BLL.Managers;
    using Mistletoe.Models;

    /// <summary>
    ///     Campaigns controller
    /// </summary>
    public class CampaignsController : ApiController, ICampaignManager
    {
        private ICampaignManager campaignManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignsController"/> class.
        /// </summary>
        public CampaignsController()
        {
            this.campaignManager = new CampaignManager();
        }

        /// <inheritdoc/>
        public bool ArchiveCampaign(int campaignID)
        {
            return this.campaignManager.ArchiveCampaign(campaignID);
        }

        /// <inheritdoc/>
        public bool ChangeCampaignStatus(int campaignID, bool newStatus)
        {
            return this.campaignManager.ChangeCampaignStatus(campaignID, newStatus);
        }

        /// <inheritdoc/>
        public bool ChangeCampaignStatusByUser(string userID, bool newStatus)
        {
            return this.campaignManager.ChangeCampaignStatusByUser(userID, newStatus);
        }

        /// <inheritdoc/>
        public bool CheckUserAccess(int campaignID, string userId)
        {
            return this.campaignManager.CheckUserAccess(campaignID, userId);
        }

        /// <inheritdoc/>
        public int CreateCampaignModel([FromBody]CampaignModel campaignToCreate)
        {
            return this.campaignManager.CreateCampaignModel(campaignToCreate);
        }

        /// <inheritdoc/>
        public bool DeleteCampaignModel(int campaign_ID)
        {
            return this.campaignManager.DeleteCampaignModel(campaign_ID);
        }

        /// <inheritdoc/>
        public IEnumerable<CampaignModel> GetActiveCampaignModels()
        {
            return this.campaignManager.GetActiveCampaignModels();
        }

        /// <inheritdoc/>
        public List<CampaignModel> GetAllCampaignModelsByUser(string userID)
        {
            return this.campaignManager.GetAllCampaignModelsByUser(userID);
        }

        /// <inheritdoc/>
        public string GetCampaignFrequency(int campaignId)
        {
            return this.campaignManager.GetCampaignFrequency(campaignId);
        }

        /// <inheritdoc/>
        public CampaignModel GetCampaignModel(int campaignId)
        {
            return this.campaignManager.GetCampaignModel(campaignId);
        }

        /// <inheritdoc/>
        public string GetDataFilePath(int campaignId)
        {
            return this.campaignManager.GetDataFilePath(campaignId);
        }

        /// <inheritdoc/>
        public bool UpdateCampaignFilePath(int campaignID, string newFilePath)
        {
            return this.campaignManager.UpdateCampaignFilePath(campaignID, newFilePath);
        }

        /// <inheritdoc/>
        public bool UpdateCampaignModel(CampaignModel campaignToUpdate)
        {
            return this.campaignManager.UpdateCampaignModel(campaignToUpdate);
        }
    }
}
