// <copyright file="AccountController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IdentityModel.Services;
    using System.Web;
    using System.Web.Mvc;
    using Auth0.AuthenticationApi;
    using Auth0.AuthenticationApi.Models;

    /// <summary>
    ///     Account Controller
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        ///     Login user
        /// </summary>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>Redirect based on authentication</returns>
        public ActionResult Login(string returnUrl)
        {
            var client = new AuthenticationApiClient(
                new Uri(string.Format("https://{0}", ConfigurationManager.AppSettings["auth0:Domain"])));

            var request = this.Request;
            var redirectUri = new UriBuilder(request.Url.Scheme, request.Url.Host, this.Request.Url.IsDefaultPort ? -1 : request.Url.Port, "/Mistletoe.Web/LoginCallback.ashx");

            var authorizeUrlBuilder = client.BuildAuthorizationUrl()
                .WithClient(ConfigurationManager.AppSettings["auth0:ClientId"])
                .WithRedirectUrl(redirectUri.ToString())
                .WithResponseType(AuthorizationResponseType.Code)
                .WithScope("openid profile")
                .WithAudience("https://" + @ConfigurationManager.AppSettings["auth0:Domain"] + "/userinfo");

            if (!string.IsNullOrEmpty(returnUrl))
            {
                var state = "ru=" + HttpUtility.UrlEncode(returnUrl);
                authorizeUrlBuilder.WithState(state);
            }

            return new RedirectResult(authorizeUrlBuilder.Build().ToString());
        }

        /// <summary>
        ///     Logout user
        /// </summary>
        /// <returns>Redirect user</returns>
        public RedirectResult Logout()
        {
            // Clear the session cookie
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            // Redirect to Auth0's logout endpoint
            var returnTo = this.Url.Action("Index", "Home", null, protocol: this.Request.Url.Scheme);
            return this.Redirect(
              string.Format(
                  CultureInfo.InvariantCulture,
                "https://{0}/v2/logout?returnTo={1}&client_id={2}",
                ConfigurationManager.AppSettings["auth0:Domain"],
                this.Server.UrlEncode(returnTo),
                ConfigurationManager.AppSettings["auth0:ClientId"]));
        }
    }
}