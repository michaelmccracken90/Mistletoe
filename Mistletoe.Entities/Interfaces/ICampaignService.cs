namespace Mistletoe.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ICampaignService : IDisposable
    {
        List<Campaign> GetAllCampaignsByUser(string UserID);
        IEnumerable<Campaign> GetActiveCampaigns();
        Campaign GetCampaign(int campaignId);
        bool CreateCampaign(Campaign CampaignToCreate, out int Campaign_ID);
        bool UpdateCampaign(Campaign CampaignToUpdate);
        bool UpdateCampaignFilePath(int CampaignID, string NewFilePath);
        bool ChangeCampaignStatus(int CampaignID, bool newStatus);
        bool ChangeCampaignStatusByUser(string UserID, bool newStatus);
        bool ArchiveCampaign(int CampaignID);
        string GetDataFilePath(int campaignId);
        string GetCampaignFrequency(int campaignId);
    }
}
