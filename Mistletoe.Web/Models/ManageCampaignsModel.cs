using System.Collections.Generic;

namespace Mistletoe.Web.Models
{
    public class ManageCampaignsModel
    {
        public List<CampaignModel> CampaignList;
        public string CampaignIDs;

        public ManageCampaignsModel()
        {
            CampaignList = new List<CampaignModel>();
            CampaignIDs = string.Empty;
        }
    }
}