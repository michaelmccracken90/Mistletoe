// <copyright file="ITemplateEmailAddressesManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Mistletoe.Models;

    /// <summary>
    ///     Template Email Addresses Manager Interface
    /// </summary>
    public interface ITemplateEmailAddressesManager : IDisposable
    {
        /// <summary>
        ///     Update Template and Email Addresses references
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <param name="senderEmailID">Sender Email ID</param>
        /// <param name="receiverEmailIDs">Comma separated Receiver Email IDs</param>
        /// <returns>True or False</returns>
        bool UpdateTemplateAndEmailAddressReferences(int templateID, int senderEmailID, string receiverEmailIDs);

        /// <summary>
        ///     Get all Template Email Addresses items
        /// </summary>
        /// <returns>List of Template Email Addresses</returns>
        IEnumerable<TemplateEmailAddressesModel> GetAllItems();
    }
}
