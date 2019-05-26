// <copyright file="Helper.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL
{
    using Mistletoe.DAL;
    using Mistletoe.DAL.Interfaces;
    using NLog;

    /// <summary>
    ///     Helper class
    /// </summary>
    public class Helper
    {
        /// <summary>
        ///     Gets or sets logger instance
        /// </summary>
        public static Logger GlobalLogger { get; set; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Gets or sets unit of work instance
        /// </summary>
        public static IUnitOfWork UnitOfWorkInstance { get; set; } = new UnitOfWork();
    }
}
