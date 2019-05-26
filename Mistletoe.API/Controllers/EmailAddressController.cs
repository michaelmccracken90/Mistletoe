// <copyright file="EmailAddressController.cs" company="Moss and Lichens">
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
    ///     Email Address Controller
    /// </summary>
    public class EmailAddressController : ApiController, IEmailAddressManager
    {
        private IEmailAddressManager emailAddressManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressController"/> class.
        /// </summary>
        public EmailAddressController()
        {
            this.emailAddressManager = new EmailAddressManager();
        }

        /// <inheritdoc/>
        public int AddEmail(string emailAddress)
        {
            return this.emailAddressManager.AddEmail(emailAddress);
        }

        /// <inheritdoc/>
        public bool EmailExists(string emailAddress)
        {
            return this.emailAddressManager.EmailExists(emailAddress);
        }

        /// <inheritdoc/>
        public IEnumerable<EmailAddressModel> GetAllItems()
        {
            return this.emailAddressManager.GetAllItems();
        }

        /// <inheritdoc/>
        public int GetEmailId(string emailAddress)
        {
            return this.emailAddressManager.GetEmailId(emailAddress);
        }
    }
}
