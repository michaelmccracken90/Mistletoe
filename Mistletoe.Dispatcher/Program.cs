namespace Mistletoe.Dispatcher
{
    using Hangfire;
    using Hangfire.SqlServer;
    using Mistletoe.DAL.Managers;
    using Mistletoe.Dispatcher.Managers;
    using Mistletoe.Email;
    using NLog;
    using System;
    using System.Net.Mail;

    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            Helper.LoggerInstance = LogManager.GetCurrentClassLogger();
            Helper.LoggerInstance.Log(LogLevel.Debug, "Application Start");

            ScheduleCampaign(5);

            // Get active campaigns
            /*
            var activeCampaigns = CampaignManager.GetActiveCampaigns();
            foreach(var activeCampaign in activeCampaigns)
            {
                // Get emails
                var mailMessages = EmailManager.GetCampaignMailMessages(activeCampaign.Campaign_ID);

                foreach (var mailMessage in mailMessages)
                {
                    // Send emails
                    EmailManager.SendMailMessage(mailMessage);                        
                }
            }
            */
            
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Helper.LoggerInstance.Log(LogLevel.Error, e.ExceptionObject);
        }

        public static void ScheduleCampaign(int campaignId)
        {
            var frequency = CampaignManager.GetCampaignFrequency(campaignId);

            var testEmail = new MailMessage
            {
                From = new MailAddress("support@mossandlichens.com", "Support"),
                Subject = "Hello Mistletoe",
                Body = "Let us change the future!"
            };

            testEmail.To.Add(new MailAddress("ranjith.venkatesh@mossandlichens.com", "Ranjith Venkatesh"));

            JobStorage.Current = new SqlServerStorage("Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=Mistletoe.Data;User Id=MistletoeAdmin;Password=Mistletoe80993;MultipleActiveResultSets=True");

            RecurringJob.AddOrUpdate("Campaign:" + campaignId, () => Mailer.RunAsync("support@mossandlichens.com", "ranjith.venkatesh@mossandlichens.com", "Hello Mistletoe", "Let us change the future!"), frequency);
        }

    }
}
