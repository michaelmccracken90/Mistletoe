// <copyright file="UserModel.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Models
{
    /// <summary>
    ///     User model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        ///     Gets or sets user ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        ///     Gets or sets user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether isActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether isAdmin
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     Gets status for display
        /// </summary>
        public string StatusForDisplay
        {
            get
            {
                if (this.IsActive)
                {
                    return "Active";
                }
                else
                {
                    return "Inactive";
                }
            }
        }

        /// <summary>
        ///     Gets status color
        /// </summary>
        public string StatusColor
        {
            get
            {
                if (this.IsActive)
                {
                    return "success";
                }
                else
                {
                    return "danger";
                }
            }
        }
    }
}