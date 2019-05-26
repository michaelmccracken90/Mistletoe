namespace Mistletoe.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ITemplateService : IDisposable
    {
        IEnumerable<Template> GetTemplates(int CampaignID);
        Template GetTemplate(int campaignId);
        Template GetTemplate(int CampaignID, ref List<string> ReceiverEmails, out string SenderEmail);
        int CreateTemplate(Template TemplateToCreate);
        bool UpdateTemplate(Template TemplateToUpdate);
        int EmailExists(string EmailAddress);
        int AddEmail(Email_Address Email);
        bool UpdateTemplateAndEmailAddressReferences(int TemplateID, int SenderEmailID, List<int> ReceiverEmailIDs);
    }
}
