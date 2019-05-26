// <copyright file="CampaignManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Mistletoe.BLL.Interfaces;
    using Mistletoe.Entities;
    using Mistletoe.Models;
    using NLog;

    /// <summary>
    ///     Campaign Manager
    /// </summary>
    public class CampaignManager : ICampaignManager, IDisposable
    {
        private IEnumerable<Campaign> campaignList;
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        ///     Initializes a new instance of the <see cref="CampaignManager"/> class.
        /// </summary>
        /// <param name="campaignModelListValue">List of campaign models</param>
        public CampaignManager(IEnumerable<CampaignModel> campaignModelListValue = null)
        {
            if (campaignModelListValue != null)
            {
                this.UpdateCampaignsList(Mapper.Map<IEnumerable<Campaign>>(campaignModelListValue));
            }
            else
            {
                this.UpdateCampaignsList();
            }
        }

        /// <inheritdoc/>
        public List<CampaignModel> GetAllCampaignModelsByUser(string userID)
        {
            List<CampaignModel> campaignModels = null;

            try
            {
                var campaigns = this.campaignList.Where(c => c.User.UserID == userID && c.IsArchived == false).ToList();
                campaignModels = Mapper.Map<List<CampaignModel>>(campaigns);
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:GetAllCampaignModelsByUser. Exception: " + Environment.NewLine + ex.Message);
            }

            return campaignModels;
        }

        /// <inheritdoc/>
        public IEnumerable<CampaignModel> GetActiveCampaignModels()
        {
            List<CampaignModel> campaignModels = null;

            try
            {
                var campaigns = this.campaignList.Where(c => c.User.IsActive == true && c.EndDate <= DateTime.UtcNow).ToList();
                campaignModels = Mapper.Map<List<CampaignModel>>(campaigns);
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:GetActiveCampaignModels. Exception: " + Environment.NewLine + ex.Message);
            }

            return campaignModels;
        }

        /// <inheritdoc/>
        public CampaignModel GetCampaignModel(int campaignId)
        {
            CampaignModel campaignModel = null;

            try
            {
                var campaign = this.campaignList.Where(c => c.CampaignID == campaignId).FirstOrDefault();
                campaignModel = Mapper.Map<CampaignModel>(campaign);
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:GetCampaignModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return campaignModel;
        }

        /// <inheritdoc/>
        public int CreateCampaignModel(CampaignModel campaignToCreateValue)
        {
            int campaignId = -1;

            try
            {
                var campaignToCreate = Mapper.Map<Campaign>(campaignToCreateValue);
                Helper.UnitOfWorkInstance.CampaignRepository.Insert(campaignToCreate);
                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                campaignId = campaignToCreate.CampaignID;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:CreateCampaignModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return campaignId;
        }

        /// <inheritdoc/>
        public bool UpdateCampaignModel(CampaignModel campaignToUpdateValue)
        {
            bool success = false;

            try
            {
                var campaignToUpdate = Mapper.Map<Campaign>(campaignToUpdateValue);
                Campaign dbCampaign = this.campaignList.Where(c => c.CampaignID == campaignToUpdate.CampaignID).FirstOrDefault();
                dbCampaign.CampaignName = campaignToUpdate.CampaignName;
                dbCampaign.StartDate = campaignToUpdate.StartDate;
                dbCampaign.EndDate = campaignToUpdate.EndDate;
                dbCampaign.Frequency = campaignToUpdate.Frequency;
                dbCampaign.DataFilePath = campaignToUpdate.DataFilePath;

                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:UpdateCampaignModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool UpdateCampaignFilePath(int campaignID, string newFilePath)
        {
            bool success = false;

            try
            {
                Campaign dbCampaign = this.campaignList.Where(c => c.CampaignID == campaignID).FirstOrDefault();
                dbCampaign.DataFilePath = newFilePath;

                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:UpdateCampaignFilePath. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool ChangeCampaignStatus(int campaignID, bool newStatus)
        {
            bool success = false;

            try
            {
                Campaign dbCampaign = this.campaignList.Where(c => c.CampaignID == campaignID).FirstOrDefault();
                dbCampaign.IsActive = newStatus;

                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:ChangeCampaignStatus. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool ChangeCampaignStatusByUser(string userID, bool newStatus)
        {
            bool success = false;

            try
            {
                List<Campaign> userCampaigns = this.campaignList.Where(c => c.User.UserID == userID).ToList();
                userCampaigns.ForEach(c =>
                {
                    c.IsActive = newStatus;
                });

                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:ChangeCampaignStatusByUser. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool ArchiveCampaign(int campaignID)
        {
            bool success = false;

            try
            {
                Campaign dbCampaign = this.campaignList.Where(c => c.CampaignID == campaignID).FirstOrDefault();
                dbCampaign.IsArchived = true;

                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:ArchiveCampaign. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public string GetDataFilePath(int campaignId)
        {
            var dataFilePath = string.Empty;
            try
            {
                dataFilePath = this.campaignList.Where(c => c.CampaignID == campaignId).FirstOrDefault().DataFilePath;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:GetDataFilePath. Exception: " + Environment.NewLine + ex.Message);
            }

            return dataFilePath;
        }

        /// <inheritdoc/>
        public string GetCampaignFrequency(int campaignId)
        {
            string frequency = string.Empty;
            try
            {
                frequency = this.campaignList.Where(c => c.CampaignID == campaignId).FirstOrDefault().Frequency;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:GetCampaignFrequency. Exception: " + Environment.NewLine + ex.Message);
            }

            return frequency;
        }

        /// <inheritdoc/>
        public bool CheckUserAccess(int campaignID, string userId)
        {
            bool userHasAccess = false;

            var tempCampaign = this.GetCampaignModel(campaignID);
            if (tempCampaign != null)
            {
                // Check if campaign is associated to logged-in user
                if (tempCampaign.UserID == userId)
                {
                    userHasAccess = true;
                }
                else
                {
                    userHasAccess = false;
                }
            }

            return userHasAccess;
        }

        /// <inheritdoc/>
        public bool DeleteCampaignModel(int campaign_ID)
        {
            bool success = false;

            try
            {
                Campaign campaignModel = Helper.UnitOfWorkInstance.CampaignRepository.GetByID(campaign_ID);
                Helper.UnitOfWorkInstance.CampaignRepository.Delete(campaignModel);
                Helper.UnitOfWorkInstance.Save();
                this.UpdateCampaignsList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "CampaignManager:DeleteCampaignModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <summary>
        ///     This code added to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose implementation
        /// </summary>
        /// <param name="disposing">True or False</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.campaignList = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this.disposedValue = true;
            }
        }

        /// <summary>
        ///     Update campaigns
        /// </summary>
        /// <param name="campaignListValue">List of campaigns</param>
        private void UpdateCampaignsList(IEnumerable<Campaign> campaignListValue = null)
        {
            if (campaignListValue != null)
            {
                this.campaignList = campaignListValue;
            }
            else
            {
                this.campaignList = Helper.UnitOfWorkInstance.CampaignRepository.Get();
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CampaignManager() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }
    }
}