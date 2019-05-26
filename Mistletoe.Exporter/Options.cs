// <copyright file="Options.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Exporter
{
    using CommandLine;

    /// <summary>
    ///     Options for Exporter
    /// </summary>
    public class Options
    {
        /// <summary>
        ///     Gets or sets server option
        /// </summary>
        [Option('s', "server", Required = true, HelpText = "Server")]
        public string Server { get; set; }

        /// <summary>
        ///     Gets or sets port option
        /// </summary>
        [Option('o', "port", Required = true, HelpText = "Port")]
        public int Port { get; set; }

        /// <summary>
        ///     Gets or sets fingerprint option
        /// </summary>
        [Option('f', "fingerprint", Required = true, HelpText = "Fingerprint")]
        public string Fingerprint { get; set; }

        /// <summary>
        ///     Gets or sets User
        /// </summary>
        [Option('u', "user", Required = true, HelpText = "User")]
        public string User { get; set; }

        /// <summary>
        ///     Gets or sets Password
        /// </summary>
        [Option('p', "password", Required = true, HelpText = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets File
        /// </summary>
        [Option('i', "input file", Required = true, HelpText = "Input File")]
        public string File { get; set; }

        /// <summary>
        ///     Gets or sets Key
        /// </summary>
        [Option('k', "key", Required = false, HelpText = "Key")]
        public string Key { get; set; }
    }
}
