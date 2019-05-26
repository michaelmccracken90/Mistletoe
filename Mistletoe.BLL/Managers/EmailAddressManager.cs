// <copyright file="EmailAddressManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Mistletoe.BLL.Interfaces;
    using Mistletoe.Entities;
    using Mistletoe.Models;
    using NLog;

    /// <summary>
    ///     Email Address Manager
    /// </summary>
    public class EmailAddressManager : IEmailAddressManager, IDisposable
    {
        private IEnumerable<Email_Address> emailAddressList;

        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressManager"/> class.
        /// </summary>
        /// <param name="emailAddressModelListValue">List of email addresses</param>
        public EmailAddressManager(IEnumerable<EmailAddressModel> emailAddressModelListValue = null)
        {
            if (emailAddressModelListValue != null)
            {
                this.UpdateEmailAddressList(Mapper.Map<IEnumerable<Email_Address>>(emailAddressModelListValue));
            }
            else
            {
                this.UpdateEmailAddressList();
            }
        }

        /// <inheritdoc/>
        public int AddEmail(string emailAddress)
        {
            int email_ID = -1;

            try
            {
                // Enter new Email
                Helper.UnitOfWorkInstance.EmailAddressRepository.Insert(new Email_Address { Email = emailAddress });
                Helper.UnitOfWorkInstance.Save();
                this.UpdateEmailAddressList();
                email_ID = this.emailAddressList.FirstOrDefault(x => x.Email == emailAddress).EmailID;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "EmailAddressManager:AddEmail. Exception: " + Environment.NewLine + ex.Message);
            }

            return email_ID;
        }

        /// <inheritdoc/>
        public bool EmailExists(string emailAddress)
        {
            bool result = false;

            try
            {
                Email_Address tempEmail = this.emailAddressList.Where(e => e.Email == emailAddress).FirstOrDefault();
                if (tempEmail != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "EmailAddressManager:EmailExists. Exception: " + Environment.NewLine + ex.Message);
            }

            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<EmailAddressModel> GetAllItems()
        {
            return Mapper.Map<List<EmailAddressModel>>(this.emailAddressList);
        }

        /// <inheritdoc/>
        public int GetEmailId(string emailAddress)
        {
            int emailId = -1;

            var emailAddressValue = this.emailAddressList.FirstOrDefault(x => x.Email == emailAddress);
            if (emailAddressValue != null)
            {
                emailId = emailAddressValue.EmailID;
            }

            return emailId;
        }

        /// <summary>
        ///     Dispose managed objects
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        ///     Dispose managed and unmanaged objects
        /// </summary>
        /// <param name="disposing">Flag for disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.emailAddressList = null;
                }

                this.disposedValue = true;
            }
        }

        private void UpdateEmailAddressList(IEnumerable<Email_Address> emailAddressListValue = null)
        {
            if (emailAddressListValue != null)
            {
                this.emailAddressList = emailAddressListValue;
            }
            else
            {
                this.emailAddressList = Helper.UnitOfWorkInstance.EmailAddressRepository.Get();
            }
        }
    }
}
