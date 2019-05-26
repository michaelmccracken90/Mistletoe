// <copyright file="IUnitOfWork.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.DAL.Interfaces
{
    using System;
    using Mistletoe.Entities;

    /// <summary>
    ///     Interface for Unit of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Gets or sets Campaign Repository
        /// </summary>
        IRepository<Campaign> CampaignRepository { get; set; }

        /// <summary>
        ///     Gets or sets Template Repository
        /// </summary>
        IRepository<Template> TemplateRepository { get; set; }

        /// <summary>
        ///     Gets or sets User Repository
        /// </summary>
        IRepository<User> UserRepository { get; set; }

        /// <summary>
        ///     Gets or sets Email Address Repository
        /// </summary>
        IRepository<Email_Address> EmailAddressRepository { get; set; }

        /// <summary>
        ///     Gets or sets Template Email Addresses Repository
        /// </summary>
        IRepository<Template_Email_Addresses> TemplateEmailAddressesRepository { get; set; }

        /// <summary>
        ///     Save changes
        /// </summary>
        void Save();
    }
}
