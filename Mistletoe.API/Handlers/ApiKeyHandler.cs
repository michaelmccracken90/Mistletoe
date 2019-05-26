// <copyright file="ApiKeyHandler.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.API.Handlers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     API Key Handler
    /// </summary>
    public class ApiKeyHandler : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyHandler"/> class.
        /// </summary>
        /// <param name="key">API Key</param>
        public ApiKeyHandler(string key)
        {
            this.Key = key;
        }

        /// <summary>
        ///  Gets or sets API Key
        /// </summary>
        public string Key { get; set; }

        /// <inheritdoc/>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!this.ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }

            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateKey(HttpRequestMessage message)
        {
            var query = message.RequestUri.ParseQueryString();
            string key = query["key"];
            return key == this.Key;
        }
    }
}