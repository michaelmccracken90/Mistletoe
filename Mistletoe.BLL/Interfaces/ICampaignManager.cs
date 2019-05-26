// <copyright file="ICampaignManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Mistletoe.Models;

    /// <summary>
    ///     Business layer interface
    /// </summary>
    public interface ICampaignManager : IDisposable
    {
        /// <summary>
        /// Update Campaign File Path
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="newFilePath">New File Path</param>
        /// <returns>True or False</returns>
        bool UpdateCampaignFilePath(int campaignID, string newFilePath);

        /// <summary>
        ///     Change Campaign Status
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="newStatus">New Status</param>
        /// <returns>True or False</returns>
        bool ChangeCampaignStatus(int campaignID, bool newStatus);

        /// <summary>
        ///     Change Campaign Status by User
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="newStatus">New Status</param>
        /// <returns>True or False</returns>
        bool ChangeCampaignStatusByUser(string userID, bool newStatus);

        /// <summary>
        ///     Archive Campaign
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>True or False</returns>
        bool ArchiveCampaign(int campaignID);

        /// <summary>
        ///     Get Data File Path
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>Path to Data File</returns>
        string GetDataFilePath(int campaignId);

        /// <summary>
        ///     Get Campaign Frequency
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>Frequency as Cron String</returns>
        string GetCampaignFrequency(int campaignId);

        /// <summary>
        ///     Check User Access
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>True or False</returns>
        bool CheckUserAccess(int campaignID, string userId);

        /// <summary>
        ///     Get all campaign models by user
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>List of campaigns</returns>
        List<CampaignModel> GetAllCampaignModelsByUser(string userID);

        /// <summary>
        ///     Get Active Campaign Models
        /// </summary>
        /// <returns>List of active campaigns</returns>
        IEnumerable<CampaignModel> GetActiveCampaignModels();

        /// <summary>
        ///     Get campaign model
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>Campaign model</returns>
        CampaignModel GetCampaignModel(int campaignId);

        /// <summary>
        ///     Create campaign model
        /// </summary>
        /// <param name="campaignToCreate">Campaign to create</param>
        /// <returns>-1 if it fails, otherwise the correct campaign ID</returns>
        int CreateCampaignModel(CampaignModel campaignToCreate);

        /// <summary>
        ///     Update campaign model
        /// </summary>
        /// <param name="campaignToUpdate">Campaign to update</param>
        /// <returns>True or False</returns>
        bool UpdateCampaignModel(CampaignModel campaignToUpdate);

        /// <summary>
        ///     Delete Campaign Model
        /// </summary>
        /// <param name="campaign_ID">Campaign ID</param>
        /// <returns>True or False</returns>
        bool DeleteCampaignModel(int campaign_ID);
    }
}
