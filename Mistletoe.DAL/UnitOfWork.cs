// <copyright file="UnitOfWork.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.DAL
{
    using System;
    using Mistletoe.DAL.Interfaces;
    using Mistletoe.Entities;

    /// <summary>
    ///     Unit of Work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private MistletoeDataEntities context;

        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            if (this.context == null)
            {
                this.context = new MistletoeDataEntities();
            }

            if (this.CampaignRepository == null)
            {
                this.CampaignRepository = new GenericRepository<Campaign>(this.context);
            }

            if (this.TemplateRepository == null)
            {
                this.TemplateRepository = new GenericRepository<Template>(this.context);
            }

            if (this.UserRepository == null)
            {
                this.UserRepository = new GenericRepository<User>(this.context);
            }

            if (this.EmailAddressRepository == null)
            {
                this.EmailAddressRepository = new GenericRepository<Email_Address>(this.context);
            }

            if (this.TemplateEmailAddressesRepository == null)
            {
                this.TemplateEmailAddressesRepository = new GenericRepository<Template_Email_Addresses>(this.context);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="mistletoeDataEntities">Database entities</param>
        /// <param name="campaignRepository">Campaign repository</param>
        /// <param name="templateRepository">Template repository</param>
        /// <param name="userRepository">User repository</param>
        /// <param name="emailAddressRepository">Email Address repository</param>
        /// <param name="templateEmailAddressesRepository">Template Email Addresses repository</param>
        public UnitOfWork(
                        MistletoeDataEntities mistletoeDataEntities = null,
                        IRepository<Campaign> campaignRepository = null,
                        IRepository<Template> templateRepository = null,
                        IRepository<User> userRepository = null,
                        IRepository<Email_Address> emailAddressRepository = null,
                        IRepository<Template_Email_Addresses> templateEmailAddressesRepository = null)
        {
            if (mistletoeDataEntities == null)
            {
                this.context = new MistletoeDataEntities();
            }
            else
            {
                this.context = mistletoeDataEntities;
            }

            if (campaignRepository == null)
            {
                this.CampaignRepository = new GenericRepository<Campaign>(this.context);
            }
            else
            {
                this.CampaignRepository = campaignRepository;
            }

            if (templateRepository == null)
            {
                this.TemplateRepository = new GenericRepository<Template>(this.context);
            }
            else
            {
                this.TemplateRepository = templateRepository;
            }

            if (userRepository == null)
            {
                this.UserRepository = new GenericRepository<User>(this.context);
            }
            else
            {
                this.UserRepository = userRepository;
            }

            if (emailAddressRepository == null)
            {
                this.EmailAddressRepository = new GenericRepository<Email_Address>(this.context);
            }
            else
            {
                this.EmailAddressRepository = emailAddressRepository;
            }

            if (templateEmailAddressesRepository == null)
            {
                this.TemplateEmailAddressesRepository = new GenericRepository<Template_Email_Addresses>(this.context);
            }
            else
            {
                this.TemplateEmailAddressesRepository = templateEmailAddressesRepository;
            }
        }

        /// <inheritdoc/>
        public IRepository<Campaign> CampaignRepository { get; set; }

        /// <inheritdoc/>
        public IRepository<Template> TemplateRepository { get; set; }

        /// <inheritdoc/>
        public IRepository<User> UserRepository { get; set; }

        /// <inheritdoc/>
        public IRepository<Email_Address> EmailAddressRepository { get; set; }

        /// <inheritdoc/>
        public IRepository<Template_Email_Addresses> TemplateEmailAddressesRepository { get; set; }

        /// <inheritdoc/>
        public void Save()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        ///     Dispose managed objects
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose managed objects
        /// </summary>
        /// <param name="disposing">Check if disposal inprogress</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
