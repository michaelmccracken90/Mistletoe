// <copyright file="Mailer.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Email
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Mistletoe.Email.Properties;
    using Newtonsoft.Json.Linq;
    using NLog;

    /// <summary>
    ///     Mailer class
    /// </summary>
    public class Mailer
    {
        private static Logger loggerInstance = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Send Email using Mailjet
        /// </summary>
        /// <param name="mailMessage">Mail message</param>
        /// <returns>Mail response</returns>
        public static async Task<MailerResponse> SendEmailAsync(MailMessage mailMessage)
        {
            var client = new MailjetClient(Config.MailjetAPIKey, Config.MailjetSecretKey);
            var recipientValue = new JArray
                {
                    new JObject
                    {
                        {
                            "Email", mailMessage.To[0].Address
                        }
                    }
                };
            var request = new MailjetRequest
                {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "support@mossandlichens.com")
               .Property(Send.FromName, "Support")
               .Property(Send.Subject, mailMessage.Subject)
               .Property(Send.TextPart, mailMessage.Body)
               .Property(Send.HtmlPart, mailMessage.Body)
               .Property(Send.Recipients, recipientValue);

            MailjetResponse response = await client.PostAsync(request);

            var mailerResponse = new MailerResponse();
            if (response.IsSuccessStatusCode)
            {
                mailerResponse.Response = string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount());
                mailerResponse.Response += Environment.NewLine;
                mailerResponse.Response += response.GetData();
            }
            else
            {
                mailerResponse.Response = string.Format("StatusCode: {0}\n", response.StatusCode);
                mailerResponse.Response += Environment.NewLine;
                mailerResponse.Response += string.Format("ErrorInfo: {0}\n", response.GetErrorInfo());
                mailerResponse.Response += Environment.NewLine;
                mailerResponse.Response += string.Format("ErrorMessage: {0}\n", response.GetErrorMessage());
            }

            return mailerResponse;
        }
    }
}
