// <copyright file="EmailHelper.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using Mistletoe.Models;

    /// <summary>
    ///     Email Helper
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        ///     Get Email Messages based on campaign data
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <param name="campaignList">List of Campaigns</param>
        /// <param name="template">Template data</param>
        /// <param name="templateEmailAddressesList">Template Email Addresses</param>
        /// <param name="emailAddressList">Email Addresses</param>
        /// <param name="footerPath">Path to Footer</param>
        /// <returns>List of filled mail messages</returns>
        public static IList<MailMessage> GetMailMessages(
                                                         int campaignId,
                                                         IList<ExcelData> campaignList,
                                                         TemplateModel template,
                                                         IEnumerable<TemplateEmailAddressesModel> templateEmailAddressesList,
                                                         IEnumerable<EmailAddressModel> emailAddressList,
                                                         string footerPath)
        {
            var mailMessages = new List<MailMessage>();

            for (int i = 0; i < campaignList.Count; i++)
            {
                var campaignData = campaignList[i];

                var mailMessage = new MailMessage();

                mailMessage.Subject = template.Subject;
                mailMessage.Body = template.Body + Environment.NewLine + FileHelper.ReadFile(footerPath);
                mailMessage.IsBodyHtml = true;

                var emailRefs = templateEmailAddressesList.Where(x => x.TemplateID.ToString() == template.TemplateID).ToList();
                foreach (var emailRef in emailRefs)
                {
                    var email = emailAddressList.Where(x => x.EmailID == emailRef.EmailID).FirstOrDefault();
                    if (emailRef.IsSender)
                    {
                        mailMessage.From = new MailAddress(email.Email);
                    }
                }

                mailMessage.To.Add(new MailAddress(campaignData.Receiver));

                mailMessages.Add(mailMessage);
            }

            return mailMessages;
        }

        /// <summary>
        ///     Check Email Addresses
        /// </summary>
        /// <param name="sender">Sender of Email</param>
        /// <param name="receivers">List of Receivers</param>
        /// <param name="verifiedReceivers">List of verified receivers</param>
        /// <returns>True or False</returns>
        public static bool CheckEmailAddresses(string sender, string receivers, ref List<string> verifiedReceivers)
        {
            bool emailAddressesAreOkay = true;
            verifiedReceivers = new List<string>();
            MailAddress mailAddress;

            try
            {
                // Verify sender
                mailAddress = new MailAddress(sender);
            }
            catch (FormatException)
            {
                emailAddressesAreOkay = false;
            }

            if (emailAddressesAreOkay)
            {
                try
                {
                    // Verify receivers
                    List<string> tempList = receivers.Split(';').ToList();
                    foreach (string rc in tempList)
                    {
                        string tempReceiver = rc.Trim();
                        if (tempReceiver != string.Empty)
                        {
                            mailAddress = new MailAddress(tempReceiver);
                            verifiedReceivers.Add(tempReceiver);
                        }
                    }
                }
                catch (FormatException)
                {
                    emailAddressesAreOkay = false;
                }
            }

            return emailAddressesAreOkay;
        }
    }
}
