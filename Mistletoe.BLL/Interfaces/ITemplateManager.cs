// <copyright file="ITemplateManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Mistletoe.Models;

    /// <summary>
    ///     Template Manager Interface
    /// </summary>
    public interface ITemplateManager : IDisposable
    {
        /// <summary>
        ///     Template exists
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>True or False</returns>
        bool TemplateExists(int campaignID);

        /// <summary>
        ///     Get templates
        /// </summary>
        /// <param name="campaignId">Campaign ID</param>
        /// <returns>Template items</returns>
        IEnumerable<TemplateModel> GetTemplateModels(int campaignId);

        /// <summary>
        ///     Get template
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Template item</returns>
        TemplateModel GetTemplateModel(int campaignID);

        /// <summary>
        ///     Create template
        /// </summary>
        /// <param name="templateToCreate">Template item</param>
        /// <returns>Template ID</returns>
        int CreateTemplateModel(TemplateModel templateToCreate);

        /// <summary>
        ///     Update template
        /// </summary>
        /// <param name="templateToUpdate">Template to update</param>
        /// <returns>True or False</returns>
        bool UpdateTemplateModel(TemplateModel templateToUpdate);
    }
}
