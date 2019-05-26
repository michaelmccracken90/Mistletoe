// <copyright file="TemplateEmailAddressesManager.cs" company="Moss and Lichens">
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
    ///     Template Email Addresses Manager
    /// </summary>
    public class TemplateEmailAddressesManager : ITemplateEmailAddressesManager, IDisposable
    {
        private IEnumerable<Template_Email_Addresses> templateEmailAddressesList;

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateEmailAddressesManager"/> class.
        /// </summary>
        /// <param name="templateEmailAddressesModelListValue">List of template email addresses mapping</param>
        public TemplateEmailAddressesManager(IEnumerable<TemplateEmailAddressesModel> templateEmailAddressesModelListValue = null)
        {
            if (templateEmailAddressesModelListValue != null)
            {
                this.UpdateTemplateEmailAddressesList(Mapper.Map<IEnumerable<Template_Email_Addresses>>(templateEmailAddressesModelListValue));
            }
            else
            {
                this.UpdateTemplateEmailAddressesList();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TemplateEmailAddressesModel> GetAllItems()
        {
            return Mapper.Map<IEnumerable<TemplateEmailAddressesModel>>(this.templateEmailAddressesList);
        }

        /// <inheritdoc/>
        public bool UpdateTemplateAndEmailAddressReferences(int templateID, int senderEmailID, string receiverEmailIDs)
        {
            bool success = false;

            try
            {
                // Delete the existing references
                var tempList = this.templateEmailAddressesList.Where(t => t.TemplateID == templateID).ToList();
                tempList.ForEach(cr =>
                {
                    Helper.UnitOfWorkInstance.TemplateEmailAddressesRepository.Delete(cr);
                });
                Helper.UnitOfWorkInstance.Save();
                this.UpdateTemplateEmailAddressesList();

                // Add sender reference
                var temp = new Template_Email_Addresses();
                temp.TemplateID = templateID;
                temp.EmailID = senderEmailID;
                temp.IsSender = true;
                Helper.UnitOfWorkInstance.TemplateEmailAddressesRepository.Insert(temp);
                Helper.UnitOfWorkInstance.Save();
                this.UpdateTemplateEmailAddressesList();

                // Add receipient references
                string[] receiversIDs = receiverEmailIDs.Split(',');
                foreach (string rc in receiversIDs)
                {
                    temp = new Template_Email_Addresses();
                    temp.TemplateID = templateID;
                    temp.EmailID = Convert.ToInt32(rc);
                    temp.IsSender = false;
                    Helper.UnitOfWorkInstance.TemplateEmailAddressesRepository.Insert(temp);
                }

                Helper.UnitOfWorkInstance.Save();
                this.UpdateTemplateEmailAddressesList();

                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "TemplateEmailAddressesManager:UpdateTemplateAndEmailAddressReferences. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <summary>
        ///     Dispose managed and unmanaged objects
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        ///     Dispose managed and unmanaged objects
        /// </summary>
        /// <param name="disposing">Dispose flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.templateEmailAddressesList = null;
                }

                this.disposedValue = true;
            }
        }

        private void UpdateTemplateEmailAddressesList(IEnumerable<Template_Email_Addresses> templateEmailAddressesListValue = null)
        {
            if (templateEmailAddressesListValue != null)
            {
                this.templateEmailAddressesList = templateEmailAddressesListValue;
            }
            else
            {
                this.templateEmailAddressesList = Helper.UnitOfWorkInstance.TemplateEmailAddressesRepository.Get();
            }
        }
    }
}
