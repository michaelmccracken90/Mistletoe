// <copyright file="JobPackager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Helpers
{
    using System.Threading.Tasks;
    using Mistletoe.Common;
    using Mistletoe.Email;

    /// <summary>
    ///     Job packager for sending messages
    /// </summary>
    public class JobPackager
    {
        /// <summary>
        ///     Send messages
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>void</returns>
        public static async Task SendMessages(int campaignId)
        {
            string campaignDataFilePath = MistletoeAPIHelper.GetDataFilePath(campaignId);

            if (string.IsNullOrEmpty(campaignDataFilePath) == false)
            {
                var campaignData = ExcelHelper.GetCampaignData(campaignDataFilePath);

                // Create mail messages
                var mailMessages = EmailHelper.GetMailMessages(
                                                               campaignId,
                                                               campaignData,
                                                               MistletoeAPIHelper.GetTemplateModel(campaignId),
                                                               MistletoeAPIHelper.GetAllTemplateEmailAddressesItems(),
                                                               MistletoeAPIHelper.GetAllEmailAddressItems(),
                                                               MvcApplication.GlobalEmailFooterFilePath);

                // Send mail
                foreach (var mailMessage in mailMessages)
                {
                    await Mailer.SendEmailAsync(mailMessage);
                }
            }
        }
    }
}