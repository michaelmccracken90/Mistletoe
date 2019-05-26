// <copyright file="TemplateEmailAddressesModel.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Models
{
    /// <summary>
    ///     Template Email Addresses Model
    /// </summary>
    public class TemplateEmailAddressesModel
    {
        /// <summary>
        ///     Gets or sets iD
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        ///     Gets or sets template ID
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        ///     Gets or sets email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether isSender
        /// </summary>
        public bool IsSender { get; set; }
    }
}
