// <copyright file="Program.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Exporter
{
    using System;
    using System.Collections;
    using CommandLine;
    using NLog;

    /// <summary>
    ///     Mistletoe Exporter Entry
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Mistletoe Exporter Main
        /// </summary>
        /// <param name="args">Mistletoe Exporter Arguments</param>
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            Helper.LoggerInstance = LogManager.GetCurrentClassLogger();
            Helper.LoggerInstance.Log(LogLevel.Debug, "Application Start");

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed((errs) => HandleParseError(errs));
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Helper.LoggerInstance.Log(LogLevel.Error, e.ExceptionObject);
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            Helper.LoggerInstance.Log(LogLevel.Debug, "RunOptionsAndReturnExitCode");

            WinSCPHelper.Upload(options);
        }

        private static void HandleParseError(IEnumerable errors)
        {
            Helper.LoggerInstance.Log(LogLevel.Debug, "HandleParseError");
        }
    }
}
