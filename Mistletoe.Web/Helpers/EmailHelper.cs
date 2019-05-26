namespace Mistletoe.Web.Helpers
{
    using Mistletoe.BLL.Managers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;
    using System.Net.Mail;
    using Mistletoe.Entities;

    internal class EmailHelper
    {
        internal static IList<MailMessage> GetMailMessages(int campaignId, IList<ExcelData> campaignList)
        {
            var mailMessages = new List<MailMessage>();

            var template = MvcApplication.TemplateManager.GetTemplateModel(campaignId);
            
            using (var context = new MistletoeDataEntities())
            {
                for (int i = 0; i < campaignList.Count; i++)
                {
                    var campaignData = campaignList[i];

                    var mailMessage = new MailMessage();

                    mailMessage.Subject = template.Subject;
                    mailMessage.Body = template.Body;

                    var emailRefs = context.Template_CrossRef_Email_Address.Where(x => x.TemplateID.ToString() == template.TemplateID).ToList();
                    foreach (var emailRef in emailRefs)
                    {
                        var email = context.Email_Address.Where(x => x.EmailID == emailRef.EmailID).FirstOrDefault();
                        if (emailRef.IsSender)
                        {
                            mailMessage.From = new MailAddress(email.Email);
                        }
                    }

                    mailMessage.To.Add(new MailAddress(campaignData.Receiver));

                    mailMessages.Add(mailMessage);
                }
            }

            return mailMessages;
        }
    }
}