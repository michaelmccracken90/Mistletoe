// <copyright file="TemplateModel.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     Template Model
    /// </summary>
    public class TemplateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateModel"/> class.
        /// </summary>
        public TemplateModel()
        {
            this.ReceiverList = new List<string>();
        }

        /// <summary>
        ///     Gets or sets template ID
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        ///     Gets or sets campaign ID
        /// </summary>
        public string CampaignID { get; set; }

        /// <summary>
        ///     Gets or sets template Name
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        ///     Gets or sets subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     Gets or sets body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets sender
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        ///     Gets or sets receiver List
        /// </summary>
        public List<string> ReceiverList { get; set; }

        /// <summary>
        ///     Gets receivers
        /// </summary>
        public string Receivers
        {
            get
            {
                string tempStr = string.Empty;
                this.ReceiverList.ForEach(r => { tempStr += r + "; "; });
                return tempStr;
            }
        }
    }
}