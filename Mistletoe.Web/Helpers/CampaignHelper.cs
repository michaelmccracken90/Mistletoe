namespace Mistletoe.Web.Helpers
{
    using Mistletoe.Entities;
    using Mistletoe.BLL.Models;
    using System;
    
    internal class CampaignHelper
    {
        internal static bool CheckUserAccess(int CampaignID)
        {
            bool userHasAccess = false;

            CampaignModel tempCampaign = MvcApplication.CampaignManager.GetCampaignModel(CampaignID);
            if (tempCampaign != null)
            {
                // Check if campaign is associated to logged-in user
                if (tempCampaign.UserID == MvcApplication.UserID)
                    userHasAccess = true;
                else
                    userHasAccess = false;
            }

            return userHasAccess;
        }

        internal static Campaign ConvertToDBEntity(CampaignModel CampaignToConvert)
        {
            Campaign tempModel = new Campaign();
            tempModel.CampaignID = Convert.ToInt32(CampaignToConvert.CampaignID);
            tempModel.UserID = CampaignToConvert.UserID;
            tempModel.CampaignName = CampaignToConvert.CampaignName;
            tempModel.StartDate = Convert.ToDateTime(CampaignToConvert.StartDate);
            tempModel.EndDate = Convert.ToDateTime(CampaignToConvert.EndDate);
            tempModel.Frequency = CampaignToConvert.Frequency;
            tempModel.DataFilePath = CampaignToConvert.DataFilePath;
            tempModel.IsActive = CampaignToConvert.IsActive;
            tempModel.IsArchived = CampaignToConvert.IsArchived;

            return tempModel;
        }

        /*
        internal static CampaignModel ConvertToModel(Campaign CampaignToConvert)
        {
            CampaignModel tempModel = new CampaignModel();
            tempModel.CampaignID = CampaignToConvert.CampaignID.ToString();
            tempModel.UserID = CampaignToConvert.UserID.ToString();
            tempModel.CampaignName = CampaignToConvert.CampaignName;
            tempModel.StartDate = CampaignToConvert.StartDate.ToString("dd.MM.yyyy");
            tempModel.EndDate = CampaignToConvert.EndDate.ToString("dd.MM.yyyy");
            tempModel.Frequency = CampaignToConvert.Frequency;
            tempModel.DataFilePath = CampaignToConvert.DataFilePath;
            tempModel.IsActive = CampaignToConvert.IsActive;
            tempModel.IsArchived = CampaignToConvert.IsArchived;

            return tempModel;
        }
        */
    }
}