// <copyright file="WinSCPHelper.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Exporter
{
    using System;
    using WinSCP;

    /// <summary>
    ///     WinSCP Helper Class
    /// </summary>
    internal class WinSCPHelper
    {
        /// <summary>
        ///     Upload file to server
        /// </summary>
        /// <param name="options">FTP options</param>
        internal static void Upload(Options options)
        {
            var sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = options.Server,
                PortNumber = options.Port,
                UserName = options.User,
                Password = options.Password,
                TlsHostCertificateFingerprint = options.Fingerprint,
                FtpSecure = FtpSecure.Explicit
            };

            using (var session = new Session())
            {
                session.Open(sessionOptions);

                var transferOptions = new TransferOptions { TransferMode = TransferMode.Binary };

                var transferResult = session.PutFiles(options.File, "./", false, transferOptions);

                transferResult.Check();

                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                }
            }
        }
    }
}