// <copyright file="TemplateEmailAddressesController.cs" company="Moss and Lichens">
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
    ///     Template Email Addresses Controller
    /// </summary>
    public class TemplateEmailAddressesController : ApiController, ITemplateEmailAddressesManager
    {
        private ITemplateEmailAddressesManager templateEmailAddressesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateEmailAddressesController"/> class.
        /// </summary>
        public TemplateEmailAddressesController()
        {
            this.templateEmailAddressesManager = new TemplateEmailAddressesManager();
        }

        /// <inheritdoc/>
        public IEnumerable<TemplateEmailAddressesModel> GetAllItems()
        {
            return this.templateEmailAddressesManager.GetAllItems();
        }

        /// <inheritdoc/>
        public bool UpdateTemplateAndEmailAddressReferences(int templateID, int senderEmailID, string receiverEmailIDs)
        {
            return this.templateEmailAddressesManager.UpdateTemplateAndEmailAddressReferences(templateID, senderEmailID, receiverEmailIDs);
        }
    }
}
