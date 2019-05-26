// <copyright file="TemplateManager.cs" company="Moss and Lichens">
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
    ///     Template Manager
    /// </summary>
    public class TemplateManager : ITemplateManager, IDisposable
    {
        private IEnumerable<Template> templatesList;

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateManager"/> class.
        /// </summary>
        /// <param name="templateModelListValue">List of templates</param>
        public TemplateManager(IEnumerable<TemplateModel> templateModelListValue = null)
        {
            if (templateModelListValue != null)
            {
                this.UpdateTemplatesList(Mapper.Map<IEnumerable<Template>>(templateModelListValue));
            }
            else
            {
                this.UpdateTemplatesList();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TemplateModel> GetTemplateModels(int campaignId)
        {
            List<TemplateModel> templateModels = null;
            try
            {
                var templates = this.templatesList.Where(t => t.CampaignID == campaignId).ToList();
                templateModels = Mapper.Map<List<TemplateModel>>(templates);
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "TemplateManager:GetTemplateModels. Exception: " + Environment.NewLine + ex.Message);
            }

            return templateModels;
        }

        /// <inheritdoc/>
        public TemplateModel GetTemplateModel(int campaignID)
        {
            TemplateModel templateModel = new TemplateModel();

            try
            {
                // Get Template
                var dbTemplate = this.templatesList.Where(t => t.CampaignID == campaignID).FirstOrDefault();
                if (dbTemplate != null)
                {
                    templateModel = Mapper.Map<TemplateModel>(dbTemplate);

                    // Get Sender
                    var sender = dbTemplate.Template_Email_Addresses.Where(cr => cr.IsSender == true).FirstOrDefault();
                    if (sender != null)
                    {
                        templateModel.Sender = sender.Email_Address.Email;
                    }

                    // Get Receiver(s)
                    var receiverList = dbTemplate.Template_Email_Addresses.Where(cr => cr.IsSender == false).ToList();
                    receiverList.ForEach(rc =>
                    {
                        templateModel.ReceiverList.Add(rc.Email_Address.Email);
                    });
                }
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "TemplateManager:GetTemplateModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return templateModel;
        }

        /// <inheritdoc/>
        public int CreateTemplateModel(TemplateModel templateToCreate)
        {
            int template_ID = -1;

            try
            {
                var templateItem = Mapper.Map<Template>(templateToCreate);

                // Enter new Template
                Helper.UnitOfWorkInstance.TemplateRepository.Insert(templateItem);
                Helper.UnitOfWorkInstance.Save();
                this.UpdateTemplatesList();
                template_ID = templateItem.TemplateID;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "TemplateManager:CreateTemplateModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return template_ID;
        }

        /// <inheritdoc/>
        public bool UpdateTemplateModel(TemplateModel templateToUpdate)
        {
            bool success = false;

            try
            {
                var templateItem = Mapper.Map<Template>(templateToUpdate);

                Template dbTemplate = this.templatesList.Where(c => c.TemplateID == templateItem.TemplateID).FirstOrDefault();
                dbTemplate.TemplateName = templateItem.TemplateName;
                dbTemplate.Subject = templateItem.Subject;
                dbTemplate.Body = templateItem.Body;

                Helper.UnitOfWorkInstance.Save();
                this.UpdateTemplatesList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "TemplateManager:UpdateTemplateModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool TemplateExists(int campaignID)
        {
            bool templateExists = false;
            try
            {
                templateExists = this.templatesList.Any(t => t.CampaignID == campaignID);
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "TemplateManager:TemplateExists. Exception: " + Environment.NewLine + ex.Message);
            }

            return templateExists;
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
        /// <param name="disposing">Disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.templatesList = null;
                }

                this.disposedValue = true;
            }
        }

        private void UpdateTemplatesList(IEnumerable<Template> templateListValue = null)
        {
            if (templateListValue != null)
            {
                this.templatesList = templateListValue;
            }
            else
            {
                this.templatesList = Helper.UnitOfWorkInstance.TemplateRepository.Get();
            }
        }
    }
}