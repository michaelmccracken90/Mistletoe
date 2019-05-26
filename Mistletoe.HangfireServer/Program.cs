// <copyright file="Program.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.HangfireServer
{
    using System;
    using System.Configuration;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Hangfire;
    using Hangfire.SqlServer;
    using Mistletoe.Email;
    using NLog;

    /// <summary>
    ///     Hangfire Server Class
    /// </summary>
    public class Program
    {
        private static Logger loggerInstance;

        /// <summary>
        ///     Hangfire Server Main
        /// </summary>
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            loggerInstance = LogManager.GetCurrentClassLogger();
            loggerInstance.Log(LogLevel.Debug, "Mistletoe.HangfireServer: Application Start");

            JobStorage.Current = new SqlServerStorage(ConfigurationManager.ConnectionStrings["MistletoeData"].ConnectionString);

            using (new BackgroundJobServer())
            {
                Console.WriteLine("Mistletoe Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }

            loggerInstance.Log(LogLevel.Debug, "Mistletoe.HangfireServer: Application Close");
        }

        /// <summary>
        ///     Hangfire Server Unhandled Exception Handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Exception details</param>
        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            loggerInstance.Log(LogLevel.Error, exception);

            Task.Run(async () =>
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("support@mossandlichens.com");
                mailMessage.To.Add(new MailAddress("support@mossandlichens.com"));
                mailMessage.Subject = "Mistletoe:Mistletoe.HangfireServer:UnhandledExceptionTrapper:" + exception.Message;
                mailMessage.Body = exception.ToString();

                var sendEmailAsyncResult = await Mailer.SendEmailAsync(mailMessage);
            });

            Console.WriteLine("Mistletoe Hangfire Server stopped with an error:" + exception.Message);
            Console.ReadLine();
        }
    }
}
