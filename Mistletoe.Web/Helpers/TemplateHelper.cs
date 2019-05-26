namespace Mistletoe.Web.Helpers
{
    using Mistletoe.Entities;
    using Mistletoe.BLL.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;

    public class TemplateHelper
    {
        //internal static Campaign ConvertToDBEntity(CampaignModel CampaignToConvert)
        //{
        //    Campaign tempModel = new Campaign();
        //    tempModel.Campaign_ID = Convert.ToInt32(CampaignToConvert.ID);
        //    tempModel.User_ID = Convert.ToInt32(CampaignToConvert.UserID);
        //    tempModel.Campaign_Name = CampaignToConvert.Name;
        //    tempModel.Start_Date = Convert.ToDateTime(CampaignToConvert.StartDate);
        //    tempModel.End_Date = Convert.ToDateTime(CampaignToConvert.EndDate);
        //    tempModel.Frequency = CampaignToConvert.Frequency;
        //    tempModel.Data_File_Path = CampaignToConvert.DataFilePath;
        //    tempModel.Is_Active = CampaignToConvert.IsActive;
        //    tempModel.Is_Archieved = CampaignToConvert.IsArchieved;

        //    return tempModel;
        //}

        internal static TemplateModel ConvertToModel(Template TemplateToConvert, List<string> ReceiverEmails, string SenderEmail)
        {
            TemplateModel tempModel = new TemplateModel();
            tempModel.TemplateID = TemplateToConvert.TemplateID.ToString();
            tempModel.CampaignID = TemplateToConvert.CampaignID.ToString();
            tempModel.TemplateName = TemplateToConvert.TemplateName;
            tempModel.Subject = TemplateToConvert.Subject;
            tempModel.Body = TemplateToConvert.Body;

            // Attach sender
            tempModel.Sender = SenderEmail;

            // Attach receiver(s)
            ReceiverEmails.ForEach(r =>
            {
                tempModel.ReceiverList.Add(r);
            });

            return tempModel;
        }

        internal static bool CheckEmailAddresses(string Sender, string Receivers, ref List<string> VerifiedReceivers)
        {
            bool emailAddressesAreOkay = true;
            VerifiedReceivers = new List<string>();
            MailAddress mailAddress;

            try
            {
                // Verify sender
                mailAddress = new MailAddress(Sender);
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
                    List<string> tempList = Receivers.Split(';').ToList();
                    foreach (string rc in tempList)
                    {
                        string tempReceiver = rc.Trim();
                        if (tempReceiver != string.Empty)
                        {
                            mailAddress = new MailAddress(tempReceiver);
                            VerifiedReceivers.Add(tempReceiver);
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