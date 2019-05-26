namespace Mistletoe.Dispatcher.Managers
{
    using Mistletoe.DAL;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    internal class CampaignManager
    {
        internal static IEnumerable<Campaign> GetActiveCampaigns()
        {
            var campaigns = new List<Campaign>();
            using (var context = new MistletoeDataEntities())
            {
                campaigns = context.Campaign.Where(c => c.User.Is_Active == true && c.End_Date <= DateTime.UtcNow).ToList();
            }
            return campaigns;
        }
    }
}