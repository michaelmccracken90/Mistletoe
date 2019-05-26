// <copyright file="AdminPageModel.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     Admin Page model
    /// </summary>
    public class AdminPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPageModel"/> class.
        /// </summary>
        public AdminPageModel()
        {
            this.UserHasAccess = false;
            this.Footer = string.Empty;
            this.UsersList = new List<UserModel>();
        }

        /// <summary>
        ///     Gets or sets list of users
        /// </summary>
        public List<UserModel> UsersList { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether user has access
        /// </summary>
        public bool UserHasAccess { get; set; }

        /// <summary>
        ///     Gets or sets footer
        /// </summary>
        public string Footer { get; set; }
    }
}