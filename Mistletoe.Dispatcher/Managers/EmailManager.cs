namespace Mistletoe.Dispatcher.Managers
{
    using Mistletoe.DAL.Managers;
    using Mistletoe.Email;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net.Mail;
    using Mistletoe.DAL;

    internal class EmailManager
    {
        internal static IEnumerable<MailMessage> GetCampaignMailMessages(int campaignId)
        {
            var mailMessages = new List<MailMessage>();

            var templates = TemplateManager.GetTemplates(campaignId);

            foreach(var template in templates)
            {
                var emails = GetTemplateMailMessages(template.Template_ID);
                mailMessages.AddRange(emails);
            }

            return mailMessages;
        }

        private static IEnumerable<MailMessage> GetTemplateMailMessages(int template_ID)
        {
            var mailMessages = new List<MailMessage>();
            using (var context = new MistletoeDataEntities())
            {
                var mailMessage = new MailMessage();
                var template = context.Template.Where(x => x.Template_ID == template_ID).FirstOrDefault();
                mailMessage.Subject = template.Subject;
                mailMessage.Body = template.Body;

                var emailRefs = context.Template_CrossRef_Email_Address.Where(x => x.Template_ID == template_ID).ToList();
                foreach(var emailRef in emailRefs)
                {
                    var email = context.Email_Address.Where(x => x.Email_ID == emailRef.Email_ID).FirstOrDefault();
                    if(emailRef.Is_Sender)
                    {
                        mailMessage.From = new MailAddress(email.Email);
                    }
                    else
                    {
                        mailMessage.To.Add(new MailAddress(email.Email));
                    }
                }
            }
            return mailMessages;
        }

        internal static void SendMailMessage(MailMessage mailMessage)
        {
            // Mailer.RunAsync(mailMessage).Wait();
        }
    }
}