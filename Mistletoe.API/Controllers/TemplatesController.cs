// <copyright file="TemplatesController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Mistletoe.BLL.Interfaces;
    using Mistletoe.BLL.Managers;
    using Mistletoe.Models;

    /// <summary>
    ///     Templates Controller
    /// </summary>
    public class TemplatesController : ApiController, ITemplateManager
    {
        private ITemplateManager templateManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesController"/> class.
        /// </summary>
        public TemplatesController()
        {
            this.templateManager = new TemplateManager();
        }

        /// <inheritdoc/>
        public int CreateTemplateModel(TemplateModel templateToCreate)
        {
            return this.templateManager.CreateTemplateModel(templateToCreate);
        }

        /// <inheritdoc/>
        public TemplateModel GetTemplateModel(int campaignId)
        {
            return this.templateManager.GetTemplateModel(campaignId);
        }

        /// <inheritdoc/>
        public IEnumerable<TemplateModel> GetTemplateModels(int campaignID)
        {
            return this.templateManager.GetTemplateModels(campaignID);
        }

        /// <inheritdoc/>
        public bool TemplateExists(int campaignID)
        {
            return this.templateManager.TemplateExists(campaignID);
        }

        /// <inheritdoc/>
        public bool UpdateTemplateModel(TemplateModel templateToUpdate)
        {
            return this.templateManager.UpdateTemplateModel(templateToUpdate);
        }
    }
}
