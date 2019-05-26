// <copyright file="MailerResponse.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Email
{
    /// <summary>
    ///     Mail Response
    /// </summary>
    public class MailerResponse
    {
        /// <summary>
        ///     Gets or sets Response value
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        ///     Gets or sets Error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}