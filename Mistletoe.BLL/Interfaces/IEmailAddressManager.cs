// <copyright file="IEmailAddressManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Mistletoe.Models;

    /// <summary>
    ///     Email Address Manager Interface
    /// </summary>
    public interface IEmailAddressManager : IDisposable
    {
        /// <summary>
        ///     Add Email
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <returns>Email ID</returns>
        int AddEmail(string emailAddress);

        /// <summary>
        ///     Email exists
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <returns>True or False</returns>
        bool EmailExists(string emailAddress);

        /// <summary>
        ///     Email exists
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <returns>Email Id</returns>
        int GetEmailId(string emailAddress);

        /// <summary>
        ///     Get all email addresses
        /// </summary>
        /// <returns>List of email addresses</returns>
        IEnumerable<EmailAddressModel> GetAllItems();
    }
}
